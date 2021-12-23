using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_control : MonoBehaviour
{
    //��� ������ ����
    public bool freeze;

    public Animator anime_enemy;

    public Vector3 position;

    public float int_posX, int_posY;

    //�⺻ �̵� ����
    public float move_hor = 2;
    //�⺻ ���� ����
    public bool way_change = false;

    //���� ��忡 ���� �۵��Ѵ�.
    //0�� 
    public int mode = 0;

    public float enemy_speed = 0.05f;
    float enemy_speed_base = 0.01f;

    //�ֱ� ������ ���� ����
    public float update_cycle = 0;
    public float update_cycle2 = 0;
    public float update_cycle3 = 0;
    //�� �ð� ���� ���� �ȵǸ� mode�� 2�� �����Ѵ�.
    public float rel_cycle_3to2 = 0;

    public float cycle_second = 0;
    public float attack_second = 0;
    public float attack_iter_second = 0;
    public float rel_second_3to2 = 0.4f;

    //����
    Vector3 VectorR = new Vector3(1,1,1);
    Vector3 VectorL = new Vector3(-1, 1, 1);

    Vector3 VectorR2 = new Vector3(1, 0, 0);
    Vector3 VectorL2 = new Vector3(-1, 0, 0);
    public float detection_range = 1f;

    int layerMask = 0;

    //źȯ
    public GameObject lightning_missile;

    void Start()
    {
        freeze = true;

        position = transform.position;

        int_posX = transform.position.x;
        int_posY = transform.position.y;

        layerMask = (1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("World"));

        //anime_enemy.SetTrigger("move_normal_trigg");

        mode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(freeze == false)
        { 


        update_cycle += Time.deltaTime;


        if (update_cycle > cycle_second)
        {

            if (way_change == false)
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, 0), VectorR2, detection_range, layerMask);
                    
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.name);

                    if (hit.collider.name == "wolf_base_psb" && mode == 0)
                    {
                        mode = 1;
                    }
                    else if (hit.collider.name != "wolf_base_psb" && mode == 3)
                    {
                        rel_cycle_3to2 += Time.deltaTime;
                    }
                }
                else
                {

                }

            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, 0), VectorL2, detection_range, layerMask);

                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.name);

                    if (hit.collider.name == "wolf_base_psb" && mode == 0)
                    {
                        mode = 1;
                    }
                    else if (hit.collider.name != "wolf_base_psb" && mode == 3)
                    {
                        rel_cycle_3to2 += Time.deltaTime;
                    }

                }
                else
                {

                }

            }

            update_cycle = 0.0f;

        }

        if (mode == 1 || mode == 2)
        {
            update_cycle2 += Time.deltaTime;
        }

        if (mode == 3)
        {
            update_cycle3 += Time.deltaTime;
        }


        if (mode == 0)
        {
            patrol();

        }
        else if (mode == 1)
        {
            anime_enemy.SetTrigger("cancle_trigger");
            anime_enemy.SetTrigger("attack_trigg");
            mode = 2;
        }
        else if (mode == 2)
        {

            if (update_cycle2 > attack_second)
            {
                mode = 3;
                update_cycle2 = 0;
            }

        }
        else if (mode == 3)
        {

            if (rel_cycle_3to2 > rel_second_3to2)
            {
                mode = 0;
                anime_enemy.SetTrigger("cancle_trigger");
                anime_enemy.SetTrigger("move_normal_trigg");
                update_cycle = 0;
                update_cycle2 = 0;
                update_cycle3 = 0;
                rel_cycle_3to2 = 0;
            }
            //������ �����Ѵ�.
            else if (update_cycle3 > attack_iter_second)
            {
                if (way_change == false)
                {
                    GameObject imp_object = Instantiate(lightning_missile, new Vector3(transform.position.x + 0.7f, transform.position.y + 0.35f, 0), Quaternion.identity);
                }
                else
                {
                    GameObject imp_object = Instantiate(lightning_missile, new Vector3(transform.position.x - 0.7f, transform.position.y + 0.35f, 0), Quaternion.identity);
                }

                update_cycle3 = 0;
            }

        }

    }
    }

    void patrol()
    {

        //��ġ ����
        position = transform.position;

        //���� ��ġ�� ����
        if (((position.x >= int_posX + move_hor - 0.1f) && (position.x <= int_posX + move_hor + 0.1f)) && (way_change == false))
        {
            way_change = true;
            transform.localScale = VectorL;

        }

        if (((position.x >= int_posX - move_hor - 0.1f) && (position.x <= int_posX - move_hor + 0.1f)) && (way_change == true))
        {
            way_change = false;
            transform.localScale = VectorR;

        }

        if (way_change == false)
        {
            position.x += enemy_speed * enemy_speed_base;
        }
        else if (way_change == true)
        {
            position.x -= enemy_speed * enemy_speed_base;
        }

        transform.position = position;
    }

    public void MakeFreeze()
    {
        freeze = true;
        if(! anime_enemy.GetCurrentAnimatorStateInfo(0).IsName("Enemy_stay"))
        {
            anime_enemy.SetTrigger("cancle_trigger");
        }

    }

    public void RelFreeze()
    {
        freeze = false;
        anime_enemy.SetTrigger("move_normal_trigg");
    }

}
