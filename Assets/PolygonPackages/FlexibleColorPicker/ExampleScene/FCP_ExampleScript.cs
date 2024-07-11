using UnityEngine;
using UnityEngine.UI;

public class FCP_ExampleScript : MonoBehaviour {

    public bool getStartingColorFromMaterial;
    public FlexibleColorPicker fcp;
    public Material material;

    [SerializeField] public Slider _metallicSlider;
    [SerializeField] public Slider _smoothnessSlider;
    private void Start() {

        if(getStartingColorFromMaterial)
            fcp.color = material.color;

        fcp.onColorChange.AddListener(OnChangeColor);
        _metallicSlider.onValueChanged.AddListener(MetallicSliderOnValueChanged);
        
    }

    private void OnChangeColor(Color co) {
        print("Renk degisti");
        material.color = co;
        fcp.startingColor = co;
    }
    public void MetallicSliderOnValueChanged(float value)
    {
        material.SetFloat("_Glossiness", value);
    }
}
