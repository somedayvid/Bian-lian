using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private List<Card> mainDeck = new List<Card>();
    [SerializeField] private List<Card> tempDeck = new List<Card>();
    private List<Card> discardDeck = new List<Card>();

    public TextMeshProUGUI discardPileCount;
    public TextMeshProUGUI combatDeckCount;

    private static DeckManager instance;

    public static DeckManager GetInstance()
    {
        return instance;
    }

    void shuffle()
    {
        for (int i = tempDeck.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Card temp = tempDeck[i];
            tempDeck[i] = tempDeck[j];
            tempDeck[j] = temp;
        }
    }
    public Card Draw()
    {
        if (tempDeck.Count == 0)
        {
            tempDeck = new List<Card>(discardDeck);
            discardDeck.Clear();
            discardPileCount.text = discardDeck.Count.ToString();
            combatDeckCount.text = tempDeck.Count.ToString();
            shuffle();
        }
        if (tempDeck.Count == 0) return null;
        Card card = tempDeck[0];
        tempDeck.RemoveAt(0);
        combatDeckCount.text = tempDeck.Count.ToString();
        return card;
    }
    public void AddCard(Card card)
    {
        tempDeck.Add(card);
        shuffle();
        combatDeckCount.text = tempDeck.Count.ToString();
    }

    public void AddCardMain(Card card)
    {
        mainDeck.Add(card);
    }

    public void RemoveCard(Card card)
    {
        tempDeck.Remove(card);
        combatDeckCount.text = tempDeck.Count.ToString();
    }

    public void StartBattle()
    {
        tempDeck = new List<Card>(mainDeck);
        shuffle();
    }

    public void EndBattle()
    {
        tempDeck = null;
        discardDeck.Clear();
    }

    public void DiscardCard(Card card)
    {
        discardDeck.Add(card);
        discardPileCount.text = discardDeck.Count.ToString();
    }

    public void MoveHandToDiscard(List<Card> playerHand)
    {
        discardDeck.AddRange(playerHand);
        discardPileCount.text = discardDeck.Count.ToString();
    }

    void Awake()
    {
        instance = this;
        Card newCard = new Card();
        newCard.cardEffect = CardEffect.PalmStrike;
        AddCardMain(newCard);
        Card newCard2 = new Card();
        newCard2.cardEffect = CardEffect.RockThrow;
        AddCard(newCard2);

        Card newCard3= new Card();
        newCard3.cardEffect = CardEffect.DrunkenFist;
        AddCard(newCard3);

        Card newCard4 = new Card();
        newCard4.cardEffect = CardEffect.SmallShieldPotion;
        AddCard(newCard4);

        Card newCard5 = new Card();
        newCard5.cardEffect = CardEffect.ShieldPotion;
        AddCard(newCard5);

        Card newCard6 = new Card();
        newCard6.cardEffect = CardEffect.OrientalMedicineJug;
        AddCard(newCard6);

        Card newCard7 = new Card();
        newCard7.cardEffect = CardEffect.SubmachineGun;
        AddCard(newCard7);

        Card newCard8 = new Card();
        newCard8.cardEffect = CardEffect.OrientalDaggerRitual;
        AddCard(newCard8);

        Card newCard9 = new Card();
        newCard9.cardEffect = CardEffect.OrientalDagger;
        AddCard(newCard9);

        Card newCard10 = new Card();
        newCard10.cardEffect = CardEffect.Meditate;
        AddCard(newCard10);

        Card newCard11 = new Card();
        newCard11.cardEffect = CardEffect.OrientalTigerBalm;
        AddCard(newCard11);

        Card newCard12 = new Card();
        newCard12.cardEffect = CardEffect.GinsengRoot;
        AddCard(newCard12);

        Card newCard13 = new Card();
        newCard13.cardEffect = CardEffect.HeavenlyInsight;
        AddCard(newCard13);

        Card newCard14 = new Card();
        newCard14.cardEffect = CardEffect.SunTzusInsight;
        AddCard(newCard14);

        Card newCard15 = new Card();
        newCard15.cardEffect = CardEffect.DragonStrike;
        AddCard(newCard15);

        Card newCard16 = new Card();
        newCard16.cardEffect = CardEffect.HeavenSplit;
        AddCard(newCard16);

        Card newCard17 = new Card();
        newCard17.cardEffect = CardEffect.JadeBarrier;
        AddCard(newCard17);

        Card newCard18 = new Card();
        newCard18.cardEffect = CardEffect.Pills;
        AddCard(newCard18);

        Card newCard19 = new Card();
        newCard19.cardEffect = CardEffect.BuddahStrike;
        AddCard(newCard19);

        shuffle();
    }
}
