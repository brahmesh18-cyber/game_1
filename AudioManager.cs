using UnityEngine;

namespace EchoBound
{
    public class AudioManager : MonoBehaviour
    {
        public void PlayOneShot(AudioClip clip, Vector3 position)
        {
            if (clip == null) return;
            AudioSource.PlayClipAtPoint(clip, position);
        }
    }
}
