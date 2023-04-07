using System;
using UnityEngine;

public class SliderSwitch : MonoBehaviour
{
    public event EventHandler OnSliderReached;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Slider" && OnSliderReached != null)
        {
            OnSliderReached(this, EventArgs.Empty);
        }
    }
}
