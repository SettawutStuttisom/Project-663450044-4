using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f; // ความเร็วในการเคลื่อนที่
    public GameObject dropItemPrefab; // ไอเท็มที่จะดรอปเมื่อศัตรูถูกฆ่า

    public int health = 3;

    private bool movingLeft = true; // ตำแหน่งการเคลื่อนที่
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // รับข้อมูล Rigidbody2D
    }

    private void Update()
    {
        Move(); // เรียกฟังก์ชันเคลื่อนที่
        // เคลื่อนที่ลงทุกเฟรม
        transform.position += new Vector3(0, -0.1f * Time.deltaTime, 0);
    }

    private void Move()
    {
        float moveDirection = movingLeft ? -moveSpeed : moveSpeed;

        // ตรวจสอบการชนกันกับ Wall
        Vector2 newVelocity = new Vector2(moveDirection, rb.velocity.y);
        rb.velocity = newVelocity;

        // เปลี่ยนทิศทางเมื่อชนกรอบ
        if (transform.position.x < -8f || transform.position.x > 8f) // ขอบเขต
        {
            movingLeft = !movingLeft; // สลับทิศทาง
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ตรวจสอบว่าชนกับ Wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Collided with wall"); // ตรวจสอบว่าชน
            movingLeft = !movingLeft; // เปลี่ยนทิศทาง
            // ปรับตำแหน่งศัตรูกลับเพื่อไม่ให้ทะลุ
            if (movingLeft)
            {
                transform.position += new Vector3(0.5f, 0, 0); // ย้ายไปขวา
            }
            else
            {
                transform.position += new Vector3(-0.5f, 0, 0); // ย้ายไปซ้าย
            }
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
        TakeDamage(1); // ลดพลังชีวิต 1 เมื่อถูกกระสุน
        Destroy(collision.gameObject); // ทำลายกระสุนเมื่อชนศัตรู
        }
        
    }

    public void TakeDamage(int damage)
{
    health -= damage; // ลดพลังชีวิตตามดาเมจที่ได้รับ
    Debug.Log("Enemy hit! Remaining health: " + health);

    if (health <= 0)
    {
        Die(); // เรียกฟังก์ชันตายเมื่อพลังชีวิตหมด
    }
}
    public void Die()
    {
        DropItem();
        Debug.Log("Enemy died!");
        Destroy(gameObject); // ลบ GameObject นี้ออกจากฉาก
    }
     private void DropItem()
    {
        if (dropItemPrefab != null)
        {
            Instantiate(dropItemPrefab, transform.position, Quaternion.identity); // ดรอปไอเท็มที่ตำแหน่งของศัตรู
        }
    }


}