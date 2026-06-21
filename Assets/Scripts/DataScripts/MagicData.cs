using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicData",menuName = "GameData/Magic Data")]
public class MagicData : ScriptableObject
{
    [Header("基本情報")]
    public int magicID;

    public string magicName;

    [TextArea]
    public string description;

    public Sprite icon;

    [Header("分類")]
    public MagicType magicType;

    [Header("性能")]
    public int power;

    [Header("特殊効果")]
    public List<EffectData> effects = new();
}