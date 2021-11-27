using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandleUI : MonoBehaviour
{
    [SerializeField] GameObject roleta, spinButton, selectTimer, inputField;
    [SerializeField] TextMeshProUGUI TimerText;
    [SerializeField] float timerValue = 90;

    float speedSpin;
    bool spin;
    bool startTimer;

    private void Update()
    {
        Timer();
        Spin();
    }

    void Timer()
    {
        if (startTimer)
        {

            if(timerValue > 0)
            {
                timerValue -= Time.deltaTime;
            }
            else
            {
                timerValue = 0;
                spinButton.GetComponent<Button>().interactable = true;
                startTimer = false;
            }

            DisplayTime(timerValue);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
            timeToDisplay = 0;

        float Hours = Mathf.FloorToInt((timeToDisplay / 3600) % 24);
        float minutes = Mathf.FloorToInt((timeToDisplay / 60) % 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        TimerText.text = string.Format("{0:0}:{1:00}:{2:00}", Hours, minutes, seconds);
    }

    void Spin()
    {
        if (!spin)
            return;

        roleta.transform.Rotate(new Vector3(0, 0, 1), speedSpin * Time.deltaTime);
        speedSpin = Mathf.Lerp(speedSpin, 0, Time.deltaTime);

        if (speedSpin < 0.2f)
            spin = false;
    }

    public void StartSpin()
    {
        speedSpin = Random.Range(5000, 10000);
        spinButton.GetComponent<Button>().interactable = false;
        spin = true;
    }

    public void SelectHour()
    {
        GameObject obj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        timerValue = obj.GetComponentInChildren<SelectHours>().realHours;

        DisplayTime(timerValue);     
    }

    public void StartTimer()
    {
        startTimer = true;
        selectTimer.GetComponent<Button>().interactable = false;
        inputField.GetComponent<TMP_InputField>().interactable = false;
    }
}
