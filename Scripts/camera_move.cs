using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_move : MonoBehaviour
{
    //0 상태에서는 valult
    //1 상태에서는 machine
    public int version = 1;

    public float mov_speed = 100;

    public Vector3 position;
    public Vector3 imp_pos;

    public Vector3 target0 = new Vector3(1.21f, 0.28f, -2.24f);

    public Vector3 target1 = new Vector3(1.21f, 2.43f, -1.94f);

    public GameObject mach_on;
    public GameObject mach_off;

    public GameObject detection;

    public float fade_time = 400.0f;

    public GameObject audio_wake;
    public GameObject audio_success;

    public GameObject success_item;

    public float x_pos = 0.93f;
    public float y_pos = 2.17f;

    public float x_det_pos = 0.81f;
    public float y_det_pos = -1.47f;

    public int x_corr = 0;
    public int y_corr = 0;

    //public float imp_color_a = 0.0f;
    //public GameObject detection_On;
    //public GameObject detection_Off;

    //public GameObject imp_image;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Color imp_color = detection.GetComponent<SpriteRenderer>().color;


        if (imp_color.a > 0)
        {
            imp_color.a -= Time.deltaTime * fade_time;
        }

        detection.GetComponent<SpriteRenderer>().color = imp_color;

        if (Input.GetMouseButtonDown(0))
        {
            //StartCoroutine("Detection");
            Detection();

            mach_on.SetActive(true);
            mach_off.SetActive(false);

            //성공 조건
            Vector3 imp_vec3 = transform.position;

            Vector3 det_pos_imp;

            det_pos_imp = detection.transform.localPosition;

            //x축
            if(imp_vec3.x < 0.93f - 0.4f)
            {
                det_pos_imp.x = x_det_pos - 0.1f;
            }
            else if (imp_vec3.x > 0.93f + 0.4f)
            {
                det_pos_imp.x = x_det_pos + 0.1f;
            }
            else
            {
                det_pos_imp.x = x_det_pos;
            }

            //y축
            if (imp_vec3.y < 2.17f - 0.4f)
            {
                det_pos_imp.y = y_det_pos - 0.05f;
            }
            else if (imp_vec3.y > 2.17f + 0.4f)
            {
                det_pos_imp.y = y_det_pos + 0.05f;
            }
            else
            {
                det_pos_imp.y = y_det_pos;
            }


            detection.transform.localPosition = new Vector3(det_pos_imp.x, det_pos_imp.y, 0);

            //조건에 맞으면
            if ( ( (imp_vec3.x > 0.93f - 0.4f) && (imp_vec3.x < 0.93f + 0.4f)) && ((imp_vec3.y > 2.17f - 0.4f) && (imp_vec3.y < 2.17f + 0.4f)))
            {
                audio_success.SetActive(true);

                success_item.SetActive(true);

            }
            else
            {
                audio_wake.SetActive(true);
            }

 


        }
        if(Input.GetMouseButtonUp(0))
        {
            mach_on.SetActive(false);
            mach_off.SetActive(true);

            audio_wake.SetActive(false);

        }


        position.x += mov_speed * Input.GetAxisRaw("Horizontal");

        position.y += mov_speed * Input.GetAxisRaw("Vertical");


        transform.position = position;
    }

    //보드 판 움직이기
    /*
    public void move_camera(int i)
    {
        if (i == 0)
        {
            if (transform.position != target0)
            {
                transform.position = Vector3.Slerp(transform.position, target0, mov_speed);
            }

        }
        else if (i == 1)
        {
            if (transform.position != target1)
            {
                transform.position = Vector3.Slerp(transform.position, target1, mov_speed);
            }
        }


    }
    */

    public void version_change()
    {
        if (version == 0)
        {
            version = 1;
        }
        else if (version == 1)
        {
            version = 0;
        }
    }

    void Detection()
    {

        detection.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);

        detection.transform.localScale = new Vector3(0.1f, 0.1f, 1);

    }


}
