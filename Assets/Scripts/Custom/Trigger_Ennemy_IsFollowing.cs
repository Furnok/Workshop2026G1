using UnityEngine;

public class Trigger_Ennemy_IsFollowing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            AI_NavMeshAgent.Instance.isfollowing = true;
    }

}
