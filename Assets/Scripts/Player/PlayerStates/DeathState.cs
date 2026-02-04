public class DeathState : PlayerState
{
    public DeathState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        Player.Animation?.Play("Death");
        Player.Movement.ResetMovement();
    }
}
