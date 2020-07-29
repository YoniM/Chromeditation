using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtScript : MonoBehaviour
{
    public float SizeAtStart = 1f;
    public Color[] BasicColors;
    public Transform Aura;
    public float moverate = 1f; // [m/sec]
    public float fallspeed = 1f; // centering movement speed relative to randomspeed
    public float randomspeed = 1f; // random movement speed relative to fallspeed
    public float maxturnrate = 50f; // [deg/sec]

    float size;
    Color color;
    SpriteRenderer sr;
    public float GetSize() { return size; }
    public void SetSize(float sizeinput) { size = sizeinput; }

    public Color GetColor() { return color; }
    public void SetColor(Color colorinput) { color = colorinput; sr.color = color; }


    //public float turnrate_changerate = 5f; // [deg/sec]
    float minturnrate = 0.1f;
    float turnrate;
    // Start is called before the first frame update
    void Start()
    {
        // initiate size:
        size = SizeAtStart;
        transform.localScale = size * transform.localScale;

        // initiate color
        sr = GetComponent<SpriteRenderer>();
        int NumberOfBasicColors = BasicColors.Length;
        SetColor(BasicColors[Random.Range(0, NumberOfBasicColors)]);
        

        // initiate rotation and movement
        Rotate(Random.Range(0,360)); // [deg]
        turnrate = (Random.Range(0,2)*2-1) * Random.Range(minturnrate, maxturnrate) * Time.deltaTime;
        moverate = moverate * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 FallDirection;
        Rotate(turnrate);
        //turnrate += (Random.Range(0, 2) * 2 - 1) * turnrate_changerate * Time.deltaTime;
        FallDirection = Vector3.Normalize(Aura.position - transform.position);
        transform.position += (FallDirection * fallspeed + transform.up * randomspeed) * moverate;

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
