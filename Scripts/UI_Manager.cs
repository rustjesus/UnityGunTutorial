using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private GameObject deathMenuObj;
    public static bool isPlayerDead = false;
    private PauseMenu pauseMenu;
    public Image playHealthImg;
    public TextMeshProUGUI playerHealthTxt;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();  
        isPlayerDead = false;
        deathMenuObj.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = PlayerManager.currentAmmo + "/" + PlayerManager.currentMaxAmmo;

        if (isPlayerDead)
        {
            pauseMenu.Pause();
            deathMenuObj.SetActive(true);
            isPlayerDead = false;
        }
    }
}
