using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    private int fireballDamage;

    private float Rotation { get; set; }

    private void Start()
    {
        fireballDamage = FireAttacks.Instance.FireballAttack;
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(22, transform.position.y, transform.position.z), 10 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ApplyDamage(collision.gameObject);
            Explode();
        }
    }

    public void Explode()
    {
        transform.rotation = Quaternion.identity;
        Destroy(gameObject);
    }

    private void ApplyDamage(GameObject enemy)
    {
        enemy.GetComponent<EnemyLogic>().TakeDamage(fireballDamage);
    }
}
