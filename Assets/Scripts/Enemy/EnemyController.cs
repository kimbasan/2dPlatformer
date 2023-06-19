
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float idleTime;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    private Rigidbody2D rigidBody;

    private State state = State.Idle;
    private float currentIdleTime = 0;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentIdleTime > idleTime)
        {
            state = State.Turning;
            currentIdleTime = 0;
        }
        animator.SetFloat(Constants.VELOCITY, rigidBody.velocity.magnitude);

        switch(state)
        {
            case State.Idle:
                {
                    currentIdleTime += Time.deltaTime; 
                }
                break;
            case State.Walking:
                {
                    rigidBody.velocity = Vector3.right * speed;
                }
                break;
            case State.Turning: 
                {
                    speed = -speed;
                    sprite.flipX = !sprite.flipX;
                    state = State.Walking;
                } break;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.ENEMY_STOPPER))
        {
            state = State.Idle;
            //
            rigidBody.velocity = Vector3.zero;
        }
    }

    private enum State
    {
        Idle,
        Walking,
        Turning
    }
}
