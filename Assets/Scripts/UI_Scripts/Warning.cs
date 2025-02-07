using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : AddBase
{
    // Start is called before the first frame update
    void Start()
    {
        setActive = false;
        InvokeRepeating("CoarseFlash", 0.0f, flashInterval);
    }
}
