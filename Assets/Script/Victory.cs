using UnityEngine;
using UnityEngine.UI;

public class Victory : MonoBehaviour
{
    public AudioSource victoryAudio;
    public GameObject victoryText; 

    public GameObject PressEsctoleave;
    public AudioSource backgroundMusic;
    private void Start()
    {
        victoryText.SetActive(false); 
        PressEsctoleave.SetActive(false);
    }
    
    public void ShowVictoryText()
    {
        victoryText.SetActive(true); 
        if (victoryAudio != null)
        {
            victoryAudio.Play();
        }

        PressEsctoleave.SetActive(true); 
    

        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }
    }
}