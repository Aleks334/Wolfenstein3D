using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementManager : MonoBehaviour
{
    #region Player movement states definitions

    public PlayerMvmtInitState PlayerMvmtInitState;
    public WaitForInputState WaitForInputState;

    #endregion

    public CharacterController _characterController;

    public StateMachine PlayerStateMachine { get; private set; }

    void Awake()
    {
        #region Game Manager FSM initialization

        //instance of state machine class
        PlayerStateMachine = new StateMachine();

        #region  Instances of player states

        PlayerMvmtInitState = new PlayerMvmtInitState(this);
        WaitForInputState = new WaitForInputState(this);

        #endregion

        PlayerStateMachine.Initialize(PlayerMvmtInitState);

        #endregion 
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerStateMachine.CurrentState.UpdateState();
    }
}
