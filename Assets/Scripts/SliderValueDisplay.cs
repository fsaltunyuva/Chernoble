using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValueDisplay : MonoBehaviour
{
    [SerializeField] private Slider slider;      // Slider referansı
    [SerializeField] private TMP_Text valueText; // TextMeshPro referansı

    void Start()
    {
        if (slider == null || valueText == null)
        {
            Debug.LogWarning("Slider veya TextMeshPro atanmamış!");
            return;
        }

        // Başlangıçta güncelle
        UpdateText(slider.value);

        // Slider değiştiğinde otomatik güncelle
        slider.onValueChanged.AddListener(UpdateText);
    }

    private void UpdateText(float value)
    {
        // Slider değeri / maksimum değer şeklinde yaz
        valueText.text = $"{Mathf.RoundToInt(value)}/{Mathf.RoundToInt(slider.maxValue)}";
    }

    private void OnDestroy()
    {
        // Listener'ı kaldırmayı unutma
        if (slider != null)
            slider.onValueChanged.RemoveListener(UpdateText);
    }
}