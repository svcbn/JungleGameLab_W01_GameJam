using UnityEngine;

public class SensorHandler : MonoBehaviour
{
    public int sensorID; // 각 센서 오브젝트에 고유한 ID 부여

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌했을 때만 동작
        {
            TriggerSensor();
        }
    }

    public void TriggerSensor()
    {
        TutorialManager tutorialManager = FindObjectOfType<TutorialManager>();
        if (tutorialManager != null)
        {
            tutorialManager.GoToSensorStep(sensorID);
        }
    }
}
