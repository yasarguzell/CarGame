using UnityEngine;
using UnityEngine.UI;
public enum UIEventSubscriptionTypes
{
    Play, Quit, Pause, Resume, Restart, FailTest, UpgradeTest, Upgrade1, Upgrade2, Upgrade3
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
            case UIEventSubscriptionTypes.Pause:
                button.onClick.AddListener(_manager.GamePause);
                break;
            case UIEventSubscriptionTypes.Resume:
                button.onClick.AddListener(_manager.GameResume);
                break;
            case UIEventSubscriptionTypes.Restart:
                button.onClick.AddListener(_manager.GameRestart);
                break;
            case UIEventSubscriptionTypes.Upgrade1:
                button.onClick.AddListener(_manager.GameUpgradeOne);
                break;
            case UIEventSubscriptionTypes.Upgrade2:
                button.onClick.AddListener(_manager.GameUpgradeTwo);
                break;
            case UIEventSubscriptionTypes.Upgrade3:
                button.onClick.AddListener(_manager.GameUpgradeThree);
                break;



            // test buttons
            case UIEventSubscriptionTypes.FailTest: // player dead signals CoreGameSignals.onLevelFailed
                button.onClick.AddListener(_manager.GameFail);
                break;
            case UIEventSubscriptionTypes.UpgradeTest: // player collider upgrade signals CoreGameuuıSignals.onPlayerUpgrade
                button.onClick.AddListener(_manager.GameUpgrade);
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
            case UIEventSubscriptionTypes.Resume:
                button.onClick.RemoveListener(_manager.GameResume);
                break;
            case UIEventSubscriptionTypes.Restart:
                button.onClick.RemoveListener(_manager.GameRestart);
                break;
            case UIEventSubscriptionTypes.Upgrade1:
                button.onClick.RemoveListener(_manager.GameUpgradeOne);
                break;
            case UIEventSubscriptionTypes.Upgrade2:
                button.onClick.RemoveListener(_manager.GameUpgradeTwo);
                break;
            case UIEventSubscriptionTypes.Upgrade3:
                button.onClick.RemoveListener(_manager.GameUpgradeThree);
                break;


        }
    }

}
