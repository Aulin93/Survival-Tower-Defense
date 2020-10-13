using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    public int Count { get { return queue.Count; } }

    private GameObject prefab;
    private Queue<T> queue;

    public ObjectPool(GameObject prefabObject)
    {
        prefab = prefabObject;
        queue = new Queue<T>();
    }

    public void Add(T item)
    {
        queue.Enqueue(item);
        item.gameObject.SetActive(false);
    }

    public T Retrieve()
    {
        if(queue.Count == 0)
        {
            GameObject spawnedObject = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            T spawnedT = spawnedObject.GetComponent<T>();
            queue.Enqueue(spawnedT);
        }
        T retrievedObject = queue.Dequeue();
        retrievedObject.gameObject.SetActive(true);
        return retrievedObject;
    }
}
