public enum EffectType
{
    None,

    Buff, //自分のステータス変化
    Debuff, //敵のステータス変化

    HealHP, //回復
    AddStatus, //状態異常付与
    RemoveStatus, //状態異常回復
    CriticalRateUp, //クリティカル倍率
    Counter, //ダメージ受けた時の反撃
    MultiAttack //追加ダメージ


}