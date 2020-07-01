using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_DateSelect : MonoBehaviour
{
    public int m_maxDate;
    public int m_minDate;

    public int m_currentDate;
    public TMPro.TextMeshProUGUI m_currentDateText;

    void Start()
    {
        SetDate();
    }
    public void CountUp()
    {
        if (m_currentDate < m_maxDate)
        {
            m_currentDate++;
            SetDate();
        }
        else { m_currentDate = m_minDate; SetDate(); }
    }
    public void CountDown()
    {
        if (m_currentDate > m_minDate)
        {
            m_currentDate--;
            SetDate();
        }
        else { m_currentDate = m_maxDate; SetDate(); }
    }

    void SetDate()
    {
        m_currentDateText.text = m_currentDate.ToString();
    }
}
