using System.Collections;

public interface IEnemyView: IPooledObject
{
    EnemySpawnModel EnemySpawnModel { get; set; }
    GameObjectPool ProjectilesPool { get; set; }
    EnemyModel Model { get; set; }
    void InitPosition();
    void Hit();
    void Die();
    void Activate();
    IEnumerator MoveToTarget(int index, float time);
}
