using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/// <summary>
/// This script manages the time pictured on the clock
/// </summary>
public class Clock : MonoBehaviour
{
    public TMP_Text mClock;
    int mHours, mMinutes;
    float mTimer = 0.0f;
    int mStartHour, mEndHour, mHalfway;

    void Start()
    {
        mStartHour = DateTime.Now.Hour;
        mHours = mStartHour;
        mEndHour = mStartHour + 2;
        mHalfway = mEndHour - 1;
    }

    void Update()
    {
        CountTime();
    }

    void CountTime()
    {
        if(mHours != mEndHour)
        {
            mTimer += Time.deltaTime;
            mMinutes = Convert.ToInt32(mTimer % 60);

            if (mMinutes >= 60)
            {
                mTimer = 0.0f;
                mMinutes = 0;
                mHours++;
            }

            if (mHours == mHalfway && mMinutes > 30)
            {
                ChangeClockColor();
                FlashText();
            }

            if (mHours >= mEndHour)
                mClock.alpha = 1.0f;

            mClock.text = LeadingZero(mHours) + ":" + LeadingZero(mMinutes);
        }
    }

    string LeadingZero(int time)
    {
        return time.ToString().PadLeft(2, '0');
    }

    void ChangeClockColor()
    {
        mClock.color = Color.red;
    }

    void FlashText()
    {
        mClock.alpha = Mathf.PingPong(Time.time * 2, 1.0f);
    }
}
