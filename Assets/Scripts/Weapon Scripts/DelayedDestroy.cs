using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestroy : MonoBehaviour
{
    //variables
    [Header("Variables")]
    public float seconds = -1f;

    private void Start()
    {
        if (!Mathf.Approximately(seconds, -1f))
        {
            StartCoroutine(DelayedDestroyCoroutine(seconds));
        }
    }

    public IEnumerator DelayedDestroyCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Destroy(this.gameObject);
    }
}
