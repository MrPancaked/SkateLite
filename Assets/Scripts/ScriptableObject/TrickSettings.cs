using UnityEngine;

[CreateAssetMenu(fileName = "TrickSettings", menuName = "Scriptable Objects/TrickSettings")]
public class TrickSettings : ScriptableObject
{
    [Header("Trick Names")] 
    public string upTrickName;
    public string downTrickName;
    public string leftTrickName;
    public string rightTrickName;
    public string noneTrickName;
    [Header("Grind Names")]
    public string upGrindName;
    public string downGrindName;
    public string leftGrindName;
    public string rightGrindName;
    
    [Header("Trick Scores")]
    public int upTrickScore;
    public int downTrickScore;
    public int leftTrickScore;
    public int rightTrickScore;
    public int noneTrickScore;
    [Header("Grind Scores")]
    public int upGrindScore;
    public int downGrindScore;
    public int leftGrindScore;
    public int rightGrindScore;
}
