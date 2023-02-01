using UnityEngine;

public class ScoreUIController : Controller
{
    [SerializeField]
    private ScoreUIView _view;

    private void Awake()
    {
        _view.gameObject.SetActive(false);
    }

    public override void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case ApplicationEvents.GainedScore:
                //Update score UI
                int newScore = (int)p_data[0];
                UpdateScoreView(newScore);
                break;
            case ApplicationEvents.StartingGame:
                _view.gameObject.SetActive(true);
                break;
        }
    }

    private void UpdateScoreView(int scoreToAdd)
    {
        //Update score's text
        _view.UpdateScoreText(scoreToAdd);
    }
}
