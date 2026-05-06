using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class AI_NavMeshAgent : MonoBehaviour
{
    public static AI_NavMeshAgent Instance;

    #region References

    [Header("References")]
    [SerializeField] private NavMeshAgent ai_NavMeshAgent;
    [SerializeField] private Ennemy_Perception ennemy_Perception;

    #endregion

    #region Parameters

    [Header("Parameters")]

    [SerializeField] private float timelosingtarget;
    [SerializeField] private float rotationTime;
    public bool isfollowing = false;
    public bool ispatroling = false;
    public bool isloosingtarget = false;
    private Coroutine stopfollowing = null;
    private Tween rotateTween = null;
    private Transform target = null;

    #endregion
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ispatroling = true;
    }

    private void Update()
    {
        if (isfollowing) RotateEnemy();
    }

    public void RotateEnemy()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0;

            Quaternion targetRot = Quaternion.LookRotation(direction);

            rotateTween?.Kill();
            rotateTween = transform.DORotateQuaternion(targetRot, rotationTime);
        }
    }

    public void StopFollowCooldown()
    {
        if (isloosingtarget == false && isfollowing == true)
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
        if (isfollowing == false)
        {
            isfollowing = true;
            ispatroling = false;
            isloosingtarget = false;
        }
        
        ai_NavMeshAgent.SetDestination(target.transform.position);
    }

    public void ColliderPerception(Collider other)
    {
        ennemy_Perception.SetPlayer(other.transform);
        target = other.transform;
    }

    public IEnumerator TimeToLoseTarget()
    {
        yield return new WaitForSeconds(timelosingtarget);

        Debug.Log("Test");

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