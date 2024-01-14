using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : StateMachineBehaviour
{
    private float timer;
    List<Transform> wayPoint = new List<Transform>();
    NavMeshAgent enemy;
    Transform player;
    public float playerDistance = 8.0f;

    public bool enabled { get; internal set; }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = animator.GetComponent<NavMeshAgent>();
        enemy.speed = 1.5f;
        timer = 0;
        GameObject patroll = GameObject.FindGameObjectWithTag("WayPoint");
        foreach (Transform t in patroll.transform)
            wayPoint.Add(t);

        enemy.SetDestination(wayPoint[Random.Range(0, wayPoint.Count)].transform.position);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy.remainingDistance <= enemy.stoppingDistance)
            enemy.SetDestination(wayPoint[Random.Range(0, wayPoint.Count)].position);

        timer += Time.deltaTime;

        if (timer > 10)
        {
            animator.SetBool("isWalk", false);
        }

        float distance = Vector3.Distance(player.position, animator.transform.position);

        if (distance < playerDistance)
        {
            AudioManager.instance.Play("Roar");
            animator.SetBool("isRun", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.SetDestination(enemy.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
