using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // ความเร็วของกระสุน
    public int damage = 1; // ความเสียหายที่กระสุนทำได้

    private void Start()
    {
        // เริ่มต้นความเร็วของกระสุน
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed; // กระสุนเคลื่อนที่ไปในทิศทางที่มันหันหน้าไป
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่าชนกับศัตรู
        if (collision.CompareTag("Enemy"))
        {
            Boss enemy = collision.GetComponent<Boss>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // เรียกใช้ฟังก์ชันลดพลังชีวิต
            }
            Destroy(gameObject); // ลบกระสุนเมื่อชนกับศัตรู
        }
    }
     private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Wall"))
    {
        Debug.Log("Bullet hit wall!");
        Destroy(gameObject); // ลบกระสุนเมื่อชนกับกำแพง
    }
}
}