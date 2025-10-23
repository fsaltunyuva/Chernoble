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
    [SerializeField] AudioClip coin_SFX;
    [SerializeField] AudioClip aiStartChase_SFX;
    [SerializeField] AudioClip aiDeath_SFX;
    [SerializeField] AudioClip teleport1_SFX;
    [SerializeField] AudioClip teleport2_SFX;
    [SerializeField] AudioClip grab_SFX;
    [SerializeField] AudioClip health_SFX;

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
        else if (sfx_name == "coin_SFX")
        {
            audioSource1.PlayOneShot(coin_SFX);
        }
        else if (sfx_name == "aiStartChase_SFX")
        {
            audioSource1.PlayOneShot(aiStartChase_SFX);
        }
        else if (sfx_name == "aiDeath_SFX")
        {
            audioSource1.PlayOneShot(aiDeath_SFX);
        }
        else if (sfx_name == "teleport1_SFX")
        {
            audioSource1.PlayOneShot(teleport1_SFX);
        }
        else if (sfx_name == "teleport2_SFX")
        {
            audioSource1.PlayOneShot(teleport2_SFX);
        }
        else if (sfx_name == "grab_SFX")
        {
            audioSource1.PlayOneShot(grab_SFX);
        }
        else if (sfx_name == "health_SFX")
        {
            audioSource1.PlayOneShot(health_SFX);
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