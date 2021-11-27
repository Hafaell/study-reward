using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectHours : MonoBehaviour
{
    [HideInInspector] public float realHours;
    float hoursTextToFloat;

    private void Start()
    {
        hoursTextToFloat = int.Parse(GetComponentInChildren<TextMeshProUGUI>().text);
        realHours = hoursTextToFloat * 3600;

        float Hours = Mathf.FloorToInt((realHours / 3600) % 24);
        float minutes = Mathf.FloorToInt((realHours / 60) % 60);
        float seconds = Mathf.FloorToInt(realHours % 60);

        GetComponentInChildren<TextMeshProUGUI>().text = string.Format("{0:0}:{1:00}:{2:00}", Hours, minutes, seconds);
    }
}
