using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource menuMusic; // Ana menü müziği için AudioSource
    public GameObject gameContent; // Oyunun kendisi

    private void Start()
    {
        menuMusic.Play();
    }

    public void StartGame()
    {
        menuMusic.Stop();
        gameContent.SetActive(true); // Oyunun başlamasını sağlayan işlemler
    }

    public void ReturnToMenu()
    {
        menuMusic.Play();
        gameContent.SetActive(false); // Oyundan çıkıp ana menüye dönüş işlemleri
    }
}