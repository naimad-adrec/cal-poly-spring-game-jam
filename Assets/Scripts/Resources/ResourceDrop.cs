using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDrop : MonoBehaviour
{
    private CapsuleCollider2D _coll;
    [SerializeField] private string resourceName;

    private void Awake()
    {
        _coll = GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (resourceName == "Wood")
            {
                PlayerStateMachine.Instance.WoodCount++;
                Destroy(gameObject);
            }
            else
            {
                PlayerStateMachine.Instance.CoalCount++;
                Destroy(gameObject);
            }
        }
    }
}
