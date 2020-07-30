using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtScript : MonoBehaviour
{
    public Color[] BasicColors;
    public Transform Aura;
    public float moverate = 1f; // [m/sec]
    public float fallspeed = 1f; // centering movement speed relative to randomspeed
    public float randomspeed = 1f; // random movement speed relative to fallspeed
    public float maxturnrate = 50f; // [deg/sec]

    public float swallowfactor = 0.5f; // how much of the smaller thought size is transfered to the larger size
    public float SizeGrowthAtPerfectBalance = -1f;
    public float BalanceThresholdAffectingGrowth = 0.5f;

    SizeControl sc;
    ColorControl cc;

    //public float turnrate_changerate = 5f; // [deg/sec]
    readonly float minturnrate = 0.1f;
    float turnrate;
    

    void Start()
    {
        sc = GetComponent<SizeControl>();
        cc = GetComponent<ColorControl>();


        // initiate color
        int NumberOfBasicColors = BasicColors.Length;
        cc.SetColor(BasicColors[Random.Range(0, NumberOfBasicColors)]);

        SetSizeGrowth();

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

    public void SetSizeGrowth()
    {
        //sc.SizeGrowth = SizeGrowthAtPerfectBalance * cc.ColorBalance();
        sc.SizeGrowth = SizeGrowthAtPerfectBalance * 2 * Mathf.Max(0f,(cc.ColorBalance() - BalanceThresholdAffectingGrowth));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // This deals with colliding thoughts:
        ThoughtScript other_thought;
        SizeControl other_sc;
        ColorControl other_cc;
        float othersize;

        other_thought = collision.gameObject.GetComponent<ThoughtScript>();
        if (other_thought != null)
        {
            other_sc = other_thought.GetComponent<SizeControl>();
            other_cc = other_thought.GetComponent<ColorControl>();
            othersize = other_sc.GetSize();
            if (sc.GetSize()>othersize) // i.e. if I am bigger
            {
                sc.AddSize(othersize); // larger sized thought "swallows" the othe thought

                cc.AddColor(other_cc.GetColor() * othersize * swallowfactor);

                SetSizeGrowth();

                Destroy(other_thought.gameObject);
            }       
        }
    }

    private void OnDestroy()
    {
        Debug.Log("sound pop");
    }

}
