using UnityEngine;

namespace Utils
{
    /*  
     *  A generic singleton abstract class. 
    */
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        //Static instance access
        protected static T instance;
        public static T Instance
        {
            get { return instance; }
        }

        //Init
        public static bool isInitialized
        {
            get { return instance != null; }
        }

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("[Singleton] trying to instantiate a second instance in a singleton.");
            }
            else
            {
                instance = (T)this;
            }
        }

        //If destroyed, set instance to null
        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }

}
