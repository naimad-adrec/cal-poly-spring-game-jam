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

    // Game Component Variables
    private Rigidbody2D _rb;
    private Animator _animator;

    // Game Component Getters and Setters
    public Rigidbody2D Rb { get { return _rb; } set { _rb = value; } }
    public Animator Animator { get { return _animator; } set { _animator = value; } }

    // Input Variables
    private float _dirX;
    private bool _isInteracting;

    // Input Getters and Setters
    public float DirX { get { return _dirX; } set { _dirX = value; } }
    public bool IsInteracting { get { return _isInteracting; } set { _isInteracting = value; } }

    // Resource Variables
    private int _woodCount;
    private int _coalCount;

    // Resource Getters and Setters
    public int WoodCount { get { return _woodCount; } set { _woodCount = value; } }
    public int CoalCount { get { return _woodCount; } set { _woodCount = value; } }

    private void Awake()
    {
        // Initialize Singleton
        Instance = this;

        // Call Game Components
        _rb = GetComponent<Rigidbody2D>();

        // Initialize Input System
        CustomInputs customInput = new CustomInputs();
        customInput.Player.Enable();
        customInput.Player.Movement.performed += OnMove;
        customInput.Player.Movement.canceled += OnMoveStop;
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
        _dirX = 0;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        _isInteracting = true;
    }
}
