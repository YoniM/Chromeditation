using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtSpawnScript : MonoBehaviour
{
    public Transform Aura;
    public Transform ThoughtPrefab;
    public Transform ThoughtHolder;
    public float SpawnRate; // thoughts per second
    public float RestTime = 2f; // [sec]
    float t0, RestTimeActual;
    public float moverate;
    int moveUp, moveLeft;
    
    
    void Start()
    {
        t0 = Time.time;
        RestTimeActual = 0;
        if (Mathf.Abs(transform.position.x) > Mathf.Abs(transform.position.y))
        {
            moveUp = Random.Range(0,2) * 2 - 1;
            moveLeft = 0;
        }
        else
        {
            moveUp = 0;
            moveLeft = Random.Range(0, 2) * 2 - 1;
        }
        
    }

    private void Update()
    {
        Transform thought;

        transform.position += moverate * (Vector3.up*moveUp + Vector3.left*moveLeft);

        if ((Random.Range(0f, 1f) < SpawnRate) && Time.time > RestTimeActual + t0)
        {
            RestTimeActual = RestTime;
            t0 = Time.time;
            thought = Instantiate(ThoughtPrefab, transform.position, Quaternion.identity, ThoughtHolder);
            thought.GetComponent<ThoughtScript>().SetAuraInstance(Aura);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpawnerBlockerIndicator indicator;
        indicator = collision.GetComponent<SpawnerBlockerIndicator>();
        if (indicator != null)
        {
            moveUp = -moveUp;
            moveLeft = -moveLeft;
        }
    }
}
