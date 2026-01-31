using UnityEngine;
using UnityEngine.UI;

public class MaskQueueHUD : MonoBehaviour
{
    [Header("Source (optional drag)")]
    [SerializeField] private MaskManager maskManager;

    [Header("3 Icons")]
    [SerializeField] private Image[] icons = new Image[3];

    [Header("3 Backgrounds (for highlight)")]
    [SerializeField] private Image[] backgrounds = new Image[3];

    [Header("Highlight")]
    [SerializeField] private Color activeBg = new Color(1f, 1f, 1f, 1f);
    [SerializeField] private Color inactiveBg = new Color(1f, 1f, 1f, 0.25f);

    private void Awake()
    {
        // Auto-bind if not assigned in Inspector
        if (maskManager == null)
        {
            maskManager = FindFirstObjectByType<MaskManager>();
            if (maskManager == null)
                Debug.LogError("[MaskQueueHUD] No MaskManager found in scene. HUD will not update.");
        }
    }

    private void OnEnable()
    {
        if (maskManager != null)
            maskManager.OnMaskStateChanged += Refresh;

        Refresh();
    }

    private void OnDisable()
    {
        if (maskManager != null)
            maskManager.OnMaskStateChanged -= Refresh;
    }

    public void Refresh()
    {

        if (maskManager == null)
        {
            Debug.LogWarning("[MaskQueueHUD] maskManager is null (not assigned / not found).");
            return;
        }

        int active = maskManager.ActiveIndex;

        for (int i = 0; i < 3; i++)
        {
            MaskData m = maskManager.GetQueueMask(i);

            if (icons != null && i < icons.Length && icons[i] != null)
            {
                if (m != null && m.icon != null)
                {
                    icons[i].sprite = m.icon;
                    icons[i].enabled = true;
                }
                else
                {
                    icons[i].sprite = null;
                    icons[i].enabled = false;
                }
            }

            if (backgrounds != null && i < backgrounds.Length && backgrounds[i] != null)
            {
                backgrounds[i].color = (i == active) ? activeBg : inactiveBg;
            }
        }
    }
}
