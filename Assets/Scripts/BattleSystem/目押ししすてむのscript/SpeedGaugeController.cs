using System;
using UnityEngine;

public class SpeedGaugeController : MonoBehaviour
{
    public static SpeedGaugeController Instance;

    [Header("UI")]
    [SerializeField] private RectTransform gaugeBar;   // ゲージ全体
    [SerializeField] private RectTransform arrow;      // 動く矢印

    [Header("移動速度")]
    [SerializeField] private float minMoveTime = 1.1f;
    [SerializeField] private float maxMoveTime = 1.6f;

    private float playerSPD;
    private float enemySPD;

    private float timer;
    private float moveTime;
    private bool isRunning;

    private Action<bool> callback;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    //=================================================
    // ゲージ開始
    //=================================================
    public void StartGauge(float playerSPD, float enemySPD, Action<bool> resultCallback)
    {
        this.playerSPD = playerSPD;
        this.enemySPD = enemySPD;

        callback = resultCallback;

        timer = 0f;
        moveTime = UnityEngine.Random.Range(minMoveTime, maxMoveTime);

        isRunning = true;
    }

    //=================================================
    // 更新
    //=================================================
    void Update()
    {
        if (!isRunning)
            return;

        timer += Time.deltaTime;

        float width = gaugeBar.rect.width;

        // 0～1で往復
        float t = Mathf.PingPong(timer / moveTime, 1f);

        float x = Mathf.Lerp(0f, width, t);

        arrow.anchoredPosition = new Vector2(x, arrow.anchoredPosition.y);

        // 仮入力
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopGauge();
        }
    }

    //=================================================
    // 停止
    //=================================================
    public void StopGauge()
    {
        if (!isRunning)
            return;

        isRunning = false;

        float width = gaugeBar.rect.width;

        float playerRatio = playerSPD / (playerSPD + enemySPD);

        float playerArea = width * playerRatio;

        bool playerFirst = arrow.anchoredPosition.x <= playerArea;

        callback?.Invoke(playerFirst);
    }
}