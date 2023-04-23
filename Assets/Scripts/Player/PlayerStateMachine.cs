using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    // Singleton
    public static PlayerStateMachine Instance;

    // State Variables
    PlayerBaseState CurrentState;
    public PlayerMovingState MovingState = new PlayerMovingState();
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerInteractingState InteractingState = new PlayerInteractingState();
    public PlayerDodgeState DodgeState = new PlayerDodgeState();

    // Game Component Variables
    private Rigidbody2D _rb;
    private Animator _animator;
    private BoxCollider2D _coll;
    private SpriteRenderer _sp;

    // Game Component Getters and Setters
    public Rigidbody2D Rb { get { return _rb; } set { _rb = value; } }
    public Animator Animator { get { return _animator; } set { _animator = value; } }
    public BoxCollider2D Coll { get { return _coll; } set { _coll = value; } }
    public SpriteRenderer Sp { get { return _sp; } set { _sp = value; } }



    // Input Variables
    private float _dirX;
    private float _lastDirX;
    private bool _isInteracting;
    private bool inRange = false;
    private bool _canDodge = true;
    private bool _isDodging = false;

    // Input Getters and Setters
    public float DirX { get { return _dirX; } set { _dirX = value; } }
    public float LastDirX { get { return _lastDirX; } set { _lastDirX = value; } }
    public bool IsInteracting { get { return _isInteracting; } set { _isInteracting = value; } }
    public bool CanDodge { get { return _canDodge; } set { _canDodge = value; } }
    public bool IsDodging { get { return _isDodging; } set { _isDodging = value; } }

    // Movement Variables
    [SerializeField] private float _moveSpeed = 200f;
    [SerializeField] private float _dodgeSpeed = 500f;

    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float DodgeSpeed { get { return _dodgeSpeed; } set { _dodgeSpeed = value; } }

    // Resource Variables
    private int _woodCount;
    private int _coalCount;

    // Resource Getters and Setters
    public int WoodCount { get { return _woodCount; } set { _woodCount = value; } }
    public int CoalCount { get { return _coalCount; } set { _coalCount = value; } }

    private void Awake()
    {
        // Initialize Singleton
        Instance = this;

        // Call Game Components
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _sp = GetComponent<SpriteRenderer>();

        // Initialize Input System
        CustomInputs customInput = new CustomInputs();
        customInput.Player.Enable();
        customInput.Player.Movement.performed += OnMove;
        customInput.Player.Movement.canceled += OnMoveStop;
        customInput.Player.Dodge.performed += OnDodge;
        customInput.Player.Interact.performed += OnInteract;
    }

    private void Start()
    {
        // Starting state for the state machine
        CurrentState = IdleState;
        // Reference to the apples context (This exact monobehavior script)
        CurrentState.EnterState(this);
    }

    private void Update()
    {
        CurrentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        CurrentState = state;
        state.EnterState(this);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _dirX = context.ReadValue<Vector2>().x;
    }

    private void OnMoveStop(InputAction.CallbackContext context)
    {
        _lastDirX = _dirX;
        _dirX = 0;

    }

    private void OnDodge(InputAction.CallbackContext context)
    {
        IsDodging = true;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (inRange == true)
        {
            _isInteracting = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Enemy Left"))
        {
            if (WoodCount > 0)
            {
                WoodCount -= 1;
            }
            if (CoalCount > 0)
            {
                CoalCount -= 1;
            }
            StartCoroutine(DisableHitbox());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            inRange = false;
        }
    }

    private IEnumerator DisableHitbox()
    {
        _rb.simulated = false;
        yield return new WaitForSeconds(1f);
        _rb.simulated = true;
    }
}
