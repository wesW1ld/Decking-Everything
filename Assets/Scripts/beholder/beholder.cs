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

    //hitbox of line
    GameObject hitbox;
    public float width = .5f;
    Vector2 startpoint;
    Vector2 endpoint;
    public GameObject prefabHitbox;
    CapsuleCollider2D capsuleCollider;

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

        //initialize hitbox
        hitbox = Instantiate(prefabHitbox, transform.position, Quaternion.identity);
        //get collider from child
        capsuleCollider = hitbox.GetComponentInChildren<CapsuleCollider2D>();
        if (capsuleCollider == null)
        {
            Debug.LogError("CapsuleCollider component not found on the GameObject's child.");
        }

        StartCoroutine(AttackPlayer());
    }

    void Update() 
    {
        if(lazer.startColor == Color.white)
        {
            lazer.SetPosition(0, new Vector3(firepoint.position.x, firepoint.position.y, -3));
            hitbox.transform.position = transform.position;
            capsuleCollider.size = new Vector2(0, 0);
        }
    }

    IEnumerator LazerOn()
    {
        //laser
        lazer.enabled = true;
        yield return new WaitForSeconds(lazerDuration / 2);
        lazer.startColor = Color.red;
        lazer.endColor = Color.red;

        //hitbox
        startpoint = lazer.GetPosition(0);
        endpoint = lazer.GetPosition(1);
        hitbox.transform.position = (endpoint + startpoint) / 2;

        float distance = Vector2.Distance(new Vector2(startpoint.x, startpoint.y), new Vector2(endpoint.x, endpoint.y));
        capsuleCollider.size = new Vector2(distance, width);
        capsuleCollider.direction = CapsuleDirection2D.Horizontal;

        Vector3 direction = endpoint - startpoint;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        hitbox.transform.rotation = Quaternion.Euler(0, 0, angle);

        //laser
        yield return new WaitForSeconds(lazerDuration / 2);
        lazer.enabled = false;
        lazer.startColor = Color.white;
        lazer.endColor = Color.white;
    }

    IEnumerator AttackPlayer()
    {
        lazer.enabled = false;
        while(true)
        {
            yield return new WaitForSeconds(shootDelay);
            lazer.SetPosition(0, new Vector3(firepoint.position.x, firepoint.position.y, -3));
            lazer.SetPosition(1, new Vector3(player.transform.position.x, player.transform.position.y, -3));
            StartCoroutine(LazerOn());
        }
    }
}
