using System.Collections;
using UnityEngine;

public class EnemyPoolController : Controller
{
    //Object pooling for enemies
    private GameObjectPool _pool;

    [SerializeField]
    private Transform SpawnPosition;

    [SerializeField]
    private GameObjectPool _projectilesPool;

    private void Awake()
    {
        _pool = GetComponent<GameObjectPool>();
    }

    //Events notify for enemy controller
    public override void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case ApplicationEvents.SpawningEnemy:
                //Create an enemy in a given position
                EnemySpawnModel newSpawn = p_data[0] as EnemySpawnModel;
                Vector3 gridRealPosition = (Vector3)p_data[1];
                Vector2 gridPosition = (Vector2)p_data[2];
                CreateEnemy(newSpawn, gridRealPosition, gridPosition);
                break;
            default:
                break;
        }
    }

    //Recycle a destroyed enemy using the pooling system
    public void RecycleEnemy(GameObject viewToRecycle)
    {
        _pool.ReturnToPool(viewToRecycle);
    }

    //Assign a new enemy using the pooling system
    private void CreateEnemy(EnemySpawnModel enemySpawn, Vector2 gridRealPosition, Vector2 gridPosition, float waitTime = 0)
    {
        var newEnemy = _pool.GetOrAllocateGameObject(enemySpawn.SpawnTypeIndex);
        newEnemy.transform.position = SpawnPosition.position;
        var interfaceView = newEnemy.GetComponent<IEnemyView>();
        interfaceView.EnemySpawnModel = enemySpawn;
        interfaceView.Model.GridRealPosition = gridRealPosition;
        interfaceView.Model.Health = enemySpawn.Health;
        interfaceView.Model.GridPosition = gridPosition;
        interfaceView.ProjectilesPool = _projectilesPool;

        StartCoroutine(ActivateEnemy(newEnemy, waitTime));
    }

    //Activate a newly pooled enemy
    IEnumerator ActivateEnemy(GameObject newEnemy, float waitTime)
    {
        newEnemy.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        newEnemy.SetActive(true);
        var interfaceView = newEnemy.GetComponent<IEnemyView>();
        interfaceView.Activate();
    }
}
