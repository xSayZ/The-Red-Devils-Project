using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Create new move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] private string name;

    [TextArea]
    [SerializeField] private string description;

    [SerializeField] private MoveType moveTypes;
    [SerializeField] private int power;
    [SerializeField] private int accuracy;
    [SerializeField] private int pp;

    #region Properties
    public string Name {
        get { return name; }
    }
    public string Description {
        get { return description; }
    }
    public MoveType Type {
        get { return moveTypes; }
    }
    public int Power {
        get { return power; }
    }
    public int Accuracy {
        get { return accuracy; }
    }
    public int PP {
        get { return pp; }
    }
    #endregion
}



public enum MoveType
{
    None,
    Gun,
    Grenade,
    Melee,
    Reload,
    Special,
    Fire,
    Smoke,
    AT
}
