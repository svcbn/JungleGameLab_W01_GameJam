using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.GraphicRaycaster;

//HK 
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    
    
    public GameObject playerPrefab; // �÷��̾� ������ -> 17�� Ʃ�丮�� ���� ��
    public Vector2 fixedPlayerPosition; // �÷��̾� ���� ���� ��ġ
    public GameObject[] tutorialObjects; // Ʃ�丮�� ���� ������Ʈ ����Ʈ
    public Button tutorialButton; //Ʃ�丮�� ������ Ʈ���� �� ��ư ������Ʈ
    private int currentStep = 0;

    private int[] sensorStepMapping = { 20, 21, 22, 19, 23, 24, 25};
    //������ �� 7�� ���� �Ѵٴ� ��. ������ �ε����� �پ�������, ������ Ʈ���� �Ǵ� Step�� ������ 1:1 �� ���� �Ǿ��ִٴ� ��!

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
        //Ʃ�丮�� ���� �� ���� 2�� �Ѿ��.
        if (stepIndex >= 25)
        {
            UIManager.instance.Tutorial_Reset();
            instance = null;
            GameManager.Instance.CompleteTutorial();
        }
        
        if (stepIndex >= 0 && stepIndex < tutorialObjects.Length)
        {
            // �⺻ ��� �Լ�
            for (int i = 0; i < tutorialObjects.Length; i++)
            {
                tutorialObjects[i].SetActive(i == stepIndex);
            }

            //(Ư�� ����)1 ~ 8 ���� ���� ���� UI�� ���� �ϴ� ���
            if ((stepIndex >= 1 && stepIndex <= 8))
            {
                tutorialObjects[1].SetActive(true);
                UIManager.instance.Tutorial_Shop();

            }

            //(Ư�� ����) 12 ~ 15 ���� ���� �� ���� ����
            if ((stepIndex >= 12 && stepIndex <= 15))
            {
                tutorialObjects[12].SetActive(true);
            }

            //(Ư������) 17 ����(�� �� ����)�� �÷��̾ ���� �ϰ� �� ���� ī�޶� ũ�⸦ ���� �Ѵ�.
            if (stepIndex == 17)
            {
                
                // Player Spawn
                GameObject playerInstance = Instantiate(playerPrefab, fixedPlayerPosition, Quaternion.identity);

                //Night Camera
                Camera.main.orthographicSize /= 4.0f;
            }

            //(Ư������) 17 ~ 26 ���� ���� �� ���� ����
            if ((stepIndex >= 17 && stepIndex <= 26))
            {
                tutorialObjects[17].SetActive(true);                      
            }

            //(Ư������) 17 ����(�� ��) �� �Ǹ� Ʃ�丮�� ��ư�� ��Ȱ��ȭ �Ѵ�.
            if (stepIndex == 17)
            {
                tutorialButton.gameObject.SetActive(false);
            }
            //(������ Ư������) 9 ~ 11 ���� ���� ��ħ�� ������Ʈ�� ���� �Ѵ�.        
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
        //1, 8, 12, 15 �϶� ���� ���� ���ܵ��� ���� ������ �Ǵ� ��� �������!
        if (stepIndex != 1 && stepIndex != 8 && stepIndex != 12 && stepIndex != 15)
        {
            tutorialObjects[stepIndex].SetActive(false);
        }
    }

    //���� ���� ���ο� �ο��� Ÿ�� ���� �� ���� ���ش�!
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

