using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damageToDo = 25f;
    [SerializeField] private float returnTime = 3f;
    private float timer;
    [SerializeField] private bool destroyOnAnything = true;
    private Vector3 originalScale;
    private Quaternion originalRotation;
    private float startReturnTime;
    [SerializeField] private bool isPlayers = false;

    private void Awake()
    {
        startReturnTime = returnTime;    
        originalScale = transform.localScale;
        originalRotation = transform.rotation;  
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > returnTime)
        {
            gameObject.SetActive(false);    
            timer = 0;
        }
    }

    private void OnDisable()
    {
        returnTime = startReturnTime;
        timer = 0;
        transform.position = Vector3.zero;
        gameObject.transform.localScale = originalScale;
        transform.rotation = originalRotation;

        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(isPlayers)
        {
            if (collision.collider.GetComponent<EnemyHealth>() != null)
            {
                EnemyHealth eh = collision.collider.GetComponent<EnemyHealth>();
                eh.health -= damageToDo;
                gameObject.SetActive(false);
            }

        }
        else//is not players (enemys)
        {
            if (collision.collider.GetComponent<PlayerHealth>() != null)
            {
                PlayerHealth eh = collision.collider.GetComponent<PlayerHealth>();
                eh.health -= damageToDo;
                gameObject.SetActive(false);
            }
        }



        if (destroyOnAnything)
        {
            gameObject.SetActive(false);
        }

    }
}
