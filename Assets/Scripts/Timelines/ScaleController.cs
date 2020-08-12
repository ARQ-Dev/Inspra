using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleController : MonoBehaviour
{
    [SerializeField]
    Slider _slider;
    
    public void OnSliderValueChanged()
    {
        transform.localScale = new Vector3(_slider.value, _slider.value, _slider.value);
    }
    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("ScaleVis"))
        {
            _slider.value = PlayerPrefs.GetFloat("ScaleVis");
            OnSliderValueChanged();
        }
        else
        {
            _slider.value = 0.8f;
            OnSliderValueChanged();
        }
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("ScaleVis", _slider.value);
    }
}
