using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSplashAttack : MonoBehaviour
{
    private int splashDamage = 25;
    private HashSet<GameObject> EnemiesInHurtBox { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        EnemiesInHurtBox = new();
        GetComponent<SpriteRenderer>().sortingLayerName = "Fire";
        StartCoroutine(HitEnemy());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        if (target.CompareTag("Enemy") && !EnemiesInHurtBox.Contains(target))
        {
            EnemiesInHurtBox.Add(target);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        if (EnemiesInHurtBox.Contains(target))
        {
            EnemiesInHurtBox.Remove(target);
        }
    }

    private IEnumerator HitEnemy()
    {
        yield return new WaitForSeconds(1.0f);

        // copy set so enumeration errors don't happen on concurrent modification
        HashSet<GameObject> targeted = new(EnemiesInHurtBox);

        foreach (GameObject enemy in targeted)
        {
            ApplyDamage(enemy);
        }
    }

    private void ApplyDamage(GameObject enemy)
    {
        enemy.GetComponent<EnemyLogic>().TakeDamage(splashDamage);
    }
}
