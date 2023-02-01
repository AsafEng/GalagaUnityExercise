using UnityEngine;

public enum PickableType
{
    SplitShot = 0,
}

public abstract class Pickable: MonoBehaviour
{
    public PickableType Type;
}
