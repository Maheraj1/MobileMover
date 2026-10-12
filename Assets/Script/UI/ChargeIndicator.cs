using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChargeIndicator : MonoBehaviour {
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI percentText;
    void Start() {
        Player.Instance.OnChargeChanged += OnChanged_Handler;
        slider.minValue = 0;
        slider.maxValue = Player.Instance.MaxCharge();
        slider.value = Player.Instance.MaxCharge();
        percentText.text = "100%";
    }

    private void OnChanged_Handler(object sender, Player.OnChargeChangedEventArgs e) {
        slider.value = e.NewCharge;
        percentText.text = e.NewCharge.ToString("F1") + "%";
    }
}
