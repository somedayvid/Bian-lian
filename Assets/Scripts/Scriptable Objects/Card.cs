using UnityEngine;

public enum CardType
{
    Attack,
    Defense,
    Power,
    Heal
}
public enum CardEffect
{
    Null,
    DrunkenFist,
    PalmStrike,
    SmallShieldPotion,
    ShieldPotion,
    OrientalMedicineJug, 
    SubmachineGun,
    OrientalDaggerRitual,
    OrientalDagger,
    Meditate,
    OrientalTigerBalm,
    GinsengRoot,
    HeavenlyInsight,
    MandateOfHeaven,
    SunTzusInsight,
    DragonStrike,
    HeavenSplit,
    JadeBarrier,
    Momentum,
    RockThrow,
    Pills,
}
//[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card", order = 1)]

public class Card
{
    public int ID;
    public Mood moodType;
    public string cardName;
    public CardType cardType;
    public CardEffect cardEffect;

    public int damage = 5;
    public int shield = 0;

    public Card() { }

    private void ResolveEffect()
    {
        switch (cardEffect)
        {
            case CardEffect.DrunkenFist:
                int rng = Random.Range(0, 2);
                damage = (rng == 0) ? 0 : 12;
                break;
            case (CardEffect.PalmStrike):
                damage = 5;
                break;
            case (CardEffect.SmallShieldPotion):
                shield = 5;
                cardType = CardType.Defense;
                break;
            case (CardEffect.ShieldPotion):
                shield = 25;
                cardType = CardType.Defense;
                break;
            case (CardEffect.OrientalMedicineJug):
                shield = 50;
                cardType = CardType.Defense;
                break;
            case (CardEffect.OrientalDaggerRitual):
                break;
            case (CardEffect.OrientalDagger):
                damage = 3;
                break;
            case (CardEffect.Meditate):
                break;
            case (CardEffect.OrientalTigerBalm):
                break;
            case (CardEffect.GinsengRoot):
                break;
            case (CardEffect.HeavenlyInsight):
                break;
            case (CardEffect.MandateOfHeaven):
                break;
            case (CardEffect.SunTzusInsight):
                break;
            case (CardEffect.DragonStrike):
                break;
            case (CardEffect.HeavenSplit):
                damage = 5;
                break;
            case (CardEffect.JadeBarrier):
                break;
            case (CardEffect.Momentum):
                break;
            case (CardEffect.RockThrow):
                break;
            case (CardEffect.Pills):
                break;
        }
    }

    public virtual void UseCard()
    {
        ResolveEffect();
        Debug.Log($"Using card: {cardName} with ID: {ID}");
    }
}
