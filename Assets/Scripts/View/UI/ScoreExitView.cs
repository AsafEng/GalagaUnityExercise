using UnityEngine;
using UnityEngine.UI;

public class ScoreExitView : ApplicationElement, IButtonView
{
    [SerializeField]
    private Text _inputView;

    public void PressedButton()
    {
        //Notify to the controllers that a user pressed on the button
        app.Notify(ApplicationEvents.UpdatedPlayerName, this, _inputView.text);
        app.Notify(ApplicationEvents.RestartingGame, this);
    }
}
