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


  float horizontalInput = Input.GetAxis("Horizontal");
  float verticalInput = Input.GetAxis("Vertical");
  Vector3 pos=new Vector3(horizontalInput,0,verticalInput);
  transform.Translate(pos*15*Time.deltaTime);
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
