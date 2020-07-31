using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NeighborhoodList : MonoBehaviour
{
    public cs_CSVData m_csvData;
    public GameObject m_neighborhoodList;
    public Button m_neighborhoodButton;
    public TextMeshProUGUI m_ugui;

    void Start()
    {
        m_csvData = FindObjectOfType<cs_CSVData>();
    }

    public void PopulateTheNeighborhoodList()
    {
        foreach (cs_CSVData.Districts i in m_csvData.m_CSVData)
        {
            Button p_newButton = Instantiate(m_neighborhoodButton, m_neighborhoodList.transform);
            m_ugui = p_newButton.GetComponentInChildren<TextMeshProUGUI>();
            m_ugui.text = i.m_districtName.ToString();
            p_newButton.name = i.m_districtName;
        }
    }
}