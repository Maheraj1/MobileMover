using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageDisplayManager : MonoBehaviour {
    public static MessageDisplayManager Instance;
    
    [SerializeField] private RectTransform messagePosition;
    [SerializeField] private GameObject messagePrefab;
    [SerializeField] private float messageDuration = 5f;
    [SerializeField] private float messagePadding = 0.01f;
    [SerializeField] private float fadeDuration = 1f;
    [Range(0, 1f)]
    [SerializeField] private float fadeStep = .05f;

    private List<TextMeshProUGUI> _messages;
    private float _refY;
    private int newTextId = 0;
    
    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _messages = new List<TextMeshProUGUI>();
    }
    
    void Start() {
        _refY = GameObject.FindGameObjectWithTag("Overlay-UI")
            .GetComponent<CanvasScaler>().referenceResolution.y;
    }

    IEnumerator FadeInOut(int index) {
        var tmp = _messages[index];

        for (float a = 0f; a < 1f; a += fadeStep / fadeDuration) {
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, a);
            yield return new WaitForSecondsRealtime(fadeStep);
        }
        
        yield return new WaitForSecondsRealtime(messageDuration);
        
        for (float a = 1f; a > 0f; a -= fadeStep / fadeDuration) {
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, a);
            yield return new WaitForSecondsRealtime(fadeStep);
        }
        
        Destroy(_messages[index].gameObject);
        _messages[index] = null;
    }

    IEnumerator MoveText(int id) {
        if (_messages.Count == 1)
            yield break;
        float t = 0;
        while (t < 1f) {
            if (id != newTextId) {
                yield break;
            }
            int offset = 0;
            for (int i = 0; i < _messages.Count; i++) {
                if (_messages[i] is null) {
                    continue;
                }

                float padding = messagePadding;
                float y = messagePosition.position.y - (2 * (padding + _messages[i].rectTransform.rect.height) *
                                                        (_messages.Count - i - 1) *
                                                        (Screen.height / _refY));
                if (Mathf.Approximately(y, _messages[i].rectTransform.position.y)) continue;

                float y1 = Mathf.Lerp(_messages[i].rectTransform.position.y, y, t);
                _messages[i].rectTransform.position = new Vector3(
                    _messages[i].rectTransform.position.x, y1, _messages[i].rectTransform.position.z
                );
            }

            t += fadeStep;
            yield return new WaitForSecondsRealtime(fadeStep);
        }
    }

    public void DisplayMessage(string message) {
        newTextId++;
        var tmp_ugui = Instantiate(messagePrefab, messagePosition).GetComponent<TextMeshProUGUI>();
        _messages.Add(tmp_ugui);
        tmp_ugui.text = message;
        StartCoroutine(FadeInOut(_messages.Count - 1));
        StartCoroutine(MoveText(newTextId));
    }
}
