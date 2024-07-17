using UnityEngine;
using UnityEngine.UI;
public enum UIEventSubscriptionTypes
{
    Play, Quit, Pause, Resume, Restart,MainMenu
}
public class UIEventSubscriber : MonoBehaviour
{

    [SerializeField] private UIEventSubscriptionTypes type;
    [SerializeField] Button button;
    private UIManager _manager;

    void Awake()
    {
        GetReferences();
    }

    private void GetReferences()
    {
        _manager = FindObjectOfType<UIManager>();
    }

    void OnEnable()
    {
        SubscribeEvents();
    }

    void OnDisable()
    {
        UnsubscribeEvents();
    }

    void SubscribeEvents()
    {
        switch (type)
        {
            case UIEventSubscriptionTypes.Play:
                button.onClick.AddListener(_manager.GameStart);
                break;
            case UIEventSubscriptionTypes.Quit:
                button.onClick.AddListener(_manager.GameQuit);
                break;
            case UIEventSubscriptionTypes.Pause:
                button.onClick.AddListener(_manager.GamePause);
                break;
            case UIEventSubscriptionTypes.Resume:
                button.onClick.AddListener(_manager.GameResume);
                break;
            case UIEventSubscriptionTypes.Restart:
                button.onClick.AddListener(_manager.GameRestart);
                break;
            case UIEventSubscriptionTypes.MainMenu:
                   button.onClick.AddListener(_manager.GameMainMenu); 
                break;


        }
    }

    void UnsubscribeEvents()
    {
        switch (type)
        {
            case UIEventSubscriptionTypes.Play:
                button.onClick.RemoveListener(_manager.GameStart);
                break;
            case UIEventSubscriptionTypes.Pause:
                button.onClick.RemoveListener(_manager.GamePause);
                break;
            case UIEventSubscriptionTypes.Quit:
                button.onClick.RemoveListener(_manager.GameQuit);
                break;
            case UIEventSubscriptionTypes.Resume:
                button.onClick.RemoveListener(_manager.GameResume);
                break;
            case UIEventSubscriptionTypes.Restart:
                button.onClick.RemoveListener(_manager.GameRestart);
                break;
            case UIEventSubscriptionTypes.MainMenu:
                button.onClick.RemoveListener(_manager.GameMainMenu);
                break;

        }
    }

}
