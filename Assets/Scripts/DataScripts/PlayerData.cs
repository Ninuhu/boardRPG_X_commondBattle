using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [Header("基本情報")]
    public string playerName;
    public int playerID; //ID
    public int level = 1; //現在のレベル
    public int experience; //現在経験値
    public int totalExperience; //累計経験値
    public int gold; //現在の資金

    [Header("職業")]
    public JobData currentJob; //今のジョブ
    public List<JobMasteryData> jobMasteries = new(); //今までついた職

    [Header("ステータス")]

    /*初期ステータスは
    HP 100
    ATK 10
    DEF 10
    MAT 5
    MDE 5
    SPd 5*/
   public int maxHP = 100;
   public int currentHP = 100;
   public int attack = 10;
   public int defense = 10;
   public int magicAttack = 5;
   public int magicDefense = 5;
   public int speed = 5;

   
   [Tooltip("現在の状態異常")]
   public List<ActiveStatusEffect> statusEffects = new();

    [Header("自由スキル")]
    //public string equippedFreeSkill;
    public SkillData equippedFreeSkill; //SkillData作ったらこれに入れ替える

    [Header("装備中魔法")]
    //public string equippedAttackMagic;
    public MagicData equippedAttackMagic; //MagicData作ったら入れ替え
    //public string equippedDefenseMagic;
    public MagicData equippedDefenseMagic;


    [Header("所持品")]
    public InventoryData inventory = new();

    
    [Header("村")]
    public List<int> ownedVillageIDs = new(); //ID(個数)でカウント

    [Header("教会")]
    public int registeredChurchID = -1;
    [Header("死亡時")]
    public bool isDead;
    public int reviveTurnRemaining;
}