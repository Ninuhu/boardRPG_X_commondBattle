using UnityEngine;
//戦闘処理だけ
/////


public static class BattleActionExecutor
{
    //||||||||||||||||||||||||||||||||||||||
    // // 通常攻撃
    static void ExecuteAttack(BattleCommand attack,BattleCommand defense)
    {
        // カウンター
        if(BattleDefenseCalculator.IsCounter(defense))
        {
            int counterDamage =BattleCalculator.CalculateCounterDamage(defense.user,attack.user);
            attack.user.currentHP -= counterDamage;
            Debug.Log($"Counter {counterDamage}");
            return;
        }
        float multiplier =BattleDefenseCalculator.GetMultiplier(defense.commandType);
        
        int damage =BattleCalculator.CalculateAttackDamage(attack.user,defense.user,multiplier);
        
        if(BattleCalculator.IsCritical(attack.user,defense.user)) damage =BattleCalculator.ApplyCritical(damage);
        
        defense.user.currentHP -= damage;
        Debug.Log($"{damage}");
    }

    //|||||||||||||||||||||||||||
    //全力攻撃
    static void ExecuteAllOutAttack(BattleCommand attack, BattleCommand defense)
    {
        if(BattleDefenseCalculator.IsCounter(defense))
        {
            int counterDamage =BattleCalculator.CalculateCounterDamage(defense.user,attack.user);       
            attack.user.currentHP -= counterDamage;
            return;
        }
        
        float multiplier =BattleDefenseCalculator.GetMultiplier(defense.commandType);
        int damage =BattleCalculator.CalculatePowerAttackDamage(attack.user,defense.user,multiplier);
        defense.user.currentHP -= damage;
    }
    //||||||||||||||||||||||||||||||||
    //魔法攻撃
    static void ExecuteAttackMagic(BattleCommand attack,BattleCommand defense)
    {
        Debug.Log("Magic Attack (TODO)");
    }


    //||||||||||||||||||||||||||||||||
    //職業スキル
    static void ExecuteJobSkill(BattleCommand attack,BattleCommand defense)
    {
        Debug.Log("Job Skill (TODO)");
    }
    
    //|||||||||||||||||||||||||||
    //自由選択スキル
    static void ExecuteFreeSkill(BattleCommand attack,BattleCommand defense)
    {
        Debug.Log("Free Skill (TODO)");
    }
    
    //||||||||||||||||||||||\
    //降参
    static void ExecuteSurrender(BattleCharacter user)
    {
        user.hasSurrendered = true;
    }






    //||||||||||||||||||||||||||||||||||||||
    // コマンド実行
    public static void Execute(BattleCommand action,BattleCommand target)
    {
        //もし使用回数0なら選択不可
        if(!CanExecute(action))
        {
            Debug.Log("使用回数不足");
            return;
        }

        switch(action.commandType)
        {
            //攻撃
            case BattleCommandType.Attack:
                ExecuteAttack(action,target);
                break;

                //全力攻撃
            case BattleCommandType.AllOutAttack:
                ExecuteAllOutAttack(action,target);
                break;


                //魔法こうげっき
            case BattleCommandType.AttackMagic:
                ExecuteAttackMagic(action,target);
                ConsumeSkillUse(action.user, action.commandType);
                break;

                //職業スキル
            case BattleCommandType.JobSkill:
                ExecuteJobSkill(action,target);
                ConsumeSkillUse(action.user, action.commandType);
                break;

                //自由選択スキル
            case BattleCommandType.FreeSkill:
                ExecuteFreeSkill(action,target);
                ConsumeSkillUse(action.user, action.commandType);
                break;

                //降参
            case BattleCommandType.Surrender:
                ExecuteSurrender(action.user);
                
                break;

                
        }
    }


    //||||||||||||||||||||||||||||||||||||||||||||||
    // 使用回数減少
    static void ConsumeSkillUse(BattleCharacter character, BattleCommandType type)
    {
        switch(type)
        {
            //無制限が-1なので>0なら大丈夫

            case BattleCommandType.AttackMagic:
            if(character.remainingAttackMagicUses > 0) character.remainingAttackMagicUses--;
            break;

            case BattleCommandType.JobSkill:
            if(character.remainingJobSkillUses > 0) character.remainingJobSkillUses--;
            break;
            
            case BattleCommandType.FreeSkill:
            if(character.remainingFreeSkillUses > 0) character.remainingFreeSkillUses--;
            break;
        }
    }

    //使用回数のあるskillや魔法が0か0以上か
    static bool CanExecute(BattleCommand command)
    {
        BattleCharacter user = command.user;
        switch(command.commandType)
        {
            case BattleCommandType.AttackMagic:
            return user.remainingAttackMagicUses != 0;
            
            case BattleCommandType.JobSkill:
            return user.remainingJobSkillUses != 0;
            
            case BattleCommandType.FreeSkill:
            return user.remainingFreeSkillUses != 0;
        }
        //通常攻撃などは常に可能
        return true;
    }

}