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

    public void ChangeState(State nextState)
    {
        StopAllCoroutines();
        CurrentState?.End();
        CurrentState = nextState;
        CurrentState.Start(GetContext());
        StartCoroutine(CurrentState.Act());
    }

    protected abstract Context GetContext();
    
    public class Context
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
    
    public abstract class State
    {
        public abstract void Start(Context context);
        
        public abstract IEnumerator Act();
        
        public abstract void End();

        public abstract bool IsRelaxed();
    }
}