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
                    Sequence seq = DOTween.Sequence();
                    switch (target.gameObject.name)
                    {
                        case "Top":
                            seq.Append(blackScreen.DOFade(1, fadeDuration));
                            seq.AppendCallback(() => Singleton.Instance.isMovementEnabled = true);
                            seq.AppendCallback(() => transform.position = GetComponent<Player>().topSpawnPoint.position); 
                            seq.Append(blackScreen.DOFade(0, fadeDuration));
                            break;
                        case "Top Exit": // It says exit but it's actually the entrance from the top
                            seq.Append(blackScreen.DOFade(1, fadeDuration));
                            seq.AppendCallback(() => Singleton.Instance.isMovementEnabled = true);
                            seq.AppendCallback(() => transform.position = GetComponent<Player>().topExitSpawnPoint.position);
                            seq.Append(blackScreen.DOFade(0, fadeDuration));
                            break;
                        case "Bottom":
                            seq.Append(blackScreen.DOFade(1, fadeDuration));
                            seq.AppendCallback(() => Singleton.Instance.isMovementEnabled = true);
                            seq.AppendCallback(() => transform.position = GetComponent<Player>().bottomSpawnPoint.position);
                            seq.Append(blackScreen.DOFade(0, fadeDuration));
                            // Singleton.Instance.spawnPoint = SpawnPoint.Bottom;
                            break;
                        case "Bottom Exit":
                            seq.Append(blackScreen.DOFade(1, fadeDuration));
                            seq.AppendCallback(() => Singleton.Instance.isMovementEnabled = true);
                            seq.AppendCallback(() => transform.position = GetComponent<Player>().bottomExitSpawnPoint.position);
                            seq.Append(blackScreen.DOFade(0, fadeDuration));
                            // Singleton.Instance.spawnPoint = SpawnPoint.Bottom;
                            break;
                        case "Left":
                            seq.Append(blackScreen.DOFade(1, fadeDuration));
                            seq.AppendCallback(() => Singleton.Instance.isMovementEnabled = true);
                            seq.AppendCallback(() => transform.position = GetComponent<Player>().leftSpawnPoint.position);
                            seq.Append(blackScreen.DOFade(0, fadeDuration));
                            // Singleton.Instance.spawnPoint = SpawnPoint.Left;
                            break;
                        case "Left Exit":
                            seq.Append(blackScreen.DOFade(1, fadeDuration));
                            seq.AppendCallback(() => Singleton.Instance.isMovementEnabled = true);
                            seq.AppendCallback(() => transform.position = GetComponent<Player>().leftExitSpawnPoint.position);
                            seq.Append(blackScreen.DOFade(0, fadeDuration));
                            // Singleton.Instance.spawnPoint = SpawnPoint.Left;
                            break;
                        case "Right":
                            seq.Append(blackScreen.DOFade(1, fadeDuration));
                            seq.AppendCallback(() => Singleton.Instance.isMovementEnabled = true);
                            seq.AppendCallback(() => transform.position = GetComponent<Player>().rightSpawnPoint.position);
                            seq.Append(blackScreen.DOFade(0, fadeDuration));
                            // Singleton.Instance.spawnPoint = SpawnPoint.Left;
                            break;
                        case "Right Exit":
                            seq.Append(blackScreen.DOFade(1, fadeDuration));
                            seq.AppendCallback(() => Singleton.Instance.isMovementEnabled = true);
                            seq.AppendCallback(() => transform.position = GetComponent<Player>().rightExitSpawnPoint.position);
                            seq.Append(blackScreen.DOFade(0, fadeDuration));
                            // Singleton.Instance.spawnPoint = SpawnPoint.Left;
                            break;
                        case "Down":
                            seq.Append(blackScreen.DOFade(1, fadeDuration));
                            seq.AppendCallback(() => Singleton.Instance.isMovementEnabled = true);
                            seq.AppendCallback(() => transform.position = GetComponent<Player>().bottomSpawnPoint.position);
                            seq.Append(blackScreen.DOFade(0, fadeDuration));
                            // Singleton.Instance.spawnPoint = SpawnPoint.Right;
                            break;
                        case "Down Exit":
                            seq.Append(blackScreen.DOFade(1, fadeDuration));
                            seq.AppendCallback(() => transform.position = GetComponent<Player>().bottomExitSpawnPoint.position);
                            seq.AppendCallback(() => Singleton.Instance.isMovementEnabled = true);
                            seq.Append(blackScreen.DOFade(0, fadeDuration));
                            // Singleton.Instance.spawnPoint = SpawnPoint.Right;
                            break;
                    }
                    // blackScreen.DOFade(1, fadeDuration).OnComplete(() =>
                    // {
                    //     Random rand = new Random();
                    //
                    //     int nextSceneIndex = rand.Next(1, SceneManager.sceneCountInBuildSettings);
                    //     while (nextSceneIndex == Singleton.Instance.previoslyLoadedSceneIndex)
                    //     {
                    //         nextSceneIndex = rand.Next(1, SceneManager.sceneCountInBuildSettings);
                    //     }
                    //     SceneManager.LoadScene(nextSceneIndex);
                    // });
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

                else if (target.CompareTag("coin"))
                {
                    Singleton.Instance.AddCurrency(5);
                    Destroy(target.gameObject);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("health") || other.CompareTag("door") || other.CompareTag("teleporter") || other.CompareTag("market") || other.CompareTag("coin"))
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
