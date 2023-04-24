using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawnPoint : MonoBehaviour
{
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
        else
        {
            isPlanted = true;
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
