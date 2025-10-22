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

    private int antibioticsCount = 0;
    [SerializeField] GameObject gameOverPanel;

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
                    Singleton.Instance.isMovementEnabled = false;
                    Sequence seq = DOTween.Sequence();

                    switch (target.gameObject.name)
                    {
                        case "Top":
                            ExitingTween(GetComponent<Player>().topSpawnPoint.position, seq);
                            break;
                        case "Top Exit": // It says exit but it's actually the entrance from the top
                            EnteringTween(GetComponent<Player>().topExitSpawnPoint.position, seq);
                            break;
                        case "Bottom":
                            ExitingTween(GetComponent<Player>().bottomSpawnPoint.position, seq);
                            // Singleton.Instance.spawnPoint = SpawnPoint.Bottom;
                            break;
                        case "Bottom Exit":
                            EnteringTween(GetComponent<Player>().bottomExitSpawnPoint.position, seq);
                            // Singleton.Instance.spawnPoint = SpawnPoint.Bottom;
                            break;
                        case "Left":
                            ExitingTween(GetComponent<Player>().leftSpawnPoint.position, seq);
                            // Singleton.Instance.spawnPoint = SpawnPoint.Left;
                            break;
                        case "Left Exit":
                            EnteringTween(GetComponent<Player>().leftExitSpawnPoint.position, seq);
                            // Singleton.Instance.spawnPoint = SpawnPoint.Left;
                            break;
                        case "Right":
                            ExitingTween(GetComponent<Player>().rightSpawnPoint.position, seq);
                            // Singleton.Instance.spawnPoint = SpawnPoint.Left;
                            break;
                        case "Right Exit":
                            EnteringTween(GetComponent<Player>().rightExitSpawnPoint.position, seq);
                            // Singleton.Instance.spawnPoint = SpawnPoint.Left;
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
                    Singleton.Instance.AddCurrency(target.gameObject.GetComponent<Coin>().GetCoinValue());
                    Destroy(target.gameObject);
                }

                else if (target.CompareTag("antibiotic"))
                {
                    antibioticsCount += 1;
                    Destroy(target.gameObject);
                    if (antibioticsCount == 3)
                    {
                        Debug.Log("game over!");
                        Singleton.Instance.isMovementEnabled = false;
                        gameOverPanel.SetActive(true);
                        // TODO: İlaçları bulup oyunu kazanınca yaratıkların hareketi durmalı, Zone'dayken damage almamalı?
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("health") || other.CompareTag("door") || other.CompareTag("teleporter") || other.CompareTag("market") || other.CompareTag("coin") || other.CompareTag("antibiotic"))
        {
            isInRange = true;
            target = other;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other == target)
        {
            if (target.CompareTag("market"))
            {
                marketCanvas.SetActive(false);
                Singleton.Instance.isMovementEnabled = true;
            }
            isInRange = false;
            target = null;
        }
    }

    public void EnteringTween(Vector3 pos, Sequence seq)
    {
        SingletonMusic.Instance.PlaySFX("doorCrack_SFX");
        seq.Append(blackScreen.DOFade(1, fadeDuration));
        seq.AppendCallback(() => Singleton.Instance.isMovementEnabled = true);
        seq.AppendCallback(() => transform.position = pos);
        seq.AppendCallback(() => _zoneDamageScript.SetHealth(_zoneDamageScript.GetMaxHealth()));
        seq.AppendCallback(() => Singleton.Instance.gunAmmo = Singleton.Instance.maxAmmo);
        seq.AppendCallback(() => Singleton.Instance.UpdateAmmoText());
        seq.Append(blackScreen.DOFade(0, fadeDuration));
    }

    public void ExitingTween(Vector3 pos, Sequence seq)
    {
        SingletonMusic.Instance.PlaySFX("doorCrack_SFX");
        seq.Append(blackScreen.DOFade(1, fadeDuration));
        seq.AppendCallback(() => Singleton.Instance.isMovementEnabled = true);
        seq.AppendCallback(() => transform.position = pos);
        seq.Append(blackScreen.DOFade(0, fadeDuration));
    }

}
