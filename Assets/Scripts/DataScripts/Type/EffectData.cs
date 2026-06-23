using System;
using UnityEngine;

public enum EffectValueType
{
    Add, // 加算
    Multiply, // 倍率
    Set //現在の値を無視して、指定した値に強制的に上書き
}

[Serializable]
public class EffectData
{
    public EffectType effectType;

    public StateType targetStat;
    [Header("値の計算方法")]
    public EffectValueType valueType;
    [Header("値(固定値「50回復」→50、割合「体力半分回復」→0.5)")]

    public float value; /*足し算じゃなくて基本掛け算 
    (戦士の力ため→攻撃力二倍、僧侶の治癒→体力半分回復（ *0.5）)*/

    [Range(0,100)]
    public int chance;

    public StatusEffectType statusEffect;
}