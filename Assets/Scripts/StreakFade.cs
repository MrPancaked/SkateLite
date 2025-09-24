using UnityEngine;

public class StreakFade : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 3f; // higher = faster fade
    private SpriteRenderer[] renderers;

    void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        foreach (var sr in renderers)
        {
            if (sr == null) continue;
            Color c = sr.color;
            c.a -= fadeSpeed * Time.deltaTime;
            sr.color = c;
        }

        // destroy when fully invisible
        if (renderers.Length > 0 && renderers[0].color.a <= 0f)
            Destroy(gameObject);
    }
}
