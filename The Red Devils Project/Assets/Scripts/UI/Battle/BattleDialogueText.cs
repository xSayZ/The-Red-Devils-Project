using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Ink.Runtime;

public class BattleDialogueText : MonoBehaviour {

    [SerializeField] private float typingSpeed = 0.04f;
    [SerializeField] private Color highlightedColor;

    [Header("Dialogue UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject actionSelection;
    [SerializeField] private GameObject moveSelection;
    [SerializeField] private GameObject moveDetails;

    [Header("Lists")]
    [SerializeField] private List<TextMeshProUGUI> actionTexts;
    [SerializeField] private List<TextMeshProUGUI> moveTexts;

    [Header("Text Elements")]
    [SerializeField] private TextMeshProUGUI ppText;
    [SerializeField] private TextMeshProUGUI typeText;

    public void SetDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
    }

    public IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = "";
        foreach (var letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(1f);
    }

    public void EnableDialogueText (bool enabled)
    {
        dialogueText.enabled = enabled;
    }
    public void EnableActionSelection(bool enabled)
    {
        actionSelection.SetActive(enabled);
    }
    public void EnableMoveSelection(bool enabled)
    {
        moveSelection.SetActive(enabled);
        moveDetails.SetActive(enabled);
    }

    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionTexts.Count; i++)
        {
            if (i == selectedAction)
            {
                actionTexts[i].color = highlightedColor;
            }
            else
            {
                actionTexts[i].color = Color.black;
            }
        }
    }

    public void UpdateMovesSelection(int selectedAction, Move move)
    {
        for (int i = 0; i < moveTexts.Count; i++)
        {
            if (i == selectedAction)
            {
                moveTexts[i].color = highlightedColor;
            }
            else
            {
                moveTexts[i].color = Color.black;
            }
        }

        ppText.text = $"PP {move.PP}/{move.Base.PP}";
        typeText.text = move.Base.Type.ToString();

    }

    public void SetMoveNames(List<Move> moves)
    {
        for (int i = 0; i < moveTexts.Count; i++)
        {
            if (i < moves.Count)
            {
                moveTexts[i].text = moves[i].Base.Name;
            }
            else
            {
                moveTexts[i].text = "-";
            }
        }
    }

}