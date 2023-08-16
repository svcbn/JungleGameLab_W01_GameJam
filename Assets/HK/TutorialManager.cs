using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Vector2 fixedPlayerPosition;
    public GameObject[] tutorialObjects;
    public string[] soundEvent;
    private int currentStep = 0;
    private bool waitForCollision = false;

    private int[] sensorStepMapping = { 17, 19, 12 };

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
        if (stepIndex >= 0 && stepIndex < tutorialObjects.Length)
        {
            for (int i = 0; i < tutorialObjects.Length; i++)
            {
                tutorialObjects[i].SetActive(i == stepIndex);
            }

            if ((stepIndex >= 1 && stepIndex <= 8))
            {
                tutorialObjects[1].SetActive(true); // Step 1 활성화
                Debug.Log("Previous Step - stepIndex: 1");
            }

            if ((stepIndex >= 11 && stepIndex <= 14))
            {
                tutorialObjects[11].SetActive(true); // Step 1 활성화
                Debug.Log("Previous Step - stepIndex: 1");
            }

            if (stepIndex == 16)
            {
                // 튜토리얼 전용 플레이어 위치에 스폰
                GameObject playerInstance = Instantiate(playerPrefab, fixedPlayerPosition, Quaternion.identity);
                
                // 밤 버전 카메라
                Camera.main.orthographicSize /= 4.0f;
            }

            if ((stepIndex >= 16 && stepIndex <= 18))
            {
                tutorialObjects[16].SetActive(true); // Step 1 활성화
                Debug.Log("Previous Step - stepIndex: 1");
            }

            if (tutorialObjects[stepIndex].CompareTag("TutorialSensor")) // 튜토리얼 센서와 충돌했을 때
            {
                TutorialSensor sensor = tutorialObjects[stepIndex].GetComponent<TutorialSensor>();
                if (sensor != null)
                {
                    int sensorID = sensor.sensorID;
                    GoToSensorStep(sensorID);
                }
            }


            else if (stepIndex == 10)
            {
                tutorialObjects[9].SetActive(true); // Step 8 활성화
                Debug.Log("Previous Step - stepIndex: 8");
            }
        }
    }

    private void HideStep(int stepIndex)
    {
        if (stepIndex != 1 && stepIndex != 8)
        {
            tutorialObjects[stepIndex].SetActive(false);
            Debug.Log("HideStep - stepIndex: " + currentStep);
        }

        if (stepIndex != 11 && stepIndex != 14)
        {
            tutorialObjects[stepIndex].SetActive(false);
            Debug.Log("HideStep - stepIndex: " + currentStep);
        }

        if (stepIndex == 8)
        {
            tutorialObjects[1].SetActive(false);
            tutorialObjects[8].SetActive(false);
            Debug.Log("HideStep - stepIndex: " + currentStep);
        }

        else if (stepIndex == 15)
        {
            tutorialObjects[11].SetActive(false);
            tutorialObjects[14].SetActive(false);
            Debug.Log("HideStep - stepIndex: " + currentStep);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TutorialSensor")) // 튜토리얼 센서와 충돌했을 때만 동작
        {
            TutorialSensor sensor = other.GetComponent<TutorialSensor>();
            if (sensor != null)
            {
                int sensorID = sensor.sensorID;
                GoToSensorStep(sensorID);
            }
        }
    }

    private void GoToSensorStep(int sensorID)
    {
        if (sensorID >= 0 && sensorID < sensorStepMapping.Length)
        {
            int targetStep = sensorStepMapping[sensorID];
            HideStep(currentStep);
            ShowStep(targetStep);
        }
    }

}
