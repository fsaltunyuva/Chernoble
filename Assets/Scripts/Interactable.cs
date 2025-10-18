using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class Interactable : MonoBehaviour
{
    ZoneDamage _zoneDamageScript;
    private bool isInRange = false;
    private Collider2D target;
    [SerializeField] Image blackScreen;
    [SerializeField] private float fadeDuration = 5f;
    [SerializeField] private GameObject marketCanvas;

    private void Start()
    {
        _zoneDamageScript = GetComponent<ZoneDamage>();
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (target != null)
            {
                if (target.CompareTag("health"))
                {
                    Destroy(target.gameObject);
                    _zoneDamageScript.AddHealth(30);
                }
                else if (target.CompareTag("market"))
                {
                    if (!Singleton.Instance.isMarketCanvasOn)
                    {
                        marketCanvas.SetActive(true);
                        Singleton.Instance.isMarketCanvasOn = true;
                        Singleton.Instance.isMovementEnabled = false;
                    }
                    else
                    {
                        marketCanvas.SetActive(false);
                        Singleton.Instance.isMarketCanvasOn = false;
                        Singleton.Instance.isMovementEnabled = true;
                    }

                }
                else if (target.CompareTag("door"))
                {
                    // TODO: Door crack sound effect
                    Singleton.Instance.isMovementEnabled = false;
                    // switch (target.gameObject.name)
                    // {
                    //     case "Top":
                    //         Singleton.Instance.spawnPoint = SpawnPoint.Top;
                    //         break;
                    //     case "Bottom":
                    //         Singleton.Instance.spawnPoint = SpawnPoint.Bottom;
                    //         break;
                    //     case "Left":
                    //         Singleton.Instance.spawnPoint = SpawnPoint.Left;
                    //         break;
                    //     case "Down":
                    //         Singleton.Instance.spawnPoint = SpawnPoint.Right;
                    //         break;
                    // }
                    blackScreen.DOFade(1, fadeDuration).OnComplete(() =>
                    {
                        Random rand = new Random();

                        int nextSceneIndex = rand.Next(1, SceneManager.sceneCountInBuildSettings);
                        while (nextSceneIndex == Singleton.Instance.previoslyLoadedSceneIndex)
                        {
                            nextSceneIndex = rand.Next(1, SceneManager.sceneCountInBuildSettings);
                        }
                        SceneManager.LoadScene(nextSceneIndex);
                    });
                }
                else if (target.CompareTag("teleporter"))
                {
                    Singleton.Instance.isMovementEnabled = false;
                    GameObject teleporter = target.transform.gameObject;
                    GameObject teleportSlider = teleporter.transform.GetChild(0).gameObject;
                    GameObject movingPart = teleportSlider.transform.GetChild(2).gameObject;
                    teleportSlider.SetActive(true);
                    movingPart.GetComponent<Teleport>().GetInteractedTeleporter(teleporter, this.gameObject);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("health") || other.CompareTag("door") || other.CompareTag("teleporter") || other.CompareTag("market"))
        {
            isInRange = true;
            target = other;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other == target)
        {
            if(target.CompareTag("market"))
            {
                marketCanvas.SetActive(false);
                Singleton.Instance.isMovementEnabled = true;
            }
            isInRange = false;
            target = null;
        }
    }
}
