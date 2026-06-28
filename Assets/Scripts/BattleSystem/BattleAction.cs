using System;

[Serializable]
public class BattleAction
{
    public BattleCharacter user; // 行動するキャラクター
    public BattleCharacter target; // 対象
    public BattleCommandType commandType;// 行動種類
    public bool isFirstAttacker; //誰が先攻・後攻だったか


    public SkillData skill; // 使用するスキル（職業・自由）
    public MagicData magic; // 使用する魔法
    /*
    // 使用する魔導書
    public SpellBookData spellBook;

    // 使用するアイテム
    public ItemData item;
    */

    public bool isCritical; // クリティカル判定済みか
    public bool isDodged; // 回避されたか
}