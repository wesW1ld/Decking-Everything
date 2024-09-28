using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beholder : MonoBehaviour
{
    private GameObject player;

    //eye to shoot out of
    private Transform firepoint;

    //shooting
    public float lazerDuration = 1f;
    private LineRenderer lazer;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize firepoint
        firepoint = transform.Find("firepoint1");
        if (firepoint == null)
        {
            Debug.LogError("Firepoint1 not found as a child of the GameObject.");
        }

        // Initialize player
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject with tag 'Player' not found in the scene.");
        }

        // Initialize lazer
        lazer = GetComponent<LineRenderer>();
        if (lazer == null)
        {
            Debug.LogError("LineRenderer component not found on the GameObject.");
        }

        lazer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire1 pressed");
            lazer.SetPosition(0, new Vector3(firepoint.position.x, firepoint.position.y, -3));
            lazer.SetPosition(1, new Vector3(player.transform.position.x, player.transform.position.y, -3));
            StartCoroutine(LazerShoot(lazerDuration));
        }
    }

    IEnumerator LazerShoot(float seconds)
    {
        lazer.enabled = true;
        yield return new WaitForSeconds(lazerDuration);
        lazer.enabled = false;
    }

    // void ShootLazer()
    // {
    //         RaycastHit2D hit = Physics2D.Raycast(firepoint.position, player.transform.position - firepoint.position);
    //         Draw2DRay(hit.point);
    //         StartCoroutine(LazerShoot(lazerDuration));
    // }

    // void Draw2DRay(Vector2 end)
    // {
    //     lazer.SetPosition(0, firepoint.position);
    //     lazer.SetPosition(1, end);
    // }
}
