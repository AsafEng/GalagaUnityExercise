using UnityEngine;

[System.Serializable]
public struct EnemyGridModel
{
    public float MinTime;
    public float MaxTime;
    [HideInInspector]
    public bool ReadyToMove;
    [HideInInspector]
    public Vector3 LastPosition;
}
