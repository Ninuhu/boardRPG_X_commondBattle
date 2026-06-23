using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellBookData",menuName = "GameData/SpellBook Data")]
public class SpellBookData : ScriptableObject
{
    [Header("基本情報")]
    public int spellbookID;

    public string spellbookName;

    [TextArea]
    public string description; //説明

    public Sprite icon;

    [Header("価格")]
    public int price; //値段
    [Header("威力")]
    public int power; //デバフなどは０

    [Header("効果")]
    public List<EffectData> effects = new();
}