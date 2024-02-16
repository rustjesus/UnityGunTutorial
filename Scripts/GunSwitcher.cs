using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] guns;

    private int currentGunIndex = 0;//set the first gun to enable by def (0)

    private void Awake()
    {
        for(int i = 0; i < guns.Length; i++)
        {
            guns[i].SetActive(true);
        }
    }
    void Start()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].SetActive(false);
        }
        guns[currentGunIndex].SetActive(true);
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            guns[currentGunIndex].SetActive(false);//disable curr

            currentGunIndex = (currentGunIndex + 1) % guns.Length;//inc next

            guns[currentGunIndex].SetActive(true);//enable
        }
    }
}
