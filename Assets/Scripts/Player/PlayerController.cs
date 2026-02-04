using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(HealthSystem))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private float dashInvulnerabilityBuffer = 0.05f;
    [SerializeField] private float hurtInvulnerabilityDuration = 0.35f;

    public InputManager Input => InputManager.Instance;
    public PlayerMovement Movement { get; private set; }
    public WeaponController Weapon => weaponController;
    public HealthSystem Health { get; private set; }
    public AnimationController Animation => animationController;
    public PlayerStateMachine StateMachine { get; private set; }
    public bool IsInvulnerable => Time.time < invulnerableUntil;
    public float DashInvulnerabilityBuffer => dashInvulnerabilityBuffer;
    public float HurtInvulnerabilityDuration => hurtInvulnerabilityDuration;

    public IdleState IdleState { get; private set; }
    public RunState RunState { get; private set; }
    public JumpState JumpState { get; private set; }
    public FallState FallState { get; private set; }
    public DashState DashState { get; private set; }
    public AttackState AttackState { get; private set; }
    public HurtState HurtState { get; private set; }
    public DeathState DeathState { get; private set; }

    private float invulnerableUntil;

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

    private void OnDestroy()
    {
        Health.Died -= OnDeath;
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
        Weapon?.ResetCombo();
        invulnerableUntil = 0f;
        StateMachine.ChangeState(IdleState);
    }

    public void ApplyDamage(float amount)
    {
        if (IsInvulnerable)
        {
            return;
        }

        Health.TakeDamage(amount);
        if (!Health.IsDead)
        {
            SetInvulnerable(hurtInvulnerabilityDuration);
            StateMachine.ChangeState(HurtState);
        }
    }

    public void SetInvulnerable(float duration)
    {
        if (duration <= 0f)
        {
            return;
        }

        invulnerableUntil = Mathf.Max(invulnerableUntil, Time.time + duration);
    }

    private void OnDeath()
    {
        Weapon?.ResetCombo();
        StateMachine.ChangeState(DeathState);
    }
}
