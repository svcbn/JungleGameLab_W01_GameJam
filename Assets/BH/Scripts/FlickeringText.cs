using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlickeringText : MonoBehaviour
{
    float speed = 1f;
    float alpha = 0f;

    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        alpha = Mathf.Sin(speed * Time.time);
        text.color = new Color(1, 1, 1, Mathf.Abs(alpha));
    }
}