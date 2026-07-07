using UnityEngine;
using System.Collections.Generic;

public static class BattleEffectProcessor
{
    //||||||||||||||||||||||||||||||||||||||||||||||
    // 複数のEffectDataをまとめて実行
    public static void ApplyEffects(BattleCharacter user,BattleCharacter target,List<EffectData> effects)
    {
        // 効果が無ければ終了
        if(effects == null || effects.Count==0) return;

        // 順番に効果を実行
        foreach (EffectData effect in effects) ApplyEffect(user,target,effect);
        
    }

    public static void ProcessTurnEnd(BattleCharacter character)
    {
        ProcessStatus(character);
        ProcessBuffs(character);
        ProcessDebuffs(character);
    }







    //||||||||||||||||||||||||||||||||||||||||||||||
    /* EffectDataを1つ実行
    発動率判定もここで行う*/
    public static void ApplyEffect(BattleCharacter user,BattleCharacter target,EffectData effect)
    {
        if(effect == null) return;
        // 発動率判定
        if(Random.Range(0,100) >= effect.chance) return;

        // 自分対象か敵対象か
        BattleCharacter receiver = effect.target 
                                == EffectTarget.Self ? user : target;

        switch (effect.effectType)
        {   
            case EffectType.Buff:
                AddBuff(receiver,effect);
                break;

            case EffectType.Debuff:
                AddDebuff(receiver,effect);
                break;

            case EffectType.HealHP:
                Heal(receiver,effect);
                break;

            case EffectType.AddStatus:
                AddStatus(receiver,effect);
                break;

            case EffectType.RemoveStatus:
                RemoveStatus(receiver,effect);
                break;

            case EffectType.GainGold:
            case EffectType.LoseGold:
                ChangeGold(receiver,effect);
                break;

            case EffectType.GainExp:
                GainExp(receiver,effect);
                break;

            case EffectType.Revive:
                Revive(receiver);
                break;

            // BattleManager側で処理
            case EffectType.Counter:// Counter : カウンター待機
            case EffectType.AdditionalAttack:// AdditionalAttack : 追加攻撃予約
            case EffectType.Warp: break; // Warp : マップ処理
        }
    }

    //||||||||||||||||||||||||||||||||||||||||||||||
    // バフ付与
    static void AddBuff(BattleCharacter character,EffectData effect)
    {
        character.activeBuffs.Add(new ActiveEffect()
        {
            effect = effect,
            remainingTurns = effect.duration,
            isPermanent = effect.duration <=-1
        });
    }
    /*duration = -1 → 永続
    duration = 0  → 即時効果
    duration = 1以上 → 継続ターン*/



    //||||||||||||||||||||||||||||||||||||||||||||||
    // デバフ付与
    static void AddDebuff(BattleCharacter character,EffectData effect)
    {
        character.activeDebuffs.Add(new ActiveEffect()
        {
            effect = effect,
            remainingTurns = effect.duration,
            isPermanent = effect.duration ==-1
        });
    }




    //||||||||||||||||||||||||||||||||||||||||||||||
    /*HP回復
    Add       固定値回復
    Multiply  最大HP割合回復
    Set       HPを指定値へ変更*/
    static void Heal(
        BattleCharacter character,
        EffectData effect)
    {
        int value=0;

        switch (effect.valueType)
        {
            //+のとき
            case EffectValueType.Add:

                value = Mathf.RoundToInt(effect.value);
                break;

            //* のとき
            case EffectValueType.Multiply:
                value = Mathf.RoundToInt(character.maxHP * effect.value);
                break;
            
            //基本allのとき
            case EffectValueType.Set:

                character.currentHP = Mathf.RoundToInt(effect.value);
                character.currentHP =Mathf.Clamp(character.currentHP,0,character.maxHP);

                return;
        }


        character.currentHP += value;
        character.currentHP = Mathf.Clamp(character.currentHP,0,character.maxHP);
    
    
    }




    //||||||||||||||||||||||||||||||||||||||||||||||
    // 状態異常付与
    // 同じ状態異常ならターン更新
    static void AddStatus(BattleCharacter character,EffectData effect)
    {
        ActiveStatusEffect status =character.statusEffects.Find(x => x.statusType == effect.statusEffect);



        // 既についているならターン更新
        if(status != null)
        {
            status.remainingTurns = effect.duration;
            status.permanent = effect.duration == -1;
            return;
        }




        // 状態異常を新しく追加
        character.statusEffects.Add(new ActiveStatusEffect()
        {
            statusType = effect.statusEffect,
            remainingTurns = effect.duration,
            permanent = effect.duration ==-1
        });
    }




    //||||||||||||||||||||||||||||||||||||||||||||||
    // 状態異常解除
    static void RemoveStatus(BattleCharacter character,EffectData effect)
    {
        character.statusEffects.RemoveAll( x => x.statusType == effect.statusEffect);
    }




    //||||||||||||||||||||||||||||||||||||||||||||||
    /* お金増減
    GainGold / LoseGold 共通*/
    static void ChangeGold(BattleCharacter character,EffectData effect)
    {
        if(!character.isPlayer) return;

        int value = Mathf.RoundToInt(effect.value);


        if(effect.effectType == EffectType.GainGold) character.playerData.gold += value;
        else character.playerData.gold =Mathf.Max(0,character.playerData.gold-value);
        
    }

    //||||||||||||||||||||||||||||||||||||||||||||||
    // 経験値獲得
    static void GainExp(BattleCharacter character,EffectData effect)
    {
        if(!character.isPlayer) return;

        character.playerData.experience += Mathf.RoundToInt(effect.value);
        //LevelManager.CheckLevelUp(character.playerData); 多分入れる
    }

    //||||||||||||||||||||||||||||||||||||||||||||||
    // 復活   HP1で復帰
    static void Revive(
        BattleCharacter character)
    {
        character.isDead = false;

        if(character.currentHP <=0) character.currentHP = 1;
        
    }




    //||||||||||||||||||||||||||||||||||||||||
    //ターン終了時の状態異常・Buff・Debuff処理
    //||||||||||||||||||||||||||||||||||||||||
    //状態異常処理
    static void ProcessStatus(BattleCharacter character)
    {
        for(int i=character.statusEffects.Count -1;i>=0;i--)
        {
            ActiveStatusEffect status = character.statusEffects[i];
            
            switch(status.statusType)
            
            {
                case StatusEffectType.Poison:
                character.currentHP -= Mathf.Max(1, character.maxHP / 10);
                break;
                
                case StatusEffectType.HighPoison:
                character.currentHP -= Mathf.Max(1, character.maxHP / 5);
                break;

            // Sleep
            // Curse
            // Paralysis
            // あとで追加
            }
            
            if(!status.permanent)
            {
                status.remainingTurns--;
                if(status.remainingTurns <= 0)
                character.statusEffects.RemoveAt(i);
            }
        }
        character.currentHP = Mathf.Max(0, character.currentHP);
    }

    //バフ府減少
    static void ProcessBuffs(BattleCharacter character)
    {
        for(int i = character.activeBuffs.Count - 1; i >= 0; i--)
        {
            ActiveEffect buff = character.activeBuffs[i];
            if(buff.isPermanent) continue;
            
            buff.remainingTurns--;
            if(buff.remainingTurns <= 0) character.activeBuffs.RemoveAt(i);
        }
    }


    //Debuff減少
    static void ProcessDebuffs(BattleCharacter character)
    {
        for(int i = character.activeDebuffs.Count - 1; i >= 0; i--)
        {
            ActiveEffect debuff = character.activeDebuffs[i];
            if(debuff.isPermanent) continue;
            
            
            debuff.remainingTurns--;
            
            if(debuff.remainingTurns <= 0) character.activeDebuffs.RemoveAt(i);
        }
    }

    

}