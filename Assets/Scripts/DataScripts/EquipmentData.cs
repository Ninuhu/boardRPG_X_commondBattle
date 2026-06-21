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