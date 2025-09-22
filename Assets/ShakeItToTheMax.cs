using System.Collections;
using System.Data;
using UnityEngine;

public class ShakeItToTheMax : MonoBehaviour
{
    public static bool start = false;
    public AnimationCurve curve;
    public float duration = 1f;
    public PlayerController player;

    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = player.transform.position;
        startPosition.x = player.transform.position.x + 26;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            elapsedTime += Time.deltaTime;
            startPosition.x = player.transform.position.x + 26;
            yield return null;
        }
        

        transform.position = startPosition;
    }
}
