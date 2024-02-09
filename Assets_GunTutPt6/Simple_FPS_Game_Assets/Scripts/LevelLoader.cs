using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    [SerializeField] private Image progFill;
    [SerializeField] private TextMeshProUGUI progTxt;
    private PauseMenu pauseMenu;
    private void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();  
    }
    public void LoadLevel(string sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    IEnumerator LoadAsynchronously(string sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            progFill.fillAmount = progress;
            float percent = progress * 100;
            progTxt.text = percent.ToString("n0") + "%";

            yield return null;

        }
    }

    public void ReloadScene()
    {
        pauseMenu.Resume();

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void DeletePrefs()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
