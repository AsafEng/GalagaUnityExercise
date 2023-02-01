//interface of any pooled object
using System.Collections;

public interface IPooledObject
{
    GameObjectPool Pool { get; set; }
    IEnumerator RecycleSelf(float time);

    int Type { get; set; }
}
