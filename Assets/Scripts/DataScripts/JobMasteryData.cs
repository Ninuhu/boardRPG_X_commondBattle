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

    [Tooltip("マスター済みか")]
    public bool isMastered;
}