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
    Poison,
    Paralysis,
    Sleep,
    Curse,
    Confusion
}
