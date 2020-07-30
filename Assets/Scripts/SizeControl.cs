using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeControl : MonoBehaviour
{
    public float MinSizeAtStart = 0.8f;
    public float MaxSizeAtStart = 1.5f;
    public float SizeGrowth; // [size/sec]
    public float MinSize = 0.5f;
    public bool destructable = true;

    float size;
    Vector3 LocalScale0;
    
    public float GetSize() { return size; }
    public void SetSize(float sizeinput)
    {
        if (destructable && sizeinput < MinSize)
            Destroy(gameObject);
        else
        {
            size = Mathf.Max(MinSize, sizeinput);
            transform.localScale = size * LocalScale0;
        }
            
    }
    public void AddSize(float SizeChange) { if (size>0) SetSize(size + SizeChange); }
    

    void Start()
    {
        LocalScale0 = transform.localScale;
        if (MaxSizeAtStart > MinSizeAtStart)
            SetSize(Random.Range(MinSizeAtStart, MaxSizeAtStart));
        else if (MaxSizeAtStart == MinSizeAtStart)
            SetSize(MaxSizeAtStart);
        else
            Debug.LogError("MaxSizeAtStart should be larger than MinSizeAtStart");

    }

    
    void Update()
    {
        AddSize(SizeGrowth * Time.deltaTime); // incrimental growth of size per second
    }
}