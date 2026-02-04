using UnityEngine;

public class HurtState : PlayerState
{
    private float hurtDuration = 0.35f;
    private float recoverTime;

    public HurtState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        Player.Animation?.Play("Hurt");
        recoverTime = Time.time + hurtDuration;
        Player.SetInvulnerable(Player.HurtInvulnerabilityDuration);
    }

    public override void LogicUpdate()
    {
        if (Player.Health.IsDead)
        {
            StateMachine.ChangeState(Player.DeathState);
            return;
        }

        if (Time.time < recoverTime)
        {
            return;
        }

        if (!Player.Movement.IsGrounded)
        {
            StateMachine.ChangeState(Player.FallState);
        }
        else
        {
            StateMachine.ChangeState(Player.IdleState);
        }
    }
}
