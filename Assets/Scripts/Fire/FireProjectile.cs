using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    private int fireballDamage;
    private GameObject Target { get; set; }

    private float Rotation { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
        Target = FindNearestEnemy();

        Rotation = Target.transform.position.x < transform.position.x ? 135.0f : 45.0f;
        fireballDamage = FireAttacks.Instance.FireballAttack;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Target == null)
        {
            Target = FindNearestEnemy();
            if (Target == null) {
                // there are no more enemies to lock on to
                Explode();
                return;
            }
        }

        Vector2 displacement = Target.transform.position - transform.position;
        float targetRotation = Mathf.Rad2Deg * Mathf.Atan2(displacement.y, displacement.x);
        Rotation = Mathf.LerpAngle(Rotation, targetRotation, Time.deltaTime * 2);

        transform.rotation = Quaternion.Euler(0f, 0f, Rotation + 45.0f);

        Vector3 moveVector = new(Mathf.Cos(Mathf.Deg2Rad * Rotation), Mathf.Sin(Mathf.Deg2Rad * Rotation));
        transform.position += Time.deltaTime * moveVector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Target)
        {
            ApplyDamage(collision.gameObject);
            Explode();
        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject closestObject = null;
        float closestDistance = float.MaxValue;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float distance = (enemy.transform.position - transform.position).sqrMagnitude;
            if (distance < closestDistance)
            {
                closestObject = enemy;
                closestDistance = distance;
            }
        }
        return closestObject;
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
