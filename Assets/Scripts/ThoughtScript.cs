using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtScript : MonoBehaviour
{
    public Transform Aura;
    public float moverate = 1f; // [pixels/sec]
    public float movenoise = 1f; // relative to centering force (value grater than 1 would allow random movement away from center)
    public float maxturnrate = 50f; // [deg/sec]
    
    //public float turnrate_changerate = 5f; // [deg/sec]
    float minturnrate = 0.1f;
    float turnrate;
    // Start is called before the first frame update
    void Start()
    {
        Rotate(Random.Range(0,360)); // [deg]
        turnrate = (Random.Range(0,2)*2-1) * Random.Range(minturnrate, maxturnrate);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 FallDirection;
        Rotate(turnrate * Time.deltaTime);
        //turnrate += (Random.Range(0, 2) * 2 - 1) * turnrate_changerate * Time.deltaTime;
        FallDirection = Vector3.Normalize(Aura.position - transform.position);
        transform.position += (FallDirection + transform.up * movenoise )* moverate * Time.deltaTime;

    }

    private void Rotate(float turn)
    {
        transform.Rotate(Vector3.forward, turn);
    }

    public void SetAuraInstance(Transform instance)
    {
        Aura = instance;
    }

}
