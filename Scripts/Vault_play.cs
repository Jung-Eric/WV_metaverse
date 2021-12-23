using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vault_play : MonoBehaviour
{

    //0이면 미선택
    //1 청색 일반, 2 청색 세부
    //3 적색 일반, 4 적색 세부
    public bool play_avail = true;
    int control_version = 0;

    // Start is called before the first frame update
    public float blue_rot_all = 0f;
    public float blue_rot = 0f;
    public float blue_rot2 = 0f;

    public Vector2 blue_anchor = new Vector2(0, 0);
    public Vector2 blue_anchor2 = new Vector2(0, 0);
    //public int blue_selected = 0;

    public float red_rot_all = 0f;
    public float red_rot = 0f;
    public float red_rot2 = 0f;

    public Vector2 red_anchor = new Vector2(0, 0);
    public Vector2 red_anchor2 = new Vector2(0, 0);
    //public int red_selected = 0;

    public float rot_speed = 1.2f;
    public float rot_sub_speed = 1f;
    Vector2 touchPos;

    //성공 범위
    public float blue_rot_success = -3578.0f;
    public float red_rot_success = 1624.0f;

 

    //----------------------------------------------------
    public GameObject blue_controller;
    Transform bc;
    public GameObject blue_controller_small;
    Transform bc_s;
    public GameObject blue_controller_small_shade;
    Transform bc_ss;
    //----------------------------------------------------
    public GameObject red_controller;
    Transform rc;
    public GameObject red_controller_small;
    Transform rc_s;
    public GameObject red_controller_small_shade;
    Transform rc_ss;
    //---------------------------------------------------
    public GameObject reset_work;
    public GameObject success_work;
    public GameObject return_machine;

    public GameObject return_menu_1;
    //public GameObject return_menu_2;
    public GameObject for_camera_move;

    //효과음 변경
    public GameObject audio_base;
    public GameObject audio_success;

    //일반 금고, 성공 금고
    public GameObject vault_base;
    public GameObject vault_success;

    float first_x = 0f;
    float first_y = 0f;
    //회전
    float first_rot_z = 0f;
    //좌표에 따른 계수 조정
    float first_x_cal = 0f;
    float first_y_cal = 0f;

    float last_x = 0f;
    float last_y = 0f;

    //자동 회전 용도
    Quaternion rotation_imp;

    //진동 빈도
    float vibrate_cycle = 1.0f;
    float vibrate_timer = 0;
    

    void Start()
    {

        
        //touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);
        bc = blue_controller.transform;
        bc_s = blue_controller_small.transform;
        bc_ss = blue_controller_small_shade.transform;

        rc = red_controller.transform;
        rc_s = red_controller_small.transform;
        rc_ss = red_controller_small_shade.transform;

        play_avail = true;

        rotation_imp = Quaternion.Euler(0, 0, 0);

        //작업 목적 좌표계
        Vector3 start_position = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 end_position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

      
        blue_anchor.x = ((140 * Screen.width) / 641);
        blue_anchor.y = ((139  * Screen.height) / 320);

        blue_anchor2.x = ((244 * Screen.width) / 641);
        blue_anchor2.y = ((102 * Screen.height) / 320);

        red_anchor.x = ((375 * Screen.width) / 641);
        red_anchor.y = ((139 * Screen.height) / 320);

        red_anchor2.x = ((479 * Screen.width) / 641);
        red_anchor2.y = ((102 * Screen.height) / 320);

        rot_sub_speed = (Screen.width / 641);

    }

    // Update is called once per frame
    void Update()
    {
        //진동 조건 변경
        if ((blue_rot_all < blue_rot_success + 100 && blue_rot_all > blue_rot_success - 100) && (red_rot_all < red_rot_success + 100 && red_rot_all > red_rot_success - 100))
        {
            vibrate_cycle = 3.0f;
        }
        else if ((blue_rot_all < blue_rot_success+300 && blue_rot_all > blue_rot_success-300) && (red_rot_all < red_rot_success+300 && red_rot_all > red_rot_success-300))
        {
            vibrate_cycle = 1.0f;
        }
        else
        {
            vibrate_cycle = 0.3f;
        }


        //-----------------------------------------------------------------------
        Vector2 wp = new Vector2();

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 M_point;
            M_point = Input.mousePosition;

            //카메라 위치 받기
            float camera_z = Camera.main.transform.position.z * (-1);
            //이러한 작업에서 꼭 음수를 취해주자
            wp = Camera.main.ScreenToWorldPoint(new Vector3(M_point.x, M_point.y, camera_z));

            //Debug.Log(wp);
            Ray2D ray = new Ray2D(wp, Vector2.zero);
            RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);
            //Debug.Log(rayHit.collider);

            float work_x = 0;
            float work_y = 0;

            //call_vibe();

            if (rayHit.collider != null)
            {
                //Debug.Log("충돌 발생");
                //Debug.Log(Input.mousePosition.x);
                //Debug.Log(Input.mousePosition.y);

                if (rayHit.collider.gameObject == blue_controller)
                {
                    //Debug.Log("충돌 발생");
                    first_x = Input.mousePosition.x;
                    first_y = Input.mousePosition.y;

                    work_x = first_x - blue_anchor.x;
                    work_y = first_y - blue_anchor.y;

                    control_version = 1;
                }
                else if (rayHit.collider.gameObject == blue_controller_small)
                {
                    first_x = Input.mousePosition.x;
                    first_y = Input.mousePosition.y;

                    work_x = first_x - blue_anchor2.x;
                    work_y = first_y - blue_anchor2.y;

                    control_version = 2;
                }
                else if (rayHit.collider.gameObject == red_controller)
                {
                    first_x = Input.mousePosition.x;
                    first_y = Input.mousePosition.y;

                    work_x = first_x - red_anchor.x;
                    work_y = first_y - red_anchor.y;

                    control_version = 3;
                }
                else if (rayHit.collider.gameObject == red_controller_small)
                {
                    first_x = Input.mousePosition.x;
                    first_y = Input.mousePosition.y;

                    work_x = first_x - red_anchor2.x;
                    work_y = first_y - red_anchor2.y;

                    control_version = 4;
                }
                else if (rayHit.collider.gameObject == reset_work)
                {
                    blue_rot = 0;
                    blue_rot2 = 0;

                    red_rot = 0;
                    red_rot2 = 0;

                    blue_rot_all = 0;
                    red_rot_all = 0;

                    play_avail = false;
                }
                else if (rayHit.collider.gameObject == success_work)
                {
                    view_change();
                }

                else if (rayHit.collider.gameObject == return_machine)
                {
                    view_change();
                }
                

                else if (rayHit.collider.gameObject == return_menu_1)
                {
                    call_menu();
                }


                    //계수를 지정한다.
                    float len_imp = Mathf.Sqrt(work_x * work_x + work_y * work_y);

                if (len_imp != 0)
                {
                    first_x_cal = (-1) * work_x / len_imp;

                    first_y_cal = work_y / len_imp;
                }

            }
        }

        //눌러져 있는 상태 확인
        if (Input.GetMouseButton(0) && control_version == 1 && play_avail == true)
        {

            last_x = Input.mousePosition.x;
            last_y = Input.mousePosition.y;

            float imp_x2 = last_x - blue_anchor.x;
            float imp_y2 = last_y - blue_anchor.y;

            float len_imp2 = Mathf.Sqrt(imp_x2 * imp_x2 + imp_y2 * imp_y2);

            first_x_cal = (-1) * imp_y2 / len_imp2;

            first_y_cal = imp_x2 / len_imp2;

            //--------------------------------------------------------------------------

            float new_angle = ((last_x - first_x) * first_x_cal) + ((last_y - first_y) * first_y_cal) * rot_speed;

            blue_rot += new_angle;

            first_x = last_x;
            first_y = last_y;

        }
        else if (Input.GetMouseButton(0) && control_version == 2 && play_avail == true)
        {
            last_x = Input.mousePosition.x;
            last_y = Input.mousePosition.y;

            float imp_x2 = last_x - blue_anchor2.x;
            float imp_y2 = last_y - blue_anchor2.y;

            float len_imp2 = Mathf.Sqrt(imp_x2 * imp_x2 + imp_y2 * imp_y2);

            first_x_cal = (-1) * imp_y2 / len_imp2;

            first_y_cal = imp_x2 / len_imp2;

            //--------------------------------------------------------------------------

            float new_angle = ((last_x - first_x) * first_x_cal) + ((last_y - first_y) * first_y_cal) * rot_speed;

            blue_rot2 += new_angle;

            first_x = last_x;
            first_y = last_y;
        }
        else if (Input.GetMouseButton(0) && control_version == 3 && play_avail == true)
        {

            last_x = Input.mousePosition.x;
            last_y = Input.mousePosition.y;

            float imp_x2 = last_x - red_anchor.x;
            float imp_y2 = last_y - red_anchor.y;

            float len_imp2 = Mathf.Sqrt(imp_x2 * imp_x2 + imp_y2 * imp_y2);

            first_x_cal = (-1) * imp_y2 / len_imp2;

            first_y_cal = imp_x2 / len_imp2;

            //--------------------------------------------------------------------------

            float new_angle = ((last_x - first_x) * first_x_cal) + ((last_y - first_y) * first_y_cal) * rot_speed;

            red_rot += new_angle;

            first_x = last_x;
            first_y = last_y;

        }
        else if (Input.GetMouseButton(0) && control_version == 4 && play_avail == true)
        {
            last_x = Input.mousePosition.x;
            last_y = Input.mousePosition.y;

            float imp_x2 = last_x - red_anchor2.x;
            float imp_y2 = last_y - red_anchor2.y;

            float len_imp2 = Mathf.Sqrt(imp_x2 * imp_x2 + imp_y2 * imp_y2);

            first_x_cal = (-1) * imp_y2 / len_imp2;

            first_y_cal = imp_x2 / len_imp2;

            //--------------------------------------------------------------------------

            float new_angle = ((last_x - first_x) * first_x_cal) + ((last_y - first_y) * first_y_cal) * rot_speed * rot_sub_speed;

            red_rot2 += new_angle;

            first_x = last_x;
            first_y = last_y;
        }



        if (Input.GetMouseButtonUp(0))
        {
            control_version = 0;
        }

        //reset에 의해 rot 0이 되면 해제한다.
        if (play_avail == false)
        {


            bc.rotation = Quaternion.Slerp(bc.rotation, rotation_imp, 0.1f);
            bc_s.rotation = Quaternion.Slerp(bc_s.rotation, rotation_imp, 0.1f);
            bc_ss.rotation = Quaternion.Slerp(bc_ss.rotation, rotation_imp, 0.1f);


            rc.rotation = Quaternion.Slerp(rc.rotation, rotation_imp, 0.1f);
            rc_s.rotation = Quaternion.Slerp(rc_s.rotation, rotation_imp, 0.1f);
            rc_ss.rotation = Quaternion.Slerp(rc_ss.rotation, rotation_imp, 0.1f);

            if ((-0.1f <bc.rotation.z) && (0.1f > bc.rotation.z) && (-0.1f < rc.rotation.z) && (0.1f > rc.rotation.z) && (-0.1f < bc_s.rotation.z) && (0.1f > bc_s.rotation.z) && (-0.1f < rc_s.rotation.z) && (0.1f > rc_s.rotation.z))
            {
                bc.rotation = Quaternion.Euler(0, 0, 0);
                bc_s.rotation = Quaternion.Euler(0, 0, 0);
                bc_ss.rotation = Quaternion.Euler(0, 0, 0);


                rc.rotation = Quaternion.Euler(0, 0, 0);
                rc_s.rotation = Quaternion.Euler(0, 0, 0);
                rc_ss.rotation = Quaternion.Euler(0, 0, 0);

                play_avail = true;

            }

        }
        else
        {

            vibrate_timer += Time.deltaTime;

            if(vibrate_timer > vibrate_cycle)
            {
                Handheld.Vibrate();
                vibrate_timer = 0;
            }


            blue_rot_all = 5 * blue_rot + blue_rot2;
            red_rot_all = 5 * red_rot + red_rot2;

            bc.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, blue_rot);
            bc_s.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, blue_rot2);
            bc_ss.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, blue_rot2);

            rc.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, red_rot);
            rc_s.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, red_rot2);
            rc_ss.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, red_rot2);


        }

        //Handheld.Vibrate();
        //Handheld.Vibrate();
    }

    public void call_vibe()
    {
        /*
        long[] pattern = new long[4];
        pattern[0] = 1000;
        pattern[1] = 5000;
        pattern[2] = 2000;
        pattern[3] = 1000;


        Vibration.Vibrate(pattern, -1);

        Vibration.Vibrate((long)5000);
        */

        //Vibration.Vibrate((long)300000);
        //Vibrate(long milliseconds);
    }

    public void call_menu()
    {
        SceneManager.LoadScene("TitleScene_new");
    }

    public void view_change()
    {
        vault_base.SetActive(true);
        vault_success.SetActive(false);

        for_camera_move.GetComponent<Camera_control_vault>().version_change();

        //조건이 맞으면 성공으로 바꾼다.
        if ((blue_rot_all < blue_rot_success + 100 && blue_rot_all > blue_rot_success - 100) && (red_rot_all < red_rot_success + 100 && red_rot_all > red_rot_success - 100))
        {
            vault_base.SetActive(false);
            vault_success.SetActive(true);

            audio_base.SetActive(false);
            audio_success.SetActive(true);
        }



    }

}
