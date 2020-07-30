using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorControl : MonoBehaviour
{
    SpriteRenderer sr;
    Color color;
    public float balance;

    public Color GetColor() { return color; }


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    public void SetColor(Color colorinput)
    {
        color = colorinput;
        Color colorshow = color;
        // saturate showcolor
        colorshow.r += (1 - GetColor().maxColorComponent);
        colorshow.g += (1 - GetColor().maxColorComponent);
        colorshow.b += (1 - GetColor().maxColorComponent);
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
        float meancolor = (GetColor().r + GetColor().g + GetColor().b) / 3;
        float varcolor = Mathf.Pow((GetColor().r - meancolor), 2) + Mathf.Pow((GetColor().g - meancolor), 2) + Mathf.Pow((GetColor().b - meancolor), 2); // the variance is not devided by 3 so as to have it normalized between 0-1
        balance = 1 - Mathf.Sqrt(varcolor);
        if (balance < 0 || balance > 1) Debug.LogError("Something is wrong!");
        return balance;
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
