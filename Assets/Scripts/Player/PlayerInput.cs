using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement movement;
    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        float moveDirection = Input.GetAxis(Constants.HORIZONTAL);
        bool jump = Input.GetButtonDown(Constants.JUMP_BUTTON);
        movement.Move(moveDirection, jump);
        if (Input.GetButtonDown(Constants.FIRE1))
        {
            movement.ShootAnimation();
        }
    }
}
