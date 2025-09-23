using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;


public class VFXsManager : MonoBehaviour
{
    public static VFXsManager instance;

    public CinemachineImpulseSource source;

    public GameObject liniarTrails;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void EchoEffect(bool start)
    {
        SpriteTrail.active = start;
    }

    public void LiniarTrails(bool start)
    {
        liniarTrails.SetActive(start);
    }

    public void Shake()
    {
        source.GenerateImpulse();
    }
}
