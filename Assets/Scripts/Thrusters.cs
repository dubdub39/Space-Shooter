using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thrusters : MonoBehaviour
{
    public Image fillImage;

    private Slider slider;

    public Player player;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = 0f;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (slider.value <= slider.minValue)
            fillImage.enabled = false;
        if (slider.value > slider.minValue && !fillImage.enabled)
            fillImage.enabled = true;

        
    }

    public void SliderAmount(float sliderValue)
    {
        
        slider.value = sliderValue;
     }
}
