using UnityEngine;

public class RunState : PlayerState
{
    public RunState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        Player.Animation?.Play("Run");
    }

    public override void HandleInput()
    {
        var input = Player.Input;
        if (input == null)
        {
            return;
        }

        Player.Movement.SetMoveInput(input.MoveInput);

        if (input.JumpPressed)
        {
            Player.Movement.RequestJump();
        }
    }

    public override void LogicUpdate()
    {
        if (!Player.Movement.IsGrounded)
        {
            StateMachine.ChangeState(Player.FallState);
            return;
        }

        if (Mathf.Abs(Player.Input?.MoveInput.x ?? 0f) <= 0.1f)
        {
            StateMachine.ChangeState(Player.IdleState);
            return;
        }

        if (Player.Input != null && Player.Input.DashPressed && Player.Movement.CanDash())
        {
            StateMachine.ChangeState(Player.DashState);
            return;
        }

        if (Player.Input != null && Player.Input.AttackPressed)
        {
            StateMachine.ChangeState(Player.AttackState);
            return;
        }

        if (Player.Movement.TryJump())
        {
            StateMachine.ChangeState(Player.JumpState);
        }
    }
}
