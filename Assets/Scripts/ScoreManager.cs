using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton for easy global access
    // references
    private InputManager inputManager;
    [SerializeField] private TrickSettings trickSettings;
    
    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI trickText;
    [SerializeField] private TextMeshProUGUI comboScoreText;
    [SerializeField] private TextMeshProUGUI multiplierText;
    
    //private stuff
    private int score;
    private List<string> tricklist = new List<string>();
    private List<int> comboScoreList = new List<int>();

    private void Awake()
    {
        // Make sure only one ScoreManager exists
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
        
        if (scoreText == null) scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        if (trickText == null) trickText = GameObject.FindGameObjectWithTag("TrickText").GetComponent<TextMeshProUGUI>();
        if (comboScoreText == null) comboScoreText = GameObject.FindGameObjectWithTag("ComboScoreText").GetComponent<TextMeshProUGUI>();
        if (multiplierText == null) multiplierText = GameObject.FindGameObjectWithTag("MultiplierText").GetComponent<TextMeshProUGUI>();
        UpdateScore();
        UpdateComboUI();
    }

    public void AddTrickToCombo()
    {
        switch (inputManager.trick)
        {
            case TrickDirection.Up:
            {
                tricklist.Add(trickSettings.upTrickName);
                comboScoreList.Add(trickSettings.upTrickScore);
                Debug.Log("UpTrick");
                break;
            }
            case TrickDirection.Down:
            {
                tricklist.Add(trickSettings.downTrickName);
                comboScoreList.Add(trickSettings.downTrickScore);
                Debug.Log("DownTrick");
                break;
            }
            case TrickDirection.Left:
            {
                tricklist.Add(trickSettings.leftTrickName);
                comboScoreList.Add(trickSettings.leftTrickScore);
                Debug.Log("LeftTrick");
                break;
            }
            case TrickDirection.Right:
            {
                tricklist.Add(trickSettings.rightTrickName);
                comboScoreList.Add(trickSettings.rightTrickScore);
                Debug.Log("RightTrick");
                break;
            }
            case TrickDirection.None:
            {
                tricklist.Add(trickSettings.noneTrickName);
                comboScoreList.Add(trickSettings.noneTrickScore);
                Debug.Log("Ollie");
                break;
            }
        }
        UpdateComboUI();
    }

    public void AddGrindToCombo()
    {
        switch (inputManager.trick)
        {
            case TrickDirection.Up:
            {
                tricklist.Add(trickSettings.upGrindName);
                comboScoreList.Add(trickSettings.upGrindScore);
                Debug.Log("UpGrind");
                break;
            }
            case TrickDirection.Down:
            {
                tricklist.Add(trickSettings.downGrindName);
                comboScoreList.Add(trickSettings.downGrindScore);
                Debug.Log("DownGrind");
                break;
            }
            case TrickDirection.Left:
            {
                tricklist.Add(trickSettings.leftGrindName);
                comboScoreList.Add(trickSettings.leftGrindScore);
                Debug.Log("LeftGrind");
                break;
            }
            case TrickDirection.Right:
            {
                tricklist.Add(trickSettings.rightGrindName);
                comboScoreList.Add(trickSettings.rightGrindScore);
                Debug.Log("RightGrind");
                break;
            }
            case TrickDirection.None:
            {
                Debug.Log("No Grind");
                break;
            }
        }
        UpdateComboUI();
    }
    public void StopCombo()
    {
        comboScoreList.Clear();
        tricklist.Clear();
        UpdateComboUI();
    }
    
    public void CalculateNewScore()
    {
        int cumulativeScore = 0;
        for (int i = 0; i < comboScoreList.Count; i++)
        {
            cumulativeScore += comboScoreList[i];
        }
        score += cumulativeScore * comboScoreList.Count; //Does not take into account any future items. Multiplier variable should be added and tracked!
        UpdateScore();
    }
    private void UpdateScore()
    {
        if (scoreText != null) scoreText.text = $"Score: {score}";
    }
    public void UpdateComboUI()
    {
        if (tricklist.Count > 0)
        {
            multiplierText.text = $"x{tricklist.Count}";
            
            trickText.text = $"";
            comboScoreText.text = $"";
            for (int i = 0; i < tricklist.Count; i++)
            {
                if (i == 0)
                {
                    trickText.text += tricklist[i];
                    comboScoreText.text += $"{comboScoreList[i]}";
                }
                else
                {
                    trickText.text += $" + {tricklist[i]}";
                    comboScoreText.text += $" + {comboScoreList[i]}";
                }
            }
        }
        else
        {
            trickText.text = $"";
            comboScoreText.text = $"";
            multiplierText.text = $"";
        }
        
    }
    public int GetScore()
    {
        return score;
    }
    public void AddScore(int points)
    {
        score += points;
        UpdateScore();
    }
}
