using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.GraphicRaycaster;

//HK 
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    
    
    public GameObject playerPrefab; // 플레이어 프리팹 -> 17번 튜토리얼때 스폰 됨
    public Vector2 fixedPlayerPosition; // 플레이어 스폰 고정 위치
    public GameObject[] tutorialObjects; // 튜토리얼에 사용될 오브젝트 리스트
    public Button tutorialButton; //튜토리얼 스텝을 트리거 할 버튼 오브젝트
    private int currentStep = 0;

    private int[] sensorStepMapping = { 20, 21, 22, 19, 23, 24, 25};
    //센서가 총 7개 존재 한다는 뜻. 센서에 인덱스가 붙어있으며, 센서와 트리거 되는 Step의 순서가 1:1 로 부착 되어있다는 뜻!

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        ShowStep(currentStep);
    }

    public void NextStep()
    {
        HideStep(currentStep);
        currentStep++;
        ShowStep(currentStep);
        Debug.Log("NextStep - stepIndex: " + currentStep);
    }

    private void ShowStep(int stepIndex)
    {
        //튜토리얼 종료 후 데이 2로 넘어간다.
        if (stepIndex >= 25)
        {
            UIManager.instance.Tutorial_Reset();
            instance = null;
            GameManager.Instance.CompleteTutorial();
        }
        
        if (stepIndex >= 0 && stepIndex < tutorialObjects.Length)
        {
            // 기본 출력 함수
            for (int i = 0; i < tutorialObjects.Length; i++)
            {
                tutorialObjects[i].SetActive(i == stepIndex);
            }

            //(특수 스텝)1 ~ 8 스텝 까지 상점 UI를 유지 하는 기능
            if ((stepIndex >= 1 && stepIndex <= 8))
            {
                tutorialObjects[1].SetActive(true);
                UIManager.instance.Tutorial_Shop();

            }

            //(특수 스텝) 12 ~ 15 스텝 동안 낮 맵을 유지
            if ((stepIndex >= 12 && stepIndex <= 15))
            {
                tutorialObjects[12].SetActive(true);
            }

            //(특수스텝) 17 스텝(밤 맵 등장)에 플레이어를 스폰 하고 밤 전용 카메라 크기를 적용 한다.
            if (stepIndex == 17)
            {
                
                // Player Spawn
                GameObject playerInstance = Instantiate(playerPrefab, fixedPlayerPosition, Quaternion.identity);

                //Night Camera
                Camera.main.orthographicSize /= 4.0f;
            }

            //(특수스텝) 17 ~ 26 스텝 동안 밤 맵을 유지
            if ((stepIndex >= 17 && stepIndex <= 26))
            {
                tutorialObjects[17].SetActive(true);                      
            }

            //(특수스텝) 17 스텝(밤 맵) 이 되면 튜토리얼 버튼을 비활성화 한다.
            if (stepIndex == 17)
            {
                tutorialButton.gameObject.SetActive(false);
            }
            //(마지막 특수스텝) 9 ~ 11 스텝 동안 나침반 오브젝트를 유지 한다.        
            else if ((stepIndex >= 9 && stepIndex <= 11))
            {
                UIManager.instance.Tutorial_Game();
                tutorialObjects[9].SetActive(true); 
            }


        }

    }

    public void ActiveButton()
    {
        tutorialButton.gameObject.SetActive(true);
    }


    private void HideStep(int stepIndex)
    {
        //1, 8, 12, 15 일때 빼고 남은 스텝들은 다음 스텝이 되는 즉시 사라진다!
        if (stepIndex != 1 && stepIndex != 8 && stepIndex != 12 && stepIndex != 15)
        {
            tutorialObjects[stepIndex].SetActive(false);
        }
    }

    //위의 센서 매핑에 부여한 타겟 스텝 을 부착 해준다!
    public void GoToSensorStep(int sensorID)
    {
        if (sensorID >= 0 && sensorID < sensorStepMapping.Length)
        {
            int targetStep = sensorStepMapping[sensorID];
            HideStep(currentStep);
            ShowStep(targetStep);
        }
    }

}

