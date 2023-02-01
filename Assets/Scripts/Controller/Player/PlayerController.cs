using System;
using UnityEngine;

public class PlayerController : Controller
{
    //List of possible triggers
    public Action<Vector2> MovingPlayer;
    public Action StoppingPlayerView;

    [SerializeField]
    private PlayerView _view;

    [SerializeField]
    private PlayerModel _model;

    private void Start()
    {
        //Listen to movement keys
        InputEventManager.Instance.UpdatedMovementInput += _view.OnMovingPlayer;
        InputEventManager.Instance.PressedActionButton += OnPressedActionButton;

        //Inject model dependency (scriptable object)
        _view.Constructor(_model, 3);
    }

    //Notifications for the player controller
    public override void OnNotification(string p_event_path, UnityEngine.Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case ApplicationEvents.EnemyCollidedPlayer:
                //Hit the player model health
                _model.Health--;

                //Animate the player view
                _view.Hit();

                //Reduce UI hearts
                app.Notify(ApplicationEvents.PlayerHit, this, _model.Health);

                //Restart level if the health is 0
                if (_model.Health == 0)
                {
                    Die();
                }
                break;
            default:
                break;
        }
    }

    private void OnPressedActionButton()
    {
        //Trigger shot event if alive
        if (_model.Health > 0)
        {
            app.Notify(ApplicationEvents.PlayerShooting, this, _view.gameObject.transform.position + _model.PivotPosition);
        }
    }

    private void Die()
    {
        //Notify to controllers that the game is finished 
        app.Notify(ApplicationEvents.FinishedGame, this);

        //Destroy self
        Destroy(_view.gameObject);

        //Play sound
        AudioController.Instance.Play("PlayerDie");
    }
}
