using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageDisplayManager : MonoBehaviour {
    public static MessageDisplayManager Instance;
    
    [SerializeField] private RectTransform messagePosition;
    [SerializeField] private GameObject messagePrefab;
    [SerializeField] private float messageDuration = 5f;
    [SerializeField] private float fadeDuration = 1f;
    [Range(0, 1f)]
    [SerializeField] private float fadeStep = .05f;

    private List<TextMeshProUGUI> messages;
    
    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        messages = new List<TextMeshProUGUI>();
    }
    
    void Update() {
        int offset = 0;
        for (int i = 0; i < messages.Count; i++) {
            if (messages[i] == null) {
                offset++;
                continue;
            }
            float y = messages[i].rectTransform.position.y  + messages[i].rectTransform.rect.height * (messages.Count - (i-offset) - 1);
            if (Mathf.Approximately(y, messages[i].rectTransform.position.y)) continue;

            float y1 = Mathf.Lerp(messages[i].rectTransform.position.y, y, Time.deltaTime);
            messages[i].rectTransform.position = new Vector3(
                messages[i].rectTransform.position.x, y1, messages[i].rectTransform.position.z
            );
        }
    }

    IEnumerator FadeInOut(int index) {
        var tmp = messages[index];

        for (float a = 0f; a < 1f; a += fadeStep / fadeDuration) {
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, a);
            yield return new WaitForSecondsRealtime(fadeStep);
        }
        
        yield return new WaitForSecondsRealtime(messageDuration);
        
        for (float a = 1f; a > 0f; a -= fadeStep / fadeDuration) {
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, a);
            yield return new WaitForSecondsRealtime(fadeStep);
        }
        
        Destroy(messages[index].gameObject);
        messages[index] = null;
    }

    public void DisplayMessage(string message) {
        var tmp_ugui = Instantiate(messagePrefab, messagePosition).GetComponent<TextMeshProUGUI>();
        messages.Add(tmp_ugui);
        tmp_ugui.text = message;
        StartCoroutine(FadeInOut(messages.Count - 1));
    }
}
