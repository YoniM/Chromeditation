using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAnchor_Script : MonoBehaviour
{
    bool isHolding;
    private void Start()
    {
        isHolding = false;
        Invoke("DestroyThis", 0.5f);
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

    void DestroyThis()
    {
        if (!isHolding)
            Destroy(this.gameObject);
    }
}
