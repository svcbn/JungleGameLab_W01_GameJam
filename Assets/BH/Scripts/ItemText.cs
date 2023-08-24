using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemText : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(MoveUp());
    }

    IEnumerator MoveUp()
    {
        float currentTime = 0;


        while (currentTime < 1f)
        {
            currentTime += Time.deltaTime;
            transform.localPosition = transform.localPosition + new Vector3(0, 0.2f, 0);
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1 - currentTime);

            yield return null;
        }
        Destroy(gameObject);
    }
}