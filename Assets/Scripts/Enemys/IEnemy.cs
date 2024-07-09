using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy 
{
    void Patrol();
    void Chase();
    void Attack();
}
