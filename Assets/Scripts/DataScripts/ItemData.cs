using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemData",menuName = "GameData/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("基本情報")]
    public int itemID;

    public string itemName;

    [TextArea]
    public string description; //説明

    public Sprite icon;

    [Header("価格")]
    public int price; //値段

    /*[Header("HP回復")]
    public int healHP;

    [Header("状態異常回復")]
    public List<StatusEffectType> cureStatuses = new();*/

    [Header("効果")]
    public List<EffectData> effects = new();
}