using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteTrail : MonoBehaviour
{
    [SerializeField] private GameObject streakPrefab; 
    [SerializeField] private float spawnInterval = 0.05f;
    [SerializeField] private Color streakColor = Color.cyan;

    private List<SpriteRenderer> playerSprites = new List<SpriteRenderer>();
    public static bool active;

    void Awake()
    {
        playerSprites.AddRange(GetComponentsInChildren<SpriteRenderer>());
    }

    public void StartTrail()
    {
        if (active)
            StartCoroutine(SpawnTrail());
    }

    public void StopTrail()
    {
        active = false;
    }

    private IEnumerator SpawnTrail()
    {
        active = true;
        while (active)
        {
            GameObject streak = Instantiate(streakPrefab, transform.position, transform.rotation);

            SpriteRenderer[] streakRenderers = streak.GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < playerSprites.Count && i < streakRenderers.Length; i++)
            {
                streakRenderers[i].sprite = playerSprites[i].sprite;
                streakRenderers[i].flipX = playerSprites[i].flipX;
                streakRenderers[i].color = streakColor;
                streakRenderers[i].transform.position = playerSprites[i].transform.position;
                streakRenderers[i].transform.rotation = playerSprites[i].transform.rotation;
                streakRenderers[i].sortingLayerID = playerSprites[i].sortingLayerID;
                streakRenderers[i].sortingOrder = playerSprites[i].sortingOrder - 1; 
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
