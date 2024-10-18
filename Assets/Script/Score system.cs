using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // สำหรับการสลับ Scene

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public int scoreToBoss = 1000; // กำหนดคะแนนที่ต้องถึงเพื่อเข้าสู่ฉากบอส
    public string bossSceneName = "BossScene"; // ชื่อ Scene ของบอส

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();

        // ตรวจสอบว่าคะแนนถึงเป้าหมายหรือยัง
        if (score >= scoreToBoss)
        {
            GoToBossScene();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private void GoToBossScene()
    {
        // สลับไปยัง Scene บอส
        SceneManager.LoadScene(bossSceneName);
    }
}