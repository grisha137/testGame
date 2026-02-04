using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float deceleration = 40f;
    [SerializeField] private Transform visualRoot;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float jumpBufferTime = 0.1f;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 0.6f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D body;
    private Vector2 moveInput;
    private float lastGroundedTime;
    private float lastJumpPressedTime;
    private float lastDashTime;
    private float dashEndTime;
    private bool isDashing;

    public bool IsGrounded { get; private set; }
    public bool IsDashing => isDashing;
    public float VerticalVelocity => body.velocity.y;
    public float DashDuration => dashDuration;
    public float FacingDirection { get; private set; } = 1f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void ApplyCharacterData(CharacterData data)
    {
        if (data == null)
        {
            return;
        }

        moveSpeed = data.moveSpeed;
        acceleration = data.acceleration;
        deceleration = data.deceleration;
        jumpForce = data.jumpForce;
        coyoteTime = data.coyoteTime;
        jumpBufferTime = data.jumpBufferTime;
        dashSpeed = data.dashSpeed;
        dashDuration = data.dashDuration;
        dashCooldown = data.dashCooldown;
    }

    private void Update()
    {
        UpdateGrounded();

        if (IsGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (isDashing && Time.time >= dashEndTime)
        {
            isDashing = false;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        float targetSpeed = moveInput.x * moveSpeed;
        float speedDiff = targetSpeed - body.velocity.x;
        float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : deceleration;
        float movement = speedDiff * accelRate;
        body.AddForce(Vector2.right * movement);
    }

    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
        UpdateFacing(input.x);
    }

    public void RequestJump()
    {
        lastJumpPressedTime = Time.time;
    }

    public bool TryJump()
    {
        if (Time.time - lastJumpPressedTime <= jumpBufferTime && Time.time - lastGroundedTime <= coyoteTime && !isDashing)
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            lastJumpPressedTime = -999f;
            return true;
        }

        return false;
    }

    public bool CanDash()
    {
        return Time.time - lastDashTime >= dashCooldown && !isDashing;
    }

    public void StartDash(Vector2 direction)
    {
        if (!CanDash())
        {
            return;
        }

        lastDashTime = Time.time;
        dashEndTime = Time.time + dashDuration;
        isDashing = true;
        body.velocity = direction.normalized * dashSpeed;
    }

    public void ResetMovement()
    {
        body.velocity = Vector2.zero;
        isDashing = false;
        lastDashTime = -999f;
        lastJumpPressedTime = -999f;
        lastGroundedTime = -999f;
    }

    private void UpdateGrounded()
    {
        if (groundCheck == null)
        {
            IsGrounded = false;
            return;
        }

        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) != null;
    }

    private void UpdateFacing(float horizontal)
    {
        if (Mathf.Abs(horizontal) <= 0.01f)
        {
            return;
        }

        FacingDirection = Mathf.Sign(horizontal);
        Transform target = visualRoot != null ? visualRoot : transform;
        Vector3 scale = target.localScale;
        scale.x = Mathf.Abs(scale.x) * FacingDirection;
        target.localScale = scale;
    }
}
