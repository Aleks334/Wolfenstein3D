using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementManager : MonoBehaviour
{
    #region Fields and Properties

    #region Player movement states definitions

    public StandingState Standing { get; private set; }
    public RunningState Running { get; private set; }
    public StrafingState Strafing { get; private set; }

    #endregion

    public StateMachine PlayerMovementFSM { get; private set; }
    public CharacterController CharacterController { get; private set; }

    public PlayerMovementSettings MvmtSettings;

    #endregion

    void Awake()
    {
        #region Game Manager FSM initialization

        //instance of state machine class
        PlayerMovementFSM = new StateMachine();

        #region  Instances of player states

        Standing = new StandingState(this, PlayerMovementFSM);
        Strafing = new StrafingState(this, PlayerMovementFSM);
        Running = new RunningState(this, PlayerMovementFSM);

        #endregion

        PlayerMovementFSM.Initialize(Standing);
        #endregion
    }

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        PlayerMovementFSM.CurrentState.HandleInput();
        PlayerMovementFSM.CurrentState.LogicUpdate();
    }
}
