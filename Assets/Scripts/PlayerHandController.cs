using System.Collections.Generic;
using UnityEngine;

public class PlayerHandController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private BattleManager battleManager;

    [Header("Hand (Inspector-visible)")]
    [SerializeField] private int handLimit = 10;
    [SerializeField] private List<Card> hand = new List<Card>();

    [Header("Selection")]
    [SerializeField] private int selectedIndex = 0;

    [Header("Play Settings")]
    [SerializeField] private bool useDefaultTarget = true;
    [SerializeField] private MonoBehaviour explicitTargetEnemy;

    // --------- Public API ---------
    public IReadOnlyList<Card> Hand => hand;
    public int HandLimit => handLimit;
    public int SelectedIndex => selectedIndex;

    public int HandCount => hand != null ? hand.Count : 0;

    public bool AddCardFromPrefab(GameObject cardPrefab)
    {
        if (cardPrefab == null)
        {
            Debug.LogWarning("[Hand] AddCardFromPrefab got null prefab.");
            return false;
        }

        var info = cardPrefab.GetComponent<CardPrefabInfo>();
        if (info == null)
        {
            Debug.LogError($"[Hand] Prefab '{cardPrefab.name}' missing CardPrefabInfo component.");
            return false;
        }

        Card created = info.CreateCardInstance();
        return AddCardToHand(created); 
    }

    public bool RemoveFirstCard()
    {
        if (hand.Count == 0) return false;
        return RemoveCardAt(0); 
    }

    public Card GetSelectedCard()
    {
        if (hand.Count == 0) return null;
        selectedIndex = Mathf.Clamp(selectedIndex, 0, hand.Count - 1);
        return hand[selectedIndex];
    }

    public bool AddCardToHand(Card card)
    {
        if (card == null) return false;

        if (hand.Count >= handLimit)
        {
            Debug.LogWarning("[Hand] Hand is full (limit=10). Card rejected.");
            return false;
        }

        hand.Add(card);

        selectedIndex = Mathf.Clamp(selectedIndex, 0, hand.Count - 1);
        return true;
    }

    public bool RemoveCardAt(int index)
    {
        if (index < 0 || index >= hand.Count) return false;

        hand.RemoveAt(index);

        if (hand.Count == 0)
        {
            selectedIndex = 0;
        }
        else
        {
            selectedIndex = Mathf.Clamp(selectedIndex, 0, hand.Count - 1);
        }

        return true;
    }

    public void SelectNext()
    {
        if (hand.Count == 0) return;
        selectedIndex = (selectedIndex + 1) % hand.Count;
    }

    public void SelectPrev()
    {
        if (hand.Count == 0) return;
        selectedIndex--;
        if (selectedIndex < 0) selectedIndex = hand.Count - 1;
    }

    public void PlaySelected()
    {
        if (battleManager == null)
        {
            Debug.LogError("[Hand] battleManager is null.");
            return;
        }

        Card card = GetSelectedCard();
        if (card == null)
        {
            Debug.LogWarning("[Hand] No selected card to play.");
            return;
        }

        Enemy target = null;

        if (!useDefaultTarget)
        {
            if (explicitTargetEnemy is Enemy e) target = e;
            else target = null;
        }

        bool success = battleManager.TryPlayCard(card, target);

        if (success)
        {
            RemoveCardAt(selectedIndex);
        }
        else
        {
            Debug.Log("[Hand] Play failed (turn state / target / rules). Card not removed.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) SelectNext();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) SelectPrev();
        if (Input.GetKeyDown(KeyCode.Return)) PlaySelected();
    }
}
