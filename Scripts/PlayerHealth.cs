using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float curentHealth = 100;
    private float maxHealth;
    private bool doOnce = false;
    private UI_Manager ui_manager;
    [HideInInspector]public bool playerIsHit = false;
    // Start is called before the first frame update
    void Start()
    {
        playerIsHit = false;
        ui_manager = FindObjectOfType<UI_Manager>();
        
        //set max hp
        maxHealth = curentHealth;

        //set 
        float percent = curentHealth / maxHealth;
        ui_manager.playHealthImg.fillAmount = percent;
        ui_manager.playerHealthTxt.text = curentHealth.ToString("n0") + "/" + maxHealth.ToString("n0");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Player Health = " + curentHealth);

        if (curentHealth <= 0)
        {
            if(doOnce == false)
            {
                MakeNewCamera(true);
                UI_Manager.isPlayerDead = true;
                Destroy(gameObject);
                doOnce = false;
            }
        }

        if(playerIsHit == true)
        {
            float percent = curentHealth / maxHealth;
            ui_manager.playHealthImg.fillAmount = percent;
            ui_manager.playerHealthTxt.text = curentHealth.ToString("n0") + "/" + maxHealth.ToString("n0");
            playerIsHit = false;
        }
    }
    private void MakeNewCamera(bool addAudioListener)
    {
        // Create a new camera object
        GameObject cameraGameObject = new GameObject("SpawnedCamera");

        // Add the Camera component to the object
        Camera cam = cameraGameObject.AddComponent<Camera>();

        // Optionally, set the camera properties
        cam.backgroundColor = Color.black;
        cam.orthographic = false; // Set to true for an Orthographic camera
        cam.nearClipPlane = 0.3f;
        cam.farClipPlane = 1000f;
        cam.fieldOfView = 60f;

        // Make this camera the active main camera
        Camera.main.gameObject.SetActive(false); // Disable the current main camera if there is one
        cam.tag = "MainCamera"; // Setting the tag to "MainCamera" makes it the active main camera

        if (addAudioListener)
        {
            cameraGameObject.AddComponent<AudioListener>();
        }
    }
}
