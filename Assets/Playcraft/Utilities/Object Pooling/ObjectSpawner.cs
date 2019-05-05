using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour 
{
    List<GameObject> pooledObjects = new List<GameObject>();
    GameObject pooledObject; 		
    int currentPoolSize = 0;
    int index = 0;

    string templateName;
    int totalSpawnCount;

    public void Initialize(GameObject pooledObject, int startPoolSize)
    {
        this.pooledObject = pooledObject;
        currentPoolSize = startPoolSize;
        templateName = pooledObject.name;

        for (int i = 0; i < currentPoolSize; i++)
            Add(false);
    }

    public GameObject Spawn(Vector3 spawnLoc)
    {
		GameObject selected = GetPooledObject();

        selected.transform.position = spawnLoc;
        selected.transform.SetParent(gameObject.transform);  
        selected.SetActive(true);
        
        selected.name = templateName + " " + totalSpawnCount.ToString();
        totalSpawnCount++;

        Rigidbody rb = selected.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        return selected;
    }

	private GameObject GetPooledObject()
	{
		bool poolEmpty = true;
		
	    for(index = 0; index < currentPoolSize; index++)
		{
            if(pooledObjects[index] == null)
            {
                Debug.LogError("WARNING: object " + index + " in " + gameObject.name + " cannot be found!");
                return null;
            }

			if (!pooledObjects[index].activeInHierarchy)
			{
				poolEmpty = false;
				break;
			}
		}

        if (poolEmpty)
	    {
            Add(true);
            currentPoolSize++;
            index = currentPoolSize - 1;
        }

	    return pooledObjects[index];
	}

    private void Add(bool isActive)
    {
        GameObject obj = Instantiate(pooledObject);
        obj.transform.SetParent(gameObject.transform);
        obj.SetActive(isActive);
        pooledObjects.Add(obj);
    }
}
