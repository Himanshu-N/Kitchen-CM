using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    public static bool isFirstUpdate = true;
    // Update is called once per frame
    void Update()
    {
        if(isFirstUpdate)
        {
            isFirstUpdate = false;
            //isn't needed since scene is loaded right away

            Loader.LoaderCallback();
        }
    }
}
