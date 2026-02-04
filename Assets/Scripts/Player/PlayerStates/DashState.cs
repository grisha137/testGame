using UnityEngine;

public class DashState : PlayerState
{
    public DashState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        Player.Animation?.Play("Dash");
        Vector2 direction = Player.Input != null && Player.Input.MoveInput != Vector2.zero
            ? Player.Input.MoveInput
            : new Vector2(Player.transform.localScale.x >= 0f ? 1f : -1f, 0f);
        Player.Movement.StartDash(direction);
    }

    public override void HandleInput()
    {
        if (Player.Input != null)
        {
            Player.Movement.SetMoveInput(Player.Input.MoveInput);
        }
    }

    public override void LogicUpdate()
    {
        if (Player.Movement.IsDashing)
        {
            return;
        }

        if (!Player.Movement.IsGrounded)
        {
            StateMachine.ChangeState(Player.FallState);
            return;
        }

        if (Mathf.Abs(Player.Input?.MoveInput.x ?? 0f) > 0.1f)
        {
            StateMachine.ChangeState(Player.RunState);
        }
        else
        {
            StateMachine.ChangeState(Player.IdleState);
        }
    }
}
