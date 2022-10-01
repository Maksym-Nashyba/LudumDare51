using System.Collections;
using NPCs.Navigation;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPC : MonoBehaviour
{
    protected WaypointsContainer WaypointsContainer;
    protected NavMeshAgent NavMeshAgent;
    protected Transform Transform;
    protected State CurrentState;
    private Coroutine _runningStateCoroutine;

    private void Awake()
    {
        WaypointsContainer = FindObjectOfType<WaypointsContainer>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Transform = GetComponent<Transform>();
    }

    private void Update()
    {
        CurrentState.Act();
    }

    protected void ChangeState(State nextState)
    {
        if(_runningStateCoroutine is not null)StopCoroutine(_runningStateCoroutine);
        CurrentState?.OnEnd();
        CurrentState = nextState;
        StartCoroutine(CurrentState.Act());
        CurrentState.OnStart(GetContext());
    }

    protected abstract Context GetContext();
    
    protected class Context
    {
        public WaypointsContainer WaypointsContainer;
        public NavMeshAgent NavMeshAgent;
        public Transform Transform;

        public Context(WaypointsContainer waypointsContainer, NavMeshAgent navMeshAgent, Transform transform)
        {
            WaypointsContainer = waypointsContainer;
            NavMeshAgent = navMeshAgent;
            Transform = transform;
        }
    }
    
    protected abstract class State
    {
        public abstract void OnStart(Context context);
        
        public abstract IEnumerator Act();
        
        public abstract void OnEnd();
    }
}