using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    public bool FreezeOn;

    // Start is called before the first frame update

    void Start()
    {
        int imp_num2 = transform.childCount;
        //FreezeOn = true;
        if (FreezeOn == false)
        {
            for (int i = 0; i < imp_num2; i++)
            {
                transform.GetChild(i).gameObject.GetComponent<enemy_control>().RelFreeze();

            }

        }

    }

    // Update is called once per frame
    /*
    void Update()
    {
        
    }
    */

    public void FreezeButton()
    {
        int imp_num = transform.childCount;



        if (FreezeOn == false)
        {
            for(int i=0; i<imp_num; i++)
            {
                transform.GetChild(i).gameObject.GetComponent<enemy_control>().MakeFreeze();
                
            }

            FreezeOn = true;
        }
        else
        {
            for (int i = 0; i < imp_num; i++)
            {
                transform.GetChild(i).gameObject.GetComponent<enemy_control>().RelFreeze();
                
            }

            FreezeOn = false;
        }
    }
}
