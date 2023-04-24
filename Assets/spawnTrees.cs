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
        transform.GetComponentInChildren<TreeSpawnPoint>().SpawnTree();
    }
}
