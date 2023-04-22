using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Start()
    {
        transform.position = new Vector3(PlayerStateMachine.Instance.transform.position.x, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        transform.position = new Vector3(PlayerStateMachine.Instance.transform.position.x, transform.position.y, transform.position.z);
    }
}
