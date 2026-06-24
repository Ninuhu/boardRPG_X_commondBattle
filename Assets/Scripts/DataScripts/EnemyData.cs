using System;
using UnityEngine;
using System.Collections.Generic;
[Serializable]
public class EnemyDropData
{
    [Header("ドロップ内容")]
    public ItemData item;
    public SpellBookData spellBook;
    public SkillData skill;
    public EquipmentData equipment; //武器、防具、装飾品

    [Range(0,100)]
    public float dropWeight = 1f;
}
public enum EnemyAIType
{
    Aggressive,      //脳筋
    Balanced,        //バランス
    MagicFocused,    //魔法重視
    Boss             //ボス専用
}
public enum RaceType
{
    Slime,
    Beast,
    Undead,
    Human,
    Demon,
    Dragon,
    Machine,
    Plant
}

[CreateAssetMenu(fileName = "EnemyData",menuName = "GameData/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("基本情報")]
    public int enemyID;

    public string enemyName;

    [TextArea]
    public string description;

    public Sprite icon;
    [Header("レベル")]
    public int level = 1;
    [Header("ステータス")]
    public int maxHP;

    public int attack;

    public int defense;

    public int magicAttack;

    public int magicDefense;

    public int speed;
    [Header("装備中魔法")]
    public MagicData equippedAttackMagic; 
   
    public MagicData equippedDefenseMagic;
    [Header("攻撃魔法使用回数")]
    public int attackMagicUses = 3;

    
    [Header("属性(種族)")]
    //public ElementType element; 
    public RaceType race;
    [Header("ボスか否か")]
    public bool isBoss;
    
    [Header("AI")]
    public EnemyAIType aiType; //雑魚敵なら三種類のAIタイプ　ボスならゆうしゅうAI
    
    [Header("状態異常耐性")]
    public List<StatusResistanceData> resistances = new();

    [Header("報酬")]
    public int expReward; //報酬経験値

    public int goldReward; //報酬の金

    [Header("行動")]
    [Tooltip("通常攻撃等のみなら空でOK")]
    public List<SkillData> skills = new();

    [Header("ドロップ率")]
    [Range(0,100)]
    public float dropChance = 50f;



    [Header("レアドロップ候補")]
    public List<EnemyDropData> rareDrops = new();

    [Header("通常ドロップ候補①")]
    public List<EnemyDropData> normalDrops1 = new();

    [Header("通常ドロップ候補②")]
    public List<EnemyDropData> normalDrops2 = new();

    [Header("ドロップ枠割合")]
    //2:9:9= 10% : 45% :45%
    public float rareWeight = 2f;

    public float normal1Weight = 9f;

    public float normal2Weight = 9f;
}