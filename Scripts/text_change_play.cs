using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text_change_play : MonoBehaviour
{
    public Text text;
    // 0 : 맵 에딧 중. 1 : 플레이 중, 2 : 대화 중(연출)
    public int play_version = 0;

    public string imp_string0 = "Play!";
    public string imp_string1 = "Edit";

    void Start()
    {
        play_version = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void text_change()
    {
        if(play_version == 0)
        {
            play_version = 1;
            
            text.GetComponent<Text>().text = imp_string1;
        }
        else if(play_version == 1)
        {
            play_version = 0;
            
            text.GetComponent<Text>().text = imp_string0;
        }
    }

}
