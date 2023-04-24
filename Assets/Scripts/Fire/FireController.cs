using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireController : MonoBehaviour
{
    // Singleton
    public static FireController Instance;

    // Events
    [SerializeField] private UnityEvent endGame;

    // Game Component Variables
    private Animator anim;

    // Health Variables
    [SerializeField] private int _maxFireHealth;
    private int _fireHealth;
    private bool _isBurning = true;

    // Health Getters and Setters
    public int MaxFireHealth { get { return _maxFireHealth; } set { _maxFireHealth = value; } }
    public int FireHealth { get { return _fireHealth; } set { _fireHealth = value; } }
    public bool IsBurning { get { return _isBurning; } set { _isBurning = value; } }

    // Projectile Variables
    [SerializeField] private int waterProjectileDamage = 5;

    // Upgrade Variables
    private Upgrades upgrades;
    private int woodValue = 20;
    private bool _dropRateIncreased = false;

    public Upgrades Upgrades { get { return upgrades; } set { upgrades = value; } }

    // Upgrade Getters and Setters
    public bool DropRateIncreased { get { return _dropRateIncreased; }  private set { } }

    private void Awake()
    {
        Instance = this;

        anim = GetComponent<Animator>();

        _fireHealth = _maxFireHealth;

        upgrades = new Upgrades();
    }

    private void Update()
    {

    }

    public void TakeResources()
    {
        PlayerStateMachine.Instance.Animator.SetInteger("Tool", 1);
        PlayerStateMachine.Instance.Animator.SetTrigger("Interact");
        if (PlayerStateMachine.Instance.WoodCount >= 1)
        {
            PlayerStateMachine.Instance.IsInteracting = false;
            PlayerStateMachine.Instance.WoodCount--;
            _fireHealth += woodValue;

            // Play flame noise
            PlayerAudioController audioController = PlayerStateMachine.Instance
                .gameObject.transform.GetChild(0).GetComponent<PlayerAudioController>();
            audioController.PlayTreeBreakSound();
            audioController.PlayFlameSound();
        }
        else
        {
            PlayerStateMachine.Instance.IsInteracting = false;

            // Show that there is no wood
        }
    }

    public void FireDies()
    {
        // End Game
        endGame.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            if (_fireHealth <= 0)
            {
                FireDies();
            }
            else
            {
                _fireHealth -= waterProjectileDamage;
            }
        }
    }

    public void IncreaseAttackDamage(int damageIncrease)
    {
        FireAttacks.Instance.FireballAttack = FireAttacks.Instance.FireballAttack + damageIncrease;
    }

    public void IncreaseRapidDamage(int damageIncrease)
    {
        FireAttacks.Instance.RapidFireAttack = FireAttacks.Instance.RapidFireAttack + damageIncrease;
    }

    public void SetMaxHealth(int healthIncrease)
    {
        _maxFireHealth = _maxFireHealth + healthIncrease;
    }

    public void SetWoodValue(int valueIncrease)
    {
        woodValue = woodValue + valueIncrease;
    }

    public void SetFireBallFireRate(int cooldown)
    {
        Debug.Log("I was pressed");
        FireAttacks.Instance.FireballAttackCooldowns = cooldown;
    }

    public bool CanUseHandAttack()
    {
        return upgrades.IsUpgradeUnlocked(Upgrades.UpgradeType.handAttack);
    }

    public bool CanUseSplashAttack()
    {
        return upgrades.IsUpgradeUnlocked(Upgrades.UpgradeType.splashAttack);
    }

    public bool CanUseRapidAttack()
    {
        return upgrades.IsUpgradeUnlocked(Upgrades.UpgradeType.rapidAttack);
    }

    public bool CanUseFireFlash()
    {
        return upgrades.IsUpgradeUnlocked(Upgrades.UpgradeType.fireburst);
    }

    public void DropRateIncrease()
    {
        _dropRateIncreased = true;
    }
}
