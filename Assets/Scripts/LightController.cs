using UnityEngine;
using UnityEngine.Rendering.Universal;

// This script controls the global lighting. It sets the light intensity at the start of the game because a scene is too dark by default.
// Cannot see the changes in the editor, only in play mode.

public class LightController : MonoBehaviour
{
    [SerializeField] private float lightIntensity = 0.1f;
    [SerializeField] private Light2D globalLight;
    
    void Start()
    {
        globalLight.intensity = lightIntensity;
    }
}
