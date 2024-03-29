using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandleUI : MonoBehaviour
{
    [SerializeField] GameObject timerOBJ, rewardOBJ;
    [SerializeField] TextMeshProUGUI TimerText;
    [SerializeField] float timerValue = 50;

    [Header("ColorsReward")]
    [SerializeField] GameObject win_UI;
    [SerializeField] GameObject ColorReward;

    float speedSpin;
    float decreaseSpinTimer;
    bool spin;
    bool startTimer;
    bool stop;
    public static bool sound;

    public bool checkColors;

    public static HandleUI instance;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        Timer();
        Spin();
    }

    void Timer()
    {
        if (startTimer)
        {
            if (stop)
                return;

            if(timerValue > 0)
            {
                timerValue -= Time.deltaTime;
            }
            else
            {
                timerValue = 0;
                timerOBJ.transform.Find("Start").GetComponent<Button>().interactable = true;
                timerOBJ.transform.Find("Stop").GetComponent<Button>().interactable = false;

                timerOBJ.transform.Find("SelectTimer").Find("Button").GetComponent<Button>().interactable = true;
                timerOBJ.transform.Find("SelectTimer").Find("DropDownBox").gameObject.SetActive(false);
                rewardOBJ.transform.Find("InputField").GetComponent<TMP_InputField>().interactable = true;
                ActiveSpin(true);

                SoundsManager.instance.PlaySound(SoundsManager.instance.win, 1, 0.7f);

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

        rewardOBJ.transform.Find("Quadro").Find("Roleta").transform.Rotate(new Vector3(0, 0, 1), speedSpin * Time.deltaTime);
        speedSpin = Mathf.Lerp(speedSpin, 0, Time.deltaTime * decreaseSpinTimer);

        if (speedSpin < 0.5f)
        {
            checkColors = true;
            sound = false;
            spin = false;
        }
    }

    public void StartSpin()
    {
        speedSpin = Random.Range(5000, 10000);
        decreaseSpinTimer = Random.Range(0.5f, 2f);
        ActiveSpin(false);
        sound = true;
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
        Animator animInsertText = rewardOBJ.transform.parent.transform.Find("InsertText").GetComponent<Animator>();
        Animator animInsertTimer = timerOBJ.transform.Find("TimerCount").Find("Text").GetComponent<Animator>();

        if (timerValue == 0)
        {
            if (animInsertTimer.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
                animInsertTimer.SetTrigger("SelectTimer");
            return;
        }

        if (rewardOBJ.transform.Find("InputField").GetComponent<TMP_InputField>().text == "")
        {
            if (animInsertText.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
                animInsertText.SetTrigger("insertText");
           
            return;
        }

        startTimer = true;
        timerOBJ.transform.Find("Start").GetComponent<Button>().interactable = false;
        timerOBJ.transform.Find("Stop").GetComponent<Button>().interactable = true;

        timerOBJ.transform.Find("SelectTimer").Find("Button").GetComponent<Button>().interactable = false;
        timerOBJ.transform.Find("SelectTimer").Find("DropDownBox").gameObject.SetActive(false);
        rewardOBJ.transform.Find("InputField").GetComponent<TMP_InputField>().interactable = false;
    }

    public void StopTimer()
    {
        stop = !stop;

        timerOBJ.transform.Find("Stop").GetComponentInChildren<TextMeshProUGUI>().text = stop ? "continue" : "stop";
    }

    public void RestartTimer()
    {
        startTimer = false;
        stop = false;

        timerValue = 0;

        DisplayTime(timerValue);

        timerOBJ.transform.Find("Start").GetComponent<Button>().interactable = true;
        timerOBJ.transform.Find("Stop").GetComponent<Button>().interactable = false;

        timerOBJ.transform.Find("SelectTimer").Find("Button").GetComponent<Button>().interactable = true;
        timerOBJ.transform.Find("SelectTimer").Find("DropDownBox").gameObject.SetActive(false);
        rewardOBJ.transform.Find("InputField").GetComponent<TMP_InputField>().interactable = true;
        rewardOBJ.transform.Find("InputField").GetComponent<TMP_InputField>().text = "";
    }

    public void CallColorReward(string colorName)
    {
        win_UI.GetComponent<Animator>().Play("win");
        SoundsManager.instance.winPopUp();

        for (int i = 0; i < ColorReward.transform.childCount; i++)
        {
            ColorReward.transform.GetChild(i).gameObject.SetActive(false);
        }

        ColorReward.transform.Find(colorName).gameObject.SetActive(true);
    }

    public void ActiveSpin(bool isActive)
    {
        rewardOBJ.transform.Find("Spin").GetComponent<Button>().interactable = isActive;
    }
}
