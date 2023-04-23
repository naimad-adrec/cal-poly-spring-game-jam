using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Vector3 OriginalPosition { get; private set; }
    public Vector3 ActualPosition { get; set; }
    public float ShakeFactor { get; set; }

    private void Start()
    {
        ActualPosition = new Vector3(PlayerStateMachine.Instance.transform.position.x, transform.position.y, transform.position.z);
        OriginalPosition = ActualPosition;
        transform.position = ActualPosition;
        ShakeFactor = 0.0f;
    }

    private void Update()
    {
        ActualPosition = new Vector3(PlayerStateMachine.Instance.transform.position.x, OriginalPosition.y, OriginalPosition.z);
        transform.position = ActualPosition + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f) * ShakeFactor;
        if (ShakeFactor > 0f)
            ShakeFactor -= Time.deltaTime;
        else
            ShakeFactor = 0.0f;
    }
}
