using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BlinkingLight : MonoBehaviour {
    [SerializeField] private float maxIntensity = 50f;
    [SerializeField] private float timePeriod = 1f;
    private float _timer = 0f;
    private Light2D _light;

    void Start() {
        _light = GetComponent<Light2D>();
    }
    void Update() {
        SetIntensity();
    }
    private void SetIntensity() {
        _timer += Time.deltaTime;
        if (_timer >= timePeriod)
            _timer -= 2 * timePeriod;
        _light.intensity = Mathf.Abs(_timer * maxIntensity / timePeriod);
    }
}
