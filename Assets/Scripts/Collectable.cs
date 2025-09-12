using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    [Header("Collectable Settings")]

    [SerializeField] private int points = 100;

    public enum item{ collectable, speedBooster };
    //it can be assigned to multiple collectable items.
    //based on the enum, it will have a different funciton on trigger enter
    private item col;

    [System.Serializable]
    public class IntEvent : UnityEvent<int> { }

    [Header("Events")]
    public IntEvent onCollectedWithPoints;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            if (col == item.collectable)
            {
                ScoreManager.Instance.AddScore(points);
                onCollectedWithPoints.Invoke(points);
            }
                //else


        }
    }
}
