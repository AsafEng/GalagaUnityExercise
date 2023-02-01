using System.Collections;
using UnityEngine;

public class EnemyLaserShotView : ApplicationElement, IPooledObject
{
    public GameObjectPool Pool { get; set; }

    public int Type { get; set; }

    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(RecycleSelf(2f));
    }

    public void ApplyForce(Vector2 force)
    {
        _rigidBody.velocity = force;
    }

    public IEnumerator RecycleSelf(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Pool.ReturnToPool(gameObject);
    }
}
