using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        Player.Animation?.Play("Jump");
        Player.Movement.TryJump();
    }

    public override void HandleInput()
    {
        var input = Player.Input;
        if (input == null)
        {
            return;
        }

        Player.Movement.SetMoveInput(input.MoveInput);
    }

    public override void LogicUpdate()
    {
        if (Player.Movement.VerticalVelocity <= 0f)
        {
            StateMachine.ChangeState(Player.FallState);
            return;
        }

        if (Player.Input != null && Player.Input.DashPressed)
        {
            StateMachine.ChangeState(Player.DashState);
            return;
        }

        if (Player.Input != null && Player.Input.AttackPressed)
        {
            StateMachine.ChangeState(Player.AttackState);
        }
    }
}
