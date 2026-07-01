using UnityEngine;

//戦闘中専用のBuff,DeBuffクラス（力ための2倍処理など）
[System.Serializable]
public class ActiveEffect
{
    [Header("元の効果データ")]
    public EffectData effect;
    [Header("残りターン数")]

    public int remainingTurns;
    [Header("buff等が永続か否か")]
    public bool isPermanent;
}