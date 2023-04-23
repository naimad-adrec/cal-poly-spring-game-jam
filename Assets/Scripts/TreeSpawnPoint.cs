using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreeSpawnPoint : MonoBehaviour
{
    // Event Variables
    [SerializeField] private UnityEvent spawnNewTrees;

    // Tree Variables
    [SerializeField] private GameObject tree;
    private float roundMax;
    private bool isPlanted = true;

    private void Start()
    {
        Instantiate(tree, transform.position, transform.rotation);
    }
    private void Update()
    {
        if (transform.childCount == 0)
        {
            isPlanted = false;
        }
    }

    public void SpawnTree()
    {
        if (isPlanted == false)
        {
            Instantiate(tree, transform.position, transform.rotation);
            isPlanted = true;
        }
    }
}
