public class ExitButtonView : ApplicationElement, IButtonView
{
    public void PressedButton()
    {
        //Notify to controllers that a user pressed on an exit button
        app.Notify(ApplicationEvents.ExitingGame, this);
    }
}
