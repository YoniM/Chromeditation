using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraScript : MonoBehaviour
{
    public float SizeAtStart = 1.2f; // [size]
    public Color ColorAtStart = Color.white;
    public float PushCostInAuraSize = 0.05f; // [size/push]
    public float SizeGrowth = 0.05f; // [size/sec]

    float size;
    Color color;
    
    public float GetAuraSize() { return size; }
    public Color GetAuraColor() { return color; }

    Vector3 LocalScale0;

    public Transform ColorIndicator;

    // Start is called before the first frame update
    void Start()
    {
        size = SizeAtStart;
        LocalScale0 = size * transform.localScale;
        transform.localScale = LocalScale0;
    }

    // Update is called once per frame
    void Update()
    {
        size += SizeGrowth * Time.deltaTime;
        transform.localScale = size * LocalScale0;
    }

    public void AuraSizeChange(float SizeChange)
    {
        size += SizeChange;
    }

    public void SetAuraColor(Color colorinput)
    {
        color = colorinput;
    }
}
