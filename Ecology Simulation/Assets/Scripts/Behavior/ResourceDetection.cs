using System;
using System.Collections.Generic;
using System.Linq;
using Essentials.Events;
using Essentials.References;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(Organism))]
public class ResourceDetection : MonoBehaviour
{
    [SerializeField] private ResourceData[] data;
    [SerializeField] private GameObject speciesPrefab;
    [SerializeField] private EventBus populationTracking;

    [Serializable]
    public class ResourceData
    {
        [SerializeField] private Statistic.StatType key;
        [SerializeField] private Statistic statistic;
        [SerializeField] private LayerMask layers;
        [SerializeField] private FloatReference range;
        [SerializeField] private FloatReference statisticAdditiveValue;
        [SerializeField] private UnityEvent<GameObject, Statistic> onFind;

        public Statistic.StatType Key
        {
            get
            {
                return key;
            }
        }
        public Statistic Statistic
        {
            get
            {
                return statistic;
            }
        }
        public LayerMask Layers
        {
            get
            {
                return layers;
            }
        }
        public float Range
        {
            get
            {
                return range.Value;
            }
        }
        public float StatisticAdditiveValue
        {
            get
            {
                return statisticAdditiveValue.Value;
            }
        }
        public UnityEvent<GameObject, Statistic> OnFind
        {
            get
            {
                return onFind;
            }
        }
    }

    public Dictionary<Statistic.StatType, Statistic> statistics { get; private set; }
    public Dictionary<Statistic, float> statisticAdditiveValues { get; private set; } 

    private CapsuleCollider2D objectCollider;
    
    private void Awake()
    {
        objectCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        statistics = data.ToDictionary(d => d.Key, d => d.Statistic);
        statisticAdditiveValues = data.ToDictionary(d => d.Statistic, d => d.StatisticAdditiveValue);
        populationTracking.Trigger(this, new PopulationChangeEventArgs(1));
    }

    private void Update()
    {
        foreach (var resourceData in data)
        {
            FindClosest(out var target, resourceData.Layers, resourceData.Range + objectCollider.size.y / 2);
            if (target)
                resourceData.OnFind?.Invoke(target, resourceData.Statistic);
        }
    }

    private void FindClosest(out GameObject closest, LayerMask targetLayers, float range)
    {
        var results = new List<Collider2D>();
        Physics2D.OverlapCircle(transform.position, range, new ContactFilter2D(), results);

        if (results.Contains(objectCollider))
            results.Remove(objectCollider);

        closest = results.Where(c => targetLayers == (targetLayers | 1 << c.gameObject.layer))
            .OrderBy(r => Vector3.SqrMagnitude(transform.position - r.transform.position))
            .FirstOrDefault()
            ?.gameObject;
    }

    public void EatTarget(GameObject target, Statistic statistic)
    {
        Destroy(target);
        statistic.AddToStat(statisticAdditiveValues[statistic]);
    }

    public void DrinkWater(GameObject target, Statistic statistic)
    {
        statistic.AddToStat(statisticAdditiveValues[statistic] * Time.deltaTime);
    }

    public void Mate(GameObject target, Statistic statistic)
    {
        if (statistic.Value > 0.75f)
            return;
        var targetManager = target.GetComponent<ResourceDetection>();
        if (targetManager.statistics == null)
            return;
        var targetStatistic = targetManager.statistics[Statistic.StatType.SexualSatisfaction];
        targetStatistic.AddToStat(targetManager.statisticAdditiveValues[targetStatistic]);
        statistic.AddToStat(statisticAdditiveValues[statistic]);
        Instantiate(speciesPrefab, transform.position, transform.rotation);
    }

    private void OnDestroy()
    {
        populationTracking.Trigger(this, new PopulationChangeEventArgs(-1));
    }
}
