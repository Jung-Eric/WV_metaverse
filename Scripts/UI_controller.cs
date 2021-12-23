using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_controller : MonoBehaviour
{
    //게임 실행 과정, 이거에 따라 UI가 달라진다.
    // 0 : 맵 에딧 중. 1 : 플레이 중, 2 : 대화 중(연출)
    public int play_version = 0;

    //----------------------------------------------------------
    //0번 맵 에딧 과정의 정보

    //현재 시각 버전
    // 0 : 기본, 1 : 배경만
    int vision_version = 0;

    //selector 정보
    //선택 버튼 선택 상태
    // 0 : 블록 / 1 : 오브젝트 / 2 : 적 / 3 : 커스텀(빛, 등)
    // 이 정보는 선택한 지표만이고 List에서 해당 아이템을 찾아준다.
    // button은 0부터 유효하다.
    // button_2는 기본 종류 선택, default 0
    //  1번부터 유효하다
    // button_3는 서브 선택, default 0
    //  1번주터 유효하다
    public int Select_Button = 0;
    public int Select_Button_2 = 0;
    public int Select_Button_3 = 0;
    
    // 현재 select 된 것에 따라 state를 변경
    //public int state_type
    //public int state_int


    //세부 선택 버튼 별 항목 개수
    //일단은 4개에 대해서...
    //각 항목의 참조 개수를 지정한다. 이걸로 List가 만들어진다.
    public int[] List_count = new int[4];
    //버튼 이미지로 넣기 위한 sprite 묶음
    //이는 직접 끌어다 넣는다.

    //임시 sprite renderer
    //SpriteRenderer imp_SR;

    //일반 블록---------------------------------------------------------------------
    public Sprite[] sprites_block;
    public int[] sprites_block_index;

    //오브젝트
    public Sprite[] sprites_object;

    //적군
    public Sprite[] sprites_enemy;

    //커스텀
    public Sprite[] sprites_custom;
    //-------------------------------------------------------------------------------

    //현재 리스트의 번호, 최대 개수
    //리스트별로 블록 개수
    //최대 표시 개수는 6개이다. 11개면 Max는 2개가 된다.
    //Now는 현재 리스트를 보여준다.
    int List_Now = 0;
    int List_Max = 0;

    //현재 UI표시 정도
    bool List_base = true;
    //블록 선택 후 세부 항목
    bool List_sub = false;

    //play 버튼 setactive용도 gameobject
    private GameObject[] impGameObject = new GameObject[5];

    //리스트 표기는 6개
    public GameObject[] ListGameObject = new GameObject[6];
    //하부 리스트 이미지 접근
    public GameObject[] ListGameObject_Sub = new GameObject[18];
    Image[] ListGameObject_Sub_Image = new Image[18];


    //리스트 표기 5개
    public GameObject[] ListGameObject_trans = new GameObject[5];
    //trans 리스트
    public GameObject[] ListGameObject_Trans = new GameObject[15];
    Image[] ListGameObject_Trans_Image = new Image[15];


    //상태 표기
    int type;   //0 : 일반형 1: 분할형
    public GameObject[] State_Now = new GameObject[3];
    Image[] State_Now_Image = new Image[3];


    //현재 숫자 표기
    public GameObject ListCount_Text;

    //다른 스크립트 접근
    public GameObject State_change_object;

    //뒷부분 검게 처리
    public GameObject Dark_Blocker;


    //기본 리스트
    public GameObject Selector_base;
    public bool sel_base_avail;

    //추가 리스트
    public GameObject Selector_trans;



    // Start is called before the first frame update
    void Start()
    {

        //play_version = 0;
        //기본적인 UI 활성화
        for (int i = 0; i< 5; i++)
        {
            impGameObject[i] = transform.GetChild(i).gameObject;
            //impGameObject[i].SetActive(false);
        }

        //6개 선택 버튼에 대해 이미지 18개
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

        //상태 창
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

    //Canvas play의 Edit / Play 모드를 전환한다.
    public void Change_play()
    {
        //에딧 -> 플레이
        if (play_version == 0)
        {
            play_version = 1;
            for(int i=1; i< 5; i++)
            {
                impGameObject[i].SetActive(false);
            }

        }

        //플레이 -> 에딧
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
    //공용 블록 함수
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
        //이걸로 지표를 만든다.
        //list_now는 현재 펼쳐진 리스트
        //list_block은 현재 세부 항목 0~5
        int imp_int = list_now * 6 + list_block;

        //해당 인덱스에서 몇번 째인지
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
        //시작 번호, 길이
        int start_num = 0;
        int len = 6;

        //길이 확인
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
       
        //조건에 따라 일부만 활성화
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

    //생성도 여기서 한다.
    //state 이미지도 여기서 수정한다.
    public void select_button3(int i)
    {
        bool[] imp_bool = new bool[3];

        //state 이미지 넣기
        for (int k = 0; k < 3; k++)
        {
            //기존 이미지를 복사해서 붙여버리는 형태이다. (편리)
            imp_bool[k] = ListGameObject_Trans[i * 3 + k].activeSelf;
            State_Now[k].SetActive(imp_bool[k]);

            State_Now_Image[k].sprite = ListGameObject_Trans_Image[i * 3+k].sprite;

        }

        //기존 이미지를 복사해서 붙여버리는 형태이다. (편리)
        

        //실제 블록 인덱스 넣기
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

        //이 함수는 일단 보류
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

        //조건에 따라 일부만 활성화
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

        //현재 상태
        ListCount_Text.GetComponent<Text>().text = (List_Now + 1).ToString() + "/" + List_Max.ToString();
       
       
    }

    //1~6부터의 값을 받아 활성/비활성시킨다.
    //이미지 적용도 여기서 한다.
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

    //인덱스 누적 값 받아오기
    public int Sprite_index(int i, int [] index)
    {

        int res = 0;

        for(int j=0; j<i; j++)
        {
            //지형 구조는 99로 지정
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
    //state버튼 이미지 바꾸고 world_maker에 실제 적용되는 것도 바꾼다.
    public void State_Setter()
    {
        //Select_Button
        //Select_Button_2
        bool[] imp_bool = new bool[6];

        //하부 창 키는 걸 확
        bool trans_selector;
        int trans_count = sprites_block_index.Length;
        int trans_imp = List_Now * 6 + (Select_Button_2 - 1);

        //만약 다른 곳에서도 setter2를 사용한다면 여기를 조정해준다.
        //현재는 블록 중 지형 블록만 해당한다.
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
                //기존 이미지를 복사해서 붙여버리는 형태이다. (편리)
                imp_bool[i] = ListGameObject_Sub[(Select_Button_2 - 1) * 3 + i].activeSelf;

                State_Now[i].SetActive(imp_bool[i]);
            }

            //기존 이미지를 복사해서 붙여버리는 형태이다. (편리)
            State_Now_Image[0].sprite = ListGameObject_Sub_Image[(Select_Button_2 - 1) * 3].sprite;
            State_Now_Image[1].sprite = ListGameObject_Sub_Image[(Select_Button_2 - 1) * 3 + 1].sprite;
            State_Now_Image[2].sprite = ListGameObject_Sub_Image[(Select_Button_2 - 1) * 3 + 2].sprite;

            //선택 버튼이 분류에 따라 State를 다르게 만든다.
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

    //추가 이미지 구성
    //부가 Selector_List 출현
    //다른 UI 암전
    public void State_Setter2(int trans_index_val, int block)
    {
        //지형의 경우
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

        //--------활성화, sprite 변경-------------
        //해당 인덱스에서 몇번 째인지

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
        //지형이 아닌 나머지
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
