using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_change_MapTrans : MonoBehaviour
{
    public Text text;
    // 0 : 기본 비전, 1 : 배경만
    public int vision_version = 0;

    public string imp_string0 = "Normal";
    public string imp_string1 = "Back";
    public string imp_stringVisin = "Vision";

    void Start()
    {
        vision_version = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void text_change()
    {
        if (vision_version == 0)
        {
            vision_version = 1;
            text.GetComponent<Text>().text = imp_string1 + System.Environment.NewLine + imp_stringVisin;
        }
        else if (vision_version == 1)
        {
            vision_version = 0;
            text.GetComponent<Text>().text = imp_string0 + System.Environment.NewLine + imp_stringVisin;
        }
    }


}
