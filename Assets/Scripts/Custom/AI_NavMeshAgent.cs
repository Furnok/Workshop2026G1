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
    [SerializeField] private Ennemy_Perception ennemy_Perception;

    #endregion

    #region Parameters

    [Header("Parameters")]

    [SerializeField] private float timelosingtarget;
    public bool isfollowing = false;
    public bool ispatroling = false;

    #endregion
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ispatroling = true;
    }
    /*private void Update()
    {
        EnnemyFollowing();
    }*/

    public void EnnemyFollowing()
    {
            ispatroling = false;
            ai_NavMeshAgent.destination = player.transform.position;
    }

    public void ColliderPerception(Collider other)
    {
            ennemy_Perception.SetPlayer(other.transform);
            isfollowing = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ennemy_Perception.SetPlayer(other.transform);
            isfollowing = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TimeToLoseTarget());
            isfollowing = false;
        }
    }

    public IEnumerator TimeToLoseTarget()
    {
        yield return new WaitForSeconds(timelosingtarget);
    }
}