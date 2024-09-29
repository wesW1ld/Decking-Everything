using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    private beholder beholder;
    private Color[] colors;

    RigidbodyConstraints2D originalConstraints;

    private void Start() 
    {
        colors = new Color[3];
        colors[0] = Color.red;
        colors[1] = new Color(0.678f, 0.847f, 0.902f);
        colors[2] = new Color(0.5f, 0.0f, 0.5f);
        
        beholder = beholder = FindObjectOfType<beholder>();
        if (beholder == null)
        {
            Debug.LogError("ParentScript component not found in parent GameObject.");
        }
        else
        {
            Debug.Log("ParentScript component found in parent GameObject.");
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(beholder.currentColor == colors[0])
            {
                GameManager.Instance.health -= 1;
                Debug.Log("Player hit by red laser. Health: " + GameManager.Instance.health);
            }
            else if(beholder.currentColor == colors[1])
            {
                Debug.Log("Player hit by cyan laser.");
                StartCoroutine(FreezePlayer(other));
            }
            else if(beholder.currentColor == colors[2])
            {
                Debug.Log("Player hit by purple laser.");
                StartCoroutine(ReverseControls(other));
            }
        }
    }

    IEnumerator FreezePlayer(Collider2D other)
    {
        other.GetComponent<Movement>().pushback = true;
        other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(2.5f);
        other.GetComponent<Movement>().pushback = false;
    }

    IEnumerator ReverseControls(Collider2D other)
    {
        Movement movementScript = other.GetComponent<Movement>(); 
        movementScript.reverseControls = true;
        yield return new WaitForSeconds(3f);
        movementScript.reverseControls = false;
    }
}
