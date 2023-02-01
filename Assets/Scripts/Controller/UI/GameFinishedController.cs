using UnityEngine;
using UnityEngine.UI;

public class GameFinishedController : Controller
{
    [SerializeField]
    private Transform _gameFinishedView;

    private void Awake()
    {
        //Disable popup
        _gameFinishedView.gameObject.SetActive(false);
    }

    public override void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            //Show popup
            case ApplicationEvents.FinishedGame:
                _gameFinishedView.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}
