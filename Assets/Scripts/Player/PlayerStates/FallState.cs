using UnityEngine;

public class FallState : PlayerState
{
    public FallState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        Player.Animation?.Play("Fall");
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
        if (Player.Movement.IsGrounded)
        {
            if (Mathf.Abs(Player.Input?.MoveInput.x ?? 0f) > 0.1f)
            {
                StateMachine.ChangeState(Player.RunState);
            }
            else
            {
                StateMachine.ChangeState(Player.IdleState);
            }
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

        Player.Movement.TryJump();
    }
}
