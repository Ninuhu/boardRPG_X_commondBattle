using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryData
{
    [Header("所持アイテム")]
    public List<string> items = new();

    [Header("所持魔術書")]
    public List<string> spellBooks = new();

    [Header("装備")]
    public string weapon;
    public string shield;
    public string accessory;
}