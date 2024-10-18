using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFadeIn : MonoBehaviour
{
    public float fadeInDuration = 1f;  // ระยะเวลาที่ใช้ในการค่อย ๆ ปรากฏ
    private SpriteRenderer spriteRenderer;
    private Color targetColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetColor = spriteRenderer.color;
        targetColor.a = 0f;  // เริ่มต้นที่ความโปร่งใสเต็ม (ไม่เห็น)
        spriteRenderer.color = targetColor;

        // เริ่ม Coroutine เพื่อค่อย ๆ เพิ่มค่าความโปร่งใส
        StartCoroutine(FadeIn());
    }

    System.Collections.IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration); // ค่อย ๆ เพิ่ม alpha จาก 0 ถึง 1
            targetColor.a = alpha;
            spriteRenderer.color = targetColor;
            yield return null;
        }

        // ตั้งค่าให้ศัตรูกลับมาที่ alpha = 1 เต็ม
        targetColor.a = 1f;
        spriteRenderer.color = targetColor;
    }
}