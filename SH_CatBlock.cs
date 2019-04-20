using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SH_CatBlock : MonoBehaviour
{
    private float			 fMoveSpeed;    //고먐미들믜 미돔속도
	[HideInInspector]
	public float			 fTimeAttack;   //타임어택 고양이의 시간
	[HideInInspector]
	public SH_BlockCreate	 shBlock;       //블록 타입들을 불러올 스크립트  
	[HideInInspector]
	public bool				 isTimeAttack;  //타임어택 고양이인지 아닌지
	private int				 repeatX = 1;   // X위치에서의 구간 반복
	private	int				 repeatZ = 1;   // Z위치에서의 구간 반복
	[HideInInspector]
	public bool				 isMoving;      //좌우로 움직이고 있냐? 
	[HideInInspector]
	public bool				 isLerpEnd;     //러프 완료 했냐? 
	private Rigidbody		 riKinematic;   //키네메틱 체크해줄 리지드바디
	private Vector3			 vecThisOrigin; //나의 원래 pos;
	[HideInInspector]
	public float			 fLerpSpeed;    //러프 되는 스피드
	private Vector3			 fTempPos1;		//블록이 안착 된 후의 위치 (한번만 체크) y위치
	private Vector3			 fTempPos2;     //계속 체크해줄  위치(y)
	private float			 fPosDist;      //fTempPos1과 fTempPos2의 거리
	private float			 fTimeCost;
	private Text			 TimeCount;
	[HideInInspector]
	public bool				 isOnce;
	private float			 a = 0.0f;
    private int              ranAudios;


	private void Awake()
	{
		shBlock = GameObject.Find("SH_BlockCreate").GetComponent<SH_BlockCreate>();
		isTimeAttack = false;
		fLerpSpeed = 3.0f;
		isMoving = true;
		fMoveSpeed = 1.5f;
		riKinematic = this.gameObject.GetComponentInChildren<Rigidbody>();	

		riKinematic.isKinematic = true;

		repeatX = 1; // X위치에서의 구간 반복

		repeatZ = 1; // Z위치에서의 구간 반복

		fTimeCost = 5.0f;

		if (shBlock.eCatState == SH_BlockCreate.ECATSATATE.TIMEATTACK)
		{
			TimeCount = GetComponentInChildren<Text>();
			TimeCount.gameObject.SetActive(false);
			isOnce = false;
		}

	}

	private void Update()
	{
		//블럭의 최고 높이를 항상 체크하는 Position
		fTempPos2 = transform.GetChild(0).transform.position;
		fPosDist = Vector3.Distance(fTempPos1, fTempPos2); //블럭이 안착 후의 위치와 블럭의 현재 위치의 차이를 담은 Dist

		if (isMoving)
		{
			if (shBlock.eCatState == SH_BlockCreate.ECATSATATE.BASIC)
			{
				if (shBlock.isX)
				{
					//블록이 posX라면
					//블록이 좌우로 왔다갔다 한다 
					if (this.transform.position.z > 1.5f)
					{
						repeatX = -1;
					}
					else if (this.transform.position.z < -1.5f)
					{
						repeatX = 1;
					}
					transform.position += Vector3.forward * fMoveSpeed * Time.deltaTime * repeatX;
				}
				else if (shBlock.isZ)
				{

					//블록이 posZ라면
					if (this.transform.position.x > 1.5f)
					{
						repeatZ = 1;
					}
					else if (this.transform.position.x < -1.5f)
					{
						repeatZ = -1;
					}

					transform.position += Vector3.left * fMoveSpeed * Time.deltaTime * repeatZ;
				}
			}
			else if (shBlock.eCatState == SH_BlockCreate.ECATSATATE.FAST)
			{
				fMoveSpeed = 2.0f;

				if (shBlock.isX)
				{
					//블록이 posX라면
					if (this.transform.position.z > 1.5f)
					{
						repeatX = -1;
					}
					else if (this.transform.position.z < -1.5f)
					{
						repeatX = 1;
					}
					transform.position += Vector3.forward * fMoveSpeed * Time.deltaTime * repeatX;
				}
				else if (shBlock.isZ)
				{

					//블록이 posZ라면
					if (this.transform.position.x > 1.5f)
					{
						repeatZ = 1;
					}
					else if (this.transform.position.x < -1.5f)
					{
						repeatZ = -1;
					}

					transform.position += Vector3.left * fMoveSpeed * Time.deltaTime * repeatZ; ;
				}
			}
			else if (shBlock.eCatState == SH_BlockCreate.ECATSATATE.SLOW)
			{
				fMoveSpeed = 1.0f;

				if (shBlock.isX)
				{
					//블록이 posX라면
					if (this.transform.position.z > 1.5f)
					{
						repeatX = -1;
					}
					else if (this.transform.position.z < -1.5f)
					{
						repeatX = 1;
					}
					transform.position += Vector3.forward * fMoveSpeed * Time.deltaTime * repeatX;
				}
				else if (shBlock.isZ)
				{

					//블록이 posZ라면
					if (this.transform.position.x > 1.5f)
					{
						repeatZ = 1;
					}
					else if (this.transform.position.x < -1.5f)
					{
						repeatZ = -1;
					}

					transform.position += Vector3.left * fMoveSpeed * Time.deltaTime * repeatZ;
				}
			}
			else if (shBlock.eCatState == SH_BlockCreate.ECATSATATE.BIG)
			{
				if (shBlock.isX)
				{
					//블록이 posX라면
					if (this.transform.position.z > 1.5f)
					{
						repeatX = -1;
					}
					else if (this.transform.position.z < -1.5f)
					{
						repeatX = 1;
					}
					transform.position += Vector3.forward * fMoveSpeed * Time.deltaTime * repeatX;
				}
				else if (shBlock.isZ)
				{

					//블록이 posZ라면
					if (this.transform.position.x > 1.5f)
					{
						repeatZ = 1;
					}
					else if (this.transform.position.x < -1.5f)
					{
						repeatZ = -1;
					}

					transform.position += Vector3.left * fMoveSpeed * Time.deltaTime * repeatZ;
				}
			}
			else if (shBlock.eCatState == SH_BlockCreate.ECATSATATE.TIMEATTACK)
			{
				if (shBlock.isX)
				{
					//블록이 posX라면
					if (this.transform.position.z > 1.5f)
					{
						repeatX = -1;
					}
					else if (this.transform.position.z < -1.5f)
					{
						repeatX = 1;
					}
					transform.position += Vector3.forward * fMoveSpeed * Time.deltaTime * repeatX;
				}
				else if (shBlock.isZ)
				{

					//블록이 posZ라면
					if (this.transform.position.x > 1.5f)
					{
						repeatZ = 1;
					}
					else if (this.transform.position.x < -1.5f)
					{
						repeatZ = -1;
					}

					transform.position += Vector3.left * fMoveSpeed * Time.deltaTime * repeatZ;
				}
			}
			else if(shBlock.eCatState == SH_BlockCreate.ECATSATATE.ANGEL)
			{
				if (shBlock.isX)
				{
					//블록이 posX라면
					if (this.transform.position.z > 1.5f)
					{
						repeatX = -1;
					}
					else if (this.transform.position.z < -1.5f)
					{
						repeatX = 1;
					}
					transform.position += Vector3.forward * fMoveSpeed * Time.deltaTime * repeatX;
				}
				else if (shBlock.isZ)
				{

					//블록이 posZ라면
					if (this.transform.position.x > 1.5f)
					{
						repeatZ = 1;
					}
					else if (this.transform.position.x < -1.5f)
					{
						repeatZ = -1;
					}

					transform.position += Vector3.left * fMoveSpeed * Time.deltaTime * repeatZ;
				}
			}
			CatBlockController();
			// 타임어택이라면
			if (shBlock.eCatState == SH_BlockCreate.ECATSATATE.TIMEATTACK && isOnce == false)
			{
				isOnce = true;
				// 타임어택 코루틴을 시작
				StartCoroutine("TimeAttack");

			}

            // 블럭
            // 만약 버튼을 눌렀다면
            if (CatRotButton.isSwitch)
            {
                // 초기화
                CatRotButton.isSwitch = false;

                if (shBlock.isZ)
                {
                    // 블럭 위치랑
                    transform.position = SH_BlockCreate.Instance.tPosZ.position;
                    // 블럭 회전값을 지정
                    transform.rotation = Quaternion.Euler(0, 0, 0);

                }
                else if (shBlock.isX)
                {
                    // 블럭 위치랑
                    transform.position = SH_BlockCreate.Instance.tPosX.position;
                    // 블럭 회전값을 지정
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                }


                //Destroy(gameObject);

            }
        }


		if (fPosDist > 30.0f)
		{
			HJ_ScoreManager.Instance.Score --;
            HJ_GameManager.Instance.hp--;
			Destroy(this.gameObject);
		}
    }

    private void CatBlockController()
	{
		//터치했다면 
		if(HJ_GameManager.Instance.gState == HJ_GameManager.GameState.Pause
			 || !TouchButton.isTouch)
		{
			return;
		}

        if (HJ_GameManager.Instance.isPCTest)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ButtonTouch();

            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    ButtonTouch();

                }
            }
        }
		
	}

    void ButtonTouch()
    {
        isMoving = false;
        vecThisOrigin = this.transform.position; // 터치 실행 하였을때 나의 위치 저장 
        StartCoroutine("CatMoving");// ""로 써야 StopCorutine이 먹는다		
        if (shBlock.eCatState == SH_BlockCreate.ECATSATATE.TIMEATTACK)
        {
            StopCoroutine("TimeAttack");
            TimeCount.gameObject.SetActive(false);
            fTimeCost = 0;
            a = 0;
        }

        HJ_AudioManager.Instance.EffectPlay(HJ_AudioManager.EffectSound.Tap);

        if (shBlock.eCatState != SH_BlockCreate.ECATSATATE.TIMEATTACK)
        {
            ranAudios = Random.Range(0, 5);
            if (ranAudios == 0)
            {
                HJ_AudioManager.Instance.CatPlay(HJ_AudioManager.CatSound.Cat_Basic);
            }
            else if (ranAudios == 1)
            {
                HJ_AudioManager.Instance.CatPlay(HJ_AudioManager.CatSound.Cat_Big);
            }
            else if (ranAudios == 2)
            {
                HJ_AudioManager.Instance.CatPlay(HJ_AudioManager.CatSound.Cat_Fast);
            }
            else if (ranAudios == 3)
            {
                HJ_AudioManager.Instance.CatPlay(HJ_AudioManager.CatSound.Cat_Fever);
            }
            else if (ranAudios == 4)
            {
                HJ_AudioManager.Instance.CatPlay(HJ_AudioManager.CatSound.Cat_Slow);
            }

        }
        else
        {
            HJ_AudioManager.Instance.CatPlay(HJ_AudioManager.CatSound.Cat_TimeAttack);
        }
    }

	IEnumerator CatMoving()
	{
		//블록이 x축에서 생성 되었을 경우
		if (shBlock.isX)
		{
			//나의 위치로 부터 -4만큼 이동된 위치를 저장
			Vector3 vecTargetPos = vecThisOrigin - new Vector3(4.0f, 0, 0);

			//나의 위치와 이동할 위치의 차이가 0.2(양수)만큼 날때 까지
;			while (Vector3.Distance(this.transform.position, vecTargetPos) > 0.3f) // 0.02f로 하였을때 매우 적은 값에 의해 터널링이 발생
			{
				this.transform.Translate(Vector3.back * fLerpSpeed * Time.deltaTime); //블럭을 움직인다
				yield return null; //업데이트에게 순서를 넘겨주기
			}
			this.transform.position = vecTargetPos; //나의 위치는 목표거리가 된다
		}
		//블록이 z축에서 생성 되었을 경우
		else if (shBlock.isZ)
		{
			Vector3 vecTargetPos = vecThisOrigin - new Vector3(0, 0, 4.0f);
			// 나와 목표까지의 거리가
			// 0.2f보다 클때까지 while문이 돌고
			// while문이 끝나면
			// 나의 위치는 목표 거리가 되어라
			while (Vector3.Distance(this.transform.position, vecTargetPos) > 0.3f)
			{
				this.transform.Translate(Vector3.back * fLerpSpeed * Time.deltaTime);
				yield return null;
			}
			this.transform.position = vecTargetPos;
		}

	    riKinematic.isKinematic = false; //그 후 키네메틱을 꺼주어서 블럭을 안착하게 만든다.
		HJ_ScoreManager.Instance.Score++;
		shBlock.isCreate = false;
		//isOnce = false;

		// 엔젤 냥이일때
		if (shBlock.eCatState == SH_BlockCreate.ECATSATATE.ANGEL && riKinematic.isKinematic == false)
		{
			//엔젤 냥이 답게 hp풀 회복
			HJ_GameManager.Instance.hp = HJ_GameManager.Instance.MaxHP;
		}
			

		yield return new WaitForSeconds(0.5f);

		//블록이 안착 한 후의 위치를 한번만 저장하는 Position
		fTempPos1 = transform.GetChild(0).transform.position;

        // 타워 갱신
		// 타워의 최고 높이가 갱신된 높이보다 적다면? 
        if (HJ_GameManager.Instance.towerHeight < fTempPos1.y)
        {
            HJ_GameManager.Instance.towerHeight = fTempPos1.y; //타워 최고높이 갱신

			//블록이 나오는 위치도 올려주며 갱신
            shBlock.tPosX.transform.position = new Vector3(shBlock.tPosX.transform.position.x, HJ_GameManager.Instance.towerHeight + 1.7f, shBlock.tPosX.transform.position.z);
            shBlock.tPosZ.transform.position = new Vector3(shBlock.tPosZ.transform.position.x, HJ_GameManager.Instance.towerHeight + 1.7f, shBlock.tPosZ.transform.position.z);
		}
    }

	IEnumerator TimeAttack()
	{
		TimeCount.gameObject.SetActive(true);
		fTimeCost = 5.0f;

		while (fTimeCost > 0)
		{
			fTimeCost -= Time.deltaTime;

			a = Mathf.Ceil(fTimeCost);

			TimeCount.text = "" + a;

			if (HJ_GameManager.Instance.isPCTest)
			{
				if (Input.GetMouseButtonDown(0))
				{
					yield return null;
				}
			}
			yield return null;
		}

		TimeCount.gameObject.SetActive(false);

		if (fTimeCost <= 0)
		{
			fTimeCost = 0;
			a = 0;
			isMoving = false;
			vecThisOrigin = this.transform.position;
			StartCoroutine("CatMoving");// ""로 써야 StopCorutine이 먹는다		
		}
	}

}
