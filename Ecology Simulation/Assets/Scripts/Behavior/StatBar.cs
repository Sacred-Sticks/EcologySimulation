using Essentials.References;
using Essentials.Variables;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    [SerializeField] private FloatVariable organismStatVariable;
    [SerializeField] private FloatReference statTimer;
    [SerializeField] private Image statBar;

    private const int maxStatValue = 1;

    public float organismStat
    {
        get
        {
            return organismStatVariable.Value;
        }
        set
        {
            organismStatVariable.Value = value;
        }
    }
    
    public void LowerStat(float multiplier)
    {
        organismStat -= multiplier * statTimer.Value;
        
        if (organismStat <= 0)
        {
            Destroy(transform.parent);
        }
        
        // Modify statBar data for visuals
    }

    public void ResetStat()
    {
        organismStat = maxStatValue;
    }
}
