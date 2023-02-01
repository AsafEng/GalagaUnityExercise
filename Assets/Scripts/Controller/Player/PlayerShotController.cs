using System.Collections;
using UnityEngine;

public class PlayerShotController : Controller
{
    //Object pool for player shoots
    public GameObjectPool pool;

    [SerializeField]
    private PlayerShotModel _model;

    private void Start()
    {
        //Default shot type
        _model.ShotType = PlayerShotType.Normal;
    }

    //Pool new projectiles
    protected virtual void CreateShot(Vector3 position)
    {
        var newShot = pool.GetOrAllocateGameObject((int)_model.ShotType);
        StartCoroutine(ActivateShot(newShot, position, 0.3f));
        AudioController.Instance?.Play("Laser");
    }

    //Recycle used projectiles
    protected virtual void RecycleShot(GameObject viewToRecycle)
    {
        pool.ReturnToPool(viewToRecycle);
    }

    //Active newly pooled projectiles
    protected virtual IEnumerator ActivateShot(GameObject newShot, Vector3 position, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (newShot)
        {
            newShot.transform.position = position;
            newShot.SetActive(true);
            newShot.GetComponent<IPlayerShot>().ApplyForce(_model.StartForce);
        }
    }

    //Events for player shooting
    public override void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case ApplicationEvents.PlayerShooting:
                //Create a new projectile on a given position
                CreateShot((Vector3)p_data[0]);
                break;
            case ApplicationEvents.CollectedPowerup:
                //Create a new projectile on a given position
                _model.ShotType = PlayerShotType.Split;
                AudioController.Instance.Play("Powerup");
                break;
            default:
                break;
        }
    }
}
