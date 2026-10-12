using UnityEngine;
using System;

public class MobilePhone : MonoBehaviour {
    public static MobilePhone Instance;
    
    public event EventHandler<OnAppSwitchEventArgs> OnAppSwitch;

    public class OnAppSwitchEventArgs : EventArgs {
        public MobileApp_SO Past;
        public MobileApp_SO Current;
    }

    [SerializeField] private GameObject homepage;
    [SerializeField] private MobileApp_SO[] mobileApps;
    private MobileApp_SO _current = null;
    private GameObject   _currentAppGO = null;
    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _currentAppGO = Instantiate(homepage, transform); 
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void SwitchApp(int id) {
        foreach (var app in mobileApps) {
            if (app.Id != id) continue;
            OnAppSwitch?.Invoke(this, new OnAppSwitchEventArgs {
                Past = _current,
                Current = app
            });

            if (_currentAppGO)
                Destroy(_currentAppGO);
            
            _current = app;
            _currentAppGO = Instantiate(app.MobileAppUI, transform).gameObject;
            break;
        }
    }
}
