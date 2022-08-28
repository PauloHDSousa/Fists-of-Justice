using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseScript : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas;

    bool paused = false;
    private void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                paused = true;
                Cursor.lockState = CursorLockMode.None; 
                pauseCanvas.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                paused = false;
                Cursor.lockState = CursorLockMode.Locked;
                pauseCanvas.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
