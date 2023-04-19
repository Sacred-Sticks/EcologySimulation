using Essentials.References;
using Essentials.Variables;
using UnityEngine;

public class StatBar : MonoBehaviour
{
    [SerializeField] private FloatVariable organismStatVariable;
    [SerializeField] private FloatReference statTimer;

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
        organismStat -= multiplier * organismStat;
        
        if (organismStat <= 0)
        {
            Destroy(transform.parent);
        }
    }

    public void ResetStat()
    {
        organismStat = maxStatValue;
    }
}
