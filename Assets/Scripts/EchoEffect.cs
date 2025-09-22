using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EchoEffect : MonoBehaviour
{
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;

    public GameObject echo;
    private PlayerController player;


    void Start()
    {
        player = GetComponent<PlayerController>();
    }


    void Update()
    {
        //if (player.inputManager != 0)
    }
}
