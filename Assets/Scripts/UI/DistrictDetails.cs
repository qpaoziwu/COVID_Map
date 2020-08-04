using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistrictDetails : MonoBehaviour
{
    [HideInInspector]
    public string m_districtToBeCalled;
    [HideInInspector]
    public cs_CSVData m_csvData;

    public TextMeshProUGUI m_title;
    public TextMeshProUGUI m_bottomBarTitle;
    public TextMeshProUGUI m_bottomBarTitle2;
    public TextMeshProUGUI m_cases;
    public TextMeshProUGUI m_deaths;
    public TextMeshProUGUI m_recoveries;
    public TextMeshProUGUI m_area;
    public TextMeshProUGUI m_population;

    private void Awake()
    {
        m_csvData = FindObjectOfType<cs_CSVData>();
    }

    public void UpdateDistrictDetails()     // this is how we update the left panel
    {
        foreach (cs_CSVData.Districts i in m_csvData.m_CSVData)
        {
            if (m_districtToBeCalled == i.m_districtName.ToString())
            {
                m_title.text = i.m_districtName.ToString();
                m_bottomBarTitle.text = i.m_districtName.ToString();
                m_bottomBarTitle2.text = i.m_districtName.ToString();
                m_cases.text = i.m_casesBySelectedDate.ToString();
            }
        }
    }
}
