using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SkillData",menuName = "GameData/Skill Data")]
public class SkillData : ScriptableObject
{
    [Header("基本情報")]
    public int skillID;

    public string skillName;

    [TextArea]
    public string description;

    public Sprite icon;
    [Header("特殊効果")]
    public List<EffectData> effects = new();
}