using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FireAttacks : MonoBehaviour
{
    public static FireAttacks Instance;

    // Game Status Variables
    private bool canAttack = true;

    // Damage Variables
    private int _fireballAttack;
    private int _rapidFireAttack;
    private int _handAttack;
    private int _splashAttack;

    // Damage Getters and Setters
    public int FireballAttack { get { return _fireballAttack; } set { _fireballAttack = value; } }
    public int RapidFireAttack { get { return _rapidFireAttack; } set { _rapidFireAttack = value; } }
    public int HandAttack { get { return _handAttack; } set { _handAttack = value; } }
    public int SplashAttack { get { return _splashAttack; } set { _splashAttack = value; } }

    // Attack Cooldown Variables
    [SerializeField] private const double SPLASH_ATTACK_COOLDOWN = 1.5;
    [SerializeField] private const double HAND_ATTACK_COOLDOWN = 3.0;
    [SerializeField] private int FIREBALL_ATTACK_COOLDOWN = 6;
    [SerializeField] private const double RAPID_FIRE_ATTACK_COOLDOWN = 0.2;

    // Attack Cooldwon Getters and Setters
    public int FireballAttackCooldowns { get { return FIREBALL_ATTACK_COOLDOWN; } set { FIREBALL_ATTACK_COOLDOWN = value; } }

    // Splash Variables
    private double LastSplashAttackTime { get; set; }
    [SerializeField] private GameObject splashAttackPrefab;
    [SerializeField] private Collider2D splashAttackEnemyHitZone;
    private bool SplashAttackEnemyInRange { get; set; }

    // Hand Variables
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

    // Rapidfire Variables
    public bool RapidFireAttackEnabled { get; set; } = true;
    private double LastRapidFireAttackTime { get; set; }

    // Fire Burst Variables
    [SerializeField] private GameObject updraftPrefab;
    [SerializeField] private BurstFlash flashPrefab;
    [SerializeField] private ParticleSystem explosionParticlesPrefab;
    [SerializeField] private GameObject impactFramesCollection;
    [SerializeField] private CameraController playerCamera;
    public bool IsPerformingFireBurst { get; private set; }

    // Sounds
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fireBurstSound;
    [SerializeField] private AudioClip handSound;
    [SerializeField] private AudioClip splashSound;
    [SerializeField] private AudioClip rapidFireSound;
    [SerializeField] private AudioClip fireballSound;
    [SerializeField] private AudioClip flamethrowerSound;

    [SerializeField] private LayerMask enemyLayer;

    private SpriteRenderer Sprite { get; set; }

    private void Awake()
    {
        Instance = this;

        _fireballAttack = 10;
        _rapidFireAttack = 1;
        _splashAttack = 50;
        _handAttack = 40;

    }

    private void Start()
    {
        LastSplashAttackTime = float.MinValue;
        LastHandAttackTimeLeft = float.MinValue;
        LastHandAttackTimeRight = float.MinValue;
        IsPerformingFireBurst = false;

        Sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (IsPerformingFireBurst) return;

        SplashAttackEnemyInRange = splashAttackEnemyHitZone.IsTouchingLayers(enemyLayer.value);
        HandAttackEnemyInRangeLeft = handAttackEnemyHitZoneLeft.IsTouchingLayers(enemyLayer.value);
        HandAttackEnemyInRangeRight = handAttackEnemyHitZoneRight.IsTouchingLayers(enemyLayer.value);
        EnemiesExist = GameObject.FindGameObjectWithTag("Enemy") != null ||
            GameObject.FindGameObjectWithTag("Enemy Left") != null;

        if (canAttack == true)
        {

            if (FireController.Instance.CanUseSplashAttack() && SplashAttackEnemyInRange &&
                Time.timeAsDouble >= LastSplashAttackTime + SPLASH_ATTACK_COOLDOWN)
            {
                PerformSplashAttack();
            }

            if (FireController.Instance.CanUseHandAttack())
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

            if (FireController.Instance.CanUseRapidAttack() && EnemiesExist && Time.timeAsDouble >= LastRapidFireAttackTime + RAPID_FIRE_ATTACK_COOLDOWN)
            {
                PerformRapidFireAttack();
            }
        }
    }

    public void PerformSplashAttack()
    {
        audioSource.PlayOneShot(splashSound, 0.7f);
        LastSplashAttackTime = Time.timeAsDouble;
        GameObject splashAttack = Instantiate(splashAttackPrefab, transform, false);
        splashAttack.AddComponent<FireSplashAttack>();
        Destroy(splashAttack, 1.8f);
    }

    public IEnumerator PerformHandAttack(bool rightHand)
    {
        audioSource.PlayOneShot(handSound);

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
        GameObject leftFireballAttack = Instantiate(fireballAttackPrefab, transform, false);
        fireballAttack.AddComponent<FireProjectile>();
        AudioSource newAudioSource = Instantiate(audioSource, fireballAttack.transform, false);
        newAudioSource.pitch = Random.Range(0.85f, 1.15f);
        newAudioSource.Stop();
        newAudioSource.PlayOneShot(fireballSound, Random.Range(0.3f, 0.65f));
        leftFireballAttack.AddComponent<LeftFireProjectile>();
    }

    public void PerformRapidFireAttack()
    {
        LastRapidFireAttackTime = Time.timeAsDouble;
        GameObject rapidFireAttack = Instantiate(fireballAttackPrefab, transform, false);
        GameObject rapidFireAttackLeft = Instantiate(fireballAttackPrefab, transform, false);
        rapidFireAttack.AddComponent<RapidFire>();
        AudioSource newAudioSource = Instantiate(audioSource, rapidFireAttack.transform, false);
        newAudioSource.pitch = Random.Range(0.75f, 1.25f);
        newAudioSource.Stop();
        newAudioSource.PlayOneShot(rapidFireSound, Random.Range(0.3f, 0.65f));
        rapidFireAttackLeft.AddComponent<LeftRapidFire>();
    }

    public IEnumerator PerformFireBurst()
    {
        IsPerformingFireBurst = true;

        audioSource.Stop();
        audioSource.PlayOneShot(flamethrowerSound);

        // Updraft
        GameObject updraft = Instantiate(updraftPrefab, transform, false);
        updraft.transform.localRotation = Quaternion.Euler(0f, 0f, 90.0f);
        updraft.transform.localPosition += Vector3.up * 0.2f;
        Destroy(updraft, 1.3f);

        yield return new WaitForSeconds(0.5f);

        Sprite.enabled = false;

        yield return new WaitForSeconds(0.6f);

        // Star
        BurstFlash glimmer = Instantiate(flashPrefab, transform, false);
        glimmer.transform.localPosition = new Vector3(0.0f, 0.5f, 0f);
        glimmer.RotationSpeed = 180.0f;
        glimmer.MaxSize = 1.0f;
        glimmer.Lifetime = 0.4f;

        audioSource.PlayOneShot(fireBurstSound);

        yield return new WaitForSeconds(1.0f);

        // Flashes
        for (int i = 0; i < 10; i++)
        {
            float xPos = Random.Range(-1.0f, 1.0f);
            float yPos = Random.Range(0.0f, 1.0f);
            BurstFlash flash = Instantiate(flashPrefab, transform, false);
            flash.transform.localPosition = new Vector3(xPos, yPos, 0f);
            flash.RotationSpeed = 0.0f;
            flash.MaxSize = 1.0f;
            flash.Lifetime = 0.15f;

            yield return new WaitForSeconds(0.07f);
        }

        // Fireballs
        GameObject fireballNWObject = Instantiate(fireballAttackPrefab, transform, false);
        fireballNWObject.transform.localScale = Vector3.one * 2.0f;
        ReturningFireball fireballNW = fireballNWObject.AddComponent<ReturningFireball>();
        fireballNW.Angle = 160.0f;
        fireballNW.Lifetime = 0.5f;
        fireballNW.Displacement = Vector2.down * 0.2f;

        GameObject fireballNObject = Instantiate(fireballAttackPrefab, transform, false);
        fireballNObject.transform.localScale = Vector3.one * 2.0f;
        ReturningFireball fireballN = fireballNObject.AddComponent<ReturningFireball>();
        fireballN.Angle = 90.0f;
        fireballN.Lifetime = 0.5f;
        fireballN.Displacement = Vector2.down * 0.2f;

        GameObject fireballNEObject = Instantiate(fireballAttackPrefab, transform, false);
        fireballNEObject.transform.localScale = Vector3.one * 2.0f;
        ReturningFireball fireballNE = fireballNEObject.AddComponent<ReturningFireball>();
        fireballNE.Angle = 20.0f;
        fireballNE.Lifetime = 0.5f;
        fireballNE.Displacement = Vector2.down * 0.2f;

        yield return new WaitForSeconds(0.5f);

        // Explosion
        ParticleSystem explosion = Instantiate(explosionParticlesPrefab, transform, false);
        Destroy(explosion, 2.0f);

        // Impact Frames
        GameObject whiteFrame = impactFramesCollection.transform.Find("White Frame").gameObject;
        Renderer whiteFrameRenderer = whiteFrame.GetComponent<Renderer>();
        GameObject blackFrame = impactFramesCollection.transform.Find("Black Frame").gameObject;
        Renderer blackFrameRenderer = blackFrame.GetComponent<Renderer>();
        GameObject burstFrame1 = impactFramesCollection.transform.Find("Impact Frame Burst 1").gameObject;
        Renderer burstFrame1Renderer = burstFrame1.GetComponent<Renderer>();
        GameObject burstFrame2 = impactFramesCollection.transform.Find("Impact Frame Burst 2").gameObject;
        Renderer burstFrame2Renderer = burstFrame2.GetComponent<Renderer>();

        blackFrameRenderer.enabled = true;
        burstFrame2Renderer.enabled = true;

        yield return new WaitForSeconds(0.03f);

        burstFrame2Renderer.enabled = false;
        blackFrameRenderer.enabled = false;

        whiteFrameRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);

        whiteFrameRenderer.enabled = false;

        // Camera shake
        playerCamera.ShakeFactor = 1.0f;

        // Damage Enemies
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<EnemyLogic>().TakeDamage(1000);
        }
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy Left"))
        {
            enemy.GetComponent<EnemyLogic>().TakeDamage(1000);
        }

        // Restore fire
        IsPerformingFireBurst = false;
        Sprite.enabled = true;
        audioSource.Play();
    }

    public void DisableAttack()
    {
        canAttack = false;
    }
}
