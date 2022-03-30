using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleHud : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] HPBar hpBar;

    Enemy _enemy;

    public void SetData(Enemy enemy)
    {
        _enemy = enemy;

        nameText.text = enemy.Base.Name;
        #region Level to Rank
        if (enemy.Level == 1)
        {
            rankText.text = "Rct.";
        }
        else if (enemy.Level == 2)
        {
            rankText.text = "Pte.";
        }
        else if (enemy.Level == 3)
        {
            rankText.text = "LCpl.";
        }
        else if (enemy.Level == 4)
        {
            rankText.text = "Cpl.";
        }
        else if (enemy.Level == 5)
        {
            rankText.text = "Sgt.";
        }
        else if (enemy.Level > 5)
        {
            rankText.text = "Lt.";
        }
        #endregion

        hpBar.SetHP((float)enemy.HP / enemy.MaxHP);
    }

    public IEnumerator UpdateHP()
    {
        yield return hpBar.SetHPSmooth((float)_enemy.HP / _enemy.MaxHP);
    }
}
