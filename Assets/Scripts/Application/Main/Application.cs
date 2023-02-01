using System.Collections.Generic;
using UnityEngine;

//Main application
public class Application : MonoBehaviour
{
    //Reference to the root instances of the MVC.
    private ApplicationModel _appModel;
    private ApplicationView _appView;
    private ApplicationController _appController;

    //Getters of the root instances
    public ApplicationModel AppModel { get => _appModel; }
    public ApplicationView AppView { get => _appView; }
    public ApplicationController AppController { get => _appController; }

    //Controllers from other scenes
    private List<Controller> _externalControllers = new List<Controller>();

    void Awake() {
        //Initialize
        _appModel = GetComponentInChildren<ApplicationModel>();
        _appView = GetComponentInChildren<ApplicationView>();
        _appController = GetComponentInChildren<ApplicationController>();
    }

    // Iterates all Controllers and delegates the notification data
    public void Notify(string p_event_path, Object p_target, params object[] p_data)
    {
        List<Controller> controllerList = GetAllControllers();
        foreach (Controller c in controllerList)
        {
            c.OnNotification(p_event_path, p_target, p_data);
        }
    }

    // Fetches all child controllers
    public List<Controller> GetAllControllers() {
        List<Controller> list = new List<Controller>();
        Controller[] controllerList = _appController.GetComponentsInChildren<Controller>();

        //Add internal controllers
        foreach (Controller c in controllerList)
        {
            list.Add(c);
        }

        //Add external controllers
        if (_externalControllers != null)
        {
            foreach (Controller c in _externalControllers)
            {
                list.Add(c);
            }
        }

        return list;
    }

    //Add external controllers from other scenes
    public void AddControllers(Controller[] newControllers) {
        foreach (Controller c in newControllers)
        {
            _externalControllers.Add(c);
        }
    }
}
