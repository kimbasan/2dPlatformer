using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement movement;
    [SerializeField] private PlayerHealth health;

    private float pushTimer;
    private bool pushing;
    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        health.PlayerHit += Health_PlayerHit;
        pushTimer = 0;
        pushing = false;
    }

    private void Health_PlayerHit(object sender, System.EventArgs e)
    {
        HitEventArgs hit = (HitEventArgs)e;
        pushTimer = 0.6f;
        movement.Pushback((Vector2)transform.position - hit.hitPosition);
        pushing = true;
    }

    void Update()
    {
        float moveDirection = Input.GetAxis(Constants.HORIZONTAL);
        bool jump = Input.GetButtonDown(Constants.JUMP_BUTTON);

        if (!pushing)
        {
            movement.Move(moveDirection, jump);
        }
        else
        {
            pushTimer -= Time.deltaTime;
            if (pushTimer <= 0)
            {
                pushing = false;
                pushTimer = 0;
            }
        }


        if (Input.GetButtonDown(Constants.FIRE1))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                movement.TryAttack();
            }
        }

        if (Input.GetButtonDown(Constants.FIRE2))
        {
            movement.TryShoot();
        }
    }


    public void StopForCutscene()
    {
        movement.Stop();
    }
}
