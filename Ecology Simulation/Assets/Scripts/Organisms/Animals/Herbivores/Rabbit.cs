using System;
using System.Linq;
using Essentials.References;
using UnityEngine;
using Random = UnityEngine.Random;
[RequireComponent(typeof(Move))]
public class Rabbit : Herbivore
{
    [SerializeField] private FloatReference idle;
    [SerializeField] private float SafetyProximity;

    private enum State
    {
        Idle,
        Hungry,
        Thirsty,
        Running,
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

    private void Start()
    {
        targetPosition = transform.position;
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
            case State.Running:
                FoxSpotted(Target.Predator);
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
        var FoxSearch = SearchForItem(Target.Predator);
        if (foxSearch) 
        {
            float distance = Vector3.Distance(transform.position, foxSearch.transform.position);
            if(distance <= SafetyProximity) {
                state = State.Running;
                return;
            }
        }
    
        string nameMinValue = 
            (new[] 
        {
            Tuple.Create(nameof(Sustenance), Sustenance.Value),
            Tuple.Create(nameof(Hydration), Hydration.Value),
            Tuple.Create(nameof(SexualSatisfaction), SexualSatisfaction.Value),
            Tuple.Create(nameof(idle), idle.Value),
        })
            .OrderByDescending(t => t.Item2)
            .Last().Item1;

        state = nameMinValue switch
        {
            nameof(Sustenance) => State.Hungry,
            nameof(Hydration) => State.Thirsty,
            nameof(SexualSatisfaction) => State.Horny,
            nameof(idle) => State.Idle,
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
        targetPosition = targetGameObject.transform.position;
    }
    
     private void FoxSpotted(Target target)
    {
        var Fox = SearchForItem(target);
        targetPosition = (transform.position - Fox.transform.position).normalized * 3;
    }

    private void Movement()
    {
        mover.RotateTowardsTarget(targetPosition);
        mover.MoveToTarget();
    }


}
