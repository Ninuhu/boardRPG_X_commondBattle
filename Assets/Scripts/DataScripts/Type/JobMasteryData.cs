using System;
using UnityEngine;

[Serializable]
public class JobMasteryData
{
    public JobData job;

    [Tooltip("習熟度レベル（0～5）")]
    public int masteryLevel;

    [Tooltip("現在の勝利数")]
    public int currentWins;
    [Tooltip("合計勝利数")]
    public int totalWins;

    [Tooltip("マスター済みか")]
    public bool isMastered;
    public bool isUnlocked;//この職業が解放済みか
}