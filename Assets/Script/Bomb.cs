using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float speed = 5f; // ความเร็วของบอมบ์
    public int damage = 1; // ความเสียหายที่บอมบ์ทำ

    private void Start()
    {
        // ทำลายบอมบ์หลังจาก 3 วินาที
        Destroy(gameObject, 100000f); 
    }

    private void Update()
    {
        // เคลื่อนที่บอมบ์ลงมา
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่าชนกับผู้เล่นหรือไม่
        if (collision.CompareTag("Players"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // เรียกฟังก์ชัน TakeDamage ใน PlayerHealth
            }
            Destroy(gameObject); // ทำลายบอมบ์เมื่อชนกับผู้เล่น
        }
    }
}