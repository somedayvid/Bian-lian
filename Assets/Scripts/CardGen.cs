using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CardGen : MonoBehaviour
{
    public List<Card> generatedCards = new List<Card>();
    public DeckManager deckManager;
    public GameObject cardGenPanel;
    public GameObject num1;
    public GameObject num2; 
    public GameObject num3;

    public void KardGen(int count)
    {
        cardGenPanel.SetActive(true);
        generatedCards.Clear();

        for (int i = 0; i < count; i++)
        {
            Card card = new Card();
            CardEffect randomEffect = (CardEffect)Random.Range(
                1,
                System.Enum.GetValues(typeof(CardEffect)).Length
            );

            card.cardEffect = randomEffect;
            card.cardName = card.GetName();
            generatedCards.Add(card);
            Debug.Log($"Generated card {i + 1}: {card.cardName}");
        }
        num1.GetComponent<Image>().sprite = generatedCards[0].GetImage();
        num2.GetComponent<Image>().sprite = generatedCards[1].GetImage();
        num3.GetComponent<Image>().sprite = generatedCards[2].GetImage();
    }

    public void SelectThisCard(int index)
    {
        deckManager.AddCardMain(generatedCards[index]);
        cardGenPanel.SetActive(false);
        Debug.Log($"{generatedCards[index].cardName} added to main deck");
    }
}
