using UnityEngine;

//ターン管理だけ

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    [Header("戦闘キャラクター")]
    private BattleCharacter player;
    private BattleCharacter enemy;

    public BattleCharacter Player => player; //UIのBattleManager.Instance.playerの参照用
    public BattleCharacter Enemy => enemy; //UIのBattleManager.Instance.enemyの参照用
    //private bool isBoss; //ボス化雑魚敵か

    [Header("現在のコマンド")]
    private BattleCommand attackCommand; //攻撃ターンか
    private BattleCommand defenseCommand; //防御ターンか

    [Header("ターン")]
    private int turnCount; //ターン数
    private bool playerFirst; //どっちが先か

    [Header("戦闘状態")]
    private bool battleEnded; //戦闘が終わったか否か
    public bool IsBattleEnded => battleEnded; //UIのbattleEndedを取得用
    private bool isPvP; // Enemyではなくplayerかいなか
    
   
    

    // 初期化
    private void Awake()
    {
        if (Instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }




    //||||||||||||||||||||||||||||||||||||||||
    // 戦闘開始
    public void StartBattle(PlayerData playerData,EnemyData enemyData)
    {
        battleEnded = false;
        turnCount = 0;

        // BattleCharacter生成
        player = new BattleCharacter(playerData);
        enemy = new BattleCharacter(enemyData);

        StartTurn();
    }

    

    //||||||||||||||||||||||||||||||||||||||||
    //目押しによる判定からの結果受け取り
    void OnFirstTurnDecided(bool isPlayerFirst)
    {
        playerFirst = isPlayerFirst;
        Debug.Log(playerFirst ? "Player First" : "Enemy First");
        // ここでコマンド入力フェーズ
        // // UI表示など
    }
    
    

    //||||||||||||||||||||||||||||||||||||||||
    // ターン開始
    void StartTurn()
    {
        if(battleEnded) return;

        turnCount++;

        attackCommand = null;
        defenseCommand = null;

        Debug.Log($"Turn {turnCount}");

        // TODO
        // 目押し開始
        SpeedGaugeController.Instance.StartGauge(player.speed,enemy.speed,OnFirstTurnDecided);
        // BattleUI表示
    }



    //||||||||||||||||||||||||||||||||||||||||
    // 攻撃コマンド受付
    public void SetAttackCommand(BattleCommand command)
    {
        attackCommand = command;
        CheckCommandReady();
    }



    //||||||||||||||||||||||||||||||||||||||||
    // 防御コマンド受付
    public void SetDefenseCommand(BattleCommand command)
    {
        defenseCommand = command;
        CheckCommandReady();
    }



    //||||||||||||||||||||||||||||||||||||||||
    // 両者選択完了
    void CheckCommandReady()
    {
        if(attackCommand == null){Debug.Log("Command missing");return;}
        if(defenseCommand == null){Debug.Log("Command missing");return;}

        ExecuteTurn();
    }


    //||||||||||||||||||||||||||||||||||||||||
    // ターン実行
    void ExecuteTurn()
    {
        Debug.Log("Turn Execute");
        
        BattleCommand first;
        BattleCommand second;
        
        //||||||||||||||||||||||||||||||||||||||
        // 先攻・後攻の決定
        if(playerFirst)
        {
            first = attackCommand;
            second = defenseCommand;
        }
        
        else
        {
            first = defenseCommand;
            second = attackCommand;
        }
        
        //||||||||||||||||||||||||||||||||||||||
        // 先攻行動
        BattleActionExecutor.Execute(first, second);

        // 戦闘終了チェック
        CheckBattleEnd();
        if(battleEnded) return;
        
        
        //||||||||||||||||||||||||||||||||||||||
        // 後攻行動
        BattleActionExecutor.Execute(second, first);
        
        
        // 再チェック
        CheckBattleEnd();
        
        if(!battleEnded) EndTurn();
    }
    

    //||||||||||||||||||||||||||||||||||||||||
    // ターン終了
    void EndTurn()
    {
        
        // プレイヤー
        BattleEffectProcessor.ProcessTurnEnd(player);
        
        // 敵
        BattleEffectProcessor.ProcessTurnEnd(enemy);
        
        // ターン終了時の毒・呪いなどで死亡したか確認
        CheckBattleEnd();
        if(!battleEnded) StartTurn();
    }

    //||||||||||||||||||||||||||||||||||||||||
    // 勝敗判定
    void CheckBattleEnd()
    {
        if(player.currentHP<=0)
        {
            battleEnded = true;
            ShowResult(false);

            return;
        }

        if(enemy.currentHP<=0)
        {
            battleEnded = true;
            ShowResult(true);
            return;
        }
        
        /* 
        // 通常戦のみ(PVPとボス戦を除く) 敵戦の時に10ターン越えたら強制的にplayer敗北
        if(!isPvP && !enemy.isBoss)
        {
            if(turnCount>=10)
            {
                battleEnded = true;
                ShowResult(false);
            }
        }*/
    }

    //||||||||||||||||||||||||||||||||||||||||
    // リザルト
    void ShowResult(bool playerWin)
    {
        //とりあえずデバックだけ
        if(playerWin) Debug.Log("Player Win");

        else Debug.Log("Player Lose");
        

        EndBattle();
    }

    //||||||||||||||||||||||||||||||||||||||||
    // 戦闘終了

    void EndBattle()
    {
        Debug.Log("Battle End");

        // TODO

        // マップへ戻る

        // リザルトUI

        // EXP

        // GOLD

        // DROP
    }
}