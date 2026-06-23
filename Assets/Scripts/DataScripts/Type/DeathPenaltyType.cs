public enum DeathPenaltyType
{
    GoldLoss, //所持金半減
    VillageLoss, //ランダムに一つの村を所有からなくす
    ItemLoss, //所持アイテム全部消滅
    SpellBookLoss, //魔術書全部消去
    EquipmentLoss, //装備（武器、盾、攻撃魔法、防御魔法、装飾品のランダムにどれか）
    ReviveDelay, //復活ターン延長
    Debuff //デバフ
}