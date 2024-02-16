using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuObj;
    public static bool gameIsPaused = false;
    private bool gameIsPausedLocal;
    // Start is called before the first frame update
    void Start()
    {
        gameIsPausedLocal = false;
        gameIsPaused = false;

        pauseMenuObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPausedLocal)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }


    }
    public void Resume()
    {
        gameIsPausedLocal = false;
        gameIsPaused = false;
        pauseMenuObj.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    public void Pause()
    {
        gameIsPausedLocal = true;
        gameIsPaused = true;
        pauseMenuObj.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
