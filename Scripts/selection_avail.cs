using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selection_avail : MonoBehaviour
{


    public GameObject parti1;
    public GameObject parti2;

    Transform imp1;
    Transform imp2;

    //이걸 기반으로 사용 불가 상태로 만들기도 한다.
    // 1은 사용 가능한 상태, 0은 사용 불가능한 상태
    float avail_imp = 0;

    void Start()
    {
        imp1 = parti1.GetComponent<Transform>();
        imp2 = parti2.GetComponent<Transform>();
    }
    // Update is called once per frame
    /*
    void Update()
    {

    }
    */

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "dig_grid")
        {
            imp1.localScale = new Vector3(0.2f * avail_imp, 0.2f * avail_imp);
            imp2.localScale = new Vector3(0.2f * avail_imp, 0.2f * avail_imp);

        }

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "dig_grid")
        {
            imp1.localScale = new Vector3(0.2f * avail_imp, 0.2f * avail_imp);
            imp2.localScale = new Vector3(0.2f * avail_imp, 0.2f * avail_imp);

        }

    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "dig_grid")
        {
            imp1.localScale = new Vector3(0, 0);
            imp2.localScale = new Vector3(0, 0);

        }
    }

    public void setAvail(bool imp_bool)
    {
        if (imp_bool == true)
        {
            avail_imp = 1;
        }
        else
        {
            avail_imp = 0;
        }
    }

}
