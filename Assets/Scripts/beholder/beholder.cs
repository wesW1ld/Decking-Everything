using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class beholder : MonoBehaviour
{
    private GameObject player;

    private Color[] colors;
    public Color currentColor;

    //eyes to shoot out of
    private Transform firepoint;
    private Transform[] eyes;

    //shooting
    public float lazerDuration = 1f;
    private LineRenderer lazer;
    public float shootDelay = 3f;
    public float range = 10f;

    //hitbox of line
    GameObject hitbox;
    public float width = .5f;
    Vector2 startpoint;
    Vector2 endpoint;
    public GameObject prefabHitbox;
    CapsuleCollider2D capsuleCollider;
    public float zPos = -5;

    //animation
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize firepoints
        eyes = new Transform[11];
        GetEyes(eyes);

        //Initialize color array, change in laser.cs if you change this
        colors = new Color[3];
        colors[0] = Color.red;
        colors[1] = new Color(0.678f, 0.847f, 0.902f);
        colors[2] = new Color(0.5f, 0.0f, 0.5f);

        // Initialize animator
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found in child o.0");
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
            if(firepoint != null)
            {
                lazer.SetPosition(0, new Vector3(firepoint.position.x, firepoint.position.y, zPos));
            }
            hitbox.transform.position = transform.position;
            capsuleCollider.size = new Vector2(0, 0);
        }
    }

    IEnumerator LazerOn()
    {
        animator.SetBool("isAtk", true);

        //laser
        lazer.enabled = true;
        lazer.sortingOrder = 3;
        yield return new WaitForSeconds(lazerDuration / 2);
        lazer.startColor = currentColor;
        lazer.endColor = currentColor;

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

        animator.SetBool("isAtk", false);

        //laser
        yield return new WaitForSeconds(lazerDuration / 2);
        lazer.enabled = false;
        lazer.startColor = Color.white;
        lazer.endColor = Color.white;
    }

    IEnumerator AttackPlayer()
    {
        lazer.enabled = false;
        //System.Random random = new System.Random();
        while(true)
        {
            //random eye based on side
            if(player.transform.position.x < transform.position.x)
            {
                firepoint = eyes[UnityEngine.Random.Range(0, 6)];//0-5
            }
            else
            {
                firepoint = eyes[UnityEngine.Random.Range(5, 11)];//5-10
            }

            //random color
            currentColor = colors[UnityEngine.Random.Range(0, 3)];

            yield return new WaitForSeconds(shootDelay);

            lazer.SetPosition(0, new Vector3(firepoint.position.x, firepoint.position.y, zPos));
            lazer.SetPosition(1, new Vector3(player.transform.position.x, player.transform.position.y, zPos));
            if(Mathf.Abs(lazer.GetPosition(1).x - lazer.GetPosition(0).x) < range)
            {
                StartCoroutine(LazerOn());
            }
        }
    }

    void GetEyes(Transform[] eyes)
    {
        for(int i = 0; i < 11; i++)
        {
            eyes[i] = transform.Find("firepoint" + (i + 1));
            if(eyes[i] == null)
            {
                Debug.LogError("firepoint" + i + " not found in the children of the GameObject.");
            }
        }
    }
}
