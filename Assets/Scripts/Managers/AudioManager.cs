using UnityEngine;

namespace enjoythevibes.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip hitSound = default;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();    
            EventsManager.AddListener(Events.HitPlatform, OnPlayHitSound);
        }

        private void OnPlayHitSound()
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}