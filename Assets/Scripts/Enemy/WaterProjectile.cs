using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterProjectile : MonoBehaviour
{
    [SerializeField] private float projectileMoveSpeed;

    private void Start()
    {
        transform.position = Vector2.MoveTowards(transform.position, FireController.Instance.transform.position, projectileMoveSpeed * Time.deltaTime);
    }

    private void Update()
    {
        Vector3 newTargetPos = FireController.Instance.transform.position;
        newTargetPos.y = newTargetPos.y - 0.5f;
        transform.position = Vector2.MoveTowards(transform.position, newTargetPos, projectileMoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fire") || collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
