using UnityEngine;

public class AttackState : PlayerState
{
    public AttackState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        Player.Animation?.Play("Attack");
        Player.Weapon?.StartAttack();
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
        if (Player.Weapon != null && Player.Weapon.IsAttacking)
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
