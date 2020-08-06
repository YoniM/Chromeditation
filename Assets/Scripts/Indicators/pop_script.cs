using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pop_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyThis", 1f);
    }


    void DestroyThis()
    {
        Destroy(this.gameObject);
    }

}
