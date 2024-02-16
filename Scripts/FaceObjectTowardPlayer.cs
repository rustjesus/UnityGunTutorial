using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceObjectTowardPlayer : MonoBehaviour
{
    [HideInInspector]public GameObject playerObj;
    private EnemyMove enemyMove;
    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        enemyMove = GetComponentInParent<EnemyMove>();
    }
    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {

        if (enemyMove.isAttacking == true)
        {
            if (playerObj != null)
            {
                transform.LookAt(playerObj.transform.position);
            }
        }
        else//if not attacking face gun forward
        {
            transform.forward = transform.parent.forward;
        }

    }
}
