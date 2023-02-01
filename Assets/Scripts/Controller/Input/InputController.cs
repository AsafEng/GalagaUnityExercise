using UnityEngine;

//Note: In this class we prefer to call Event Manager actions instead of MVC's controllers, to save performance
public class InputController : Controller
{
    [SerializeField]
    private Joystick _joystick;

    [SerializeField]
    private Transform _actionButton;

    private Vector2 _input;

    private void Awake()
    {
        _joystick.gameObject.SetActive(false);
        _actionButton.gameObject.SetActive(false);
    }

    //Check for user inputs and annouce them to the required controllers
    private void Update()
    {
        CheckMovementInput();
        CheckShootingInput();
    }

    private void CheckMovementInput()
    {
        //Check axis input
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            #if UNITY_ANDROID
            _input = new Vector2(_joystick.Horizontal, _joystick.Vertical) * 5f;
             #endif
             #if !UNITY_ANDROID
            _input = new Vector2(_joystick.Horizontal, _joystick.Vertical);
            #endif
        }

        //Trigger the event
        InputEventManager.Instance.UpdatedMovementInput?.Invoke(_input);
    }

    private void CheckShootingInput()
    {
        //Check action button input 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Trigger the event
            OnPressedActionButton();
        }
    }

    public void OnPressedActionButton()
    {
        //Trigger the event
        InputEventManager.Instance.PressedActionButton?.Invoke();
    }

    public override void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
        //Activate or deactivate control's UI according to the game state
        switch (p_event_path)
        {
            case ApplicationEvents.StartingGame:
                _joystick.gameObject.SetActive(true);
                _actionButton.gameObject.SetActive(true);
                break;
            case ApplicationEvents.ExitingGame:
                _joystick.gameObject.SetActive(false);
                _actionButton.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
