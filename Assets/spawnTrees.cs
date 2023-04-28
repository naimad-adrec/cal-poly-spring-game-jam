using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTrees : MonoBehaviour
{
    public static spawnTrees Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnTrees()
    {
        foreach (TreeSpawnPoint spawnPoint in transform.GetComponentsInChildren<TreeSpawnPoint>())
            spawnPoint.SpawnTree();
    }
}
