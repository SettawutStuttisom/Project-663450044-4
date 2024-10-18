using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image healthBar; // Image ที่ใช้เป็น Health Bar
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>(); // ค้นหา PlayerHealth ในฉาก
    }

    private void Update()
    {
        // อัปเดต Health Bar ตามค่าพลังชีวิตของผู้เล่น
        if (playerHealth != null)
        {
            float healthPercentage = (float)playerHealth.health / playerHealth.maxHealth; // คำนวณเปอร์เซ็นต์ของพลังชีวิต
            healthBar.fillAmount = healthPercentage; // อัปเดต Health Bar
        }
    }
}