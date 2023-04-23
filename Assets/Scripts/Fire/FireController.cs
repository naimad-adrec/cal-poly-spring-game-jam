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
    [SerializeField] private int _fireHealth;
    private bool _isBurning = true;

    // Health Getters and Setters
    public int FireHealth { get { return _fireHealth; } set { _fireHealth = value; } }
    public bool IsBurning { get { return _isBurning; } set { _isBurning = value; } }

    // Projectile Variables
    [SerializeField] private int waterProjectileDamage = 5;

    private void Awake()
    {
        Instance = this;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isBurning == true)
        {

        }
        else
        {

        }
    }

    public void TakeResources()
    {
        if (PlayerStateMachine.Instance.WoodCount >= 1)
        {
            PlayerStateMachine.Instance.IsInteracting = false;
            PlayerStateMachine.Instance.WoodCount--;
            _fireHealth += 20;

            // Play flame noise
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
}
