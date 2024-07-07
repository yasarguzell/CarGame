using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum UIEventSubscriptionTypes
{
    Play,Quit,Pause
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
                button.onClick.AddListener(GameManager.Instance.GameStart); 
                break;
            case UIEventSubscriptionTypes.Pause:
                button.onClick.AddListener(GameManager.Instance.GamePause);
                break;
          
        }
    }

    void UnsubscribeEvents()
    {
        switch (type)
        {
            case UIEventSubscriptionTypes.Play:
                button.onClick.RemoveListener(GameManager.Instance.GameStart);
                break;
            case UIEventSubscriptionTypes.Pause:
                button.onClick.RemoveListener(GameManager.Instance.GamePause);
                break;
           
        }
    }

}
