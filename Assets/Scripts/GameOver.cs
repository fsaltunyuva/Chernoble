using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void RestartGame()
    {
        Singleton.Instance.ResetValues();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
