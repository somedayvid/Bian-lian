using System;
using System.Collections.Generic;
using UnityEngine;

public class MaskManager : MonoBehaviour
{
    [Header("All Masks (8)")]
    public MaskData[] allMasks = new MaskData[8];

    [Header("Unlocked Flags (Inspector)")]
    public bool[] unlocked = new bool[8];

    [Header("Equipped Queue (3)")]
    [SerializeField] private MaskData[] queue = new MaskData[3];
    [SerializeField, Range(0, 2)] private int activeIndex = 0;

    /// <summary>
    /// Fired whenever queue/active changes.
    /// HUD and panel can listen.
    /// </summary>
    public event Action OnMaskStateChanged;

    public int ActiveIndex => activeIndex;

    public MaskData GetQueueMask(int i)
    {
        if (i < 0 || i >= 3) return null;
        return queue[i];
    }

    public MaskData GetActiveMask()
    {
        return queue[activeIndex];
    }

    public void BuildInitialQueue()
    {
        List<MaskData> unlockedList = GetUnlockedMasksInOrder();

        for (int i = 0; i < 3; i++)
            queue[i] = (i < unlockedList.Count) ? unlockedList[i] : null;

        activeIndex = Mathf.Clamp(activeIndex, 0, 2);
        OnMaskStateChanged?.Invoke();
    }

    public List<MaskData> GetUnlockedMasksInOrder()
    {
        List<MaskData> result = new List<MaskData>();
        for (int i = 0; i < allMasks.Length; i++)
        {
            if (i < unlocked.Length && unlocked[i] && allMasks[i] != null)
                result.Add(allMasks[i]);
        }
        return result;
    }

    /// <summary>
    /// Rotate active index after successful card play.
    /// </summary>
    public void RotateActive()
    {
        activeIndex = (activeIndex + 1) % 3;
        OnMaskStateChanged?.Invoke();
    }

    public MaskData PushIntoQueue(MaskData newMask)
    {
        if (newMask == null) return null;

        MaskData kicked = queue[2];
        queue[2] = queue[1];
        queue[1] = queue[0];
        queue[0] = newMask;

        OnMaskStateChanged?.Invoke();
        return kicked; // return what got kicked out
    }

    private void Awake()
    {
        // IMPORTANT: build queue even if MaskPanel is disabled
        BuildInitialQueue();
    }
}
