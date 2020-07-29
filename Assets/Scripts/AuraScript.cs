using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraScript : MonoBehaviour
{
    public float SizeAtStart = 1.2f; // [size]
    public Color ColorAtStart = Color.white;
    public float PushCostInAuraSize = 0.05f; // [size/push]
    public float SizeGrowth = 0.05f; // [size/sec]

    float SizeGrowth0;
    float size;
    Color color;
    SpriteRenderer sr;

    public float GetSize() { return size; }
    public void SetSize(float sizeinput) { size = sizeinput; }

    public Color GetColor() { return color; }
    public void SetColor(Color colorinput)
    {
        color = colorinput;
        Color colorshow = color;
        colorshow.r += (1 - color.maxColorComponent);
        colorshow.g += (1 - color.maxColorComponent);
        colorshow.b += (1 - color.maxColorComponent);
        colorshow.a = ColorBalance();
        SizeGrowth = SizeGrowth0 * ColorBalance();
        Debug.Log(SizeGrowth);
        sr.color = colorshow;
    }

    public float ColorBalance()
    {
        float meancolor = (color.r + color.g + color.b)/3;
        float varcolor = Mathf.Pow((color.r - meancolor), 2) + Mathf.Pow((color.g - meancolor), 2) + Mathf.Pow((color.b - meancolor), 2);
        return 1-Mathf.Sqrt(varcolor);
    }

    Vector3 LocalScale0;

    public Transform ColorIndicator;

    // Start is called before the first frame update
    void Start()
    {
        SizeGrowth0 = SizeGrowth;
        sr = GetComponent<SpriteRenderer>();
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

    public void AddColor(Color colorinput)
    {
        Color newcolor = (color + colorinput);
        float colormagnitude = newcolor.r + newcolor.g + newcolor.b;
        newcolor = newcolor / colormagnitude;
        SetColor(newcolor);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ThoughtScript thought;
        thought = collision.gameObject.GetComponent<ThoughtScript>();
        if (thought != null)
        {
            AddColor(thought.GetColor() * thought.GetSize());
            Destroy(thought.gameObject);
        }
        
    }
}
