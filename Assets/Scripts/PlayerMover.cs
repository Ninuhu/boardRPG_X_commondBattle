using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public CellNode currentNode;

    [SerializeField]
    private float moveSpeed = 5f;
    private CellNode targetNode;
    public bool IsMoving => targetNode != null;
    public int RemainingMove { get; private set; } //残りの移動回数(ルーレット)
    private void Start()
    {
        if(currentNode != null)
        {
            transform.position =currentNode.transform.position + Vector3.up;
            UpdateHighlights();
        }
    }

    private void Update()
    {
        if(targetNode == null) return;

        Vector3 targetPos =
            targetNode.transform.position + Vector3.up;

        transform.position =
            Vector3.MoveTowards(transform.position,targetPos,moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position,targetPos)<0.01f)
        {
            transform.position = targetPos;
            currentNode = targetNode;
            targetNode = null;

            RemainingMove--; //idouカウント減らす
            
            Debug.Log($"残り移動回数 : {RemainingMove}");
            UpdateHighlights();
            if (RemainingMove <= 0)
            {
                currentNode.OnPlayerArrived();
                TurnManager.Instance.EndTurn();
            }
            //CellEventManager.Instance.Execute(currentNode); //マスのイベント発生
        }
    }

    public void MoveTo(CellNode node)
    {
        // 移動回数が無いなら動けない
        if(RemainingMove <= 0) return;
        
        targetNode = node;
    }


    private void UpdateHighlights()
    {
        CellNode[] allCells =FindObjectsByType<CellNode>(FindObjectsSortMode.None);
        foreach(CellNode cell in allCells) cell.Highlight(false);
        
        
        //移動回数が無ければ光らせない
        if(RemainingMove <= 0) return;
        foreach(CellNode cell in currentNode.nextNodes) cell.Highlight(true);
    }
    // 移動回数を設定する,ルーレット結果をここに入れる
    public void SetMoveCount(int count)
    {
        RemainingMove = count;
        Debug.Log($"移動回数 : {RemainingMove}");
        // 移動可能マス更新
        UpdateHighlights();
    }
}