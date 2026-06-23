using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryData
{
    [Header("所持アイテム")]
    public List<ItemData> items = new();


    [Header("所持魔術書")]
    public List<SpellBookData> spellBooks = new();

    [Header("装備")]
    public EquipmentData weapon;
    public EquipmentData shield;
    public EquipmentData accessory;
}