using System;

[Serializable]
public class ActiveStatusEffect
{
    public StatusEffectType statusType; //その状態異常か
    public int remainingTurns; //何ターン継続か（永続なら0にする）
    public bool permanent; //永続か否か
}

public enum StatusEffectType
{
    None,
    Poison, //毒,（毎ターン、Maxhpの1/16ヘリ）
    HighPoison, //猛毒（毎ターン、Maxhpの1/6減る）
    Confusion, //混乱（1/2の確率で、選択したコマンドとは違うコマンドを選択（ランダム））
    Paralysis, //麻痺（1/4の確率で、選択したコマンドが発動しなくなる（無防備状態になる））
    Sleep, //睡眠（起きるまで選択したコマンドが発動しなくなる（無防備状態になる）
    frustration, //イライラ（「攻撃」しかできなくなる（魔法✖））
    Curse, //呪い　（毎ターンMaxhpの1/8減る　＆　５回攻撃（攻撃でも魔法攻撃でも全部　＆　その戦闘のみ）で強制死亡）
    ATmagicLock, //攻撃魔法封印(3ターンくらい｛仮｝)
    DFmagicLock //防御魔法封印(3ターンくらい｛仮｝)
    
}
public  enum StatusResistanceData
{
    //耐性
    None,
    PoisonResist, //毒、猛毒耐性
    ConfusionResist, 
    ParalysisResist,
    SleepResist,
    frustratResist,
    CurseResist 

}