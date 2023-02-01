public class ScoreButtonView : ApplicationElement, IButtonView
{
    public void PressedButton()
    {
        //Notify to controllers that a user pressed the score button
        app.Notify(ApplicationEvents.PressedScoreboard, this);
    }
}
