using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public static int currentAmmo;
    [HideInInspector] public static int currentMaxAmmo;
    [HideInInspector] public static bool isReloading = false;
    [HideInInspector] public static bool aimingDownSights = false;
    public LayerMask hitscanlayers;
    public LayerMask blockingLayer;
    // Start is called before the first frame update
    void Start()
    {
        aimingDownSights = false;
        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
