using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawnPoint : MonoBehaviour
{
    // Tree Variables
    [SerializeField] private GameObject tree;
    private float roundMax;
    private bool isPlanted = false;

    private void Start()
    {
        SpawnTree();
    }

    private void Update()
    {
        if (transform.childCount == 0)
        {
            isPlanted = false;
        }
        else
        {
            isPlanted = true;
        }
    }

    public void SpawnTree()
    {
        if (!isPlanted)
        {
            GameObject newTree = Instantiate(tree, transform, false);
            newTree.transform.position = transform.position;
        }
    }
}
