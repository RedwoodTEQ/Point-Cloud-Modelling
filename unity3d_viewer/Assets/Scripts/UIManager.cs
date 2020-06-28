using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public UISlider pointSizeSlider;

    public delegate void SliderChange(float value);
    public SliderChange onPointSizeSliderChange;

    // Start is called before the first frame update
    void Start()
    {
        if(pointSizeSlider)
        {
            pointSizeSlider.slider.onValueChanged.AddListener(delegate { OnPointSizeValueChanges(); });
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointSizeValueChanges()
    {
        var slider = pointSizeSlider.slider;
        var label = pointSizeSlider.label;
        var size = slider.value / 1000;
        if(onPointSizeSliderChange != null)
        {
            onPointSizeSliderChange(size);
            label.text = "Point Size: " + size.ToString();
        }
    }
}
