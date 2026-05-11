using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AI_NavMesh_Agent_Patrol : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform[] patrolPoint;

    [Header("Parameters")]

    [SerializeField] private int positionPoint = 0;

    private void Start()
    {
        GotoNextPoint();
    }

    private void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance + 0.1f)
        {
            GotoNextPoint();
        }
    }
    public void GotoNextPoint()
    {
        if (patrolPoint.Length == 0)
            return;

        _agent.SetDestination(patrolPoint[positionPoint].position);

        positionPoint = (positionPoint+1) % patrolPoint.Length;
    }    
}