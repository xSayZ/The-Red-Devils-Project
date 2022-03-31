// ########################################################
// #
// #
// #            Script written by Felix
// #
// #            Referenced and sourced:
// #            Brackeys - Youtube
// #
// #
// #
// #
// #
// ########################################################

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public bool isPaused = false;

    [SerializeField] private GameObject pauseMenuUI;

    [Header("Default Selected")]
    public GameObject defaultSelectedGameObject;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.GetInstance().GetPausePressed())
        {   
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        UpdateEventSystemDefaultSelected();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitGame()
    {
        Resume();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    void UpdateEventSystemDefaultSelected()
    {
        EventSystemManager.instance.SetCurrentSelectedGameObject(defaultSelectedGameObject);
    }
}
