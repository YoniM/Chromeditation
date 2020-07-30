using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorControl : MonoBehaviour
{
    SizeControl sc;
    SpriteRenderer sr;
    Color color;

    public Color GetColor() { return color; }


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sc = GetComponent<SizeControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColor(Color colorinput)
    {
        color = colorinput;
        Color colorshow = color;
        // saturate showcolor
        colorshow.r += (1 - color.maxColorComponent);
        colorshow.g += (1 - color.maxColorComponent);
        colorshow.b += (1 - color.maxColorComponent);
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        sr.color = colorshow;
    }


    /// <summary>
    /// An indicator of how balanced is the RGB color.
    /// </summary>
    /// <returns> 1 - std(RGB) ; For example if RGB = [1,0.5,0] returns 0.75 </returns>
    public float ColorBalance()
    {
        float meancolor = (color.r + color.g + color.b) / 3;
        float varcolor = Mathf.Pow((color.r - meancolor), 2) + Mathf.Pow((color.g - meancolor), 2) + Mathf.Pow((color.b - meancolor), 2);
        varcolor *= 3; // normalize between 0-1
        return 1 - Mathf.Sqrt(varcolor);
        //return 1 + varcolor - 2 * Mathf.Sqrt(varcolor);
    }

    public void AddColor(Color colorinput)
    {
        Color newcolor = (color + colorinput);
        float colormagnitude = newcolor.r + newcolor.g + newcolor.b;
        newcolor = newcolor / colormagnitude;
        SetColor(newcolor);
    }


}
