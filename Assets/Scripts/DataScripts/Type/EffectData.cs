using System;
using UnityEngine;

public enum EffectValueType
{
    Add, // 加算
    Multiply, // 倍率
    Set //現在の値を無視して、指定した値に強制的に上書き
}
// 誰が攻撃、バフなどの対象か
public enum EffectTarget
{
    Self, //自分
    Target //敵
}



[Serializable]
public class EffectData
{
    [Header("効果")]
    public EffectType effectType;
    
    [Header("対象")]
    public EffectTarget target = EffectTarget.Target;

    [Header("値の計算方法")]
    public EffectValueType valueType;
    [Header("値(固定値「50回復」→50、割合「体力半分回復」→0.5)")]

    public float value; /*足し算じゃなくて基本掛け算 
    (戦士の力ため→攻撃力二倍、僧侶の治癒→体力半分回復（ *0.5）)*/

    [Range(0,100)]
    public int chance;
    [Header("継続ターン（0なら即時効果,-1 → 永続,1以上 → 継続ターン）")]
    public int duration = 0;
    /*duration = -1 → 永続
    duration = 0  → 即時効果
    duration = 1以上 → 継続ターン*/

    public StatusEffectType statusEffect;
}