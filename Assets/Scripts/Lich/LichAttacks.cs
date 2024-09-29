using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichAttacks : MonoBehaviour
{
    public GameObject fireballPrefab;
    public GameObject iceBoltPrefab;
    public GameObject skullPrefab;
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
                FireBall();
            }

            yield return new WaitForSeconds(.5f);
        }
        while(attackNum == 2)
        {
            diff = transform.position.x - player.transform.position.x;
            if(Mathf.Abs(diff) < range)
            {
                StartCoroutine(IceBolt());
            }
            yield return new WaitForSeconds(2.5f);
        }
        int atk = 1;
        while(attackNum == 3)
        {
            diff = transform.position.x - player.transform.position.x;
            if(Mathf.Abs(diff) < range)
            {
                Skull();
                yield return new WaitForSeconds(1f);

                if(atk == 1)
                {
                    FireBall();
                    FireBall();
                    FireBall();
                }
                else
                {
                    StartCoroutine(IceBolt());
                }

                atk *= -1;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void FireBall()
    {
        float randomOffset = Random.Range(0f, 10f);
        if(diff > 0)
        {
            randomOffset *= -1;
        }
        Vector3 pos = transform.position + new Vector3(randomOffset, 5f, 0f);
        Instantiate(fireballPrefab, pos, Quaternion.identity);
    }

    IEnumerator IceBolt()
    {
        //add summon animation
        Vector3 pos1 = transform.position + new Vector3(-5f, 3f, 0f);
        Vector3 pos2 = transform.position + new Vector3(0, 3f, 0f);
        Vector3 pos3 = transform.position + new Vector3(5f, 3f, 0f);

        GameObject ice1 = Instantiate(iceBoltPrefab, pos1, Quaternion.identity);
        ice1.GetComponent<IceBolt>().enabled = false;
        GameObject ice2 = Instantiate(iceBoltPrefab, pos2, Quaternion.identity);
        ice2.GetComponent<IceBolt>().enabled = false;
        GameObject ice3 = Instantiate(iceBoltPrefab, pos3, Quaternion.identity);
        ice3.GetComponent<IceBolt>().enabled = false;

        //rotations for animation
        Vector2 direction1 = player.transform.position - pos1;
        float angle1 = Mathf.Atan2(direction1.y, direction1.x) * Mathf.Rad2Deg;
        ice1.transform.rotation = Quaternion.Euler(0, 0, angle1 + 180);

        Vector2 direction2 = player.transform.position - pos2;
        float angle2 = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg;
        ice2.transform.rotation = Quaternion.Euler(0, 0, angle2 + 180);

        Vector2 direction3 = player.transform.position - pos3;
        float angle3 = Mathf.Atan2(direction3.y, direction3.x) * Mathf.Rad2Deg;
        ice3.transform.rotation = Quaternion.Euler(0, 0, angle3 + 180);

        yield return new WaitForSeconds(.5f);

        ice1.GetComponent<IceBolt>().enabled = true;
        ice2.GetComponent<IceBolt>().enabled = true;
        ice3.GetComponent<IceBolt>().enabled = true;
    }

    private void Skull()
    {
        Vector3 pos1 = transform.position + new Vector3(1f, -1f, 0f);
        Vector3 pos2 = transform.position + new Vector3(-1f, -1f, 0f);
        Instantiate(skullPrefab, pos1, Quaternion.identity);
        Instantiate(skullPrefab, pos2, Quaternion.identity);
    }
}
