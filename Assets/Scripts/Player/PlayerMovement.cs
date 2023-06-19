using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Shooter))]
[RequireComponent(typeof(MeleeAttack))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement parameters")]
    [SerializeField] private float jumpForce;
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private AnimationCurve movementCurve;

    [Header("Settings")]
    [SerializeField] private Transform groundColliderTransform;
    [SerializeField] private float jumpOffset;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform firePoint;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Animator playerAnimator;
    private Shooter shooter;
    private MeleeAttack melee;
    private bool isAttacking = false;
    [SerializeField] private float pushbackForce;

    [Header("Abilities")]
    [SerializeField] private Image arrowStatus;
    [SerializeField] private bool arrowsAttackEnabled;
    [SerializeField] private Image doubleJumpStatus;
    [SerializeField] private bool doubleJumpEnabled;
    [SerializeField] private GameObject doubleJumpEffect;

    private bool secondJumpAvailable = false;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
        melee = GetComponent<MeleeAttack>();

        if (arrowsAttackEnabled)
        {
            arrowStatus.gameObject.SetActive(true);
        }

        if (doubleJumpEnabled)
        {
            doubleJumpStatus.gameObject.SetActive(true);
        }
    }

    public void Stop()
    {
        rigidbody2d.velocity = Vector2.zero;
        playerAnimator.Play("Idle");
    }

    public void EnableArrowsAttack()
    {
        arrowsAttackEnabled = true;
        arrowStatus.gameObject.SetActive(true);
    }
    public void EnableDoubleJump()
    {
        doubleJumpEnabled = true;
        secondJumpAvailable = true;
        doubleJumpStatus.gameObject.SetActive(true);
    }

    public void Move(float move, bool isJump)
    {
        if (isJump)
        {
            Jump();
        }

        if (Math.Abs(move) > 0.01f)
        {
            HorizontalMove(move);
            playerAnimator.SetBool(Constants.IS_RUNNING, true);
        }
        else
        {
            playerAnimator.SetBool(Constants.IS_RUNNING, false);
        }
    }

    public void TryAttack()
    {
        isAttacking = true;
        melee.TryAttack();
    }

    public void TryShoot()
    {
        if (arrowsAttackEnabled)
        {
            isAttacking = true;
            shooter.TryShoot();
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

    public void Shoot()
    {
        shooter.Shoot();
    }

    public void Slash()
    {
        melee.Slash();
    }

    public void AttackAnimationEnd()
    {
        isAttacking = false;
    }

    private void Jump()
    {
        bool jumpInAir = false;
        if (isGrounded || (doubleJumpEnabled && secondJumpAvailable))
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
            playerAnimator.SetTrigger(Constants.JUMP);
            if (!isGrounded)
            {
                secondJumpAvailable = false;
                jumpInAir = true;
            }

            if (jumpInAir)
            {
                doubleJumpEffect.SetActive(true);
                doubleJumpStatus.fillAmount = 0;
            }
        }
    }

    private void HorizontalMove(float move)
    {
        if (!isAttacking)
        {
            rigidbody2d.velocity = new Vector2(movementCurve.Evaluate(move), rigidbody2d.velocity.y);
            bool movingLeft = move < 0f;
            playerSprite.flipX = movingLeft;
            if (movingLeft && firePoint.localPosition.x > 0 || !movingLeft && firePoint.localPosition.x < 0)
            {
                firePoint.localPosition = new Vector2(-firePoint.localPosition.x, firePoint.localPosition.y);
            }
        }
    }
    public void Pushback(Vector2 direction)
    {
        rigidbody2d.velocity = Vector2.zero;
        rigidbody2d.AddForce(direction.normalized * pushbackForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundColliderTransform.position, jumpOffset, groundMask);
        if (doubleJumpEnabled && isGrounded)
        {
            secondJumpAvailable = true;
            doubleJumpStatus.fillAmount = 1;
        }
        playerAnimator.SetBool(Constants.IN_AIR, !isGrounded);
        playerAnimator.SetFloat(Constants.VERTICAL_SPEED, rigidbody2d.velocity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundColliderTransform.position, jumpOffset);
    }


}
