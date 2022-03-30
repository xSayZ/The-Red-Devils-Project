using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogueText dialogueBox;

    public event Action<bool> OnBattleEnd;

    [SerializeField] private SoundManager soundManager;


    BattleState state;
    int currentAction;
    int currentMove;

    private void Start()
    {
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        GameManager.GetInstance().ToggleBattleState();

        soundManager.BattleSoundSetup();
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData(playerUnit.Enemy);
        enemyHud.SetData(enemyUnit.Enemy);

        dialogueBox.SetMoveNames(playerUnit.Enemy.Moves);

        yield return (dialogueBox.TypeDialogue($"A {enemyUnit.Enemy.Base.Name} decided to attack."));

        PlayerAction();
    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogueBox.TypeDialogue("Choose an action"));
        dialogueBox.EnableActionSelection(true);
    }

    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogueBox.EnableActionSelection(false);
        dialogueBox.EnableDialogueText(false);
        dialogueBox.EnableMoveSelection(true);
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;

        var move = playerUnit.Enemy.Moves[currentMove];
        yield return dialogueBox.TypeDialogue($"{playerUnit.Enemy.Base.Name} used {move.Base.Name}");

        //playerunit.PlayAttackAnimation();
        //yield return new WaitForSeconds(1f);

        //enemyUnit.PlayHitAnimation();
        var damageDetails = enemyUnit.Enemy.TakeDamage(move, playerUnit.Enemy);
        yield return enemyHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return dialogueBox.TypeDialogue($"{enemyUnit.Enemy.Base.Name} was killed");
            //enemyUnit.PlayFaintAnimation();

            yield return new WaitForSeconds(2f);
            OnBattleEnd(true);
        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;

        var move = enemyUnit.Enemy.GetRandomMove();

        yield return dialogueBox.TypeDialogue($"{enemyUnit.Enemy.Base.Name} used {move.Base.Name}");

        var damageDetails = playerUnit.Enemy.TakeDamage(move, playerUnit.Enemy);
        yield return playerHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return dialogueBox.TypeDialogue($"{playerUnit.Enemy.Base.Name} was killed");

            yield return new WaitForSeconds(2f);
            OnBattleEnd(true);
        }
        else
        {
            PlayerAction();
        }
    }

    IEnumerator ShowDamageDetails(DamageDetails damageDetails)
    {
        if (damageDetails.Critical > 1f)
        {
            yield return dialogueBox.TypeDialogue("A critical hit!");
        }
        if (damageDetails.TypeEffectiveness > 1)
        {
            yield return dialogueBox.TypeDialogue("Super effective!");
        }
        else if (damageDetails.TypeEffectiveness < 1f)
        {
            yield return dialogueBox.TypeDialogue("Not very effective...");
        }
    }

    public void HandleUpdate()
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if (state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
    }

    private void HandleActionSelection()
    {
       if (InputManager.GetInstance().GetDownPressed())
       {
           if (currentAction < 1)
                ++currentAction;
       }
       else if (InputManager.GetInstance().GetUpPressed())
       {
            if (currentAction > 0)
                --currentAction;
       }

        dialogueBox.UpdateActionSelection(currentAction);

       if (InputManager.GetInstance().GetSubmitPressed())
       {
            if(currentAction == 0)
            {
                // Fight
                PlayerMove();
            }
       }
       else if (currentAction == 1) 
       {
            // Run
       }
    }

    private void HandleMoveSelection()
    {
        if (InputManager.GetInstance().GetRightPressed())
        {
            if (currentMove < playerUnit.Enemy.Moves.Count - 1)
                ++currentMove;
        }
        else if (InputManager.GetInstance().GetLeftPressed())
        {
            if (currentMove > 0)
                ++currentMove;
        }
        else if (InputManager.GetInstance().GetDownPressed())
        {
            if (currentMove < playerUnit.Enemy.Moves.Count - 2)
                currentMove += 2;
        }
        else if (InputManager.GetInstance().GetUpPressed())
        {
            if (currentMove > 1)
                currentMove -= 2;
        }

        dialogueBox.UpdateMovesSelection(currentMove, playerUnit.Enemy.Moves[currentMove]);

        if (InputManager.GetInstance().GetSubmitPressed())
        {
            dialogueBox.EnableMoveSelection(false);
            dialogueBox.EnableDialogueText(true);
            StartCoroutine(PerformPlayerMove());
        }
    }
}
