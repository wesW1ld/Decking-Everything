using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.health -= 1;
            Debug.Log("Player hit by laser. Health: " + GameManager.Instance.health);
        }
    }
}
