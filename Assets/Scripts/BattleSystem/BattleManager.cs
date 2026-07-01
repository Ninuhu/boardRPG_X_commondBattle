using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    [Header("戦闘キャラクター")]
    private BattleCharacter player;
    private BattleCharacter enemy;
    //private bool isBoss; //ボス化雑魚敵か

    [Header("現在のコマンド")]
    private BattleCommand attackCommand; //攻撃ターンか
    private BattleCommand defenseCommand; //防御ターンか

    [Header("ターン")]
    private int turnCount; //ターン数

    [Header("戦闘状態")]
    private bool battleEnded; //戦闘が終わったか否か
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

        // TODO
        // 先攻判定
        // 行動実行
        // BattleCalculator
        // BattleEffectProcessor

        CheckBattleEnd();

        if(!battleEnded) EndTurn();
        
    }

    //||||||||||||||||||||||||||||||||||||||||
    // ターン終了
    void EndTurn()
    {
        // TODO

        // 状態異常

        // Buff

        // Debuff

        // 使用回数

        StartTurn();
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