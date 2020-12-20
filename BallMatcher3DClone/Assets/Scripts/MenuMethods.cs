using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuMethods : MonoBehaviour
{
    [HideInInspector] public string[] sceneNames;

    //Main menu public objects fields
    [HideInInspector] public GameObject panelMainMenu;
    [HideInInspector] public GameObject panelAbout;
    [HideInInspector] public GameObject panelTransition;
    [HideInInspector] public GameObject panelLevels;

    //In-game public objects fields
    [HideInInspector] public GameObject mainCamera;
    [HideInInspector] public GameObject panelSettings;
    [HideInInspector] public GameObject panelGameOver;
    [HideInInspector] public GameObject panelLevelComplete;
    [HideInInspector] public Text levelText;
    [HideInInspector] public Text audioText;

    private AudioSource mainAudioSource;

    private void Start()
    {
        InitialValues();
    }

    public void OnSettings()
    {
        OnXTime(0.0f);
        panelSettings.SetActive(true);
    }

    public void OnXTime(float xTime)
    {
        Time.timeScale = xTime;
    }

    public void OnGameOver()
    {
        OnXTime(0.0f);
        panelGameOver.SetActive(true);
    }

    public void OnLevelComplete()
    {
        OnXTime(0.0f);
        panelLevelComplete.SetActive(true);
    }

    public void OnSoundToggle()
    {
        mainAudioSource.enabled = !mainAudioSource.enabled;
        audioText.text = (mainAudioSource.enabled) ? "Sound: On" : "Sound: Off";
    }

    public void OnMainMenu()
    {
        panelGameOver.SetActive(false);
        SceneManager.LoadScene(sceneNames[0]);
    }

    public void OnBackToGame()
    {
        OnXTime(1.0f);
        panelSettings.SetActive(false);
    }

    public void OnTryAgain()
    {
        panelGameOver.SetActive(false);
        string thisScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(thisScene);
    }

    public void OnNextLevel()
    {
        panelGameOver.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnExternalLink(int index)
    {
        string urlToOpen;
        switch (index)
        {
            case 0://WhatsApp
                urlToOpen = "https://api.whatsapp.com/send?phone=+905365183524";
                break;
            case 1://Linkedin
                urlToOpen = "https://www.linkedin.com/in/oguzcanergun";
                break;
            case 2://GitHub
                urlToOpen = "https://github.com/oguzcanergun";
                break;
            default://Google, just in case
                urlToOpen = "https://www.google.com/";
                break;
        }
        Application.OpenURL(urlToOpen);
    }

    public void OnMenuContactBridge(int index)
    {
        panelTransition.SetActive(true);
        StartCoroutine(PageTransition(index));
    }

    public void OnLoadLevel(int index)
    {
        SceneManager.LoadScene(sceneNames[index]);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    private void InitialValues()
    {
        sceneNames = new string[] {"MainMenu",
                                   "Level1",
                                   "Level2",
                                   "Level3",
                                   "Level4",
                                   "Level5",
                                   "Level6",
                                   "Level7",
                                   "Level8"};

        Time.timeScale = 1.0f;

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name.Equals(sceneNames[0]))
        {
            panelTransition.SetActive(false);
            MenuPanelAdjuster();
        }
        else
        {
            levelText.text = "Level: " + SceneManager.GetActiveScene().buildIndex;
            mainAudioSource = gameObject.GetComponent<AudioSource>();
            audioText.text = (mainAudioSource.enabled) ? "Sound: On" : "Sound: Off";
            panelSettings.SetActive(false);
            panelGameOver.SetActive(false);
            panelLevelComplete.SetActive(false);
        }
    }

    private IEnumerator PageTransition(int index)
    {
        RectTransform menuRect = panelMainMenu.GetComponent<RectTransform>();
        RectTransform aboutRect = panelAbout.GetComponent<RectTransform>();
        RectTransform levelsRect = panelLevels.GetComponent<RectTransform>();
        float menuFinalPosition;

        switch (index)
        {
            case 0:
                menuFinalPosition = 1440.0f;
                while (menuRect.anchoredPosition.x < menuFinalPosition)
                {
                    menuRect.anchoredPosition = new Vector2(menuRect.anchoredPosition.x + 35.0f, menuRect.anchoredPosition.y);
                    aboutRect.anchoredPosition = new Vector2(aboutRect.anchoredPosition.x + 35.0f, aboutRect.anchoredPosition.y);
                    yield return new WaitForFixedUpdate();
                }
                aboutRect.anchoredPosition = new Vector2(0.0f, aboutRect.anchoredPosition.y);
                break;
            case 1:
                menuFinalPosition = 0.0f;
                while (menuRect.anchoredPosition.x > menuFinalPosition)
                {
                    menuRect.anchoredPosition = new Vector2(menuRect.anchoredPosition.x - 35.0f, menuRect.anchoredPosition.y);
                    aboutRect.anchoredPosition = new Vector2(aboutRect.anchoredPosition.x - 35.0f, aboutRect.anchoredPosition.y);
                    yield return new WaitForFixedUpdate();
                }
                menuRect.anchoredPosition = new Vector2(0.0f, menuRect.anchoredPosition.y);
                break;
            case 2:
                menuFinalPosition = -1440.0f;
                while (menuRect.anchoredPosition.x > menuFinalPosition)
                {
                    menuRect.anchoredPosition = new Vector2(menuRect.anchoredPosition.x - 35.0f, menuRect.anchoredPosition.y);
                    levelsRect.anchoredPosition = new Vector2(levelsRect.anchoredPosition.x - 35.0f, levelsRect.anchoredPosition.y);
                    yield return new WaitForFixedUpdate();
                }
                levelsRect.anchoredPosition = new Vector2(0.0f, menuRect.anchoredPosition.y);
                break;
            case 3:
                menuFinalPosition = 0.0f;
                while (menuRect.anchoredPosition.x < menuFinalPosition)
                {
                    menuRect.anchoredPosition = new Vector2(menuRect.anchoredPosition.x + 35.0f, menuRect.anchoredPosition.y);
                    levelsRect.anchoredPosition = new Vector2(levelsRect.anchoredPosition.x + 35.0f, levelsRect.anchoredPosition.y);
                    yield return new WaitForFixedUpdate();
                }
                menuRect.anchoredPosition = new Vector2(0.0f, menuRect.anchoredPosition.y);
                break;
        }
        panelTransition.SetActive(false);
    }

    private void MenuPanelAdjuster()
    {
        panelMainMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, panelMainMenu.GetComponent<RectTransform>().anchoredPosition.y);
        panelAbout.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1440.0f, panelAbout.GetComponent<RectTransform>().anchoredPosition.y);
        panelLevels.GetComponent<RectTransform>().anchoredPosition = new Vector2(1440.0f, panelLevels.GetComponent<RectTransform>().anchoredPosition.y);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MenuMethods))]
public class HideField : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MenuMethods script = (MenuMethods)target;
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name.Equals("MainMenu"))
        {
            script.panelMainMenu = (GameObject)EditorGUILayout.ObjectField("Panel Main Menu", script.panelMainMenu, typeof(GameObject), true);
            script.panelAbout = (GameObject)EditorGUILayout.ObjectField("Panel About", script.panelAbout, typeof(GameObject), true);
            script.panelTransition = (GameObject)EditorGUILayout.ObjectField("Panel Transition", script.panelTransition, typeof(GameObject), true);
            script.panelLevels = (GameObject)EditorGUILayout.ObjectField("Panel Levels", script.panelLevels, typeof(GameObject), true);
        }
        else
        {
            script.mainCamera = (GameObject)EditorGUILayout.ObjectField("Main Camera", script.mainCamera, typeof(GameObject), true);
            script.panelSettings = (GameObject)EditorGUILayout.ObjectField("Panel Settings", script.panelSettings, typeof(GameObject), true);
            script.panelGameOver = (GameObject)EditorGUILayout.ObjectField("Panel Game Over", script.panelGameOver, typeof(GameObject), true);
            script.panelLevelComplete = (GameObject)EditorGUILayout.ObjectField("Panel Level Complete", script.panelLevelComplete, typeof(GameObject), true);
            script.levelText = (Text)EditorGUILayout.ObjectField("Level Text", script.levelText, typeof(Text), true);
            script.audioText = (Text)EditorGUILayout.ObjectField("Audio Text", script.audioText, typeof(Text), true);
        }
    }
}
#endif
