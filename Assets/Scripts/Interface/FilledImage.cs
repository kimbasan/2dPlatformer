using UnityEngine;
using UnityEngine.UI;

public class FilledImage : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Health health;

    private void Awake()
    {
        health.HealthChanged += Health_HealthChanged;
    }

    private void Health_HealthChanged(object sender, System.EventArgs e)
    {
        UpdateImage(health.GetFillAmount());
    }

    public void UpdateImage(float fillAmount)
    {
        image.fillAmount = fillAmount;
    }
}
