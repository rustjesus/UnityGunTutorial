using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100;
    private float maxHealth;
    [HideInInspector] public bool enemyGotHit = false;  
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        enemyGotHit = false;


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Enemy Health = " + health);

        if(health <= 0)
        {
            Destroy(gameObject);
        }

        if(health < maxHealth)
        {
            enemyGotHit=true;
        }
    }

}
