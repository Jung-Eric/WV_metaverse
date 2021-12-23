using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ImageDrag;



    public float drag_x;
    public float drag_y;

    Vector2 touchNow_position;
    Vector2 touchStart_position;
    Vector2 delta_position;

    public float vector_val_x;
    public float vector_val_y;

    public float rot_speed = 0.1f;

    private Quaternion rot = Quaternion.identity;


    float Result_x = 0.0f;
    float Result_y = 0.0f;

    public GameObject TitleLogo;

    public GameObject button_Start;
    public GameObject button_Play;
    public GameObject button_Setting;

    public GameObject image_all;

    public GameObject vault_shake;

    

    //0�� ����
    //1�� ���ۿ��� �÷���(�̵�)
    //2�� �÷���
    //3�� �÷��̿��� ����(�̵�)
    public int phase;

    public float change_speed;

    public bool UI_avail = false;

    public GameObject UI_availBox;
    public Image UI_availSprite;
    public Sprite UI_color;
    public Sprite UI_black;

    // Start is called before the first frame update
    void Start()
    {       
        button_Play.transform.Translate(new Vector3(0,-200,0));
    }

    // Update is called once per frame
    void Update()
    {
        if(phase == 1)
        {
            PlayMove();
        }
        else if(phase == 3)
        {
            StartMove();
        }

        if (Input.GetMouseButton(0))
        {
            
            touchNow_position = (Vector2)Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                touchStart_position = touchNow_position;

            }

            //�ϴ� �巡�� ��ġ�� ����� �̵��Ѵ�. �߰� ���ӵ��� ����
            delta_position = (touchNow_position - touchStart_position);

            drag_x = delta_position.x;
            drag_y = delta_position.y;

            //-------------------------------------------------------------
            //ȸ�� ���� ���ֱ�, �̿ܿ��� ���� �߻�
            vector_val_y = image_all.transform.localEulerAngles.x;
            vector_val_x = image_all.transform.localEulerAngles.y;

            //�߰� ���� (360�� ����)
            if(vector_val_y > 180)
            {
                vector_val_y = vector_val_y - 360;
            }

            if (vector_val_x > 180)
            {
                vector_val_x = vector_val_x - 360;
            }

            //����� �Ϲ���
            if (vector_val_x < 15 && drag_x >= 0)
            {
                vector_val_x += drag_x * Time.deltaTime; 
            }

            if (vector_val_x > -15 && drag_x <= 0)
            {
                vector_val_x += drag_x * Time.deltaTime;
            }

    
            if (vector_val_y < 9 && drag_y <= 0)
            {
                vector_val_y += drag_y * Time.deltaTime * (-1);
            }

            if (vector_val_y > -9 && drag_y >= 0)
            {
                vector_val_y += drag_y * Time.deltaTime * (-1);
            }
            
            //���� �Ϸ�
            image_all.transform.localEulerAngles = new Vector3(vector_val_y, vector_val_x, 0);
            //image_all.transform.localEulerAngles = new Vector3(-10, -10, 0);

            //-------------------------------------------------------------


        }
        else
        {
            rot.eulerAngles = new Vector3(-0, -0, 0);
            image_all.transform.rotation = Quaternion.RotateTowards(image_all.transform.rotation, rot, Time.deltaTime*100);
           
        }

     

    }

    IEnumerable start_toPlay2()
    {
        yield return null;

    }


    
    public void start_toPlay()
    {

        button_Start.transform.Translate(new Vector3(0, -200, 0));

        phase = 1;

        button_Play.SetActive(true);

        button_Setting.SetActive(true);

        button_Start.SetActive(false);

        UI_availBox.SetActive(false);

    }


    public void play_toStart()
    {

        button_Play.transform.Translate(new Vector3(0, -200, 0));

        phase = 3;

        button_Start.SetActive(true);

        button_Play.SetActive(false);

        button_Setting.SetActive(false);

        UI_availBox.SetActive(true);

    }

    public void PlayMove()
    {
        button_Play.transform.localPosition = Vector3.MoveTowards(button_Play.transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * change_speed);

        if (button_Play.transform.localPosition.y == 0)
        {
            phase = 2;
        }

    }

    public void StartMove()
    {
        button_Start.transform.localPosition = Vector3.MoveTowards(button_Start.transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * change_speed);

        if (button_Start.transform.localPosition.y == 0)
        {
            phase = 0;
        }

    }

    //���丮 �÷��� �̵�
    public void play_toStoryPlay()
    {
        SceneManager.LoadScene("Demo_origin_v_Story");

    }

    //���� �÷��� �̵�
    public void play_toFreePlay()
    {
        SceneManager.LoadScene("Demo_origin_vNEW");
    }

    //�ݰ� �÷��� �̵�
    public void play_toVaultPlay()
    {
        SceneManager.LoadScene("Demo_vault");
    }

    //Ž�� �÷��� �̵�
    public void play_toDetectPlay()
    {
        SceneManager.LoadScene("Demo_search");
    }
    
    //ȭ�� UI ���� Ȥ�� �����
    public void UI_Check()
    {
        if(UI_avail == false)
        {
            UI_availSprite.sprite = UI_black;
            TitleLogo.SetActive(false);
            button_Start.SetActive(false);
            UI_avail = true;
        }
        else
        {
            UI_availSprite.sprite = UI_color;
            TitleLogo.SetActive(true);
            button_Start.SetActive(true);
            UI_avail = false;
        }

    }


}
