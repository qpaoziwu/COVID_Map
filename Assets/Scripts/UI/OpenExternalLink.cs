using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenExternalLink : MonoBehaviour
{
    public string url;

    public string selectedDistrict;
    string findDistrict;

    public DistrictDetails districtDetails;
    public cs_CSVData m_csvData;


    private void Start()
    {
        districtDetails = FindObjectOfType<DistrictDetails>();
    }

    public void OpenURL()
    {

        selectedDistrict = districtDetails.m_title.text.ToString();

        url = "http://www.google.com/search?q=" + "Toronto " + selectedDistrict + " " + "COVID-19";

        Application.OpenURL(url);
    }
}
