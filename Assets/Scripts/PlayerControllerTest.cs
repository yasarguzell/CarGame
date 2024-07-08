using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    public float moveSpeed = 5f; // Hareket hýzý

    void Update()
    {
        // WASD tuþlarýyla hareket
        float horizontal = Input.GetAxis("Horizontal"); // A ve D tuþlarý
        float vertical = Input.GetAxis("Vertical"); // W ve S tuþlarý

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
}
