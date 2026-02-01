using UnityEngine;

public enum CardType { Attack, Defense, Power }
public enum CardEffect { Null, DrunkenFist }

[System.Serializable]
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
        }
    }

    public virtual void UseCard()
    {
        ResolveEffect();
        Debug.Log($"Using card: {cardName} with ID: {ID}");
    }
}
