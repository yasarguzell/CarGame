using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour, IHealth
{
    public float moveSpeed = 5f; // Hareket hýzý
    public float health = 50f; // Hareket hýzý
    public bool isDead;// Hareket hýzý

    void Update()
    {
        // WASD tuþlarýyla hareket
        float horizontal = Input.GetAxis("Horizontal"); // A ve D tuþlarý
        float vertical = Input.GetAxis("Vertical"); // W ve S tuþlarý

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    //Implement interface
    public float GetCurrentHealth()
    {
        return health;
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        CheckHealth();
    }
    public void CheckHealth()
    {
        if (health > 0)
            return;

        isDead = true;
        Die();

    }
    public void Die()
    {
        //Die
    }


}
