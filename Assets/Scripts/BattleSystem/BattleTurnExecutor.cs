using UnityEngine;

public static class BattleTurnExecutor
{
    //|||||||||||||||||||||||||
    // 1ターンの実行本体
    public static void Execute(
        BattleCharacter player,
        BattleCharacter enemy,
        BattleCommand attackCommand,
        BattleCommand defenseCommand)
    {
        if(player == null || enemy == null) return;
        if(attackCommand == null || defenseCommand == null) return;

        //////////////
        // 先攻判定（仮:SPDは確率にして先行か決める）
        bool playerFirst = Random.value < player.speed / (float)(player.speed + enemy.speed);
        //bool playerFirst = battleState.playerFirst; 本使用の目押しができるまで上のやつ


        BattleCharacter firstUser  = playerFirst ? player : enemy;
        BattleCharacter secondUser = playerFirst ? enemy : player;
        BattleCommand firstAttack  = playerFirst ? attackCommand : SwapCommand(attackCommand);
        BattleCommand secondAttack = playerFirst ? defenseCommand : SwapCommand(defenseCommand);

        //////////////
        // 1回目の行動
        ExecuteAction(firstUser,secondUser,firstAttack,secondAttack);

        // 死亡チェック
        if(enemy.currentHP<=0 || player.currentHP<=0) return;

        //////////////
        // 2回目の行動
        ExecuteAction(secondUser,firstUser,secondAttack,firstAttack);
    }

    //|||||||||||||||||||||||||
    // 行動処理
    private static void ExecuteAction(
        BattleCharacter attacker,
        BattleCharacter defender,
        BattleCommand attackCommand,
        BattleCommand defenseCommand)
    {


        if(attackCommand == null || defenseCommand == null) return;

        switch (attackCommand.commandType)
        {
            //////////////
            // 通常攻撃
            case BattleCommandType.Attack:
                ExecuteAttack(attacker,defender,defenseCommand);
                break;

            //////////////
            // 全力攻撃
            case BattleCommandType.AllOutAttack:
                ExecuteAllOutAttack(attacker,defender,defenseCommand);
                break;

            //////////////
            // 魔法攻撃
            case BattleCommandType.AttackMagic:
                ExecuteMagic(attacker,defender,attackCommand.magic,defenseCommand);
                break;

            //////////////
            // スキル
            case BattleCommandType.JobSkill:
            case BattleCommandType.FreeSkill:
                ExecuteSkill(attacker,defender,attackCommand.skill,defenseCommand);
                break;
        }
    }

    //|||||||||||||||||||||||||
    // 通常攻撃
    private static void ExecuteAttack(
        BattleCharacter attacker,
        BattleCharacter defender,
        BattleCommand defenseCommand)
    {
        float defenseMultiplier = GetDefenseMultiplier(defenseCommand);

        int damage = BattleCalculator.CalculateAttackDamage(
            attacker,
            defender,
            defenseMultiplier
        );

        defender.currentHP -= damage;
        if(defender.currentHP <0) defender.currentHP=0;
    }

    //|||||||||||||||||||||||||
    // 全力攻撃
    private static void ExecuteAllOutAttack(
        BattleCharacter attacker,
        BattleCharacter defender,
        BattleCommand defenseCommand)
    {
        float defenseMultiplier = GetDefenseMultiplier(defenseCommand);
        int damage = BattleCalculator.CalculatePowerAttackDamage(attacker,defender,defenseMultiplier);

        defender.currentHP -= damage;
        if(defender.currentHP<0) defender.currentHP=0;
    }

    //|||||||||||||||||||||||||
    // 魔法攻撃
    private static void ExecuteMagic(BattleCharacter attacker,BattleCharacter defender,MagicData magic,BattleCommand defenseCommand)
    {
        float defenseMultiplier = GetDefenseMultiplier(defenseCommand);
        int damage = BattleCalculator.CalculateMagicDamage(attacker,defender,magic,defenseMultiplier);

        defender.currentHP -= damage;
        if(defender.currentHP <0) defender.currentHP=0;
    }



    //|||||||||||||||||||||||||
    // スキル
    private static void ExecuteSkill(BattleCharacter attacker,BattleCharacter defender,SkillData skill,BattleCommand defenseCommand)
    {

        if(skill == null) return;

        // ダメージがあるスキルならここで拡張予定
        BattleEffectProcessor.ApplyEffects(attacker,defender,skill.effects);
    }




    //|||||||||||||||||||||||||
    // 防御コマンド倍率
    private static float GetDefenseMultiplier(BattleCommand defenseCommand)
    {
        switch (defenseCommand.commandType)
        {
            case BattleCommandType.Defense:
            return 1.5f;

            case BattleCommandType.DefenseMagic:
            return 2.0f;

            case BattleCommandType.Counter:
            return 2.5f;

            case BattleCommandType.SkillGuard:
            return 2.75f;

            case BattleCommandType.Surrender:
            return 999f; // 仮：即敗北扱いにするなら別処理
        
        }

        return 1f;
    }

    //|||||||||||||||||||||||||
    // コマンド入れ替え（敵視点反転用）
    private static BattleCommand SwapCommand(BattleCommand cmd)
    {
        // 現状は簡易版（PvP対応時に拡張）
        return cmd;
    }
}