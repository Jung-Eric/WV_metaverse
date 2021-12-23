using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floating : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //bool way = true;
    }

    // Update is called once per frame
    void Update()
    {


        if(transform.localPosition.y < 0.02f)
        {

        }
        else
        {

        }

        //transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(0,0.2f,0), 0.02f);
        
    }
}
