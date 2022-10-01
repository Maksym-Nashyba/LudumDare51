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

    protected virtual void Awake()
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
        CurrentState.OnStart(GetContext());
        StartCoroutine(CurrentState.Act());
    }

    protected abstract Context GetContext();
    
    protected class Context
    {
        public WaypointsContainer WaypointsContainer;
        public NavMeshAgent NavMeshAgent;
        public Transform Transform;
        public NPC NPC;

        public Context(WaypointsContainer waypointsContainer, NavMeshAgent navMeshAgent, Transform transform, NPC npc)
        {
            WaypointsContainer = waypointsContainer;
            NavMeshAgent = navMeshAgent;
            Transform = transform;
            NPC = npc;
        }
    }
    
    protected abstract class State
    {
        public abstract void OnStart(Context context);
        
        public abstract IEnumerator Act();
        
        public abstract void OnEnd();
    }
}