using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementManager : MonoBehaviour
{
    #region Player movement states definitions

    public StandingState Standing { get; private set; }
    public RunningState Running { get; private set; }
    public StrafingState Strafing { get; private set; }

    #endregion
    public StateMachine PlayerMovementFSM { get; private set; }


    public CharacterController _characterController;

    //Movement variables
    public float mvmtSpeed = 12f;
    public float runningRate = 1.7f;

    public Vector3 inputVector;
    public Vector3 movementVector;
    public bool IsRunning { get; set; }
    public bool IsStrafing { get; set; }

    public float inputRotation;
    public float sensitivity = 110f;

    void Awake()
    {
        #region Game Manager FSM initialization

        //instance of state machine class
        PlayerMovementFSM = new StateMachine();

        #region  Instances of player states

        Standing = new StandingState(this);
        Strafing = new StrafingState(this);
        Running = new RunningState(this);

        #endregion

        PlayerMovementFSM.Initialize(Standing);

        #endregion 
    }

    void Start()
    {
        _characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementFSM.CurrentState.HandleInput();

        PlayerMovementFSM.CurrentState.LogicUpdate();
    }
}
