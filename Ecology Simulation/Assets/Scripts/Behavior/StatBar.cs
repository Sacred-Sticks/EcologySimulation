using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    [SerializeField] private float statTimer;
    [SerializeField] private Image statBar;

    private const int maxStatValue = 1;

    public void LowerStat(ref float organismStat, float multiplier)
    {
        organismStat -= multiplier * statTimer;
        
        if (organismStat <= 0)
        {
            Destroy(transform.parent);
        }
        
        // Modify statBar data for visuals
    }

    public float ResetStat()
    {
        return maxStatValue;
    }
}
