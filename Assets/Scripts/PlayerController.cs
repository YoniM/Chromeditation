using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isHolding;
    CircleCollider2D col;

    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        col.isTrigger = false;

        isHolding = false;
        //Invoke("ToggleTrigger", 0.5f);
    }

    void Update()
    {
        if (TouchManager.Instance.Tap)
        {
            Debug.Log(TouchManager.Instance.StartTouch);
            transform.position = new Vector3(TouchManager.Instance.StartTouch.x, TouchManager.Instance.StartTouch.y, 0f);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ThoughtScript thought;
        Rigidbody2D rb;
        thought = collision.GetComponent<ThoughtScript>();
        if (thought != null)
        {
            isHolding = true;
            // move thought in direction of anchor:
            Vector3 delta = collision.transform.position - transform.position;
            rb = collision.GetComponent<Rigidbody2D>();
            //rb.AddForce(new Vector2(delta.x, delta.y));
            rb.AddForce(delta);
        }
    }

    void ToggleTrigger()
    {
        col.isTrigger = !col.isTrigger;
    }
}
