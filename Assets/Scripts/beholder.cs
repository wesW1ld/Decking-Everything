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
    public float shootDelay = 3f;

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

        StartCoroutine(AttackPlayer());
    }

    IEnumerator LazerOn()
    {
        lazer.enabled = true;
        yield return new WaitForSeconds(lazerDuration / 2);
        lazer.startColor = Color.red;
        lazer.endColor = Color.red;
        yield return new WaitForSeconds(lazerDuration / 2);
        lazer.enabled = false;
        lazer.startColor = Color.white;
        lazer.endColor = Color.white;
    }

    IEnumerator AttackPlayer()
    {
        while(true)
        {
            yield return new WaitForSeconds(shootDelay);
            lazer.SetPosition(0, new Vector3(firepoint.position.x, firepoint.position.y, -3));
            lazer.SetPosition(1, new Vector3(player.transform.position.x, player.transform.position.y, -3));
            StartCoroutine(LazerOn());
        }
    }
}
