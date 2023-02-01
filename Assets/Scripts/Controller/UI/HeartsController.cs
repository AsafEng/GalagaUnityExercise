using UnityEngine;

public class HeartsController : Controller
{
    [SerializeField]
    private ApplicationView _view;

    //Rect transforms array for control
    private RectTransform[] _heartViews;

    private void Awake()
    {
        //Init heart UI images
        _heartViews = _view.GetComponentsInChildren<RectTransform>();
        _view.gameObject.SetActive(false);
    }

    //Events for HeartsUI
    public override void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case ApplicationEvents.PlayerHit:
                //If a player is hit, update the hearts UI with the new health value
                int newHealth = (int)p_data[0];
                UpdateHeartsView(newHealth);
                break;
            case ApplicationEvents.StartingGame:
                _view.gameObject.SetActive(true);
                break;
        }
    }

    //Update the hearts UI with a given health amount
    private void UpdateHeartsView(int newHealth)
    {
        int index = 0;
        foreach (RectTransform rect in _heartViews)
        {
            if (index != 0)
            {
                rect.gameObject.SetActive(index <= newHealth);
            }
            index++;
        }
    }
}
