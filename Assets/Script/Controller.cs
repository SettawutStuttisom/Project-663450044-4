using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // ความเร็วในการเคลื่อนที่
    private Rigidbody2D rb;

    public GameObject bulletPrefab; // Prefab ของกระสุน
    public Transform firePoint; // จุดที่กระสุนจะถูกยิงออกไป
    public float fireRate = 1f; // ความถี่ในการยิง

    public AudioSource coinCollectSound;
    public AudioSource shootingSound;
    private PlayerHealth playerHealth;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // รับข้อมูล Rigidbody2D
        StartCoroutine(ShootContinuously()); // เริ่ม Coroutine สำหรับการยิงกระสุน
        playerHealth = GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // สร้าง vector สำหรับการเคลื่อนที่
        Vector2 move = new Vector2(moveX, moveY).normalized; // ปรับค่าเพื่อไม่ให้ผู้เล่นเคลื่อนที่เร็วขึ้นเมื่อกดสองปุ่มพร้อมกัน
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime); // ใช้ MovePosition แทนการ Translate

        // อัพเดทการเอียงซ้าย-ขวาของอนิเมชั่น
        if (moveX < 0)
        {
            animator.SetBool("isTiltingLeft", true);
            animator.SetBool("isTiltingRight", false);
        }
        else if (moveX > 0)
        {
            animator.SetBool("isTiltingLeft", false);
            animator.SetBool("isTiltingRight", true);
        }
        else
        {
            animator.SetBool("isTiltingLeft", false);
            animator.SetBool("isTiltingRight", false);
        }       

    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Enemy")) // ตรวจสอบการชนกับศัตรู
    {
        Debug.Log("Player collided with enemy");

        // เรียกใช้ฟังก์ชันลดพลังชีวิตใน PlayerHealth
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1); // ลดพลังชีวิตของผู้เล่น

            animator.SetTrigger("Spindmg");
        }

        // เรียกฟังก์ชัน Die ใน Enemy
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Die(); // เรียกฟังก์ชันตายใน Enemy
        }
        if (collision.gameObject.CompareTag("Coin")) // เมื่อชนกับเหรียญ
        {
            if (coinCollectSound != null)
            {
                coinCollectSound.Play(); // เล่นเสียงเก็บเหรียญ
            }

            Destroy(collision.gameObject); // ลบเหรียญออกจากฉาก
        }
    }
}

    private IEnumerator ShootContinuously()
    {
        while (true)
        {
            Shoot(); // เรียกใช้ฟังก์ชันยิงกระสุน
            yield return new WaitForSeconds(fireRate); // รอเวลาตามที่กำหนดก่อนยิงอีกครั้ง
        }
    }

    private void Shoot()
    {
        // สร้างกระสุนที่จุดยิง
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        if (shootingSound != null)
        {
            shootingSound.Play(); // เล่นเสียงทุกครั้งที่ยิง
        }
        else
        {
            Debug.LogWarning("No shooting sound assigned to AudioSource.");
        }
    }
}
