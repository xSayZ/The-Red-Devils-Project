using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState { FreeRoam, Battle }

public class GameManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;

    private static GameManager instance;

    [Header("Bool")]
    [SerializeField] private bool isBattling = false;

    GameState state;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;

    }

    private void Start()
    {
        playerController.onBattleStart += StartBattle;
        battleSystem.OnBattleEnd += EndBattle;
    }

    public void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        battleSystem.StartBattle();
    }

    public void EndBattle(bool won)
    {
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
        ToggleBattleState();
        SoundManager.GetInstance().FreeRoamSoundSetup();
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleFixedUpdate();
        }
        else if (state == GameState.Battle)
        {
            battleSystem.HandleUpdate();
        }
    }

    public void ToggleBattleState()
    {
        isBattling = !isBattling;

        SwitchFocusedPlayerControlScheme();
    }

    void SwitchFocusedPlayerControlScheme()
    {
        switch (isBattling)
        {
            case true:
                InputManager.GetInstance().EnableBattleControls();
                break;

            case false:
                InputManager.GetInstance().EnableGameplayControls();
                break;
        }
    }
}

