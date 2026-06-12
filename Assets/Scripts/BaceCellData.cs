using UnityEngine;

/// <summary>
/// 全てのマスデータの親クラス
/// </summary>
public abstract class BaseCellData : ScriptableObject
{
    public CellType cellType;
    [Header("基本情報")]

    [Tooltip("マスID（重複禁止）")]
    public int cellID;

    [Tooltip("マス名")]
    public string cellName;
    [Header("表示")]
    [Tooltip("マスアイコン")]
    public Sprite icon;

    [TextArea]
    [Tooltip("説明文")]
    public string description;

    [Header("大陸情報")]

    [Tooltip("所属大陸ID")]
    public int continentID;


    
}