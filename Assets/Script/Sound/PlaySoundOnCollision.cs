using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour {
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private LayerMask layer;

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == layer) {
            var go = new GameObject();
            go.transform.position = collision.contacts[0].point;
            var _audio = go.AddComponent<AudioSource>();
            _audio.clip = audioClip;
            _audio.volume = GameManager.Instance.GetVolume();
            Destroy(go, audioClip.length);
        }
    }
}
