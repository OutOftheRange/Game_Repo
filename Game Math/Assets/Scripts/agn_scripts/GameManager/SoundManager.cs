using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    private bool musicIsOn = true;

    void Start()
    {
        GameObject musicObject = GameObject.FindGameObjectWithTag("Music");

        if (musicObject != null)
        {
            audioSource = musicObject.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = musicObject.AddComponent<AudioSource>();
                audioSource.playOnAwake = false;
            }
        }
        else
        {
            Debug.LogError("Object not found");
        }
    }

    public void ToggleMusic()
    {
        musicIsOn = !musicIsOn;

        if (audioSource != null)
        {
            if (musicIsOn)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Pause();
            }
        }
        else
        {
            Debug.LogError("AudioSource not found");
        }
    }
}
