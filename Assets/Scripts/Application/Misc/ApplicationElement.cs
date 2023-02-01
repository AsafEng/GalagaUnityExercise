using UnityEngine;

// Basic class for any application element
public class ApplicationElement : MonoBehaviour
{
    // Gives access to the application and all instances.
    public Application app { get { return FindObjectOfType<Application>(); } }
}
