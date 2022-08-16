using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Manager manager;

    private void Awake()
    {
        manager = FindObjectOfType<Manager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        {
            manager.Lose();
            manager.isLose = true;
        }
    }
}
