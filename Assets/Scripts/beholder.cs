using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beholder : MonoBehaviour
{
    //public float speed; //can be changed in unity editor
    private Rigidbody2D rb;

    private GameObject player;

    //eye to shoot out of
    private GameObject eye;

    //shooting
    public float lazerDuration = 1f;
    LineRenderer lazer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        lazer = GetComponent<LineRenderer>();
        eye = transform.Find("eye1").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire1 pressed");
            Shoot();
        }
    }

    public void Shoot()
    {
        Vector2 direction = player.transform.position - eye.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(eye.transform.position, direction, 100f);
        lazer.SetPosition(0, eye.transform.position);
        lazer.SetPosition(1, hit.point);
        StartCoroutine(lazerShoot(lazerDuration));
    }

    IEnumerator lazerShoot(float seconds)
    {
        lazer.enabled = true;
        yield return new WaitForSeconds(lazerDuration);
        lazer.enabled = false;
    }
}
