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

    [Serializable]
    public class ResourceData
    {
        [SerializeField] private Statistic statistic;
        [SerializeField] private LayerMask layers;
        [SerializeField] private FloatReference range;
        [SerializeField] private UnityEvent<GameObject, Statistic> onFind;

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

    private void Update()
    {
        foreach (var resourceData in data)
        {
            FindClosest(out var target, resourceData.Layers, resourceData.Range);
            if (target)
                resourceData.OnFind?.Invoke(target, resourceData.Statistic);
        }
    }

    private void FindClosest(out GameObject closest, LayerMask target, float range)
    {
        var results = new List<Collider2D>();
        var filter = new ContactFilter2D
        {
            layerMask = target,
        };
        Physics2D.OverlapCircle(transform.position, range, filter, results);

        closest = results.OrderBy(r => Vector3.SqrMagnitude(transform.position - r.transform.position))
            .FirstOrDefault()
            ?.gameObject;
    }

    public void EatTarget(GameObject target, Statistic statistic)
    {
        Destroy(target);
        statistic.AddToStat(0.1f);
    }

    public void DrinkWater(GameObject target, Statistic statistic)
    {
        statistic.AddToStat(0.25f * Time.deltaTime);
    }
}
