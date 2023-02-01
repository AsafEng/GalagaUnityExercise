using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Model", menuName = "ScriptableObjects/Level Model", order = 5)]
public class LevelDataModel : ScriptableObject
{
    public List<EnemySpawnModel> SpawnModels = new List<EnemySpawnModel>();
}
