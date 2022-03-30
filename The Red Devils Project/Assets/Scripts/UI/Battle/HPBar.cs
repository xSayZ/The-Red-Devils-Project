using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject health;

    public void SetHP(float hpNormalized)
    {
        health.transform.localScale = new Vector3(hpNormalized, 1f);
    }

    public IEnumerator SetHPSmooth(float newHP)
    {
        float currentHP = health.transform.localScale.x;
        float changeAmt = currentHP - newHP;

        while (currentHP - newHP > Mathf.Epsilon)
        {
            currentHP -= changeAmt * Time.deltaTime;
            health.transform.localScale = new Vector3(currentHP, 1f);
            yield return null;
        }
        health.transform.localScale = new Vector3(newHP, 1f);
    }
}
