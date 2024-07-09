using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    // event test
  void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Enemy")
    {
      //  CoreGameSignals.Instance.onLevelFailed?.Invoke();
        CoreUISignals.Instance.onUpgradePanel?.Invoke();
    }
  }
}
