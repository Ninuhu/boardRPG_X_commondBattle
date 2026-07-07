using UnityEngine;
using System;
public class SpeedGaugeController : MonoBehaviour
{
    public static SpeedGaugeController Instance;
    public float playerSPD;
    public float enemySPD;

    public RectTransform arrow;
    [SerializeField] private float barWidth = 500f; //仮
    [SerializeField] private float timer;
    [SerializeField] private bool isRunning;
    private Action<bool> callback;

    void Awake()
    {
        Instance = this;
    }

    
    public void StartGauge(float pspd, float espd, Action<bool> resultCallback)
    {
        playerSPD = pspd;
        enemySPD = espd;

        callback = resultCallback;
        timer = 0f;

        isRunning = true;
    }

    void Update()
    {
        if(!isRunning) return;

        timer += Time.deltaTime;

        float ratio = playerSPD / (playerSPD + enemySPD);
        //float targetX = barWidth * ratio;

        float pingPong = Mathf.PingPong(timer * 300f, barWidth);

        arrow.anchoredPosition = new Vector2(pingPong, 0);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StopGauge();
        }
    }

    public void StopGauge()
    {
        isRunning = false;

        float ratio = playerSPD / (playerSPD + enemySPD);
        float playerArea = barWidth * ratio;

        float x = arrow.anchoredPosition.x;

        bool playerFirst = x <= playerArea;

        callback?.Invoke(playerFirst);
    }
    void ExecuteTurn()
{
    Debug.Log("Turn Execute");

    if(playerFirst)
    {
        ExecuteAction(player, enemy);
        if(enemy.currentHP > 0)
            ExecuteAction(enemy, player);
    }
    else
    {
        ExecuteAction(enemy, player);
        if(player.currentHP > 0)
            ExecuteAction(player, enemy);
    }

    CheckBattleEnd();

    if(!battleEnded)
        EndTurn();
}
void ExecuteAction(BattleCharacter attacker, BattleCharacter defender)
{
    // BattleCalculator呼び出し予定
}
}