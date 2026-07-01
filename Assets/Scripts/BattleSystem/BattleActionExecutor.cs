using UnityEngine;
//戦闘処理だけ
/////
//||||||||||||||||||||||||||||||||||||||
// 通常攻撃
//||||||||||||||||||||||||||||||||||||||||||||||
// 通常攻撃
static void ExecuteAttack(
    BattleCommand attack,
    BattleCommand defense)
{
    // カウンター
    if (BattleDefenseCalculator.IsCounter(defense))
    {
        int counterDamage =
            BattleCalculator.CalculateCounterDamage(
                defense.user,
                attack.user);

        attack.user.currentHP -= counterDamage;

        Debug.Log($"Counter {counterDamage}");

        return;
    }

    float multiplier =
        BattleDefenseCalculator.GetMultiplier(
            defense.commandType);

    int damage =
        BattleCalculator.CalculateAttackDamage(
            attack.user,
            defense.user,
            multiplier);

    if (BattleCalculator.IsCritical(
        attack.user,
        defense.user))
    {
        damage =
            BattleCalculator.ApplyCritical(damage);
    }

    defense.user.currentHP -= damage;

    Debug.Log($"{damage}");
}

public static class BattleActionExecutor
{
    //||||||||||||||||||||||||||||||||||||||
    // コマンド実行
    public static void Execute(
        BattleCommand action,
        BattleCommand target)
    {
        switch(action.commandType)
        {
            case BattleCommandType.Attack:
                ExecuteAttack(action,target);
                break;

            case BattleCommandType.AllOutAttack:
                ExecuteAllOutAttack(action,target);
                break;

            case BattleCommandType.AttackMagic:
                ExecuteAttackMagic(action,target);
                break;

            case BattleCommandType.JobSkill:
                ExecuteJobSkill(action,target);
                break;

            case BattleCommandType.FreeSkill:
                ExecuteFreeSkill(action,target);
                break;

            case BattleCommandType.Surrender:
                ExecuteSurrender(action.user);
                break;
        }
    }
}