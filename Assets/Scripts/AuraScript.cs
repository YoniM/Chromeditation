using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraScript : MonoBehaviour
{
    public Color ColorAtStart = Color.white;
    public float PushCost = 0.05f; // [size/push]
    public float HitCost = 0.1f; // [size/thoughtsize]

    public float ColorBalanceAffectOnSize = 5f; // positive value means that size will increase with balance of color
    public float SizeGrowthAtPerfectBalance = 0.05f;
    SizeControl sc;
    ColorControl cc;

    void Start()
    {
        sc = GetComponent<SizeControl>();
        cc = GetComponent<ColorControl>();

        cc.SetColor(ColorAtStart);
        SetSizeGrowth();
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void SetSizeGrowth()
    {
        sc.SizeGrowth = SizeGrowthAtPerfectBalance * cc.ColorBalance();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // This deals with thoughts colliding with Aura:
        ThoughtScript other_thought;
        SizeControl other_sc;
        ColorControl other_cc;
        float othersize;
        float colorbalance;
        Color newcolor;

        other_thought = collision.gameObject.GetComponent<ThoughtScript>();
        if (other_thought != null)
        {
            other_sc = other_thought.GetComponent<SizeControl>();
            other_cc = other_thought.GetComponent<ColorControl>();
            othersize = other_sc.GetSize();
            colorbalance = cc.ColorBalance();

            sc.AddSize(-othersize * HitCost); // thought that hits Aura reduces its size

            newcolor = other_cc.GetColor() * othersize;
            newcolor.a = colorbalance; // alpha of Aura would be small (i.e. more transparent) if aura is not balanced
            cc.AddColor(newcolor);

            SetSizeGrowth();

            Destroy(other_thought.gameObject);
        }
        
    }

}
