using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SH_BlockCreate : MonoBehaviour
{
    public enum ECATSATATE
	{
		 BASIC, SLOW, FAST, BIG, TIMEATTACK, ANGEL
	};

	public	ECATSATATE eCatState;				//고양이 상태들을 담은 enum
    [HideInInspector]
	public  Transform  tDir;                    //축이 결정이 될 Transform
	private int        nRand;					//블록의 상태가 결정될 랜덤 변수
	private Quaternion qRotZ;                   //Z축일때 y축 회전 값이 90도 이게 할 회전 값
	public GameObject gCatBlockBasic;			//생성될 기본 고양이 블록
	public GameObject gCatBlockFast;			//생성될 빠른 고양이 블록
	public GameObject gCatBlockSlow;			//생성될 느린 고양이 블록
	public GameObject gCatBlockBig;             //생성될 큰   고양이 블록
	public GameObject gCatBlockTimeAttack;      //생성될 화난 고양이 블록(타임어택)
	public GameObject gCatBlockAngel;			//생성될 천사 고양이 블록
	public Transform tPosX;					    //블럭이 생성될 X위치
	public Transform tPosZ;                     //블럭이 생성될 Z위치
	public Transform tBigPos;					//뚱냥이가 연달아 나올 위치					
	[HideInInspector]
	public bool		 isX = false;				//블럭이 X축이냐?
	[HideInInspector]
	public bool		 isZ = false;				//블럭이 Z축이냐?
	[HideInInspector]
	public bool		isCreate = false;           //생성했냐?


    // 싱글톤
    public static SH_BlockCreate Instance;
    private void Awake()
	{
        if (Instance == null)
        {
            Instance = this;
        }
        //가장 기본 상태는 기본 고양이
		eCatState = ECATSATATE.BASIC;

		qRotZ = Quaternion.Euler(0, 90, 0);
		// 생성한 모든 오브젝트들을 찾아서 연결
		tPosX = GameObject.Find("PosX").transform;
		tPosZ = GameObject.Find("PosZ").transform;
		tBigPos = GameObject.Find("BigCreatePos").transform;
	}

    private void Update()
	{
		#region 방향 전환 조건
        // ++ 버튼에 따라서 바꿔야함
		if (HJ_GameManager.Instance.isPosX)
		{
			tDir = tPosX;
			isX = true;
			isZ = false;

			qRotZ = Quaternion.Euler(0, 90, 0);

			gCatBlockBasic.transform.rotation = qRotZ;
			gCatBlockFast.transform.rotation = qRotZ;
			gCatBlockSlow.transform.rotation = qRotZ;
			gCatBlockBig.transform.rotation = qRotZ;
			gCatBlockTimeAttack.transform.rotation = qRotZ;
			gCatBlockAngel.transform.rotation = qRotZ;
		}
		else
		{
			tDir = tPosZ;
			isZ = true;
			isX = false;

			qRotZ = Quaternion.Euler(0, 0, 0);

			gCatBlockBasic.transform.rotation = qRotZ;
			gCatBlockFast.transform.rotation = qRotZ;
			gCatBlockSlow.transform.rotation = qRotZ;
			gCatBlockBig.transform.rotation = qRotZ;
			gCatBlockTimeAttack.transform.rotation = qRotZ;
			gCatBlockAngel.transform.rotation = qRotZ;
		}
		#endregion
		#region 블록 생성 조건
		if (!isCreate)
		{
			isCreate = true;
            StartCoroutine("CreateCondition");
        }
		#endregion

	}

    // 갓갓 코루틴
    IEnumerator CreateCondition()
    {
        // 3층이하면
        if (HJ_ScoreManager.Instance.Score < 3)
        {

            eCatState = ECATSATATE.BASIC;
            
        }
		else if(HJ_ScoreManager.Instance.Score % 9 == 0)
		{
			nRand = Random.Range(1, 11);

			if (nRand <= 4)
				eCatState = ECATSATATE.ANGEL;
			else
			{
				eCatState = ECATSATATE.BASIC;
			}
		}
        // 3층 초과고 10층 미만이면 
        else if (HJ_ScoreManager.Instance.Score > 3 && HJ_ScoreManager.Instance.Score < 10)
        {

            nRand = Random.Range(1, 11); // 1~10까지 랜덤 돌림

            // 2 이하로 나오면 빠른 아이
            if (nRand <= 2)
            {
                eCatState = ECATSATATE.FAST;
            }
            // 2 초과 4 이하면 느린 아이
            else if (nRand == 3 || nRand == 4)
            {
                eCatState = ECATSATATE.SLOW;
            }
            // 그 이외 부턴 기본 아이
            else
            {
                eCatState = ECATSATATE.BASIC;
            }

        }
        // 10층이면
        else if (HJ_ScoreManager.Instance.Score == 10)
        {
            //큰 냥이
            eCatState = ECATSATATE.BIG;
        }
        // 10층 초과 15층 미만이면 
        else if ((HJ_ScoreManager.Instance.Score > 10 && HJ_ScoreManager.Instance.Score < 15))
        {

            nRand = Random.Range(1, 11); // 1~10까지 랜덤 돌림

            // 2 이하로 나오면 빠른 아이
            if (nRand <= 2)
            {
                eCatState = ECATSATATE.FAST;
            }
            // 2 초과 4 이하면 느린 아이
            else if (nRand == 3 || nRand == 4)
            {
                eCatState = ECATSATATE.SLOW;
            }
            else if (nRand == 5)
            {
                eCatState = ECATSATATE.BIG;
            }
            // 그 이외 부턴 기본 아이
            else
            {
                eCatState = ECATSATATE.BASIC;
            }
        }
        // 15층이면 
        else if (HJ_ScoreManager.Instance.Score == 15)
        {
            eCatState = ECATSATATE.TIMEATTACK;
        }
        // 15층 보다 높아지면
        else if (HJ_ScoreManager.Instance.Score > 15)
        {

            nRand = Random.Range(1, 11); // 1~10까지 랜덤 돌림

            // 2 이하로 나오면 빠른 아이
            if (nRand <= 2)
            {
                eCatState = ECATSATATE.FAST;
            }
            // 2 초과 4 이하면 느린 아이
            else if (nRand == 3 || nRand == 4)
            {
                eCatState = ECATSATATE.SLOW;
            }
            else if (nRand == 5)
            {
                eCatState = ECATSATATE.TIMEATTACK;
            }
            // 6 아님 7이 나오면 큰 아이
            else if (nRand == 6 || nRand == 7)
            {
                eCatState = ECATSATATE.BIG;
            }
            // 그 이외 부턴 기본 아이
            else
            {
                eCatState = ECATSATATE.BASIC;
            }
        }

        yield return null;

		CreateCat();
    }

    // 고양이 생성하는 switch문
    void CreateCat()
    {
        switch (eCatState)
        {
            case ECATSATATE.BASIC:
                {
					Instantiate(gCatBlockBasic, tDir.transform.position, gCatBlockBasic.transform.rotation);
                    HJ_AudioManager.Instance.audios[0].pitch = 1.0f;
                    HJ_AudioManager.Instance.BGPlay(HJ_AudioManager.BGSounds.BasicBG);
                }
                break;
            case ECATSATATE.FAST:
				{ 		
					Instantiate(gCatBlockFast, tDir.transform.position, gCatBlockFast.transform.rotation);
                    HJ_AudioManager.Instance.audios[0].pitch = 1.1f;
                    HJ_AudioManager.Instance.BGPlay(HJ_AudioManager.BGSounds.BasicBG);
                }
                break;
            case ECATSATATE.SLOW:
                {
					Instantiate(gCatBlockSlow, tDir.transform.position, gCatBlockSlow.transform.rotation);
                    HJ_AudioManager.Instance.audios[0].pitch = 0.9f;
                    HJ_AudioManager.Instance.BGPlay(HJ_AudioManager.BGSounds.BasicBG);
                }
                break;
            case ECATSATATE.BIG:
				{ 
					Instantiate(gCatBlockBig, tDir.transform.position, gCatBlockBig.transform.rotation);
                    HJ_AudioManager.Instance.audios[0].pitch = 1.0f;
                    HJ_AudioManager.Instance.BGPlay(HJ_AudioManager.BGSounds.BigBG);
                }
                break;
            case ECATSATATE.TIMEATTACK:
                {
					Instantiate(gCatBlockTimeAttack, tDir.transform.position, gCatBlockTimeAttack.transform.rotation);
                    HJ_AudioManager.Instance.audios[0].pitch = 1.0f;
                    HJ_AudioManager.Instance.BGPlay(HJ_AudioManager.BGSounds.TimeBG);

                }
                break;
			case ECATSATATE.ANGEL:
				{
					Instantiate(gCatBlockAngel, tDir.transform.position, gCatBlockAngel.transform.rotation);
                    HJ_AudioManager.Instance.audios[0].pitch = 1.0f;
                    HJ_AudioManager.Instance.BGPlay(HJ_AudioManager.BGSounds.FeverBG);
                }
				break; 
        }
    }

}
