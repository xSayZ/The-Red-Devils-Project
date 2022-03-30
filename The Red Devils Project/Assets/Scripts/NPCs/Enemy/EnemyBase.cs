using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Create new enemy")]
public class EnemyBase : ScriptableObject {

    [SerializeField] private string name;

    [TextArea]
    [SerializeField] private string description;

    [SerializeField] public Sprite frontSprite;
    [SerializeField] public Sprite playerSprite;

    [SerializeField] public EnemyType type1 { get; set; }

    [SerializeField] private int maxHP;
    [SerializeField] private int attack;
    [SerializeField] private int defence;
    [SerializeField] private int spAttack;
    [SerializeField] private int spDefence;
    [SerializeField] private int speed;

    [SerializeField] private List<LearnableMoves> learnableMoves;

    #region Properties
    public string Name {
        get { return name; }
    }

    public string Description {
        get { return description; }
    }

    public Sprite FrontSprite {
        get { return frontSprite; }
    }

    public EnemyType Type1 {
        get { return type1; }
    }

    public int MaxHP {
        get { return maxHP; }
    }

    public int Attack {
        get { return attack; }
    }

    public int Defence {
        get { return defence; }
    }

    public int SpAttack {
        get { return spAttack; }
    }

    public int SpDefence {
        get { return spDefence; }
    }

    public int Speed {
        get { return speed; }
    }

    public List<LearnableMoves> LearnableMoves {
        get { return learnableMoves; }
    }

    #endregion
}

[System.Serializable]
public class LearnableMoves
{
    [SerializeField] private MoveBase moveBase;
    [SerializeField] private int level;

    public MoveBase Base {
        get { return moveBase; }
    }

    public int Level {
        get { return level; }
    }
}

public enum EnemyType 
{
    None,
    Rifleman,
    SMGRifleman,
    RifleGrenadier,
    Bomber,
    Sniper,
    Flemenwerfer,
    Panzerschrek,
    Machinegunner,
    AssistantMachinegunner,
    Officer,
    Tank
}

public class TypeChart
{
    static float[][] chart =
    {   
        //                             RM   SMG    RG    BOMB  SNP     FW    Pz   MG     A.MG  Ofz.  Tank
        /* GUN */       new float[] {  1f,   1f,   1f,    1f,  0.5f,   1f,   1f, 0,5f,    1f,  2f,  0.5f },
        /* GRENADE */   new float[] {  2f,   1f,   1f,  0.5f,    2f,   2f,   1f,   2f,    2f,  1f,  0.5f },
        /* MELEE */     new float[] {  2f, 0.5f,   2f,    1f,    1f,   1f,   1f,   2f,    2f,  1f,  0.5f },
        /* SPECIAL */   new float[] {0.5f,   1f, 0.5f,    1f,    1f,   1f,   2f,   1f,  0.5f,  1f,  0.5f },
        /* FIRE */      new float[] {  1f,   1f,   1f,    1f,    1f, 0.5f,   1f,   1f,    1f,  1f,  0.5f },
        /* AT */        new float[] {0.5f, 0.5f,   1f,    1f,    1f,   1f, 0.5f,   1f,  0.5f,  1f,    2f },
    };

    public static float GetEffectiveness(EnemyType attackType, MoveType defenceType)
    {
        if (attackType == EnemyType.None || defenceType == MoveType.None)
            return 1;

        int row = (int)attackType - 1;
        int col = (int)defenceType - 1;

        return chart[row][col];
    }
}
