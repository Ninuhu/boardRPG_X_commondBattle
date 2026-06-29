using UnityEngine;

public static class BattleCalculator
{
    //||||||||||||||||||||||||||||||||||
    /* 通常攻撃
    =====================
    使用者 攻撃
    = (自攻撃 ×2 - 敵防御) × 防御コマンド ×乱数
    (小数点以下切り捨て)
    防御　　　　1.5
    防御魔法　　2.0
    カウンター　2.5
    無防備　　　2.75
    =====================*/
    public static int CalculateAttackDamage(BattleCharacter attacker,BattleCharacter defender,float defenseMultiplier)
        // defenseMultiplier :防御コマンドせんたくごとの数値(* 2など)
    {
        float baseDamage =attacker.attack * 2f - defender.defense;

        if(baseDamage<1) baseDamage = 1; //もし与えるダメージが　(<=0)なら固定1ダメージ

        float random = Random.Range(0.95f,1.01f); //乱数　→ 0.95〜1.0

        return Mathf.FloorToInt(baseDamage * defenseMultiplier * random);
    }

    //||||||||||||||||||||||||||
    /* 全力攻撃
=====================

全力攻撃

= (自攻撃 ×3 -敵防御)×防御コマンド ×乱数

防御　　　　2.0

防御魔法　　2.5

カウンター　下記ダメージ

無防備　　　2.75

//
カウンターダメージ

= (自攻撃 ×2 +敵攻撃 ×2 -自防御) ×2.25 ×乱数
=================*/
    public static int CalculatePowerAttackDamage(BattleCharacter attacker,BattleCharacter defender,float defenseMultiplier)
    {
        float baseDamage =attacker.attack * 3f - defender.defense;

        if(baseDamage<1) baseDamage = 1;

        float random = Random.Range(0.95f,1.01f);

        return Mathf.FloorToInt(baseDamage * defenseMultiplier * random);
    }

    //||||||||||||||||||||||||||||||
    /* 魔法
使用者　攻撃魔法

= (自魔攻 ×1.5 -敵魔防)×攻撃魔法威力 ×防御コマンド(防魔時 ×防御魔法威力)×乱数

………………………

防御　2.0

防御魔法　1.0

カウンター　2.5

無防備  2.75

…………………………

　　　　攻撃魔法威力　　　防御魔法威力

極小　　　　2　　　　　　　　　なし　

小　　　　　5              0.375

中　　　　　15              0.20

大　　　　　20             0.1

極大　　　　30              なし

======================*/
    public static int CalculateMagicDamage(BattleCharacter attacker,BattleCharacter defender,MagicData magic,float defenseMultiplier)
    {
        if (magic == null) return 0; //攻撃魔法なしのおときnull
        float baseDamage =attacker.magicAttack * 1.5f- defender.magicDefense;

        if(baseDamage<1) baseDamage = 1;

        float random = Random.Range(0.95f,1.01f);

        return Mathf.FloorToInt(baseDamage* magic.power* defenseMultiplier* random);
    }

    //|||||||||||||||||||||||||||||||||||
    /* カウンター
    カウンターダメージ
    = (自攻撃 ×2 +敵攻撃 ×2 -自防御) ×2.25 ×乱数*/
    public static int CalculateCounterDamage(BattleCharacter attacker,BattleCharacter defender)
    {
        float baseDamage = attacker.attack * 2f + defender.attack * 2f- attacker.defense;

        if(baseDamage<1) baseDamage = 1;
        float random = Random.Range(0.95f,1.01f);

        return Mathf.FloorToInt(baseDamage * 2.25f * random);
    }

    //|||||||||||||||||||||||||
    // クリティカル(仮)
    public static bool IsCritical(BattleCharacter attacker,BattleCharacter defender)
    {
        /*const float Min_Critical = 0.05f;
        const float Max_Critical = 0.50f;*/
        float chance =0.05f +(attacker.speed - defender.speed) * 0.01f;

        chance = Mathf.Clamp(chance,0.05f,0.50f);

        return Random.value <= chance;
    }

    //||||||||||||||||||||||||||||||||||||||
    /* 回避
    1-（攻撃者のspd/防御者のspd）   0.4が限度で*/
    public static bool IsDodged(BattleCharacter attacker,BattleCharacter defender)
    {
        float defenderSpeed = Mathf.Max(1, defender.speed); // attacker.speed / defenderSpeed==0になったら壊れるから
        float dodgeRate =1f - (attacker.speed / defenderSpeed) + 0.01f;
        dodgeRate = Mathf.Clamp(dodgeRate,0.01f,0.40f); //さいだい 40%までしか回避率は上がらない
        return Random.value <= dodgeRate;
    }

    //||||||||||||||||||||||||||||
    // クリティカル倍率(仮)
    public static int ApplyCritical(int damage)
    {
        float multiplier =Random.Range(1.5f,1.61f);

        return Mathf.FloorToInt(damage * multiplier);
    }
}