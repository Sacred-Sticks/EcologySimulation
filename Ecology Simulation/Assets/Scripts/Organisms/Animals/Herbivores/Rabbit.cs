using System;
using System.Linq;
using Essentials.References;
using UnityEngine;
using Random = UnityEngine.Random;
[RequireComponent(typeof(Move))]
public class Rabbit : Herbivore
{
    [SerializeField] private FloatReference idle;
    [SerializeField] private FloatReference run;

    private enum State
    {
    
        Idle,
        Hungry,
        Thirsty,
        Flee,
        Horny,
    }

    private State state;
    private Move mover;
    private Vector3 targetPosition;
    private const float TOLERANCE = 0.1f;
    private void Awake()
    {
        mover = GetComponent<Move>();
    }

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
            case State.Flee:
                SearchForPredator(Target.Predator);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }  
    }

    private void FixedUpdate()
    {
        Movement();

    }

    private void CheckState()
    {
        string nameMinValue = (new[] 
        {
            Tuple.Create(nameof(Sustenance), Sustenance),
            Tuple.Create(nameof(Hydration), Hydration),
            Tuple.Create(nameof(ReproductionDesire), ReproductionDesire),
            Tuple.Create(nameof(idle), idle.Value),
            Tuple.Create(nameof(run), run.Value),
        }).OrderByDescending(t => t.Item2).Last().Item1;
        
        state = nameMinValue switch
        {
            nameof(Sustenance) => State.Hungry,
            nameof(Hydration) => State.Thirsty,
            nameof(ReproductionDesire) => State.Horny,
            nameof(idle) => State.Idle,
            nameof(run) => State.Flee,
            _ => throw new ArgumentOutOfRangeException(nameof(nameMinValue), nameMinValue, null)
        };
    }

    private void IdleBehavior()
    {
        if (Vector3.SqrMagnitude(transform.position - targetPosition) > Mathf.Pow(TOLERANCE, 2))
            return;
        var direction = GetRandomDirection();
        float distance = Random.Range(1f, 5f);
        targetPosition = transform.position + (direction * distance);
    }

    private void SearchForTarget(Target target)
    {
        if (Vector3.SqrMagnitude(transform.position - targetPosition) > Mathf.Pow(TOLERANCE, 2))
            return;
        var targetGameObject = SearchForItem(target);
        if(!targetGameObject)
        {
            IdleBehavior();
            return;
        }
        var predatorNearby = SearchForItem(Target.Predator);
        if(!predatorNearby)
        {
            targetPosition = targetGameObject.transform.position;
        }
        else
        {
            SearchForPredator(Target.Predator);
        }
    }

    private void SearchForPredator(Target target)
    {
        if (Vector3.SqrMagnitude(transform.position - targetPosition) > Mathf.Pow(TOLERANCE, 2))
            return;
        var targetGameObject = SearchForItem(target);
        if(!targetGameObject)
        {
            IdleBehavior();
            return;
        }
        targetPosition = targetGameObject.transform.position * -1;
    }

    private void Movement()
    {
        
        mover.RotateTowardsTarget(targetPosition);
        mover.MoveToTarget();
    }


}
