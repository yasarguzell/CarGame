using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

public class PickerArea : MonoBehaviour
{
    [SerializeField] Button _leftArrow, _rightArrow;
    [SerializeField] TextMeshProUGUI _areaText;
    [SerializeField] FlexibleColorPicker _picker;

    [SerializeField] Material _mainMaterial, _boruMaterial;

    [SerializeField] public Slider _metallicSlider;
    [SerializeField] public Slider _smoothnessSlider;

    Material _material;
    private bool isMainPicker;
    private void Start()
    {
        _material = _mainMaterial;
        Arrow();

        _leftArrow.onClick.AddListener(Arrow);
        _rightArrow.onClick.AddListener(Arrow);
    }
    private void OnChangeColor(Color co)
    {
        _material.color = co;
        _picker.startingColor = co;
    }
    public void MetallicSliderOnValueChanged(float value)
    {
        _material.SetFloat("_Metallic", value);
    }
    public void SmoothnessSliderOnValueChanged(float value)
    {
        _material.SetFloat("_Glossiness", value);
    }
    public void Arrow()//For button
    {
        print("Arrow");
        isMainPicker = !isMainPicker;
        _picker.color = _material.color;
        if (isMainPicker)
        {
            _material = _mainMaterial;
            _areaText.text = "Main";
        }
        else
        {
            _material = _boruMaterial;
            _areaText.text = "Secondary";
        }

        _picker.onColorChange.AddListener(OnChangeColor);
        _metallicSlider.value = _material.GetFloat("_Metallic");
        _smoothnessSlider.value = _material.GetFloat("_Glossiness");
        _metallicSlider.onValueChanged.AddListener(MetallicSliderOnValueChanged);
        _smoothnessSlider.onValueChanged.AddListener(SmoothnessSliderOnValueChanged);

    }

}
