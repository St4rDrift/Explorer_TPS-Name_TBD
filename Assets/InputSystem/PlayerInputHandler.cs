using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string crouch = "Crouch";
    [SerializeField] private string aim = "Aim";
    [SerializeField] private string fire = "Fire";
    [SerializeField] private string lightAttack = "Light Attack";
    [SerializeField] private string heavyAttack = "Heavy Attack";
    [SerializeField] private string interact = "Interact";
    [SerializeField] private string previous = "Previous";
    [SerializeField] private string next = "Next";

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction; 
    private InputAction sprintAction;
    private InputAction crouchAction;
    private InputAction aimAction;
    private InputAction fireAction;
    private InputAction lightAttackAction;
    private InputAction heavyAttackAction;
    private InputAction interactAction;
    private InputAction previousAction;
    private InputAction nextAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpTriggered { get; private set; }
    public bool SprintTriggered { get; private set; }
    public bool CrouchTriggered { get; private set; }
    public float AimValue { get; private set; }
    public bool FireTriggered { get; private set; }
    public bool LightAttackTriggered { get; private set; }
    public bool HeavyAttackTriggered { get; private set; }
    public bool InteractTriggered { get; private set; }
    public bool InteractHoldTriggered { get; private set; }
    public bool PreviousTriggered { get; private set; }
    public bool NextTriggered { get; private set; }


    public static PlayerInputHandler Instance { get; private set; }

    private void Awake()
    {
        //OFF FOR DEBUGGING/TESTING
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        crouchAction = playerControls.FindActionMap(actionMapName).FindAction(crouch);
        aimAction = playerControls.FindActionMap(actionMapName).FindAction(aim);
        fireAction = playerControls.FindActionMap(actionMapName).FindAction(fire);
        lightAttackAction = playerControls.FindActionMap(actionMapName).FindAction(lightAttack);
        heavyAttackAction = playerControls.FindActionMap(actionMapName).FindAction(heavyAttack);
        interactAction = playerControls.FindActionMap(actionMapName).FindAction(interact);
        previousAction = playerControls.FindActionMap(actionMapName).FindAction(previous);
        nextAction = playerControls.FindActionMap(actionMapName).FindAction(next);
        RegisterInputActions();
    }

    private void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;

        jumpAction.performed += contect => JumpTriggered = true;
        jumpAction.canceled += contect => JumpTriggered = false;

        aimAction.performed += context => AimValue = context.ReadValue<float>();
        aimAction.canceled += context => AimValue = 0f;

        fireAction.performed += context => FireTriggered = true;
        fireAction.canceled += context => FireTriggered = false;

        lightAttackAction.performed += context => LightAttackTriggered = true;
        lightAttackAction.canceled += context => LightAttackTriggered = false;

        heavyAttackAction.performed += context => HeavyAttackTriggered = true;
        heavyAttackAction.canceled += context => HeavyAttackTriggered = false;

        interactAction.performed += context => InteractHoldTriggered = true;
        interactAction.canceled += context => InteractHoldTriggered = false;

        previousAction.performed += context => PreviousTriggered = true;
        previousAction.canceled += context => PreviousTriggered = false;

        nextAction.performed += context => NextTriggered = true;
        nextAction.canceled += context => NextTriggered = false;

        // placeholderAction.performed += context => PlaceholderValue = context.ReadValue<float>();
        // placeholderAction.canceled += context => PlaceholderValue = 0f;
    }

    private void Update()
    {
        if (sprintAction.WasPressedThisFrame()) SprintTriggered = true;
        else SprintTriggered = false;

        if (crouchAction.WasPressedThisFrame()) CrouchTriggered = true;
        else CrouchTriggered = false;

        if (interactAction.WasPressedThisFrame()) InteractTriggered = true;
        else InteractTriggered = false;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
        crouchAction.Enable();
        aimAction.Enable();
        fireAction.Enable();
        lightAttackAction.Enable();
        heavyAttackAction.Enable();
        interactAction.Enable();
        previousAction.Enable();
        nextAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
        crouchAction.Disable();
        aimAction.Disable();
        fireAction.Disable();
        lightAttackAction.Disable();
        heavyAttackAction.Disable();
        interactAction.Disable();
        previousAction.Disable();
        nextAction.Disable();
    }
}
