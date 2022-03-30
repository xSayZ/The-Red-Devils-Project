using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [Header("Default Selected")]
    public GameObject defaultSelectedGameObject;

    // Start is called before the first frame update
    void Awake()
    {
        UpdateEventSystemDefaultSelected();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNewGame()
    {
        DataPersistenceManager.instance.NewGame();
        PlayGame();
    }

    public void OnLoadGame()
    {
        DataPersistenceManager.instance.LoadGame();
        PlayGame();
    }

    public void OnQuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void UpdateEventSystemDefaultSelected()
    {
        EventSystemManager.instance.SetCurrentSelectedGameObject(defaultSelectedGameObject);
    }
}
