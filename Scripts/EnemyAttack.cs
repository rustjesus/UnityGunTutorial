using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private bool useProjectileGavity = true;
    [SerializeField] private float rangeProjectileSpeed = 100f;
    private float timer;
    [SerializeField] private float fireRate = 1f;
    private EnemyMove enemyMove;
    [SerializeField] private GameObject projectile;
    private GameObject ammoPool;
    private ObjectPool objectPool;
    void Start()
    {
        ammoPool = new GameObject();//spawns
        objectPool = ammoPool.AddComponent<ObjectPool>();//adds script
        objectPool.prefab = projectile;//set pool prefab
        objectPool.poolSize = 10; //set size of pool
        objectPool.gameObject.name = "Enemy AmmoPool";

        enemyMove = GetComponentInParent<EnemyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyMove.isAttacking)
        {
            timer += Time.deltaTime;

            if (timer > fireRate)
            {
                //do attack logic
                CreateProjectile();
                timer = 0;
            }
        }
    }

    private void CreateProjectile()
    {
        GameObject proj = objectPool.GetObject();
        proj.transform.position = transform.position;
        proj.transform.rotation = Quaternion.LookRotation(transform.forward);

        //GameObject proj = Instantiate(proj, transform.position, Quaternion.identity);

        Rigidbody rb = proj.GetComponent<Rigidbody>();

        if (useProjectileGavity)
        {
            rb.useGravity = true;
        }
        else
        {
            rb.useGravity = false;
        }

        rb.AddForce(transform.forward * rangeProjectileSpeed + gameObject.GetComponentInParent<Rigidbody>().velocity, ForceMode.Force);
    }
}
