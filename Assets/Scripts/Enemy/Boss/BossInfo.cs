using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossInfo : MonoBehaviour
{
    [SerializeField] private Health bossHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject bossPanel;

    private void Start()
    {
        bossHealth.HealthChanged += BossHealth_HealthChanged;
        bossHealth.Death += BossHealth_Death;
    }

    private void BossHealth_Death(object sender, System.EventArgs e)
    {
        StartCoroutine(HidePanel());
    }

    private IEnumerator HidePanel()
    {
        yield return new WaitForSecondsRealtime(2);
        bossPanel.SetActive(false);
    }

    private void BossHealth_HealthChanged(object sender, System.EventArgs e)
    {
        healthBar.fillAmount = bossHealth.GetFillAmount();
    }
}
