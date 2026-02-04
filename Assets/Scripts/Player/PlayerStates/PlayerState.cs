public abstract class PlayerState
{
    protected readonly PlayerController Player;
    protected readonly PlayerStateMachine StateMachine;

    protected PlayerState(PlayerController player, PlayerStateMachine stateMachine)
    {
        Player = player;
        StateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void HandleInput() { }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() { }
}
