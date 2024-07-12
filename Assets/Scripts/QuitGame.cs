using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Bu fonksiyon butona tıklanınca çalışacak
    public void Quit()
    {
        // Unity Editor'de çalışırken uygulamayı durdurmak için
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Gerçek bir build'de oyunu kapatmak için
        Application.Quit();
#endif
    }
}
