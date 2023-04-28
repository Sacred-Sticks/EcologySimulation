using System;
using System.Collections.Generic;
using System.Linq;
using Essentials.References;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Organism))]
public class ResourceDetection : MonoBehaviour
{
    [SerializeField] private ResourceData[] data;
    [SerializeField] private GameObject speciesPrefab;
    
    [Serializable]
    public class ResourceData
    {
        [SerializeField] private Statistic.StatType key;
        [SerializeField] private Statistic statistic;
        [SerializeField] private LayerMask layers;
        [SerializeField] private FloatReference range;
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
        public UnityEvent<GameObject, Statistic> OnFind
        {
            get
            {
                return onFind;
            }
        }
    }

    public Dictionary<Statistic.StatType, Statistic> statistics { get; private set; } = new Dictionary<Statistic.StatType, Statistic>();

    private CircleCollider2D collider;
    
    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        statistics = data.ToDictionary(d => d.Key, d => d.Statistic);
    }

    private void Update()
    {
        foreach (var resourceData in data)
        {
            FindClosest(out var target, resourceData.Layers, resourceData.Range + collider.radius);
            if (target)
                resourceData.OnFind?.Invoke(target, resourceData.Statistic);
        }
    }

    private void FindClosest(out GameObject closest, LayerMask targetLayers, float range)
    {
        var results = new List<Collider2D>();
        Physics2D.OverlapCircle(transform.position, range, new ContactFilter2D(), results);

        if (results.Contains(collider))
            results.Remove(collider);

        closest = results.Where(c => targetLayers == (targetLayers | 1 << c.gameObject.layer))
            .OrderBy(r => Vector3.SqrMagnitude(transform.position - r.transform.position))
            .FirstOrDefault()
            ?.gameObject;
    }

    public void EatTarget(GameObject target, Statistic statistic)
    {
        Destroy(target);
        statistic.AddToStat(0.1f);
        Debug.Log($"Ate: Sustenance at {statistic.Value}");
    }

    public void DrinkWater(GameObject target, Statistic statistic)
    {
        statistic.AddToStat(0.25f * Time.deltaTime);
    }

    public void Mate(GameObject target, Statistic statistic)
    {
        if (statistic.Value > 0.75f)
            return;
        target.GetComponent<ResourceDetection>().statistics[statistic.StatisticType].AddToStat(1);
        statistic.AddToStat(1);
        Instantiate(speciesPrefab, transform.position, transform.rotation);
    }
}
