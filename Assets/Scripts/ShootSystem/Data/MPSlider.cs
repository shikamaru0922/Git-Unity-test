using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MPSlider : MonoBehaviour
{
    public Slider slider;

    private float sliderValue;

    public float SliderValue { get => sliderValue; set {

            slider.value = value;
        
        } }

    
}
