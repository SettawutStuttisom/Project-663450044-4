using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float moveSpeed = 2f; // ความเร็วในการเคลื่อนที่
    public float boundary = 8f; // ขอบเขตที่บอสสามารถเคลื่อนที่ได้
    public int health = 10; // พลังชีวิตของบอส
    public GameObject bombPrefab; // Prefab ของลูกระเบิด
    public Transform bombSpawnPoint; // จุดที่ลูกระเบิดจะถูกสร้างขึ้น

    private bool movingLeft = true; // ตำแหน่งการเคลื่อนที่
    private float bombDelay = 4f; // ความถี่ในการทิ้งลูกระเบิด

    private void Start()
    {
        StartCoroutine(MoveLeftAndRight()); // เริ่ม Coroutine สำหรับการเคลื่อนที่
        StartCoroutine(SpawnBombs()); // เริ่ม Coroutine สำหรับการสร้างลูกระเบิด
    }

    private IEnumerator MoveLeftAndRight()
{
    while (true)
    {
        // คำนวณตำแหน่งใหม่
        float newX = transform.position.x + (movingLeft ? -moveSpeed * Time.deltaTime : moveSpeed * Time.deltaTime);

        // ตรวจสอบว่าบอสจะออกนอกกรอบหรือไม่
        if (newX < -boundary || newX > boundary)
        {
            movingLeft = !movingLeft; // สลับทิศทางการเคลื่อนที่
        }
        else
        {
            transform.position = new Vector3(newX, transform.position.y, transform.position.z); // อัปเดตตำแหน่ง
        }

        yield return null; // รอเฟรมถัดไป
    }
}

    private IEnumerator SpawnBombs()
    {
        while (true)
        {
            Instantiate(bombPrefab, bombSpawnPoint.position, Quaternion.identity); // สร้างลูกระเบิด
            yield return new WaitForSeconds(bombDelay); // รอเวลาตามที่กำหนดก่อนสร้างลูกระเบิดอีกครั้ง
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // ลดพลังชีวิตเมื่อถูกโจมตี
        Debug.Log("Boss hit! Remaining health: " + health);

        // เช็คว่าบอสยังมีชีวิตอยู่หรือไม่
        if (health <= 0)
        {
            Die(); // เรียกใช้ฟังก์ชันตาย
        }
    }

    private void Die()
    {
        Debug.Log("Boss died!");
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Explode"); // เรียก trigger เพื่อเริ่มอนิเมชั่นระเบิด

    
        StartCoroutine(WaitForExplosion());
        
    }

    private IEnumerator WaitForExplosion()
{
    // รอเวลาของอนิเมชั่น (คุณอาจต้องตั้งค่าเวลาของอนิเมชั่นให้เหมาะสม)
    yield return new WaitForSeconds(1.5f); // เปลี่ยนเวลาตามความยาวของอนิเมชั่น

    // แสดงข้อความ Victory
    FindObjectOfType<Victory>().ShowVictoryText();

    // ลบ GameObject ของบอส
    Destroy(gameObject);
}

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Wall")) // ตรวจสอบการชนกับ wall
    {
        movingLeft = !movingLeft; // สลับทิศทางการเคลื่อนที่
    }
    else if (collision.gameObject.CompareTag("Players")) // ตรวจสอบการชนกับผู้เล่น
    {
        Debug.Log("Boss collided with player");
        // คุณสามารถเพิ่มการสร้างดาเมจที่นี่ถ้าต้องการให้บอสสร้างดาเมจให้ผู้เล่น
    }
}
}