using DG.Tweening;
using System.Collections;
using UnityEngine;

public class SmallEnemyView : ApplicationElement, IEnemyView
{
    public GameObjectPool Pool { get; set; }
    public int Type { get; set; }
    public EnemySpawnModel EnemySpawnModel { get; set; }
    public GameObjectPool ProjectilesPool { get; set; }
    public EnemyModel Model { get; set; }

    private Animator _anim;


    //private GameObjectPool
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        Model = new EnemyModel();
    }

    public void Activate()
    {
        //Init position
        InitPosition();

        //For each position, set a tween
        for (int i = 1; i < EnemySpawnModel.TargetPositions.Count; i++)
        {
            StartCoroutine(MoveToTarget(i, EnemySpawnModel.MovementDuration * i));
        }

        //Finally, move to the grid location
        StartCoroutine(MoveToGrid((EnemySpawnModel.TargetPositions.Count) * EnemySpawnModel.MovementDuration));

        StartCoroutine(Shoot(EnemySpawnModel.MovementDuration));

        Model.Health = EnemySpawnModel.Health; 
    }

    public void InitPosition()
    {
        //Initialize position
        transform.position = EnemySpawnModel.TargetPositions[0];
    }

    public IEnumerator MoveToTarget(int index, float time)
    {
        //Move to next target position
        yield return new WaitForSecondsRealtime(time);
        transform.DOMove(EnemySpawnModel.TargetPositions[index], EnemySpawnModel.MovementDuration);
    }

    public IEnumerator MoveToGrid(float time)
    {
        //Move to final position after finishing with all other movements
        yield return new WaitForSecondsRealtime(time);
        transform.DOMove(Model.GridRealPosition, EnemySpawnModel.MovementDuration);
    }

    public void Hit()
    {
        //Reduce health
        Model.Health--;

        //Animate hitting
        _anim.SetTrigger("Hit");

        //Play sound
        AudioController.Instance.Play("Hit");

        //Check if dead
        if (Model.Health == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Animate destroying
        _anim.SetTrigger("Destroy");
        transform.DOKill();
        app.Notify(ApplicationEvents.EnemyKilled, this, Model.GridPosition);
        app.Notify(ApplicationEvents.GainedScore, this, EnemySpawnModel.Score);
        AudioController.Instance.Play("Explode");

        if (EnemySpawnModel.SpawnOnDestroy != null)
        {
            Instantiate(EnemySpawnModel.SpawnOnDestroy, transform.position, Quaternion.identity, transform.parent);
        }
    }

    //Destroy end animation event
    public void DestroyEnd()
    {
        StartCoroutine(RecycleSelf(0));
    }

    public IEnumerator RecycleSelf(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Pool.ReturnToPool(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerShot")
        {
            //If collided with a player shot, hit and return to pool
            Hit();
            collision.gameObject.GetComponent<IPooledObject>().Pool.ReturnToPool(collision.gameObject);
        }
    }

    //Shoot projectile
    public IEnumerator Shoot(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        var newShot = ProjectilesPool.GetOrAllocateGameObject();
        newShot.SetActive(true);
        newShot.transform.position = transform.position;
        newShot.GetComponent<EnemyLaserShotView>().ApplyForce(Vector2.down * 15f);

        StartCoroutine(Shoot(EnemySpawnModel.MovementDuration));
    }
}
