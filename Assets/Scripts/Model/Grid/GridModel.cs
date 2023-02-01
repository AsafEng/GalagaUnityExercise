using UnityEngine;
public enum GridObjectType
{
    Empty,
    Filled
}

public enum MovementType
{
    LeftDown,
    RightDown,
    TopDown
}

[CreateAssetMenu(fileName = "GridModel", menuName = "ScriptableObjects/GridModel", order = 2)]
public class GridModel : ScriptableObject
{
    public int GridHorizontalSize = 8;
    public int GridVerticalSize = 13;
    public float GridHorizontalDiff = 0.6f;
    public float GridVerticalDiff = 0.6f;
}