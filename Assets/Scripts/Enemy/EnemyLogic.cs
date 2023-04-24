using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    // Game Object Variables
    private Animator _anim;

    // Game Object Getters and Setters
    public Animator Anim { get { return _anim; } set { _anim = value; } }

    // Position Variables
    private bool _atTarget = false;
    [SerializeField] private bool isLeft;
    private Vector3 targetPosition;

    // Movement Variables
    [SerializeField] private float moveSpeed = 1f;

    // Attack Variables
    [SerializeField] private GameObject _waterProjectile;
    private bool _canAttack = true;
    private float attackCooldown = 3f;
    private float currentCooldown;

    // Attack Getters and Setters
    public bool CanAttack { get { return _canAttack; } set { _canAttack = value; } }
    public GameObject WaterProjectile { get { return _waterProjectile; } set { _waterProjectile = value; } }

    // Health Variables
    private int _health = 100;

    // Health Getters and Setters
    public int Health { get { return _health; } set { _health = value; } }

    // Coal drop prefabs
    [SerializeField] private GameObject coalPrefab;

    private void Awake()
    {
        _anim = GetComponent<Animator>();

        if (isLeft == true)
        {
            targetPosition = LeftTargetBehavior.Instance.transform.position;
        }
        else
        {
            targetPosition = RightTargetBehavior.Instance.transform.position;
        }

        int roundCount = RoundController.Instance.RoundCount;
        Health = roundCount * roundCount * 3 + 30;
    }

    private void Update()
    {
        // Find current target position
        if (isLeft == true)
        {
            targetPosition = LeftTargetBehavior.Instance.transform.position;
        }
        else
        {
            targetPosition = RightTargetBehavior.Instance.transform.position;
        }

        // Check is target position is reached
        if (_atTarget == false)
        {
            MoveToTarget();
        }
        else
        {
            CheckTargetChange();
            if (RoundController.Instance.GameInProgress == true)
            {
                if (currentCooldown > 0f)
                {
                    currentCooldown -= Time.deltaTime;
                }
                else
                {
                    FireProjectile();
                    StartCoroutine(Attack());
                    currentCooldown = attackCooldown;
                }
            }
        }
    }

    public void FireProjectile()
    {
        Instantiate(_waterProjectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
    }

    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            _atTarget = true;
            _anim.SetBool("targetReached", true);
        }
    }

    private void CheckTargetChange()
    {
        if (transform.position != targetPosition)
        {
            _atTarget = false;
            _anim.SetBool("targetReached", false);
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Play death Animation
        RoundController.Instance.LiveEnemies--;

        if (Random.Range(0.0f, 1.0f) > 0.5f)
        {
            Instantiate(coalPrefab, transform.position, transform.rotation);
        }

        // Destroy Self
        Destroy(gameObject);
    }

    private IEnumerator Attack()
    {
        _anim.SetBool("canAttack", true);
        yield return new WaitForSeconds(.5f);
        _anim.SetBool("canAttack", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack Range"))
        {
            _atTarget = true;
        }
        else
        {
            _atTarget = false;
        }
    }
}
