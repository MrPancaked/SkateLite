using UnityEngine;

public class StreakFade : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 3f;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Color c = sr.color;
        c.a -= fadeSpeed * Time.deltaTime;
        sr.color = c;

        if (c.a <= 0f)
            Destroy(gameObject);
    }
}
