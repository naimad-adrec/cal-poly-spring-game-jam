using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFireProjectile : MonoBehaviour
{
    private int fireballDamage = 10;

    private float Rotation { get; set; }

    private void Start()
    {
        fireballDamage = FireAttacks.Instance.FireballAttack;
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy Left"))
        {
            ApplyDamage(collision.gameObject);
            Explode();
        }
    }

    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(-24, transform.position.y, transform.position.z), 10 * Time.deltaTime);
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
