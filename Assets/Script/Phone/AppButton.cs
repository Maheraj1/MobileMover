using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class AppButton : MonoBehaviour {
    [SerializeField] private MobileApp_SO app;
    [SerializeField] private Button button;
    void Start() {
        button.onClick.AddListener(() => {
            //MobilePhone.Instance.SwitchApp(app.Id);
        });
    }
}
