using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wold_foot_slopeF : MonoBehaviour
{
    public GameObject player;


    // Start is called before the first frame update
    /*
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */

    private void OnTriggerExit2D(Collider2D col)
    {

        if (col.gameObject.tag == "slope")
        {
            player.GetComponent<wolf_control>().is_slope_R = false;

        }

    }


    //경사로 진입 판정
    private void OnTriggerEnter2D(Collider2D col)
    {


        if (col.gameObject.tag == "slope")
        {
            player.GetComponent<wolf_control>().is_slope_R = true;

        }

    }

    //추가 유지 판정
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "slope")
        {
            player.GetComponent<wolf_control>().is_slope_R = true;

        }

    }

}
