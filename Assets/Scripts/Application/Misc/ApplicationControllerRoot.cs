public class ApplicationControllerRoot : ApplicationController
{
    //This app controllers
    private Controller[] controllers;

    public void Start()
    {
        //Add child controllers to the application list of controllers
        controllers = GetComponentsInChildren<Controller>();
        app.AddControllers(controllers);
    }
}

