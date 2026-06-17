using UnityEngine;

public enum CellType
{
    Battle,
    Shop,
    Village,
    Treasure,
    Transition,
    Church,
    Inn,
    Castle
}

//|||||||||||||||||||||||||||||||||||||||
//BattleCellData(通常敵、雑魚敵)
[CreateAssetMenu(fileName = "BattleCellData",menuName = "Game/Board/Battle Cell")]
public class BattleCellData : BaseCellData
{
    [Header("戦闘設定")]

    [Tooltip("推奨レベル")]
    public int recommendedLevel;

    [Tooltip("ボス戦か")]
    public bool isBossBattle;
}

//||||||||||||||||||||||||||||
//ShopCellData 店
public enum ShopType
{
    Item, //アイテム屋
    WeaponArmor, //武器・防具屋
    Magic, //魔法屋
    Job //転職屋
}

[CreateAssetMenu(fileName = "ShopCellData",menuName = "Game/Board/Shop Cell")]
public class ShopCellData : BaseCellData
{
    [Header("店設定")]

    [Tooltip("店の種類")]
    public ShopType shopType;
}



///|||||||||||||||||||||||||||||||
//VillageCellData 村
[CreateAssetMenu(
    fileName = "VillageCellData",
    menuName = "Game/Board/Village Cell")]
public class VillageCellData : BaseCellData
{
    [Header("村設定")]

    [Tooltip("占領可能か")]
    public bool canCapture = true;
}

///||||||||||||||||||||||||||||||||||||
//TreasureCellData 宝箱
public enum TreasureType
{
    Gold, //金の宝箱ます（お金が手に入る）
    Red, //ハイリスクハイリターンのアイテムます
    Blue, //青の宝箱ます（武器、防具、魔法などのアイテムます）
    Green //緑の宝箱ます（ステータス上がる、状態異常回復など）
}

[CreateAssetMenu(
    fileName = "TreasureCellData",
    menuName = "Game/Board/Treasure Cell")]
public class TreasureCellData : BaseCellData
{
    [Header("宝箱設定")]

    [Tooltip("宝箱の種類")]
    public TreasureType treasureType;
}

//||||||||||||||||||||||||||||
//TransitionCellData ダンジョンの出入口とかワープ
public enum TransitionType
{
    Entrance,
    Exit
}

[CreateAssetMenu(
    fileName = "TransitionCellData",
    menuName = "Game/Board/Transition Cell")]
public class TransitionCellData : BaseCellData
{
    [Header("シーン遷移")]

    [Tooltip("入口か出口か")]
    public TransitionType transitionType;

    [Tooltip("移動先シーン名")]
    public string targetScene;
}