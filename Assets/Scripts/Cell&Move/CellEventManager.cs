using UnityEngine;

// マスイベント管理
public class CellEventManager : MonoBehaviour
{
    public static CellEventManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    // マスイベント実行
    public void Execute(CellNode node)
    {
        BaseCellData data = node.cellData;

        // SOの型によって処理を変える

        if (data is BattleCellData battle)
        {
            StartBattle(node, battle);
            return;
        }

        if (data is VillageCellData village)
        {
            OpenVillage(node, village);
            return;
        }

        if (data is ShopCellData shop)
        {
            OpenShop(node, shop);
            return;
        }

        if (data is TreasureCellData treasure)
        {
            OpenTreasure(node, treasure);
            return;
        }

        if (data is TransitionCellData transition)
        {
            TransitionScene(node, transition);
            return;
        }

        Debug.LogWarning("未対応のマス");
    }

    // 戦闘マス
    private void StartBattle(
        CellNode node,
        BattleCellData data)
    {
        Debug.Log($"戦闘開始 : {data.cellName}");

        // 後でここで戦闘シーンへ移動
        // SceneManager.LoadScene(...)
    }

    // 村マス
    private void OpenVillage(
        CellNode node,
        VillageCellData data)
    {
        Debug.Log($"村 : 所有者 {node.ownerPlayerID}");

        // 村が未占領か確認
        // プレイヤーとの戦闘処理
        // 占領成功時は ownerPlayerID 更新
        // 税収計算
        // 村レベルアップ処理
    }

    // 店マス
    private void OpenShop(
        CellNode node,
        ShopCellData data)
    {
        Debug.Log($"店 : {data.shopType}");

        // shopItems を使ってショップUI表示
        // アイテム購入処理
        // 所持金チェック
    }

    // 宝箱マス
    private void OpenTreasure(
        CellNode node,
        TreasureCellData data)
    {
        if (node.treasureOpened)
        {
            Debug.Log("既に開封済み");
            return;
        }

        node.treasureOpened = true;

        Debug.Log($"宝箱 : {data.treasureType}");

        // treasureRewards の抽選
        // アイテム付与
        // 所持金加算
        // ステータス上昇処理
    }

    // シーン移動マス
    private void TransitionScene(
        CellNode node,
        TransitionCellData data)
    {
        Debug.Log($"シーン移動 : {data.targetScene}");

        // ダンジョン入場
        // ワープ処理
        // SceneManager.LoadScene(...)
    }
}