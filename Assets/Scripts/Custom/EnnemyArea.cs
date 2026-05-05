using UnityEngine;

public class EnnemyArea : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AI_NavMeshAgent.Instance.ColliderPerception(other);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        /*if (other.CompareTag("Player"))
        {
            StartCoroutine(TimeToLoseTarget());
            isfollowing = false;
        }*/
    }
}
