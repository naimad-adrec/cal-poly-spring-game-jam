using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTargetBehavior : MonoBehaviour
{
    public static RightTargetBehavior Instance;

    private Vector3 startPos = new Vector3(2, -3.5f, 0);

    [SerializeField] private GameObject rightWallOne;
    [SerializeField] private GameObject rightWallTwo;
    [SerializeField] private GameObject rightWallThree;

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
            if (rightWallThree.activeInHierarchy == true)
            {
                transform.position = new Vector3(rightWallThree.transform.position.x + 1, transform.position.y, transform.position.z);
            }
            else if (rightWallTwo.activeInHierarchy == true)
            {
                transform.position = new Vector3(rightWallTwo.transform.position.x + 1, transform.position.y, transform.position.z);
            }
            else if (rightWallOne.activeInHierarchy == true)
            {
                transform.position = new Vector3(rightWallOne.transform.position.x + 1, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = startPos;
            }
        }
        else
        {
            transform.position = new Vector3(22, transform.position.y, transform.position.z);
        }
    }
}
