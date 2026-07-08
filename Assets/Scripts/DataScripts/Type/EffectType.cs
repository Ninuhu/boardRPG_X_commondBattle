public enum EffectType
{
    None,
    Attack, //Skillの攻撃技用
    Buff, //自分のステータス変化
    Debuff, //敵のステータス変化

    HealHP, //回復
    AddStatus, //状態異常付与
    RemoveStatus, //状態異常回復
    CriticalRateUp, //クリティカル倍率
    Counter, //ダメージ受けた時の反撃
    AdditionalAttack ,//追加ダメージ
    GainGold, //得られる金(宝箱ますで使う)
    LoseGold, //失う金（宝箱マスに使う）
    GainExp, //得られる経験値
    Revive, //リバイブ（復活）
    Warp//ワープ


}