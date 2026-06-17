using UnityEngine;

[CreateAssetMenu(
    fileName = "ContinentData",
    menuName = "Game/Board/ContinentData")]
public class ContinentData : ScriptableObject
{
    [Header("基本情報")]
    public int continentID;          // 大陸ID
    public string continentName;     // 大陸名

    [TextArea]
    public string description;       // 大陸説明

    [Header("ゲーム進行")]
    public int recommendedLevel;     // 推奨レベル
    public int bossCellID;           // ボスマスID

    // 将来的に追加
    // public AudioGeneral continentBGM;
    // public EnemyTable enemyTable;
    // public BossData bossData;
}