using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EquipmentData",menuName = "GameData/Equipment Data")]
public class EquipmentData : ScriptableObject
{
    [Header("基本情報")]
    public int equipmentID; //ID

    public string equipmentName;

    [TextArea]
    public string description;

    public Sprite icon;

    [Header("種類")]
    public EquipmentType equipmentType; //武器o盾or装飾品
    [Header("装備可能か否か（ほとんどの装備が全職装備可能だけど特別な武器がある）")]
    [Tooltip("特定の jobのみ可能ならTrue（基本False）")]
    public bool isExclusive; //省くか
    [Tooltip("どのjobを装備可能にするか")]
    public List<JobData> exclusiveJobs; //

    [Header("能力")]
    public int hpBonus;
    public int attackBonus; //上がる攻撃（以下すてごとに沿う）

    public int defenseBonus;

    public int magicAttackBonus;

    public int magicDefenseBonus;

    public int speedBonus;

   

    [Header("ショップ")]
    public int price;

    [Header("特殊効果")]
    public List<EffectData> effects = new();
}