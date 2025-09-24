using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager Instance;
    
    private InputManager inputManager;

    [SerializeField] private GameObject playerSpriteParent;
    
    [Header("PlayerSprites")] 
    [SerializeField] private GameObject cruisingPose;
    [SerializeField] private GameObject olliePose;
    [SerializeField] private GameObject upTrickPose;
    [SerializeField] private GameObject downTrickPose;
    [SerializeField] private GameObject leftTrickPose;
    [SerializeField] private GameObject rightTrickPose;
    [SerializeField] private GameObject upGrindPose;
    [SerializeField] private GameObject downGrindPose;
    [SerializeField] private GameObject leftGrindPose;
    [SerializeField] private GameObject rightGrindPose;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    public void UpdateTrickSprite()
    {
        switch (inputManager.trick)
        {
            case TrickDirection.Up:
            {
                SwapSprite(upTrickPose);
                break;
            }
            case TrickDirection.Down:
            {
                SwapSprite(downTrickPose);
                break;
            }
            case TrickDirection.Left:
            {
                SwapSprite(leftTrickPose);
                break;
            }
            case TrickDirection.Right:
            {
                SwapSprite(rightTrickPose);
                break;
            }
            case TrickDirection.None:
            {
                SwapSprite(olliePose);
                break;
            }
        }
    }

    public void UpdateGrindSprite()
    {
        switch (inputManager.trick)
        {
            case TrickDirection.Up:
            {
                SwapSprite(upGrindPose);
                break;
            }
            case TrickDirection.Down:
            {
                SwapSprite(downGrindPose);
                break;
            }
            case TrickDirection.Left:
            {
                SwapSprite(leftGrindPose);
                break;
            }
            case TrickDirection.Right:
            {
                SwapSprite(rightGrindPose);
                break;
            }
            case TrickDirection.None:
            {
                break;
            }
        }
    }

    public void UpdateRidingSprite()
    {
        SwapSprite(cruisingPose);
    }
    private void SwapSprite(GameObject spriteToCreate)
    {
        GameObject oldSprite = GameObject.FindGameObjectWithTag("PlayerSprite");
        GameObject newSprite = Instantiate(spriteToCreate);
        newSprite.transform.SetParent(playerSpriteParent.transform, false);

        // Copy local transform so it sits exactly where the old sprite was relative to the same parent.
        newSprite.transform.localPosition = oldSprite.transform.localPosition;
        newSprite.transform.localRotation = oldSprite.transform.localRotation;
        newSprite.transform.localScale = oldSprite.transform.localScale;
        Destroy(oldSprite);
    }
}
