using System;
using UnityEngine;

public class SliderPlatformMovement : MonoBehaviour
{
    [SerializeField] private SliderJoint2D sliderJoint;
    [SerializeField] private SliderSwitch[] switches;

    void Start()
    {
        foreach(SliderSwitch sliderSwitch in switches)
        {
            sliderSwitch.OnSliderReached += SliderSwitch_OnSliderReached;
        }
    }

    private void SliderSwitch_OnSliderReached(object sender, EventArgs e)
    {
        JointMotor2D motor = sliderJoint.motor;
        motor.motorSpeed = -motor.motorSpeed;
        sliderJoint.motor = motor;
    }
}
