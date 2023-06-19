using System.Collections;
using UnityEngine;

public class BossCutsceneTrigger : MonoBehaviour
{
    public GameObject bossPanel;
    public GameObject[] liftingAreas;
    public EvilWizardBoss bossBehavior;

    public PlayerInput inputs;
    public GameObject cutsceneCamera;
    public Animator[] blackSides;

    public GameObject gameUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<PlayerHealth>() != null)
        {
            StartCoroutine(PlayCutscene());
        }
    }

    private void Curtains(bool showCurtains)
    {
        foreach (var animator in blackSides)
        {
            animator.SetBool("Show", showCurtains);
        }
        gameUI.SetActive(!showCurtains);
    }

    private IEnumerator PlayCutscene()
    {
        inputs.StopForCutscene();

        inputs.enabled= false;
        cutsceneCamera.SetActive(true);
        Curtains(true);

        yield return new WaitForSeconds(5f);
        cutsceneCamera.SetActive(false);
        Curtains(false);

        yield return new WaitForSeconds(1f);
        bossPanel.SetActive(true);
        bossBehavior.enabled = true;
        bossBehavior.gameObject.tag = Constants.DAMAGEABLE;


        inputs.enabled = true;
        // disable this trigger
        gameObject.SetActive(false);
    }
}
