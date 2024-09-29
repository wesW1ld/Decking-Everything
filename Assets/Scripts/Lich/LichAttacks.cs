using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichAttacks : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float range = 10f;
    public int attackNum = 1;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if(player == null)
        {
            Debug.LogError("No player found, LichAttacks");
        }

        StartCoroutine(Attack());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 pos = transform.position + new Vector3(5f, 0f, 0f);
            Instantiate(fireballPrefab, pos, Quaternion.identity);
        }
    }

    IEnumerator Attack()
    {
        while(attackNum == 1)
        {
            float diff = transform.position.x - player.transform.position.x;
            if(Mathf.Abs(diff) < range)
            {
                float randomOffset = Random.Range(0f, 5f);
                if(diff > 0)
                {
                    randomOffset *= -1;
                }
                Vector3 pos = transform.position + new Vector3(randomOffset, 5f, 0f);
                Instantiate(fireballPrefab, pos, Quaternion.identity);
            }

            yield return new WaitForSeconds(1f);
        }
        // while(attackNum == 2)
        // {
        //     Destroy(gameObject);
        //     //3 ice bolts spawned, sent at player
        // }
    }
}

//randomise attack placement
