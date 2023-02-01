using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Spawn Model", menuName = "ScriptableObjects/Enemy Spawn Model", order = 6)]
public class EnemySpawnModel: ScriptableObject
{
    public MovementType MoveType;
    public List<Vector3> TargetPositions;
    public int MovementDuration;
    public int Health;
    public int Score = 10;
    public int SpawnTypeIndex = 0;
    public GameObject SpawnOnDestroy;
}
