using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTargetBehavior : MonoBehaviour
{
    public static LeftTargetBehavior Instance;

    private Vector3 startPos = new Vector3(-2, -3.5f, 0);

    [SerializeField] private GameObject leftWallOne;
    [SerializeField] private GameObject leftWallTwo;
    [SerializeField] private GameObject leftWallThree;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        transform.position = startPos;
    }

    private void Update()
    {
        if (RoundController.Instance.GameInProgress == true)
        {
            if (leftWallThree.activeInHierarchy == true)
            {
                transform.position = new Vector3(leftWallThree.transform.position.x - 1, transform.position.y, transform.position.z);
            }
            else if (leftWallTwo.activeInHierarchy == true)
            {
                transform.position = new Vector3(leftWallTwo.transform.position.x - 1, transform.position.y, transform.position.z);
            }
            else if (leftWallOne.activeInHierarchy == true)
            {
                transform.position = new Vector3(leftWallOne.transform.position.x - 1, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = startPos;
            }
        }
        else
        {
            transform.position = new Vector3(-24, transform.position.y, transform.position.z);
        }
    }
}
