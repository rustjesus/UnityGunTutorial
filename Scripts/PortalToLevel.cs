using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToLevel : MonoBehaviour
{
    [SerializeField] private TextMeshPro levelToLoadLabel; //3d ltext label
    [SerializeField] private string portalLinkID = "PortalLink_1"; //link for portals
    public static string portalLinkIDStatic; //for memory (to load across level)
    [SerializeField] private GameObject portalExitPos; //pos to exit at
    [SerializeField] private string levelToLoad; //the level to load
    public static bool loadingNewScene = false;
    private LevelLoader levelLoader;
    private FPS_Controller playerController;
    Scene m_scene;
    private string sceneName;

    private void Awake()
    {
        m_scene = SceneManager.GetActiveScene();    
        sceneName = m_scene.name;   

        playerController = FindObjectOfType<FPS_Controller>();
        //disable
        levelToLoadLabel.text = levelToLoad;
        levelToLoadLabel.gameObject.SetActive(false);
    }

    private void Start()
    {
        //refresh the text for multiples
        levelToLoadLabel.gameObject.SetActive(true);
        levelLoader = FindObjectOfType<LevelLoader>();

        if(portalLinkIDStatic == portalLinkID)
        {
            StartCoroutine(SetPos());

        }

    }
    IEnumerator SetPos()
    {

        //WAIT ONE FRAME
        yield return 0;

        
        //set player pos and rot
        playerController.transform.position = portalExitPos.transform.position;
        playerController.transform.rotation = portalExitPos.transform.rotation;

        loadingNewScene = false; //reset on new scene load



    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
            if(levelToLoad != sceneName)
            {
                portalLinkIDStatic = portalLinkID;//set static id
                loadingNewScene = true;
                levelLoader.LoadLevel(levelToLoad);
            }
        }
    }



}
