using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera_control_vault : MonoBehaviour
{
    //0 상태에서는 valult
    //1 상태에서는 machine
    public int version = 1;

    public float mov_speed;

    public Vector3 position;

    public Vector3 target0 = new Vector3(1.21f, 0.28f, -2.24f);

    public Vector3 target1 = new Vector3(1.21f, 2.43f, -1.94f);



    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        move_camera(version);

    }

    //보드 판 움직이기
    public void move_camera(int i)
    {
        if (i == 0)
        {
            if (transform.position != target0)
            {
                transform.position = Vector3.Slerp(transform.position, target0, mov_speed);
            }

        }
        else if(i == 1)
        {
            if (transform.position != target1)
            {
                transform.position = Vector3.Slerp(transform.position, target1, mov_speed);
            }
        }
   

    }

    public void version_change()
    {
        if(version == 0)
        {
           version = 1;
        }
        else if(version == 1)
        {
            version = 0;
        }
    }

}
