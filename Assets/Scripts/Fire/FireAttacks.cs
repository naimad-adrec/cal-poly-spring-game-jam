using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttacks : MonoBehaviour
{
    // Attack Cooldown Variables
    [SerializeField] private const double SPLASH_ATTACK_COOLDOWN = 1.5;
    [SerializeField] private const double HAND_ATTACK_COOLDOWN = 3.0;
    [SerializeField] private const double FIREBALL_ATTACK_COOLDOWN = 1.0;

    // Splash Variables
    public bool SplashAttackEnabled { get; set; } = true;
    private double LastSplashAttackTime { get; set; }
    [SerializeField] private GameObject splashAttackPrefab;
    [SerializeField] private Collider2D splashAttackEnemyHitZone;
    private bool SplashAttackEnemyInRange { get; set; }

    // Hand Variables
    public bool HandAttackEnabled { get; set; } = true;
    private double LastHandAttackTimeLeft { get; set; }
    private double LastHandAttackTimeRight { get; set; }
    [SerializeField] private GameObject handAttackPrefab;
    [SerializeField] private GameObject handAttackExplosionPrefab;
    [SerializeField] private Collider2D handAttackEnemyHitZoneLeft;
    [SerializeField] private Collider2D handAttackEnemyHitZoneRight;
    private bool HandAttackEnemyInRangeLeft { get; set; }
    private bool HandAttackEnemyInRangeRight { get; set; }

    // Fireball Variables
    public bool FireballAttackEnabled { get; set; } = true;
    private double LastFireballAttackTime { get; set; }
    [SerializeField] private GameObject fireballAttackPrefab;
    private bool EnemiesExist { get; set; }

    [SerializeField] private LayerMask enemyLayer;

    private void Start()
    {
        LastSplashAttackTime = float.MinValue;
        LastHandAttackTimeLeft = float.MinValue;
        LastHandAttackTimeRight = float.MinValue;
    }

    private void Update()
    {
        SplashAttackEnemyInRange = splashAttackEnemyHitZone.IsTouchingLayers(enemyLayer.value);
        HandAttackEnemyInRangeLeft = handAttackEnemyHitZoneLeft.IsTouchingLayers(enemyLayer.value);
        HandAttackEnemyInRangeRight = handAttackEnemyHitZoneRight.IsTouchingLayers(enemyLayer.value);
        EnemiesExist = GameObject.FindGameObjectWithTag("Enemy") != null;

        if (SplashAttackEnabled && SplashAttackEnemyInRange &&
            Time.timeAsDouble >= LastSplashAttackTime + SPLASH_ATTACK_COOLDOWN)
        {
            PerformSplashAttack();
        }

        if (HandAttackEnabled)
        {
            if (HandAttackEnemyInRangeLeft &&
                Time.timeAsDouble >= LastHandAttackTimeLeft + HAND_ATTACK_COOLDOWN)
            {
                StartCoroutine(PerformHandAttack(false));
            }

            if (HandAttackEnemyInRangeRight &&
                Time.timeAsDouble >= LastHandAttackTimeRight + HAND_ATTACK_COOLDOWN)
            {
                StartCoroutine(PerformHandAttack(true));
            }
        }

        if (FireballAttackEnabled && EnemiesExist &&
            Time.timeAsDouble >= LastFireballAttackTime + FIREBALL_ATTACK_COOLDOWN)
        {
            PerformFireballAttack();
        }

    }

    public void PerformSplashAttack()
    {
        LastSplashAttackTime = Time.timeAsDouble;
        GameObject splashAttack = Instantiate(splashAttackPrefab, transform, false);
        splashAttack.AddComponent<FireSplashAttack>();
        Destroy(splashAttack, 1.8f);
    }

    public IEnumerator PerformHandAttack(bool rightHand)
    {
        if (rightHand)
            LastHandAttackTimeRight = Time.timeAsDouble;
        else
            LastHandAttackTimeLeft = Time.timeAsDouble;

        GameObject justHandPrefab = handAttackPrefab.transform.Find("Fire_III_Hand").gameObject;
        GameObject handAttack = Instantiate(justHandPrefab, transform, false);
        if (!rightHand)
        {
            handAttack.transform.localScale = new Vector3(-1, 1, 1);
            handAttack.transform.position += new Vector3(-2.8f, 0);
        }
        handAttack.AddComponent<FireHandAttack>();
        Destroy(handAttack, 1.4f);

        yield return new WaitForSeconds(0.92f);

        GameObject explosion = Instantiate(handAttackExplosionPrefab, transform, false);
        if (rightHand)
            explosion.transform.position += new Vector3(2.0f, 0.0f);
        else         
            explosion.transform.position += new Vector3(-2.0f, 0.0f);
        Destroy(explosion, 0.47f);
    }

    public void PerformFireballAttack()
    {
        LastFireballAttackTime = Time.timeAsDouble;
        GameObject fireballAttack = Instantiate(fireballAttackPrefab, transform, false);
        fireballAttack.AddComponent<FireProjectile>();
    }
}
