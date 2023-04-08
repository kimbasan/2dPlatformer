using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject playerDeadPanel;
    [SerializeField] private PlayerInput playerInput;

    void Awake()
    {
        playerHealth.Death += PlayerHealth_Death;
    }

    private IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(1);
        playerDeadPanel.SetActive(true);
    }

    private void PlayerHealth_Death(object sender, System.EventArgs e)
    {
        playerAnimator.SetBool(Constants.IS_DEAD, true);
        playerInput.enabled = false;
        StartCoroutine(ShowGameOverScreen());
    }
}
