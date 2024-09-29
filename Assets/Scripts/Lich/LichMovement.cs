using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 2.5f;
    private float direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(MovementDirection(2f));
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * direction, 0);
    }

    IEnumerator MovementDirection(float seconds)
    {
        while(true)
        {
            yield return new WaitForSeconds(seconds);
            direction *= -1;
        }
    }
}
