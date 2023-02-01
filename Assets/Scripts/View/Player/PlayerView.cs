using System.Collections;
using UnityEngine;

public class PlayerView : ApplicationElement
{
    private Rigidbody2D _rigidBody;
    private Animator _anim;
    private PlayerModel _model;
    private Vector2 _playerInput;

    //Inject model dependency
    public void Constructor(PlayerModel model, int health)
    {
        _model = model;
        _model.Hittable = true;
        _model.Health = health;
    }

    void Awake()
    {
        //Get required comps
        _rigidBody = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        if (_model)
            _model.Hittable = true;
    }

    public void OnMovingPlayer(Vector2 direction)
    {
        //Update input
        _playerInput = direction;
    }

    void FixedUpdate()
    {
        //Move the player according to input direction and speed
        Vector2 moveForce = _playerInput * _model.Speed;

        _rigidBody.velocity = moveForce;
    }

    public void Hit()
    {
        //Animate
        _anim.SetTrigger("Hit");
        StartCoroutine(OnHitCompleted(0.5f));

        //Play sound
        AudioController.Instance.Play("Hit");
    }

    public IEnumerator OnHitCompleted(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        _model.Hittable = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Enemy contact if hittable currently
        if (_model.Hittable && collision.gameObject.CompareTag("Enemy"))
        {
            app.Notify(ApplicationEvents.EnemyCollidedPlayer, this);
            collision.gameObject.GetComponent<IEnemyView>()?.Die();
            _model.Hittable = false;
        }
        else if (collision.gameObject.CompareTag("Pickup"))
        {
            //Pickup notify to controllers
            app.Notify(ApplicationEvents.CollectedPowerup, this);
            Destroy(collision.gameObject);
        }
    }
}
