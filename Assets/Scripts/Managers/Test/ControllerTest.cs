using UnityEngine;

public class ControllerTest : MonoBehaviour
{
  PlayerData playerData;
void Start()
{
  playerData = new PlayerData();
}
void Update()
{
  Debug.Log(DataManager.Instance.playerData.playerHp);
}
    
  void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Enemy")
    {
      //  CoreGameSignals.Instance.onLevelFailed?.Invoke();
        CoreUISignals.Instance.onUpgradePanel?.Invoke();
        
    }
  }
}
