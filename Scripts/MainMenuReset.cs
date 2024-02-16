using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.pause = false;
        Time.timeScale = 1.0f;  
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
