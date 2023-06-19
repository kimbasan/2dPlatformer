using System.Collections.Generic;
using UnityEngine;

public class EvilWizardBoss : MonoBehaviour
{
    [SerializeField] private List<Transform> movementPoints;

    [SerializeField] private float idleTime;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int attacksPerStand;
    [SerializeField] private float moveSpeed;

    [SerializeField] private Transform playerTransform;



    private Rigidbody2D rigidBody;

    private float stoppingDistance = 0.2f;

    private float currentIdleTime = 0;
    private float attacksCount = 0;

    private int targetPoint;
    private int lastPoint;

    private State state;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        state = State.Standing;
        lastPoint = movementPoints.Count - 1;
        targetPoint = 0;
    }

    void FixedUpdate()
    {
        if (currentIdleTime > idleTime)
        {
            if (attacksCount == attacksPerStand)
            {
                state = State.Moving;
                attacksCount = 0;
            }
            else
            {
                attacksCount++;
                Turn(playerTransform.position);
                animator.SetTrigger("Attack");
            }
            currentIdleTime = 0;
        }

        //animator.SetFloat(Constants.VELOCITY, rigidBody.velocity.magnitude);

        switch (state)
        {
            case State.Standing:
                {
                    currentIdleTime += Time.deltaTime;
                }
                break;
            case State.Moving:
                {
                    // move towards point
                    animator.SetBool("Moving", true);
                    MoveTowardsPoint(movementPoints[targetPoint]);
                }
                break;
        }
    }

    private void Turn(Vector2 target)
    {
        spriteRenderer.flipX = transform.position.x > target.x;
    }

    private void MoveTowardsPoint(Transform position)
    {
        Vector2 target = position.position;
        Vector2 current = transform.position;
        Turn(target);
        float distance = Vector2.Distance(current, target);

        if (distance <= stoppingDistance)
        {
            rigidBody.MovePosition(target);
            targetPoint++;
            if (targetPoint > lastPoint)
            {
                targetPoint = 0;
            }

            Debug.Log("Reached");
            state = State.Standing;
            currentIdleTime = 0;

            animator.SetBool("Moving", false);

        }
        else
        {
            Vector2 direction = target - current;
            direction.Normalize();

            rigidBody.MovePosition(current + (direction * moveSpeed * Time.deltaTime));
        }
    }

    private enum State
    {
        Standing,
        Moving
    }
}
