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

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (_atTarget == false)
        {
            MoveToTarget();

            // Attack damage player if collision detected
        }
        else
        {
            if (currentCooldown > 0f)
            {
                Debug.Log(currentCooldown);
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

    public void FireProjectile()
    {
        Instantiate(_waterProjectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
    }

    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, LeftTargetBehavior.Instance.transform.position, moveSpeed * Time.deltaTime);

        if (transform.position == LeftTargetBehavior.Instance.transform.position)
        {
            _atTarget = true;
            _anim.SetBool("targetReached", true);
        }
    }

    private IEnumerator Attack()
    {
        _anim.SetBool("canAttack", true);
        yield return new WaitForSeconds(.5f);
        _anim.SetBool("canAttack", false);
    }
}
