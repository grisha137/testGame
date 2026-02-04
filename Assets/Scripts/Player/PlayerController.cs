using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(HealthSystem))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private AnimationController animationController;

    public InputManager Input => InputManager.Instance;
    public PlayerMovement Movement { get; private set; }
    public WeaponController Weapon => weaponController;
    public HealthSystem Health { get; private set; }
    public AnimationController Animation => animationController;
    public PlayerStateMachine StateMachine { get; private set; }

    public IdleState IdleState { get; private set; }
    public RunState RunState { get; private set; }
    public JumpState JumpState { get; private set; }
    public FallState FallState { get; private set; }
    public DashState DashState { get; private set; }
    public AttackState AttackState { get; private set; }
    public HurtState HurtState { get; private set; }
    public DeathState DeathState { get; private set; }

    private void Awake()
    {
        Movement = GetComponent<PlayerMovement>();
        Health = GetComponent<HealthSystem>();
        StateMachine = new PlayerStateMachine();

        IdleState = new IdleState(this, StateMachine);
        RunState = new RunState(this, StateMachine);
        JumpState = new JumpState(this, StateMachine);
        FallState = new FallState(this, StateMachine);
        DashState = new DashState(this, StateMachine);
        AttackState = new AttackState(this, StateMachine);
        HurtState = new HurtState(this, StateMachine);
        DeathState = new DeathState(this, StateMachine);

        Health.Died += OnDeath;
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        if (Health.IsDead)
        {
            return;
        }

        StateMachine.CurrentState.HandleInput();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        if (Health.IsDead)
        {
            return;
        }

        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void ResetPlayer()
    {
        Movement.ResetMovement();
        Health.Initialize(Health.MaxHealth);
        StateMachine.ChangeState(IdleState);
    }

    public void ApplyDamage(float amount)
    {
        Health.TakeDamage(amount);
        if (!Health.IsDead)
        {
            StateMachine.ChangeState(HurtState);
        }
    }

    private void OnDeath()
    {
        StateMachine.ChangeState(DeathState);
    }
}
