using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskPanelController : MonoBehaviour
{
    [Header("Source")]
    [SerializeField] private MaskManager maskManager;

    [Header("UI - Queue Slots (panel)")]
    public Image[] queueSlotImages = new Image[3];

    [Header("UI - Inventory Slots (panel)")]
    public Image[] inventorySlotImages = new Image[5];

    [Header("Inventory Locked Icon ")]
    public Sprite lockedIcon;

    // Panel-only inventory view (5 slots)
    private MaskData[] inventory = new MaskData[5];

    private void OnEnable()
    {
        if (maskManager != null)
            maskManager.OnMaskStateChanged += RefreshUI;

        RebuildInventoryFromManager();
        RefreshUI();
    }

    private void OnDisable()
    {
        if (maskManager != null)
            maskManager.OnMaskStateChanged -= RefreshUI;
    }

    private void RebuildInventoryFromManager()
    {
        // Inventory shows unlocked masks that are NOT currently in queue.
        for (int i = 0; i < 5; i++) inventory[i] = null;

        if (maskManager == null) return;

        List<MaskData> unlocked = maskManager.GetUnlockedMasksInOrder();

        // Exclude queue masks
        HashSet<MaskData> inQueue = new HashSet<MaskData>();
        for (int i = 0; i < 3; i++)
        {
            var q = maskManager.GetQueueMask(i);
            if (q != null) inQueue.Add(q);
        }

        int write = 0;
        foreach (var m in unlocked)
        {
            if (write >= 5) break;
            if (m == null) continue;
            if (inQueue.Contains(m)) continue;
            inventory[write++] = m;
        }
    }

    public void OnClickInventory(int index)
    {
        if (maskManager == null) return;
        if (index < 0 || index >= inventory.Length) return;

        MaskData clicked = inventory[index];
        if (clicked == null) return; // locked/empty

        // Push into queue; get the kicked mask back
        MaskData kicked = maskManager.PushIntoQueue(clicked);

        // Put kicked one back into the same inventory slot (can be null)
        inventory[index] = kicked;

        RefreshUI();
    }

    private void RefreshUI()
    {
        // Queue UI
        for (int i = 0; i < 3; i++)
        {
            if (queueSlotImages[i] == null) continue;

            MaskData m = (maskManager != null) ? maskManager.GetQueueMask(i) : null;
            if (m != null && m.icon != null)
            {
                queueSlotImages[i].sprite = m.icon;
                queueSlotImages[i].enabled = true;
            }
            else
            {
                queueSlotImages[i].sprite = null;
                queueSlotImages[i].enabled = false;
            }
        }

        // Inventory UI (panel)
        for (int i = 0; i < 5; i++)
        {
            if (inventorySlotImages[i] == null) continue;

            MaskData m = inventory[i];
            if (m != null && m.icon != null)
            {
                inventorySlotImages[i].sprite = m.icon;
                inventorySlotImages[i].enabled = true;
            }
            else
            {
                if (lockedIcon != null)
                {
                    inventorySlotImages[i].sprite = lockedIcon;
                    inventorySlotImages[i].enabled = true;
                }
                else
                {
                    inventorySlotImages[i].sprite = null;
                    inventorySlotImages[i].enabled = false;
                }
            }
        }
    }
}
