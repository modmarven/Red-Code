using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float time;
    private float speedWalk = 1.5f;
    private float speedRun = 2f;
    public Transform player;
    public float playerDistance = 8.0f;
    private Animator animator;
    private bool canRun = true;
    private bool canWalk = true;

    [SerializeField] private EnemyWaypoint waypoint;
    private Transform currentWaypoint;
    [SerializeField] private float distanceThreshHold = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        time = 0;

        currentWaypoint = waypoint.NextWayPoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        currentWaypoint = waypoint.NextWayPoint(currentWaypoint);
        transform.LookAt(currentWaypoint);
    }

    // Update is called once per frame
    void Update()
    {
        EnemyIdleState();
        EnemyWalkState();
        EnemyRunState();
        EnemyAttackState();
       
    }

    private void EnemyIdleState()
    {
        time += Time.deltaTime;

        if (time > 5)
        {
            animator.SetBool("isWalk", true);

            if (canWalk)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, speedWalk * Time.deltaTime);
                if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshHold)
                {
                    currentWaypoint = waypoint.NextWayPoint(currentWaypoint);
                    transform.LookAt(currentWaypoint);
                }
            }
            
        }

        if (time > 10)
        {
            animator.SetBool("isWalk", false);
            time = 0;
        }

    }

    private void EnemyWalkState()
    {
        float distance = Vector3.Distance(player.position, animator.transform.position);

        if (distance < playerDistance)
        {
            animator.transform.LookAt(player);
            animator.SetBool("isRun", true);

            if (canRun)
            {
                transform.Translate(Vector3.forward * speedRun * Time.deltaTime);
            }
           
        }

    }

    private void EnemyRunState()
    {
        float playerRange = 3f;
        float distance = Vector3.Distance(player.position, animator.transform.position);

        if (distance > playerDistance)
        {
            animator.SetBool("isRun", false);
            canRun = false;
        }

        if ( distance < playerRange)
        {
            animator.SetBool("isAttack", true);
            canRun = false;
            canWalk = false;
        }     

    }

    private void EnemyAttackState()
    {
        float playerRange = 3.5f;
        float distance = Vector3.Distance(player.position, animator.transform.position);

        if (distance > playerRange)
        {
            animator.SetBool("isAttack", false);
            EnemyRunState();
            canRun = true;
        }

        if (distance > playerDistance)
        {
            EnemyIdleState();
            canWalk = true;
        }

    }
}
