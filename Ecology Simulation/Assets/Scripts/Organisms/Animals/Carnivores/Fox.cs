using System;
using System.Linq;
using Essentials.References;
using UnityEngine;

public class Fox : Carnivore
{
    [SerializeField] private FloatReference idle;

    private enum State
    {
        Idle,
        Hungry,
        Thirsty,
        Horny,
    }

    private State state;

    private void Update()
    {
        CheckState();

        switch (state)
        {
            case State.Idle:
                IdleBehavior();
                break;
            case State.Hungry:
                SearchForTarget(Target.Food);
                break;
            case State.Thirsty:
                SearchForTarget(Target.Water);
                break;
            case State.Horny:
                SearchForTarget(Target.Mate);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private void CheckState()
    {
        string nameMinValue = (new[]
        {
            Tuple.Create(nameof(Sustenance), Sustenance),
            Tuple.Create(nameof(Hydration), Hydration),
            Tuple.Create(nameof(ReproductionDesire), ReproductionDesire),
            Tuple.Create(nameof(idle), idle.Value),
        }).OrderByDescending(t => t.Item2).Last().Item1;

        state = nameMinValue switch
        {
            nameof(Sustenance) => State.Hungry,
            nameof(Hydration) => State.Thirsty,
            nameof(ReproductionDesire) => State.Horny,
            nameof(idle) => State.Idle,
            _ => throw new ArgumentOutOfRangeException(nameof(nameMinValue), nameMinValue, null)
        };
    }

    private void IdleBehavior()
    {
        // Return if target is not currently reached, then get random direction, set target to the direction * some range
        
        var direction = GetRandomDirection();
        
    }

    private void SearchForTarget(Target target)
    {
        var targetGameObject = SearchForItem(target);
        if (!targetGameObject)
            return;
        
        // Now move to the target
        
        Debug.Log($"Found at {targetGameObject.transform.position}");
    }
}
