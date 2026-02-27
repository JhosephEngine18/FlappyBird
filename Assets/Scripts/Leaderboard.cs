using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Leaderboard : MonoBehaviour
{
    [Header("Scores")]
    [SerializeField] private TextMeshProUGUI Score1;
    [SerializeField] private TextMeshProUGUI Score2;
    [SerializeField] private TextMeshProUGUI Score3;
    public void LoadMaxScores()
    {
        Score1.text = "Score: " + PlayerPrefs.GetInt("MaxScore1", 0);
        Score2.text = "Score: " + PlayerPrefs.GetInt("MaxScore2", 0);
        Score3.text = "Score: " + PlayerPrefs.GetInt("MaxScore3", 0);
    }
}