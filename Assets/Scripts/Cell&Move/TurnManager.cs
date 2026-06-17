using UnityEngine;

// ターン管理
public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    [SerializeField] private PlayerMover player;
    [SerializeField] private RouletteUI rouletteUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartTurn();
    }

    // プレイヤーのターン開始
    public void StartTurn()
    {
        Debug.Log("ターン開始");

        RollRoulette();
    }

    // プレイヤーのターン終了
    public void EndTurn()
    {
        Debug.Log("ターン終了");

        StartTurn(); // 今は1人プレイなのでループ
    }

    // ルーレット開始（UI経由）
    private void RollRoulette()
    {
        rouletteUI.PlayRoulette(OnRouletteResult);
    }

    // ルーレット結果受け取り
    private void OnRouletteResult(int result)
    {
        Debug.Log($"ルーレット結果 : {result}");

        if (result == 0)
        {
            Debug.Log("移動できなかった");
            EndTurn();
            return;
        }

        player.SetMoveCount(result);
    }
}