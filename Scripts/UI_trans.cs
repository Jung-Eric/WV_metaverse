using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UI_trans : MonoBehaviour
{

    public GameObject darken;

    int pointerID;

    /*
#if UNITY_EDITOR
    pointerID = -1; //PC나 유니티 상에서는 -1
#elif UNITY_IOS || UNITY_IPHONE
        pointerID = 0;  // 휴대폰이나 이외에서 터치 상에서는 0 
#endif
*/


    // Start is called before the first frame update
    
    void Start()
    {
#if UNITY_EDITOR
        pointerID = -1; //PC나 유니티 상에서는 -1
#elif UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE
        pointerID = 0;  // 휴대폰이나 이외에서 터치 상에서는 0 
#endif
    }


    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.touchCount > 0)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                cancle();
            }
        }
        */

        
        if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject(pointerID))
            {
                cancle();
            }

        }
        

    }

    public void cancle()
    {
        darken.SetActive(false);
        this.gameObject.SetActive(false);
    }



}
