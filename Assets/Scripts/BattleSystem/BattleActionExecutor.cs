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
    static void ExecuteAllOutAttack(BattleCommand attack,BattleCommand defense)
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
        BattleCharacter user = attack.user;
        BattleCharacter target = defense.user;
        
        MagicData magic = user.equippedAttackMagic;
        
        // 攻撃魔法未装備
        if(magic == null)
        {
            Debug.Log("Attack Magic is null");
            return;
        }
        
        
        // カウンター
        if(BattleDefenseCalculator.IsCounter(defense))
        {
            int counterDamage =BattleCalculator.CalculateCounterDamage(defense.user,user);
            user.currentHP -= counterDamage;
            
            
            Debug.Log($"Counter {counterDamage}");
            return;
        }
        
        // 防御倍率
        float multiplier =BattleDefenseCalculator.GetMagicMultiplier(defense.commandType,defense.user.equippedDefenseMagic);
        
        
        // ダメージ計算
        int damage =BattleCalculator.CalculateMagicDamage(user,target,magic,multiplier);
        // クリティカル
        if(BattleCalculator.IsCritical(user,target))　damage = BattleCalculator.ApplyCritical(damage);
        
        // ダメージ
        target.currentHP -= damage;
        Debug.Log($"{magic.magicName} : {damage}");
        
        // 特殊効果
        BattleEffectProcessor.ApplyEffects(user,target,magic.effects);
    }


    //||||||||||||||||||||||||||||||||
    //職業スキル
    static void ExecuteJobSkill(BattleCommand attack,BattleCommand defense)
    {
        BattleCharacter user = attack.user;
        BattleCharacter target = defense.user;
        SkillData skill = user.equippedJobSkill;
        
        // スキル未装備
        if(skill == null)
        {
            Debug.Log("Job Skill is null");
            return;
        }
        // 威力があるなら攻撃
        if(skill.power > 0)
        {
            float multiplier =BattleDefenseCalculator.GetMultiplier(defense.commandType);
            
            int damage =BattleCalculator.CalculateSkillDamage(user,target,skill,multiplier);
            target.currentHP -= damage;
        }
        
        
        Debug.Log($"Job Skill : {skill.skillName}");
        // スキル効果実行
        BattleEffectProcessor.ApplyEffects(user,target,skill.effects);
    }
    
    //|||||||||||||||||||||||||||
    // 自由スキル
    static void ExecuteFreeSkill(BattleCommand attack,BattleCommand defense)
    {
        SkillData skill = attack.user.equippedFreeSkill;
        if(skill == null) return;
        
        
        float multiplier = BattleDefenseCalculator.GetMultiplier(defense.commandType);
        int damage = BattleCalculator.CalculateSkillDamage(attack.user,defense.user,skill,multiplier);
        
        if (BattleCalculator.IsCritical(attack.user,defense.user)) damage = BattleCalculator.ApplyCritical(damage);
        
        defense.user.currentHP -= damage;
        
        BattleEffectProcessor.ApplyEffects(attack.user,defense.user,skill.effects);
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
                ConsumeSkillUse(action.user,action.commandType);
                break;

                //職業スキル
            case BattleCommandType.JobSkill:
                ExecuteJobSkill(action,target);
                ConsumeSkillUse(action.user,action.commandType);
                break;

                //自由選択スキル
            case BattleCommandType.FreeSkill:
                ExecuteFreeSkill(action,target);
                ConsumeSkillUse(action.user,action.commandType);
                break;

                //降参
            case BattleCommandType.Surrender:
                ExecuteSurrender(action.user);
                
                break;

                
        }
    }


    //||||||||||||||||||||||||||||||||||||||||||||||
    // 使用回数減少
    static void ConsumeSkillUse(BattleCharacter character,BattleCommandType type)
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