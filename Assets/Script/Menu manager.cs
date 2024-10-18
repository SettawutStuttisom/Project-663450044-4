using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    // ฟังก์ชันสำหรับเริ่มเกม
    public void StartGame()
    {
        // โหลด Scene หลักของเกม
        SceneManager.LoadScene("SampleScene"); // ชื่อ Scene ที่เป็นด่านแรกของเกมคุณ
    }

    // ฟังก์ชันสำหรับออกจากเกม
    public void QuitGame()
    {
        Debug.Log("Game is exiting...");
        Application.Quit(); // ใช้เมื่อเล่นเกมจริงเท่านั้น
    }
}
