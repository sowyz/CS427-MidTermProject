using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    protected GameObject objectToPool;
    [SerializeField]
    protected int poolSize = 10;

    protected Queue<GameObject> objectPool;

    public  Transform spawnedObjectsParent;

    private void Awake()
    {
        objectPool = new Queue<GameObject>();
    }

    public void Initalize(GameObject objectToPool, int poolSize)
    {
        this.objectToPool = objectToPool;
        this.poolSize = poolSize;
    }

    public GameObject CreateObject()
    {
        CreateObjectParentIfNeeded();

        GameObject spawnedObject = null;

        if (objectPool.Count < poolSize)
        {
            spawnedObject = Instantiate(objectToPool, transform.position, Quaternion.identity);
            spawnedObject.name = transform.root.name + "_" + objectToPool.name + "_" + objectPool.Count;
            spawnedObject.transform.SetParent(spawnedObjectsParent);
            spawnedObject.AddComponent<DestroyIfDisable>();
        }
        else
        {
            spawnedObject = objectPool.Dequeue();
            spawnedObject.transform.position = transform.position;
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.SetActive(true);
        }

        objectPool.Enqueue(spawnedObject);
        return spawnedObject;
    }

    private void CreateObjectParentIfNeeded()
    {
        if (spawnedObjectsParent == null)
        {
            string name = "ObjectPool_" + objectToPool.name;
            var parentObject = GameObject.Find(name);
            if (parentObject == null)
            {
                spawnedObjectsParent = new GameObject(name).transform;
            }
            else
            {
                spawnedObjectsParent = parentObject.transform;
            }   
        }
    }

    private void OnDisable()
    {
        foreach (var item in objectPool)
        {
            if (item != null)
            {
                if (!item.activeSelf)
                    Destroy(item);
                else
                    item.GetComponent<DestroyIfDisable>().SelfDestructionEnabled = true;
            }
        }
    }
}
