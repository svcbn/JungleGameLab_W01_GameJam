using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.GraphicRaycaster;

public class TutorialManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Vector2 fixedPlayerPosition;
    public GameObject[] tutorialObjects; 
    public string[] soundEvent;
    public Button tutorialButton;
    private int currentStep = 0;

    private int[] sensorStepMapping = { 20, 21, 22, 19, 23, 24, 25};


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
        if (stepIndex >= 26)
        {
            GameManager.instance.CompleteTutorial();
        }
        
        if (stepIndex >= 0 && stepIndex < tutorialObjects.Length)
        {
            for (int i = 0; i < tutorialObjects.Length; i++)
            {
                tutorialObjects[i].SetActive(i == stepIndex);
            }

            if ((stepIndex >= 1 && stepIndex <= 8))
            {
                tutorialObjects[1].SetActive(true);
                Debug.Log("Previous Step - stepIndex: 1");

            }

            if ((stepIndex >= 12 && stepIndex <= 15))
            {
                tutorialObjects[12].SetActive(true);
                Debug.Log("Previous Step - stepIndex: 1");
            }

            if (stepIndex == 17)
            {
                // Player Spawn
                GameObject playerInstance = Instantiate(playerPrefab, fixedPlayerPosition, Quaternion.identity);

                //Night Camera
                Camera.main.orthographicSize /= 4.0f;
            }

            if ((stepIndex >= 17 && stepIndex <= 26))
            {
                tutorialObjects[17].SetActive(true);
                tutorialButton.GetComponent<Image>().enabled = false;
            }

            else if ((stepIndex >= 9 && stepIndex <= 11))
            {
                tutorialObjects[9].SetActive(true); 
            }


        }

    }


    private void HideStep(int stepIndex)
    {
        if (stepIndex != 1 && stepIndex != 8)
        {
            tutorialObjects[stepIndex].SetActive(false);
        }

        if (stepIndex != 12 && stepIndex != 15)
        {
            tutorialObjects[stepIndex].SetActive(false);
        }

        if (stepIndex == 8)
        {
            tutorialObjects[1].SetActive(false);
            tutorialObjects[8].SetActive(false);
        }

        else if (stepIndex == 16)
        {
            tutorialObjects[12].SetActive(false);
            tutorialObjects[15].SetActive(false);
        }
    }

    public void GoToSensorStep(int sensorID)
    {
        if (sensorID >= 0 && sensorID < sensorStepMapping.Length)
        {
            int targetStep = sensorStepMapping[sensorID];
            HideStep(currentStep);
            ShowStep(targetStep);
        }
    }

    private void ChangeButtonColor(Button button, Color color)
    {
        if (button != null && button.gameObject.activeSelf)
        {
            ColorBlock colorBlock = button.colors;
            colorBlock.normalColor = color;
            button.colors = colorBlock;
        }
    }

}

