using UnityEngine;
using System.Collections.Generic;

public class PlayerMover : MonoBehaviour
{
    public CellNode currentNode;

    [SerializeField]
    private float moveSpeed = 5f;
    private CellNode targetNode;
    public bool IsMoving => targetNode != null;
    public int RemainingMove { get; private set; } //残りの移動回数(ルーレット)
    private Stack<CellNode> moveHistory = new(); //スタックで移動の戻り操作のやつ
    private HashSet<CellNode> reachableNodes = new();
    private bool showingDestination;

    private Queue<CellNode> autoMovePath = new(); //黄色（移動可能な予測ます）のクリックしたら自動化

    private Dictionary<CellNode, List<CellNode>> destinationPaths = new();

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

        Vector3 targetPos =targetNode.transform.position + Vector3.up;

        transform.position =Vector3.MoveTowards(transform.position,targetPos,moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position,targetPos)<0.01f)
        {
            transform.position = targetPos;
            


            bool movedBack = moveHistory.Count > 0 &&targetNode == moveHistory.Peek();
            if(movedBack)
            {
                // 一歩戻った
                moveHistory.Pop();
                RemainingMove++;
                Debug.Log("戻ったので移動回数+1");
            }
            else
            {
                // 新しく進んだ
                moveHistory.Push(currentNode);
                RemainingMove--;
                Debug.Log("前進したので移動回数-1");
            }
            currentNode = targetNode;
            targetNode = null;



            // 自動移動が残っているなら続行
            if(autoMovePath.Count>0)
            {
                MoveTo(autoMovePath.Dequeue());
                return;
            }



            // 自動移動終了
            showingDestination = false;
            reachableNodes.Clear();
            
            Debug.Log($"残り移動回数 : {RemainingMove}");
            
            UpdateHighlights();
            
            if(RemainingMove<=0)
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
        if(RemainingMove<=0) return;
        
        targetNode = node;
    }


    private void UpdateHighlights()
    {
        CellNode[] allCells =FindObjectsByType<CellNode>();
        foreach(CellNode cell in allCells) cell.Highlight(CellNode.HighlightType.None);
        
        
        //移動回数が無ければ光らせない
        if(RemainingMove<=0) return;
        foreach(CellNode cell in currentNode.nextNodes)
        {
            bool isBack =moveHistory.Count > 0 &&cell == moveHistory.Peek();
            cell.Highlight(isBack ?CellNode.HighlightType.Backward :CellNode.HighlightType.Forward);
        }
    }


    // 移動回数を設定する,ルーレット結果をここに入れる
    public void SetMoveCount(int count)
    {
        RemainingMove = count;
        moveHistory.Clear();
        Debug.Log($"移動回数 : {RemainingMove}");
        // 移動可能マス更新
        UpdateHighlights();
    }
    // 降った移動回数で移動可能なマスを探す
    private void ShowReachableNodes()
    {
        reachableNodes.Clear();
        destinationPaths.Clear();
        
        List<CellNode> path = new();
        
        FindReachable(currentNode, RemainingMove,
        
        new Stack<CellNode>(), path);
    }

    public void ToggleDestinationPreview()
    {
        showingDestination = !showingDestination;
        
        UpdateHighlights();

        
        if(!showingDestination)
        {
            reachableNodes.Clear();
            destinationPaths.Clear();
            return;
        }


        ShowReachableNodes();
        foreach(CellNode node in reachableNodes) node.Highlight(CellNode.HighlightType.Destination);
    }

    // 移動回数分の移動可能なマスを探す
    private void FindReachable(CellNode node,int remaining,Stack<CellNode> history,List<CellNode> path)
    {



        if(remaining == 0)
        {
            reachableNodes.Add(node);
            
            if(!destinationPaths.ContainsKey(node)) destinationPaths[node] =new List<CellNode>(path);
            
            return;
        }



        foreach(CellNode next in node.nextNodes)
        {
            bool movedBack =history.Count > 0 && next == history.Peek();
            
            if(movedBack) continue;
            Stack<CellNode> newHistory = new Stack<CellNode>(history);
            
            newHistory.Push(node);
            
            
            List<CellNode> newPath =new List<CellNode>(path);
            
            newPath.Add(next);
            
            FindReachable(next,remaining - 1,newHistory,newPath);
        }
    }

    //黄色マスをクリックで自動移動
    public void SelectDestination(CellNode destination)
    {
        if(!destinationPaths.ContainsKey(destination)) return;
        
        autoMovePath.Clear();
        
        foreach(CellNode node in destinationPaths[destination]) autoMovePath.Enqueue(node);
        
        if(targetNode == null &&autoMovePath.Count > 0) MoveTo(autoMovePath.Dequeue());

    }
    public bool IsDestinationMode => showingDestination;
    public bool IsReachable(CellNode node)
    {
        return reachableNodes.Contains(node);
    }

    
}