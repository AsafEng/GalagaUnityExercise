public class PlayButtonView : ApplicationElement, IButtonView
{
    public void PressedButton()
    {
        //Notify to controllers that a user pressed on an exit button
        app.Notify(ApplicationEvents.StartingGame, this);
    }
}
