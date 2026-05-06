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
    public bool isloosingtarget = false;
    private Coroutine stopfollowing = null;

    #endregion
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ispatroling = true;
    }
    
    public void StopFollowCooldown()
    {
        if(isloosingtarget == false && isfollowing == true)
        {
            isloosingtarget = true;

            Debug.Log("LooseTarget");

            if (stopfollowing != null)
            {
                StopCoroutine(stopfollowing);
                stopfollowing = null;
            }
            stopfollowing = StartCoroutine(TimeToLoseTarget());
        }
        
    }

    public void EnnemyFollowing()
    {
        if(isfollowing == false)
        {
            isfollowing = true;
            ispatroling = false;
            isloosingtarget = false;
        }
            ai_NavMeshAgent.destination = player.transform.position;
    }

    public void ColliderPerception(Collider other)
    {
            ennemy_Perception.SetPlayer(other.transform);
    }

    public IEnumerator TimeToLoseTarget()
    {
        yield return new WaitForSeconds(timelosingtarget);
        ai_NavMeshAgent.ResetPath();
        ispatroling = true;
        isfollowing = false;
        isloosingtarget = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //GameManager.Instance.KillPlayer();
            ispatroling = true ;
            isfollowing = false ;
            isloosingtarget = false ;
            
            //if (stopfollowing != null)
            {
                //StopCoroutine(stopfollowing);
                //stopfollowing = null;
            }

        }
    }
}