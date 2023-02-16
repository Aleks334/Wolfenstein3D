using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementManager : MonoBehaviour
{
    #region Player movement states definitions

    public StandingState Standing { get; private set; }
    public StrafingState Strafing { get; private set; }

    #endregion

    public CharacterController _characterController;

    public StateMachine PlayerMovementFSM { get; private set; }

    void Awake()
    {
        #region Game Manager FSM initialization

        //instance of state machine class
        PlayerMovementFSM = new StateMachine();

        #region  Instances of player states

        Standing = new StandingState(this);
        Strafing = new StrafingState(this);

        #endregion

        PlayerMovementFSM.Initialize(Standing);

        #endregion 
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementFSM.CurrentState.HandleInput();

        PlayerMovementFSM.CurrentState.LogicUpdate();
    }
}
