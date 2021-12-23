using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_controller : MonoBehaviour
{
    //���� ���� ����, �̰ſ� ���� UI�� �޶�����.
    // 0 : �� ���� ��. 1 : �÷��� ��, 2 : ��ȭ ��(����)
    public int play_version = 0;

    //----------------------------------------------------------
    //0�� �� ���� ������ ����

    //���� �ð� ����
    // 0 : �⺻, 1 : ��游
    int vision_version = 0;

    //selector ����
    //���� ��ư ���� ����
    // 0 : ��� / 1 : ������Ʈ / 2 : �� / 3 : Ŀ����(��, ��)
    // �� ������ ������ ��ǥ���̰� List���� �ش� �������� ã���ش�.
    // button�� 0���� ��ȿ�ϴ�.
    // button_2�� �⺻ ���� ����, default 0
    //  1������ ��ȿ�ϴ�
    // button_3�� ���� ����, default 0
    //  1������ ��ȿ�ϴ�
    public int Select_Button = 0;
    public int Select_Button_2 = 0;
    public int Select_Button_3 = 0;
    
    // ���� select �� �Ϳ� ���� state�� ����
    //public int state_type
    //public int state_int


    //���� ���� ��ư �� �׸� ����
    //�ϴ��� 4���� ���ؼ�...
    //�� �׸��� ���� ������ �����Ѵ�. �̰ɷ� List�� ���������.
    public int[] List_count = new int[4];
    //��ư �̹����� �ֱ� ���� sprite ����
    //�̴� ���� ����� �ִ´�.

    //�ӽ� sprite renderer
    //SpriteRenderer imp_SR;

    //�Ϲ� ���---------------------------------------------------------------------
    public Sprite[] sprites_block;
    public int[] sprites_block_index;

    //������Ʈ
    public Sprite[] sprites_object;

    //����
    public Sprite[] sprites_enemy;

    //Ŀ����
    public Sprite[] sprites_custom;
    //-------------------------------------------------------------------------------

    //���� ����Ʈ�� ��ȣ, �ִ� ����
    //����Ʈ���� ��� ����
    //�ִ� ǥ�� ������ 6���̴�. 11���� Max�� 2���� �ȴ�.
    //Now�� ���� ����Ʈ�� �����ش�.
    int List_Now = 0;
    int List_Max = 0;

    //���� UIǥ�� ����
    bool List_base = true;
    //��� ���� �� ���� �׸�
    bool List_sub = false;

    //play ��ư setactive�뵵 gameobject
    private GameObject[] impGameObject = new GameObject[5];

    //����Ʈ ǥ��� 6��
    public GameObject[] ListGameObject = new GameObject[6];
    //�Ϻ� ����Ʈ �̹��� ����
    public GameObject[] ListGameObject_Sub = new GameObject[18];
    Image[] ListGameObject_Sub_Image = new Image[18];


    //����Ʈ ǥ�� 5��
    public GameObject[] ListGameObject_trans = new GameObject[5];
    //trans ����Ʈ
    public GameObject[] ListGameObject_Trans = new GameObject[15];
    Image[] ListGameObject_Trans_Image = new Image[15];


    //���� ǥ��
    int type;   //0 : �Ϲ��� 1: ������
    public GameObject[] State_Now = new GameObject[3];
    Image[] State_Now_Image = new Image[3];


    //���� ���� ǥ��
    public GameObject ListCount_Text;

    //�ٸ� ��ũ��Ʈ ����
    public GameObject State_change_object;

    //�޺κ� �˰� ó��
    public GameObject Dark_Blocker;


    //�⺻ ����Ʈ
    public GameObject Selector_base;
    public bool sel_base_avail;

    //�߰� ����Ʈ
    public GameObject Selector_trans;



    // Start is called before the first frame update
    void Start()
    {

        //play_version = 0;
        //�⺻���� UI Ȱ��ȭ
        for (int i = 0; i< 5; i++)
        {
            impGameObject[i] = transform.GetChild(i).gameObject;
            //impGameObject[i].SetActive(false);
        }

        //6�� ���� ��ư�� ���� �̹��� 18��
        GameObject imp;
        //GameObject[] imp_2 = new GameObject[3];
        for(int i=0; i<6; i++)
        {
            imp = ListGameObject[i].transform.GetChild(0).gameObject;

           for(int j=0; j<3; j++)
            {
                ListGameObject_Sub[i*3 + j] = imp.transform.GetChild(j).gameObject;
                ListGameObject_Sub_Image[i * 3 + j] = ListGameObject_Sub[i * 3 + j].GetComponent<Image>();
            }

        }

        //���� â
        for(int i=0; i<3; i++)
        {
            State_Now_Image[i] = State_Now[i].GetComponent<Image>();
        }


        for(int i=0; i<5; i++)
        {
            for(int j=0; j<3; j++)
            {
                ListGameObject_Trans_Image[i * 3 + j] = ListGameObject_Trans[i * 3 + j].GetComponent<Image>();
            }

        }

        select_button(0);


        sel_base_avail = true;

    }

    // Update is called once per frame
    /*
    void Update()
    {
        
    }
    */

    //Canvas play�� Edit / Play ��带 ��ȯ�Ѵ�.
    public void Change_play()
    {
        //���� -> �÷���
        if (play_version == 0)
        {
            play_version = 1;
            for(int i=1; i< 5; i++)
            {
                impGameObject[i].SetActive(false);
            }

        }

        //�÷��� -> ����
        else if(play_version == 1)
        {
            play_version = 0;
            for (int i = 1; i < 5; i++)
            {
                impGameObject[i].SetActive(true);
            }

        }
    }

    //-------------------------------------------------------------------------------------------
    //���� ��� �Լ�
    void List_setter_All(int imp_int)
    {
        for(int i = 0; i < imp_int; i++)
        {
            switch (Select_Button)
            {
                case 0:
                    List_setter_block(List_Now, i, sprites_block_index);
                    break;

                case 1:
                    List_setter_object(List_Now, i);
                    break;

                case 2:
                    List_setter_enemy(List_Now, i);
                    break;

                case 3:
                    List_setter_custom(List_Now, i);
                    break;

                default:

                    break;
            }
        }
    }

    void List_setter_block(int list_now, int list_block, int []index)
    {
        //�̰ɷ� ��ǥ�� �����.
        //list_now�� ���� ������ ����Ʈ
        //list_block�� ���� ���� �׸� 0~5
        int imp_int = list_now * 6 + list_block;

        //�ش� �ε������� ��� °����
        int imp_int2 = Sprite_index(imp_int, index);



        if(index[imp_int] == 99)
        {
            ListGameObject_Sub[list_block * 3].SetActive(false);
            ListGameObject_Sub[list_block * 3+1].SetActive(true);
            ListGameObject_Sub[list_block * 3+2].SetActive(true);

            ListGameObject_Sub_Image[list_block * 3 + 1].sprite = sprites_block[imp_int2];
            ListGameObject_Sub_Image[list_block * 3 + 2].sprite = sprites_block[imp_int2+1];
        }
        else
        {
            ListGameObject_Sub[list_block * 3].SetActive(true);
            ListGameObject_Sub[list_block * 3 + 1].SetActive(false);
            ListGameObject_Sub[list_block * 3 + 2].SetActive(false);

            ListGameObject_Sub_Image[list_block * 3].sprite = sprites_block[imp_int2];
        }

    }

    /*
    void List_setter_All(int type, int i)
    {
        //���� ��ȣ, ����
        int start_num = 0;
        int len = 6;

        //���� Ȯ��
        if(i+1 == List_Max)
        {
            
        }
        else
        {
            len = 6;
        }

        for(int j=0; j < 6; j++)
        {

        }

        switch (type)
        {
            case 0:
                List_setter_block(0);
                break;

            case 1:

                break;

            case 2:

                break;
            case 3:

                break;

            default:

                break;


        }

    }
    */
    

    

    void List_setter_object(int list_now, int list_block)
    {
        int imp_int = list_now * 6 + list_block;

        ListGameObject_Sub[list_block * 3].SetActive(true);
        ListGameObject_Sub[list_block * 3 + 1].SetActive(false);
        ListGameObject_Sub[list_block * 3 + 2].SetActive(false);

        ListGameObject_Sub_Image[list_block * 3].sprite = sprites_object[imp_int];
    }

    void List_setter_enemy(int list_now, int list_block)
    {
        int imp_int = list_now * 6 + list_block;

        ListGameObject_Sub[list_block * 3].SetActive(true);
        ListGameObject_Sub[list_block * 3 + 1].SetActive(false);
        ListGameObject_Sub[list_block * 3 + 2].SetActive(false);

        ListGameObject_Sub_Image[list_block * 3].sprite = sprites_enemy[imp_int];
    }

    void List_setter_custom(int list_now, int list_block)
    {
        int imp_int = list_now * 6 + list_block;

        ListGameObject_Sub[list_block * 3].SetActive(true);
        ListGameObject_Sub[list_block * 3 + 1].SetActive(false);
        ListGameObject_Sub[list_block * 3 + 2].SetActive(false);

        ListGameObject_Sub_Image[list_block * 3].sprite = sprites_custom[imp_int];
    }


    public void select_button(int i)
    {
        Select_Button = i;

        List_Now = 0;

        int imp = List_count[i];
        int imp2 = (imp / 6)+1;

        List_Max = imp2;
       
        //���ǿ� ���� �Ϻθ� Ȱ��ȭ
        if (1 == List_Max)
        {
            int imp3 = List_count[Select_Button];
            imp = imp % 6;

            Block_List_active(imp);
            List_setter_All(imp);
        }
        else
        {
            Block_List_active(6);
            List_setter_All(6);
        }

        ListCount_Text.GetComponent<Text>().text =(List_Now+1).ToString()+"/" + List_Max.ToString();

        /*
        switch (i)
        {
            case 0:
                List_setter_block(List_Now);
                break;
            case 1:
                List_setter_object(List_Now);
                break;
            case 2:
                List_setter_enemy(List_Now);
                break;
            case 3:
                List_setter_custom(List_Now);
                break;
            default:
                List_setter_block(List_Now);
                break;
        }
        */
    }
    public void select_button2(int i)
    {
        Select_Button_2 = i;
        Select_Button_3 = 0;
    }

    //������ ���⼭ �Ѵ�.
    //state �̹����� ���⼭ �����Ѵ�.
    public void select_button3(int i)
    {
        bool[] imp_bool = new bool[3];

        //state �̹��� �ֱ�
        for (int k = 0; k < 3; k++)
        {
            //���� �̹����� �����ؼ� �ٿ������� �����̴�. (��)
            imp_bool[k] = ListGameObject_Trans[i * 3 + k].activeSelf;
            State_Now[k].SetActive(imp_bool[k]);

            State_Now_Image[k].sprite = ListGameObject_Trans_Image[i * 3+k].sprite;

        }

        //���� �̹����� �����ؼ� �ٿ������� �����̴�. (��)
        

        //���� ��� �ε��� �ֱ�
        int trans_imp = List_Now * 6 + (Select_Button_2);

        Select_Button_3 = i;

        State_change_object.GetComponent<WorldMaker_demo>().cell_imp.set_cell_block(0, trans_imp, Select_Button_3);

    }

    public void List_scroll(int i)
    {
        if(i == 1)
        {
            if((List_Now+1) == List_Max)
            {
                List_Now = 0;
            }
            else
            {
                List_Now += 1;
            }

        }
        else if(i == -1)
        {
            if ((List_Now - 1) == -1)
            {
                List_Now = List_Max-1;
            }
            else
            {
                List_Now -= 1;
            }

        }

        //�� �Լ��� �ϴ� ����
        /*
        while(List_Now >= List_Max || List_Now < 0)
        {
            if (List_Now >= List_Max)
            {
                List_Now = List_Now - List_Max;
            }
            else if (List_Now < 0)
            {
                List_Now = List_Now + List_Max;
            }
        }
        */

        //���ǿ� ���� �Ϻθ� Ȱ��ȭ
        if (List_Now+1 == List_Max)
        {
            int imp = List_count[Select_Button];
            imp = imp % 6;

            List_setter_All(imp);
            Block_List_active(imp);
        }
        else
        {
            List_setter_All(6);
            Block_List_active(6);
        }

        //���� ����
        ListCount_Text.GetComponent<Text>().text = (List_Now + 1).ToString() + "/" + List_Max.ToString();
       
       
    }

    //1~6������ ���� �޾� Ȱ��/��Ȱ����Ų��.
    //�̹��� ���뵵 ���⼭ �Ѵ�.
    public void Block_List_active(int i)
    {
        for(int j =0; j<i; j++)
        {
            
            //ListGameObject[j].GetComponent<List_changer>().Simp_N = sprites_block[10];
            //ListGameObject[j].GetComponent<List_changer>().List_change_N();
            //List_setter_All(Select_Button, j);

            ListGameObject[j].SetActive(true);
        }
        for(int j=i; j<6; j++)
        {
            ListGameObject[j].SetActive(false);
        }
    }

    //�ε��� ���� �� �޾ƿ���
    public int Sprite_index(int i, int [] index)
    {

        int res = 0;

        for(int j=0; j<i; j++)
        {
            //���� ������ 99�� ����
            if(index[j] == 99)
            {
                res += 4;
            }

            else
            {
                res = res + index[j];
            }
            
        }

        return res;
    }

    //----------------------------------------------------
    //state��ư �̹��� �ٲٰ� world_maker�� ���� ����Ǵ� �͵� �ٲ۴�.
    public void State_Setter()
    {
        //Select_Button
        //Select_Button_2
        bool[] imp_bool = new bool[6];

        //�Ϻ� â Ű�� �� Ȯ
        bool trans_selector;
        int trans_count = sprites_block_index.Length;
        int trans_imp = List_Now * 6 + (Select_Button_2 - 1);

        //���� �ٸ� �������� setter2�� ����Ѵٸ� ���⸦ �������ش�.
        //����� ��� �� ���� ��ϸ� �ش��Ѵ�.
        if (Select_Button == 0)
        {
            if(sprites_block_index[trans_imp] == 1)
            {
                trans_selector = false;
            }
            else
            {
                trans_selector = true;
            }

        }
        else
        {
            trans_selector = false;
        }

        if (trans_selector == false)
        {
            for (int i = 0; i < 3; i++)
            {
                //���� �̹����� �����ؼ� �ٿ������� �����̴�. (��)
                imp_bool[i] = ListGameObject_Sub[(Select_Button_2 - 1) * 3 + i].activeSelf;

                State_Now[i].SetActive(imp_bool[i]);
            }

            //���� �̹����� �����ؼ� �ٿ������� �����̴�. (��)
            State_Now_Image[0].sprite = ListGameObject_Sub_Image[(Select_Button_2 - 1) * 3].sprite;
            State_Now_Image[1].sprite = ListGameObject_Sub_Image[(Select_Button_2 - 1) * 3 + 1].sprite;
            State_Now_Image[2].sprite = ListGameObject_Sub_Image[(Select_Button_2 - 1) * 3 + 2].sprite;

            //���� ��ư�� �з��� ���� State�� �ٸ��� �����.
            if (Select_Button == 0)
            {
                //int imp_sel = List_Now * 6 + Select_Button_2;
                State_change_object.GetComponent<WorldMaker_demo>().cell_imp.set_cell_block(0, trans_imp + 1, 0);

                //Debug.Log(State_change_object.GetComponent<WorldMaker_demo>().cell_imp.get_real());
            }

        }
        else
        {
            State_Setter2(sprites_block_index[trans_imp], trans_imp);
        }

    }

    //�߰� �̹��� ����
    //�ΰ� Selector_List ����
    //�ٸ� UI ����
    public void State_Setter2(int trans_index_val, int block)
    {
        //������ ���
        int trans_count;

        int trans_sprite_index;

        if(trans_index_val == 99)
        {
            trans_count = 5;
        }
        else
        {
            trans_count = trans_index_val;
        }

        //--------Ȱ��ȭ, sprite ����-------------
        //�ش� �ε������� ��� °����

        for(int i=0; i<5; i++)
        {
            if (i < trans_count)
            {
                ListGameObject_trans[i].SetActive(true);
            }
            else
            {
                ListGameObject_trans[i].SetActive(false);
            }
        }

        trans_sprite_index = Sprite_index(block, sprites_block_index);

        
        if(trans_index_val == 99)
        {
            for(int i = 0; i < 5; i++)
            {
                ListGameObject_Trans[i * 3].SetActive(false);
                ListGameObject_Trans[i * 3 + 1].SetActive(true);
                ListGameObject_Trans[i * 3 + 2].SetActive(true);

            }


            ListGameObject_Trans_Image[1].sprite = sprites_block[trans_sprite_index];
            ListGameObject_Trans_Image[2].sprite = sprites_block[trans_sprite_index + 1];

            ListGameObject_Trans[4].SetActive(false);
            ListGameObject_Trans_Image[5].sprite = sprites_block[trans_sprite_index + 2];

            ListGameObject_Trans_Image[7].sprite = sprites_block[trans_sprite_index + 2];
            ListGameObject_Trans_Image[8].sprite = sprites_block[trans_sprite_index + 1];

            ListGameObject_Trans_Image[10].sprite = sprites_block[trans_sprite_index + 3];
            ListGameObject_Trans_Image[11].sprite = sprites_block[trans_sprite_index + 1];

            ListGameObject_Trans[13].SetActive(false);
            ListGameObject_Trans_Image[14].sprite = sprites_block[trans_sprite_index + 3];



        }
        //������ �ƴ� ������
        else
        {
            for(int i = 0; i<trans_count; i++)
            {
                ListGameObject_Trans[i * 3].SetActive(true);
                ListGameObject_Trans[i * 3 + 1].SetActive(false);
                ListGameObject_Trans[i * 3 + 2].SetActive(false);

                ListGameObject_Trans_Image[i*3].sprite = sprites_block[trans_sprite_index + i];
            }

        }

        Dark_Blocker.SetActive(true);
        Selector_trans.SetActive(true);

    }

    public void change_sel_base()
    {
        if(sel_base_avail == false)
        {
            Selector_base.SetActive(true);
            sel_base_avail = true;
        }
        else
        {
            Selector_base.SetActive(false);
            sel_base_avail = false;
        }
    }

}
