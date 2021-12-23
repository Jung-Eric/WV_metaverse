using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List_changer : MonoBehaviour
{

    public GameObject impN;
    public GameObject impU;
    public GameObject impD;

    public Sprite Simp_N;
    public Sprite Simp_U;
    public Sprite Simp_D;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //일반 버전 이미지 변환
    public void List_change_N()
    {

        impN.GetComponent<SpriteRenderer>().sprite = Simp_N;

        impN.SetActive(true);
        impU.SetActive(false);
        impD.SetActive(false);

    }

    //일반 버전 이미지 변환
    public void List_change_UP()
    {
        impU.GetComponent<SpriteRenderer>().sprite = Simp_U;
        impD.GetComponent<SpriteRenderer>().sprite = Simp_D;

        impN.SetActive(false);
        impU.SetActive(true);
        impD.SetActive(true);
    }



}
