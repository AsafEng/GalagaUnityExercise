using System.Collections;
using UnityEngine;

public class PlayerLaserShotView : ApplicationElement, IPlayerShot, IPooledObject
{
    public GameObjectPool Pool { get; set; }

    //Type of current shot
    public int Type { get; set; }

    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        //Recycle back to pooling after 2 seconds
        StartCoroutine(RecycleSelf(2f));
    }

    public void ApplyForce(Vector2 force)
    {
        //Set force
        _rigidBody.velocity = force;
    }

    public IEnumerator RecycleSelf(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Pool.ReturnToPool(gameObject);
    }
}
