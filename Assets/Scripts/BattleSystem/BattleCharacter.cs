using UnityEngine;
using System.Collections.Generic;


/*基本情報
────────────
・名前
・PlayerかEnemyか

ステータス
────────────
・最大HP
・現在HP
・攻撃
・防御
・魔攻
・魔防
・素早さ

装備
────────────
・攻撃魔法
・防御魔法
・職業スキル
・自由スキル

使用回数
────────────
・攻撃魔法残り回数
・職業スキル残り回数
・自由スキル残り回数

状態
────────────
・状態異常
・バフ
・デバフ
・防御状態
・降参状態
・呪い攻撃回数

戦闘補正
────────────
・クリティカル率補正
・回避率補正*/

//|||||||||||||||||||||||||||||||||||||||||||||||||||||||
//戦闘中専用のBuff,DeBuffクラス（力ための2倍処理など）
[System.Serializable]
public class ActiveEffect
{
    [Header("元の効果データ")]
    public EffectData effect;
    [Header("残りターン数")]

    public int remainingTurns;
    [Header("buff等が永続か否か")]
    public bool isPermanent;
}

//|||||||||||||||||||||||||||||||||||||||||||||||||||||||
[System.Serializable]
public class BattleCharacter
{
    [Header("基本情報")]
    public string characterName; //キャラ名(PlayerDataから参照)

    [Tooltip("PlayerならTrue、EnemyならFalse")]
    public bool isPlayer;
    public PlayerData playerData;
    public EnemyData enemyData;

    [Header("現在ステータス")]
    /*装備やBuff,DeBaff,アイテム効果などの
    ステータス変化分すべて込みのステータス*/
    public int maxHP;
    public int currentHP;

    public int attack;
    public int defense;
    public int magicAttack;
    public int magicDefense;
    public int speed;

    [Header("装備中")]
    public MagicData equippedAttackMagic;
    public MagicData equippedDefenseMagic;
    public SkillData equippedJobSkill;
    public SkillData equippedFreeSkill;

    [Header("使用回数（魔法、スキル）")]
    public int remainingAttackMagicUses;
    public int remainingJobSkillUses;
    public int remainingFreeSkillUses;

    [Header("状態異常")]
    public List<ActiveStatusEffect> statusEffects = new();

    [Header("戦闘中バフ・デバフ")]
    public List<ActiveEffect> activeBuffs = new();
    public List<ActiveEffect> activeDebuffs = new();

    [Header("戦闘状態(防御コマンド)")]
    public bool isDefending; //防御
    public bool isMagicDefending; //魔法防御
    public bool isCountering; //カウンター
    public bool hasSurrendered; //降参

    [Tooltip("呪いで攻撃した回数")]
    public int curseAttackCount; //状態異常の呪い状態での攻撃

    [Header("戦闘補正")]
    [Tooltip("装備・スキルなどによるクリティカル率補正")]
    public float criticalRateBonus;

    [Tooltip("装備・スキルなどによる回避率補正")]
    public float evasionRateBonus;

    [Header("戦闘終了")]
    public bool isDead;
}
