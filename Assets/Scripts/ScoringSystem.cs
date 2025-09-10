using UnityEngine;

[CreateAssetMenu(fileName = "ScoringSystem", menuName = "Scriptable Objects/ScoringSystemm")]
public class ScoringSystem : ScriptableObject
{
    public int currentScore;

    private const string saveKey = "PlayerScore"; //works like a dictionary so unity knows where to store the info

    //bc it s a scriptable object, the values remain untouched even after you exit the playmode.
    //Thus, it needs to be reset every start.
    //the scriptable object can be accessed everywhere by ScoringSystem
    public void ResetScore()
    {
        currentScore = 0;
        PlayerPrefs.SetInt(saveKey, currentScore);
        PlayerPrefs.Save();
    }

    public void AddPoints(int points)
    {
        currentScore += points;
        PlayerPrefs.SetInt(saveKey, currentScore);
        PlayerPrefs.Save();
    }

    public void LoadScore()
    {
        currentScore = PlayerPrefs.GetInt(saveKey, 0);
    }
}
