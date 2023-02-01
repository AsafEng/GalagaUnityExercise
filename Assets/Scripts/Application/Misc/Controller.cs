using UnityEngine;

//Abstract class for any controller in our app
public abstract class Controller : ApplicationElement
{
    //Handles triggered events
    public abstract void OnNotification(string p_event_path, Object p_target, params object[] p_data);
}
