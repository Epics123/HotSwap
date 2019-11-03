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
    int startHour, endHour, halfway;

    // Start is called before the first frame update
    void Start()
    {
        startHour = DateTime.Now.Hour;
        hours = startHour;
        endHour = startHour + 2;
        halfway = endHour - 1;
    }

    // Update is called once per frame
    void Update()
    {
        CountTime();
    }

    void CountTime()
    {
        if(hours != endHour)
        {
            timer += Time.deltaTime;
            minutes = Convert.ToInt32(timer % 60);

            if (minutes >= 60)
            {
                timer = 0.0f;
                minutes = 0;
                hours++;
            }

            if (hours == halfway && minutes > 30)
            {
                ChangeClockColor();
                FlashText();
            }

            if (hours >= endHour)
                clock.alpha = 1.0f;

            clock.text = LeadingZero(hours) + ":" + LeadingZero(minutes);
        }
    }

    string LeadingZero(int time)
    {
        return time.ToString().PadLeft(2, '0');
    }

    void ChangeClockColor()
    {
        clock.color = Color.red;
    }

    void FlashText()
    {
        clock.alpha = Mathf.PingPong(Time.time * 2, 1.0f);
    }
}
