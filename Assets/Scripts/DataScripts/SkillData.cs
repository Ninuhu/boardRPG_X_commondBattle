using UnityEngine;
using System.Collections.Generic;


public enum SkillType
{
    Attack,
    Guard
}
[CreateAssetMenu(fileName = "SkillData",menuName = "GameData/Skill Data")]
public class SkillData : ScriptableObject
{
    [Header("基本情報")]
    public int skillID;

    public string skillName;

    [TextArea]
    public string description;

    public Sprite icon;
    [Header("性能")]
    public SkillType skillType; //攻撃コマンドか防御コマンド用か
    public int power; //威力（バフとかなら０）

    [Header("特殊効果")]
    public List<EffectData> effects = new();

    [Header("skillの使用回数")]
    public bool hasUseLimit; //使用制限があるか（ないならfalse）
    public int maxUses; //何回までスキル使えるか
}