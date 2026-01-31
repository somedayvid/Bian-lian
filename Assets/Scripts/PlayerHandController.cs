using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Owns player's hand (data + selection) and exposes "play selected card".
///This is NOT the combat rules system. It only requests BattleManager to play.
/// </summary>
public class PlayerHandController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private BattleManager battleManager;

    [Header("Hand (Inspector-visible)")]
    [SerializeField] private int handLimit = 10;

    // This list should be visible in Inspector.
    // If it is NOT visible, your Card type is not serializable.
    [SerializeField] private List<Card> hand = new List<Card>();

    [Header("Selection")]
    [SerializeField] private int selectedIndex = 0;

    [Header("Play Settings")]
    [Tooltip("If true, PlaySelected will ask BattleManager to choose a default target.")]
    [SerializeField] private bool useDefaultTarget = true;

    [Tooltip("Explicit target (if you don't want default targeting yet).")]
    [SerializeField] private MonoBehaviour explicitTargetEnemy; 

    // --------- Public API ---------

    public IReadOnlyList<Card> Hand => hand;
    public int HandLimit => handLimit;
    public int SelectedIndex => selectedIndex;

    public Card GetSelectedCard()
    {
        if (hand.Count == 0) return null;
        selectedIndex = Mathf.Clamp(selectedIndex, 0, hand.Count - 1);
        return hand[selectedIndex];
    }

    /// <summary>
    /// Add a card into hand (enforces hand limit).
    /// </summary>
    public bool AddCardToHand(Card card)
    {
        if (card == null) return false;

        if (hand.Count >= handLimit)
        {
            Debug.LogWarning("[Hand] Hand is full (limit=10). Card rejected.");
            return false;
        }

        hand.Add(card);

        // Keep selection valid
        selectedIndex = Mathf.Clamp(selectedIndex, 0, hand.Count - 1);
        return true;
    }

    /// <summary>
    /// Remove a card by index (safe).
    /// </summary>
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

    /// <summary>
    /// Select next card (wrap).
    /// Hook to Left/Right arrows or UI buttons.
    /// </summary>
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

    /// <summary>
    /// Main play entry (bind this to a UI button).
    /// It will call BattleManager.TryPlayCard(...) and only remove from hand if success.
    /// </summary>
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
