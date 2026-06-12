using System.Collections.Generic;
using UnityEngine;

public class CellNode : MonoBehaviour
{
    [Header("接続先")]
    public List<CellNode> nextNodes = new();

    [Header("マス共通データ")]
    [Tooltip("このマスのSO")]
    public BaseCellData cellData;

    /*===========================
    村用データ
    ===========================*/
    [Header("村用")]
    [Tooltip("-1 = 未占領")]
    public int ownerPlayerID = -1;

    [Tooltip("村の収入")]
    public int villageIncome;

    [Tooltip("村の発展レベル")]
    public int villageLevel = 1;

    /*===========================
    店用データ
    ===========================*/
    [Header("店用")]
    [Tooltip("この店で販売するアイテム（将来的には ItemData 等へ変更予定）")]
    /*
    ItemData,WeaponData,MagicData   これらに変更予定
    */
    public ScriptableObject[] shopItems;

    /*===========================
    宝箱用データ
    ===========================*/
    [Header("宝箱用")]
    [Tooltip("開封済みか")]
    public bool treasureOpened;

    [Tooltip("宝箱の中身")]
    public ScriptableObject[] treasureRewards;

    /*===========================
    わーぷ用データ
    ===========================*/

    [Header("ワープ用")]
    [Tooltip("移動先")]
    public CellNode warpDestination;






    //||||||||||||||||||||||||||||||||||||||||||||||||
    //ハイライト用
    private Renderer meshRenderer;

    private Color defaultColor;

    [SerializeField]
    private Color highlightColor = Color.green;





    ///||||||||||||||||||||||||||||||||||||||||||||||||
    //線描画用
    [Header("線描画")]
    [SerializeField]
    private Material lineMaterial;

    private void Awake()
    {
        meshRenderer = GetComponent<Renderer>();

        if(meshRenderer != null)
        {
            defaultColor = meshRenderer.material.color;
        }
    }

    private void Start()
    {
        CreateConnections();
    }




    // 接続されたマス同士を線で結ぶ
    private void CreateConnections()
    {
        foreach (CellNode next in nextNodes)
        {
            if(next == null)
                continue;

            // 二重生成防止
            if(GetInstanceID() > next.GetInstanceID())
                continue;

            GameObject lineObj =
                new GameObject($"Line_{name}_{next.name}");

            lineObj.transform.SetParent(transform);

            LineRenderer lr =
                lineObj.AddComponent<LineRenderer>();

            lr.material = lineMaterial;

            lr.positionCount = 2;

            lr.useWorldSpace = true;

            lr.SetPosition(
                0,
                transform.position + Vector3.up * 0.1f);

            lr.SetPosition(
                1,
                next.transform.position + Vector3.up * 0.1f);

            // 線の太さ
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;

            // 線の端を丸くする
            lr.numCapVertices = 5;

            lr.shadowCastingMode =
                UnityEngine.Rendering.ShadowCastingMode.Off;

            lr.receiveShadows = false;
        }
    }





    /// 移動可能マスのハイライト
    public void Highlight(bool enable)
    {
        if(meshRenderer == null)
            return;

        meshRenderer.material.color =
            enable ? highlightColor : defaultColor;
    }



    /// プレイヤーがこのマスに到着した
    public void OnPlayerArrived()
    {
        if(cellData == null)
        {
            Debug.LogWarning(
                $"{name} に CellData が設定されていません");

            return;
        }

        CellEventManager.Instance.Execute(this);
    }

    

    
}