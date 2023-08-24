using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EyeFlickering : MonoBehaviour
{
    float intensity = 0.35f;
    Vignette vignette;
    [SerializeField] bool isClose = false;

    // Start is called before the first frame update
    void Start()
    {
        vignette = GetComponent<PostProcessVolume>().profile.GetSetting<Vignette>();
        StartCoroutine(EyeBlinking());
    }

    IEnumerator EyeBlinking()
    {
        

        while (true)
        {
            if(isClose)
            {
                intensity = Mathf.Lerp(intensity, 0.65f, Time.deltaTime * 0.8f);
            }
            else
            {
                intensity = Mathf.Lerp(intensity, 0.15f, Time.deltaTime * 0.8f);
            }

            if(intensity > 0.6f)
            {
                isClose = false;
            }
            if(intensity < 0.2f)
            {
                isClose = true;
            }

            vignette.intensity.value = intensity;

            yield return null;
        }
    }
}
