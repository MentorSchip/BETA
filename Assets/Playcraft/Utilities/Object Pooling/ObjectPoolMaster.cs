using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolMaster : Singleton<ObjectPoolMaster>
{
    [SerializeField] ObjectPoolData[] pools;
    List<ObjectSpawner> spawners = new List<ObjectSpawner>();

    private void Start()
    {
        foreach (var pool in pools)
        {
            if (pool == null)
                continue;

            GeneratePool(pool);
        }
    }

    private void GeneratePool(ObjectPoolData pool)
    {
        ObjectSpawner spawner = pool.container.GetComponent<ObjectSpawner>();
        spawner.Initialize(pool.prefab, pool.startSize);
        spawners.Add(spawner);
    }

    public GameObject Spawn(GameObject prefabToSpawn, Vector3 spawnLocation)
    {
        return GetSpawner(prefabToSpawn).Spawn(spawnLocation);
    }

    public void OnSpawn(GameObject prefabToSpawn, Vector3 spawnLocation)
    {
        GetSpawner(prefabToSpawn).Spawn(spawnLocation);
    }

    private ObjectSpawner GetSpawner(GameObject prefab)
    {
        for (int i = 0; i < pools.Length; i++)
        {
            if (pools[i].prefab == prefab)
                return spawners[i];
        }

        Debug.LogError("Matching prefab not found in object pooling system " + prefab);
        return null;
    }

    // * Revise so not dependent on structure of pooling system
    public void DisableEverything()
    {
        foreach (Transform pool in transform)
            foreach (Transform instance in pool)
                instance.gameObject.SetActive(false);
    }
}
