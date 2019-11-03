using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Clock : MonoBehaviour
{
    public TMP_Text clock;
    int hours, minutes;
    float timer = 0.0f;
    int startHour;

    // Start is called before the first frame update
    void Start()
    {
        startHour = DateTime.Now.Hour;
        hours = startHour;
    }

    // Update is called once per frame
    void Update()
    {
        CountTime();
    }

    void CountTime()
    {

        timer += Time.deltaTime;
        minutes = Convert.ToInt32(timer % 60);

        if (minutes >= 60)
        {
            timer = 0.0f;
            minutes = 0;
            hours++;
        }
            

        clock.text = LeadingZero(hours) + ":" + LeadingZero(minutes);
    }

    string LeadingZero(int time)
    {
        return time.ToString().PadLeft(2, '0');
    }
}
