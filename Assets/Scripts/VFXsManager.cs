using UnityEngine;

[CreateAssetMenu(fileName = "VFXsManager", menuName = "Scriptable Objects/VFXsManager")]
public class VFXsManager : ScriptableObject
{
    public GameObject liniarTrails;

    public void CameraShake(bool start)
    {
        ShakeItToTheMax.start = start;
    }


    public void EchoEffect(bool start)
    {
        SpriteTrail.active = start;
    }

    public void LiniarTrails(bool start)
    {
        liniarTrails.SetActive(start);
    }
}
