using System.Collections;
using System.Collections.Generic;
using TMG;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Vector3 firstPosOffset;
    [SerializeField] private Vector3 secondPosOffset;
    public enum AIState { Patrol, Attack, Idle}
    public AIState currentAiState;
    private FieldOfView fieldOfView;
    [SerializeField] private float viewRadius = 30f;
    [Range(0, 360)][SerializeField] private float viewAngle = 30f;
    private bool idleStop = false;
    private bool patrolMove = false;
    [HideInInspector] public bool isAttacking = false;
    private FaceObjectTowardPlayer faceTowardPlayer;
    [SerializeField] private float stopDistanceFromPlayer = 5f;
    private EnemyHealth enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        faceTowardPlayer =GetComponentInChildren<FaceObjectTowardPlayer>();
        isAttacking = false;
        fieldOfView = GetComponent<FieldOfView>();
        fieldOfView.viewRadius = viewRadius;    
        fieldOfView.viewAngle = viewAngle;

        firstPosOffset = transform.position + firstPosOffset;
        secondPosOffset = transform.position + secondPosOffset;

        agent = GetComponent<NavMeshAgent>();

    }
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stopDistanceFromPlayer);
    }
    private void LateUpdate()
    {
        switch (currentAiState)
        {
            case AIState.Patrol:
                Patrol();
                break;
            case AIState.Attack:
                Attack();
                break;
            case AIState.Idle:
                Idle();
                break;
        }

        if(enemyHealth.enemyGotHit)
        {
            currentAiState = AIState.Attack;
        }
    }
    private void Patrol()
    {
        //move between positions
        if (!patrolMove)
        {
            //Set the destination for the agent
            agent.SetDestination(firstPosOffset);
        }
        else
        {
            //Set the destination for the agent
            agent.SetDestination(secondPosOffset);
        }

        //find each ends
        //ignoring y axis because this is a ground enemy
        if (Mathf.Abs(agent.nextPosition.z - firstPosOffset.z) <= 1 && Mathf.Abs(agent.nextPosition.x - firstPosOffset.x) <= 1)
        {
            patrolMove = true;
        }
        //ignoring y axis because this is a ground enemy
        if (Mathf.Abs(agent.nextPosition.z - secondPosOffset.z) <= 1 && Mathf.Abs(agent.nextPosition.x - secondPosOffset.x) <= 1)
        {
            patrolMove = false;
        }


        SearchForPlayer();
    }
    private void Attack()
    {
        isAttacking = true;

        if (faceTowardPlayer.playerObj != null)
        {

            if (Vector3.Distance(transform.position, faceTowardPlayer.playerObj.transform.position) > stopDistanceFromPlayer)
            {
                agent.SetDestination(faceTowardPlayer.playerObj.transform.position);
            }
            else
            {
                RotateTowardsPlayerFunction();
                agent.SetDestination(transform.position);
            }
        }



    }
    private void Idle()
    {
        SearchForPlayer();
        if(idleStop == false)
        {
            agent.SetDestination(transform.position + firstPosOffset);
            idleStop = true;    
        }
    }

    private void SearchForPlayer()
    {
        for (int i = 0; i < fieldOfView.visibleTargets.Count; i++) //if target is in field of view cone
        {
            currentAiState = AIState.Attack;
        }
    }


    private void RotateTowardsPlayerFunction()
    {
        //roate towars player
        Vector3 lookPos = faceTowardPlayer.playerObj.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
    }

}
