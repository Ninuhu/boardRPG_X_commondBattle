using System;


public enum BattleCommandType
{
    //攻撃コマンド
    Attack, //通常攻撃
    AllOutAttack, //全力攻撃
    AttackMagic, //魔法攻撃
    JobSkill, //職業スキル
    FreeSkill, //自由選択スキル

    //防御コマンド
    Defense, //防御
    DefenseMagic, //防御まふぉう
    Counter, //カウンター
    SkillGuard, //職業スキル防御コマンド
    Surrender, //降参

    //SpellBook,   //将来用
    //Item         //将来用
}

[Serializable]
public class BattleCommand
{
    public BattleCharacter user; // 行動するキャラクター
    public BattleCharacter target;// 対象
    public BattleCommandType commandType; // 選択したコマンド

    public MagicData magic;// 攻撃魔法・防御魔法
    public SkillData skill;// スキル
    public SkillData guardSkill; // ssスキル防御で使用するスキル
}