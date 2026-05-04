using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.AI;

public class AI_NavMeshAgent : MonoBehaviour
{
    public static AI_NavMeshAgent Instance;

    #region References

    [Header("References")]

    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent ai_NavMeshAgent;

    #endregion

    #region Parameters

    [Header("Parameters")]

    public bool isfollowing = false;

    #endregion
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        EnnemyFollowing();
    }

    public void EnnemyFollowing()
    {
        if (isfollowing)
        {
            ai_NavMeshAgent.destination = player.transform.position;
        }
    }
}