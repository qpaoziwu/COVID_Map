using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NeighborhoodList : MonoBehaviour
{
    [HideInInspector]
    public cs_CSVData m_csvData;

    [SerializeField, HideInInspector]
    public List<DistrictInfo> m_districtInfo;

    public GameObject m_neighborhoodList;
    public Button m_neighborhoodButton;
    public TextMeshProUGUI m_ugui;

    void Start()
    {
        m_csvData = FindObjectOfType<cs_CSVData>();
    }

    public void PopulateDistrictInfo()
    {
        foreach (cs_CSVData.Districts i in m_csvData.m_CSVData)
        {
            DistrictInfo p_newData = new DistrictInfo();
            p_newData.m_name = i.m_districtName;
            p_newData.m_cases = i.m_casesBySelectedDate;
            m_districtInfo.Add(p_newData);
        }

        m_districtInfo.Sort((x, y) => x.m_name.CompareTo(y.m_name));     //sorting the list in reverse alphabetical order

        PopulateNeighbourhoodList();
    }

    #region SortByAlpha
    public void SortByAlpha()
    {
        foreach (Transform child in m_neighborhoodList.transform)
        {
            Destroy(child.gameObject);
        }
        m_districtInfo.Sort((x, y) => x.m_name.CompareTo(y.m_name));     //sorting the list in alphabetical order
        PopulateNeighbourhoodList();
    }
    #endregion

    #region SortByRevAplha
    public void SortByRevAplha()
    {
        foreach (Transform child in m_neighborhoodList.transform)
        {
            Destroy(child.gameObject);
        }
        m_districtInfo.Sort((x, y) => y.m_name.CompareTo(x.m_name));     //sorting the list in reverse alphabetical order
        PopulateNeighbourhoodList();
    }
    #endregion

    #region SortByCases
    public void SortByCases()
    {
        foreach (Transform child in m_neighborhoodList.transform)
        {
            Destroy(child.gameObject);
        }
        m_districtInfo.Sort((x, y) => x.m_cases.CompareTo(y.m_cases));     //sorting the list in case count order
        PopulateNeighbourhoodList();
    }
    #endregion

    #region SortByRevCases
    public void SortByRevCases()
    {
        foreach (Transform child in m_neighborhoodList.transform)
        {
            Destroy(child.gameObject);
        }
        m_districtInfo.Sort((x, y) => y.m_cases.CompareTo(x.m_cases));     //sorting the list in reverse case count order
        PopulateNeighbourhoodList();
    }
    #endregion

    public void PopulateNeighbourhoodList()
    {
        foreach (DistrictInfo i in m_districtInfo)
        {
            Button p_newButton = Instantiate(m_neighborhoodButton, m_neighborhoodList.transform);
            m_ugui = p_newButton.GetComponentInChildren<TextMeshProUGUI>();
            m_ugui.text = i.m_name.ToString();
            p_newButton.name = i.m_name;
        }
    }

    [SerializeField, System.Serializable]
    public class DistrictInfo
    {
        public string m_name;
        public int m_cases;
    }
}