using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_Free : MonoBehaviour
{
   /*
    void Start()
    {

    }


    void Update()
    {
        
    }
    */

    public void ResetScene()
    {
        string imp_name = SceneManager.GetActiveScene().name;

        if(imp_name == "Demo_origin_vNEW")
        {
            SceneManager.LoadScene("Demo_origin_vNEW");
        }
        else
        {
            SceneManager.LoadScene("Demo_origin_v_Story");
        }
        
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene("TitleScene_new");
    }

}
