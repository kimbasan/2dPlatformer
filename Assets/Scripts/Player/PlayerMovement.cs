using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Shooter))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement parameters")]
    [SerializeField] private float jumpForce;
    private Rigidbody2D rigidbody2d;
    [SerializeField] private float speed;
    [SerializeField] private AnimationCurve movementCurve;

    [Header("Settings")]
    [SerializeField] private Transform groundColliderTransform;
    [SerializeField] private float jumpOffset;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform firePoint;
    private SpriteRenderer playerSprite;
    [SerializeField] private Animator playerAnimator;
    private Shooter shooter;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        shooter = GetComponent<Shooter>();
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

    public void ShootAnimation()
    {
        playerAnimator.SetTrigger(Constants.SHOOT);
    }

    public void Shoot()
    {
        shooter.Shoot();
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
            playerAnimator.SetTrigger(Constants.JUMP);
        }
    }

    private void HorizontalMove(float move)
    {
        rigidbody2d.velocity = new Vector2(movementCurve.Evaluate(move), rigidbody2d.velocity.y);
        bool movingLeft = move < 0f;
        playerSprite.flipX = movingLeft;
        if (movingLeft && firePoint.localPosition.x > 0 || !movingLeft && firePoint.localPosition.x < 0)
        {
            firePoint.localPosition = new Vector2(-firePoint.localPosition.x, firePoint.localPosition.y);
        }

    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundColliderTransform.position, jumpOffset, groundMask);
        playerAnimator.SetBool(Constants.IN_AIR, !isGrounded);

    }
}
