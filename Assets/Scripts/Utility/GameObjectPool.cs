using System.Collections.Generic;
using UnityEngine;

//Object pooilng system
public class GameObjectPool : MonoBehaviour
{
    //Possible objects to spawn
    [SerializeField]
    private GameObject[] _prefabs;

    //The parent object of spawned objects
    [SerializeField]
    private GameObject _spawnerParent;

    //Queue for pooling objects
    private readonly Stack<GameObject> _objectsQueue = new Stack<GameObject>();

    //Get a new or queued object
    public GameObject GetOrAllocateGameObject(int index = 0) {

        IPooledObject nextObject = _objectsQueue.Count > 0? _objectsQueue.Peek()?.GetComponent<IPooledObject>() : null;

        if (_objectsQueue.Count == 0 || (nextObject != null && nextObject.Type != index))
        {
            AddObject(index);
        }

        return _objectsQueue.Pop();
    }

    //Recyle destroyed objects
    public void ReturnToPool(GameObject returnThisObject) {
        returnThisObject.SetActive(false);
        _objectsQueue.Push(returnThisObject);
    }

    //Instantiate a new object if the queue is empty
    private void AddObject(int index = 0) {
        var newObject = Instantiate(_prefabs[index]);
        newObject.transform.SetParent(_spawnerParent.transform, false);
        newObject.SetActive(false);
        newObject.GetComponent<IPooledObject>().Pool = this;
        newObject.GetComponent<IPooledObject>().Type = index;
        _objectsQueue.Push(newObject);
    }

    //Reset the whole pool
    public void EmptyPool()
    {
        _objectsQueue.Clear();
        foreach (Transform transform in _spawnerParent.transform)
        {
            if (transform.gameObject != null)
            Destroy(transform.gameObject);
        }
    }
}
