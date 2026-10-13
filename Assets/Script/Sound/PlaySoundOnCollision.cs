using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour {
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private LayerMask layer;

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == layer) {
            var go = new GameObject();
            go.transform.position = collision.contacts[0].point;
            go.AddComponent<AudioSource>().clip = audioClip;
            Destroy(go, audioClip.length);
        }
    }
}
