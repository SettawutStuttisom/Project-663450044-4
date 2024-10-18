using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // พลังชีวิตสูงสุด
    public int health;
    public float invulnerabilityDuration = 2f; // เวลาอมตะ
    private bool isInvulnerable = false; // สถานะอมตะ
    private SpriteRenderer spriteRenderer; // ใช้สำหรับการกระพริบ

    private void Start()
    {
        health = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>(); // รับข้อมูล SpriteRenderer
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return; // หากกำลังอมตะ ให้ไม่ทำอะไร

        health -= damage; // ลดพลังชีวิต
        Debug.Log("Player hit! Remaining health: " + health);

        // ตรวจสอบว่า player ตายหรือไม่
        if (health <= 0)
        {
            Die(); // เรียกใช้ฟังก์ชันตาย
        }
        else
        {
            StartCoroutine(Invulnerability()); // เริ่ม Coroutine อมตะ
        }
    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true; // ตั้งสถานะเป็นอมตะ
        for (float i = 0; i < invulnerabilityDuration; i += 0.1f)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // กระพริบ
            yield return new WaitForSeconds(0.1f);
        }
        spriteRenderer.enabled = true; // ทำให้แน่ใจว่าผู้เล่นมองเห็นได้
        isInvulnerable = false; // ปิดสถานะอมตะ
    }

    private void Die()
    {
        Debug.Log("Player died!");
        SceneManager.LoadScene("GameOverScene");
    }
}
