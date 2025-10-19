using UnityEngine;

public class SingletonMusic : MonoBehaviour
{
    public static SingletonMusic Instance { get; private set; }

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
}