using UnityEngine;

public static class BattleDefenseCalculator
{
    //||||||||||||||||||||||||||||||||||||||||||||||
    // 防御倍率取得
    public static float GetMultiplier(BattleCommandType commandType)
    {
        switch (commandType)
        {
            // 通常防御
            case BattleCommandType.Defense:
                return 1.5f;

            // 防御魔法
            case BattleCommandType.DefenseMagic:
                return 2.0f;

            // カウンター
            case BattleCommandType.Counter:
                return 2.5f;

            // スキル防御
            case BattleCommandType.SkillGuard:

                // TODO
                // スキルごとの倍率
                return 1.0f;

            // 降参
            case BattleCommandType.Surrender:
                return 2.75f;

            // 防御していない
            default:
                return 2.75f;
        }
    }

    //||||||||||||||||||||||||||||||||||||||||||||||
    // カウンター判定
    public static bool IsCounter(BattleCommand command)
    {
        return command.commandType == BattleCommandType.Counter;
    }

    //||||||||||||||||||||||||||||||||||||||||||||||
    // スキル防御判定
    public static bool IsSkillGuard(BattleCommand command)
    {
        return command.commandType == BattleCommandType.SkillGuard;
    }

    //||||||||||||||||||||||||||||||||||||||||||||||
    // 防御魔法判定
    public static bool IsMagicDefense(BattleCommand command)
    {
        return command.commandType == BattleCommandType.DefenseMagic;
    }

    //||||||||||||||||||||||||||||||||||||||||||||||
    // 降参判定
    public static bool IsSurrender(BattleCommand command)
    {
        return command.commandType == BattleCommandType.Surrender;
    }
}