using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class Ennemy_Perception : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private Transform nose;
    [SerializeField] private LayerMask player;
    [SerializeField] private LayerMask obstacle;

    [Header("Settings")]

    [SerializeField] private float viewrange;
    [SerializeField] private float viewangle;
    [SerializeField] private float viewradius;
    [SerializeField] private float targetoffset;

    [Header("Debug")]

    [SerializeField] private int coneArcSegment;

    private bool PlayerInCone;
    private bool PlayerInObserveRadius;
    private Transform playerTransform;
    private Vector3 playerposition;


    bool _debugBlocked;
    Vector3 _debugCastOrigin;
    Vector3 _debugCastDir;
    float _debugCastDist;
    Vector3 _debugHitPoint;
    bool _debugHasHit;


    void Update()
    {
        PlayerInCone = false;
        PlayerInObserveRadius = false;
        _debugBlocked = false;

        if (playerTransform == null)
        {
            return;
        }

        playerposition = playerTransform.position;
        Vector3 origin = nose.position;
        Vector3 target = playerTransform.position;
        target.y = targetoffset;

        Vector3 toplayer = target - origin;
        float distance = toplayer.magnitude;

        PlayerInObserveRadius = distance <= viewrange;

        _debugCastOrigin = origin;
        _debugCastDist = distance;

        Vector3 dir = toplayer / distance;
        _debugCastDir = dir;

        float angle = Vector3.Angle(nose.forward, dir);

        if (angle > viewangle * 0.5 || !PlayerInObserveRadius)
        {
            AI_NavMeshAgent.Instance.StopFollowCooldown();
            return; 
        }
            

        if (Physics.SphereCast(origin, viewradius, dir, out RaycastHit hit, distance, obstacle, QueryTriggerInteraction.Ignore))
        {
            _debugBlocked = true;
            _debugHasHit = true;
            _debugHitPoint = hit.point;
            PlayerInCone = false;
            AI_NavMeshAgent.Instance.StopFollowCooldown();
            return;
        }

        PlayerInCone = true;
        AI_NavMeshAgent.Instance.EnnemyFollowing();

    }


    public void SetPlayer(Transform player) => playerTransform = player;

    private void OnDrawGizmos()
    {
        Transform eyes = nose != null ? nose : transform;
        Vector3 origin = eyes.position;

        Gizmos.color = new Color(1f, 1f, 1f, 0.12f);
        Gizmos.DrawSphere(origin, viewrange);
        Gizmos.color = new Color(1f, 1f, 1f, 0.35f);
        Gizmos.DrawWireSphere(origin, viewrange);

        float half = viewangle * 0.5f;
        Vector3 forward = eyes.forward;
        Vector3 leftDir = Quaternion.Euler(0f, -half, 0f) * forward;
        Vector3 rightDir = Quaternion.Euler(0f, half, 0f) * forward;

        Gizmos.color = new Color(1f, 1f, 0f, 0.8f);
        Gizmos.DrawLine(origin, origin + leftDir.normalized * viewrange);
        Gizmos.DrawLine(origin, origin + rightDir.normalized * viewrange);

        if (coneArcSegment < 2) return;
        Vector3 prev = origin + leftDir.normalized * viewrange;

        for (int i = 1; i <= coneArcSegment; i++)
        {
            float t = (float)i / coneArcSegment;
            float ang = Mathf.Lerp(-half, half, t);
            Vector3 dir = Quaternion.Euler(0f, ang, 0f) * forward;
            Vector3 p = origin + dir.normalized * viewrange;

            Gizmos.DrawLine(prev, p);
            prev = p;
        }

        if (!Application.isPlaying) return;

        Color castColor =
            PlayerInCone ? new Color(0f, 1f, 0f, 0.9f) :
            _debugBlocked ? new Color(1f, 0f, 0f, 0.9f) :
            new Color(0.6f, 0.6f, 0.6f, 0.7f);
        Gizmos.color = castColor;

        Vector3 end = _debugCastOrigin + _debugCastDir.normalized * Mathf.Min(_debugCastDist, viewrange);
        Gizmos.DrawLine(_debugCastOrigin, end);

        Gizmos.DrawWireSphere(end, viewradius);

        if (_debugHasHit)
        {
            Gizmos.color = new Color(1f, 0f, 0f, 1f);
            Gizmos.DrawSphere(_debugHitPoint, 0.08f);
            Gizmos.DrawWireSphere(_debugHitPoint, viewradius);
        }
    }
}
