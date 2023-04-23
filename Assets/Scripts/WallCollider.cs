using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{
    private BoxCollider2D coll;

    [SerializeField] private int wallHealth = 100;
    private int _currentHealth;
    [SerializeField] private int projectileWallDamage = 10;

    public int CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }

    private void OnEnable()
    {
        _currentHealth = wallHealth;
    }

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        _currentHealth = wallHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            if (_currentHealth == 10)
            {
                transform.GetComponentInParent<WallBehavior>().IsBuilt = false;
            }
            else
            {
                _currentHealth -= projectileWallDamage;
            }
        }
    }
}
