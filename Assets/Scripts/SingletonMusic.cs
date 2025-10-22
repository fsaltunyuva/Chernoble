using UnityEngine;

public class SingletonMusic : MonoBehaviour
{
    public static SingletonMusic Instance { get; private set; }

    [SerializeField] AudioSource audioSource1;
    [SerializeField] AudioSource audioSource2;

    [SerializeField] AudioClip glock_SFX;
    [SerializeField] AudioClip noAmmo_SFX;
    [SerializeField] AudioClip flaregunIgnition_SFX;

    [SerializeField] AudioClip doorCrack_SFX;

    private void Awake()
    {
        if (Instance != null && Instance != this) // If there is an instance, and it's not me, delete myself. 
        {
            Destroy(gameObject);
        }
        else // Otherwise, set the instance to me and don't destroy me.
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This will keep the Singleton object alive between scenes.
        }
    }

    public void PlaySFX(string sfx_name)
    {
        if (sfx_name == "glock_SFX")
        {
            audioSource1.PlayOneShot(glock_SFX);
        }
        else if (sfx_name == "noAmmo_SFX")
        {
            audioSource1.PlayOneShot(noAmmo_SFX);
        }
        else if (sfx_name == "flaregunIgnition_SFX")
        {
            audioSource1.PlayOneShot(flaregunIgnition_SFX);
        }
        else if (sfx_name == "doorCrack_SFX")
        {
            audioSource1.PlayOneShot(doorCrack_SFX);
        }
    }

    public void PlayRadioactiveSFX()
    {
        audioSource2.Play();
    }

    public void StopRadioactiveSFX()
    {
        audioSource2.Stop();
    }
}