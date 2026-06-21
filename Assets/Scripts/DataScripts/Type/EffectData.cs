using System;
using UnityEngine;

[Serializable]
public class EffectData
{
    public EffectType effectType;

    public StateType targetStat;

    public float value; /*足し算じゃなくて基本掛け算 
    (戦士の力ため→攻撃力二倍、僧侶の治癒→体力半分回復（ *0.5）)*/

    [Range(0,100)]
    public int chance;

    public StatusEffectType statusEffect;
}