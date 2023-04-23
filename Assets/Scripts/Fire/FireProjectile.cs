using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{

    private GameObject Target { get; set; }

    private float Rotation { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Target = FindNearestEnemy();

        Rotation = Target.transform.position.x < transform.position.x ?
            (Mathf.PI * 3.0f / 4.0f) : (Mathf.PI * 1.0f / 4.0f);            
    }

    // Update is called once per frame
    void Update()
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
        float targetRotation = Mathf.Atan2(displacement.y, displacement.x);
        Rotation = Mathf.LerpAngle(Rotation, targetRotation, Time.deltaTime * 2);

        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * (Rotation + Mathf.PI / 4.0f));

        Vector3 moveVector = new(Mathf.Cos(Rotation), Mathf.Sin(Rotation));
        transform.position += Time.deltaTime * moveVector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Target)
        {
            Destroy(collision.gameObject);
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
}
