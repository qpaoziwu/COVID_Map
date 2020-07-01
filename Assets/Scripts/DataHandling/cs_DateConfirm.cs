using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_DateConfirm : MonoBehaviour
{
    public cs_DataSets m_sets;

    public cs_DateSelect m_month;
    public cs_DateSelect m_day;
    public cs_DateSelect m_year;

    public string m_dateSelected;
    public bool m_dateChosen = false;

    public void SelectDate()
    {
        m_dateSelected = (m_month.m_currentDate + "/" + m_day.m_currentDate + "/" + m_year.m_currentDate).ToString();
        //Debug.Log(m_dateSelected);
        m_dateChosen = true;

        m_sets.m_pulledData.Clear();
        StartCoroutine(m_sets.GetData());
    }
}