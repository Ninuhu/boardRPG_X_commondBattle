using UnityEngine;

[CreateAssetMenu(fileName = "JobData",menuName = "GameData/Job Data")]
public class JobData : ScriptableObject
{
    [Header("基本情報")]
    [Tooltip("職業ID（重複禁止）")]
    public int jobID;
    public string jobName;

    [TextArea]
    public string description;
    public JobRank rank; //初級職、ちゅうきゅうしょく、上級職
    public Sprite icon;


    [Header("レベルアップ成長")]
    [Tooltip("レベルアップ時のHP上昇量")]
    public int hpGrowth;
    
    [Tooltip("レベルアップ時の攻撃上昇量")]
    public int attackGrowth;
    
    [Tooltip("レベルアップ時の防御上昇量")]
    public int defenseGrowth;
    
    [Tooltip("レベルアップ時の魔攻上昇量")]
    public int magicAttackGrowth;
    
    [Tooltip("レベルアップ時の魔防上昇量")]
    public int magicDefenseGrowth;
    
    [Tooltip("レベルアップ時の素早さ上昇量")]
    public int speedGrowth;



    [Header("マスター補正")]
    /*習熟度５になってマスターしたときに、次回以降levelアップしたら
    指定のステータスが追加で上がる（どの職業についてても永続）*/
    public int masteryHPBonus;
    public int masteryAttackBonus;
    public int masteryDefenseBonus;
    public int masteryMagicAttackBonus;
    public int masteryMagicDefenseBonus;
    public int masterySpeedBonus;
    
    [Header("所持数")]
    [Tooltip("この職業で持てるアイテム数")]
    public int maxItemCount;

    [Tooltip("この職業で持てる魔術書数")]
    public int maxSpellBookCount;
    [Header("この職業での攻撃魔法使用可能回数")]
    public int attackMagicUses;

    [Header("転職")]
    [Tooltip("初めてこの職業になる時の費用")]
    public int firstChangeCost;
    [Header("転職条件")]
    public JobData requiredJob;
}