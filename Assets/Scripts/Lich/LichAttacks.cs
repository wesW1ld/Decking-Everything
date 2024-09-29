using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichAttacks : MonoBehaviour
{
    public GameObject fireballPrefab;
    public GameObject iceBoltPrefab;
    public float range = 10f;
    public int attackNum = 1;

    private GameObject player;
    float diff;

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
            diff = transform.position.x - player.transform.position.x;
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
        while(attackNum == 2)
        {
            diff = transform.position.x - player.transform.position.x;
            if(Mathf.Abs(diff) < range)
            {
                Vector3 pos1 = transform.position + new Vector3(-5f, 3f, 0f);
                Vector3 pos2 = transform.position + new Vector3(0, 3f, 0f);
                Vector3 pos3 = transform.position + new Vector3(5f, 3f, 0f);
                //add summon animation
                Instantiate(iceBoltPrefab, pos1, Quaternion.identity);
                Instantiate(iceBoltPrefab, pos2, Quaternion.identity);
                Instantiate(iceBoltPrefab, pos3, Quaternion.identity);
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
