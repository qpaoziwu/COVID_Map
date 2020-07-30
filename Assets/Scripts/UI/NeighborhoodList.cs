using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NeighborhoodList : MonoBehaviour
{

    public cs_CSVData csvData;
    public GameObject neighborhoodList;
    public Button neighborhoodButton;
    public TextMeshProUGUI ugui;


    private int numberOfDistricts = 40;

    /*
    public class Districts
    {
        public string m_districtName;
        public int m_casesBySelectedDate;
    }
    */


    // Start is called before the first frame update
    void Start()
    {
        /*
        for (int i = 0; i < numberOfDistricts; i++)
        {
            Debug.Log("Add Button " + i);
            //Instantiate(GameObject neighborhoodButton);
        }
        */
        csvData = FindObjectOfType<cs_CSVData>();



    }

    public void PopulateTheNeighborhoodList()
    {
        foreach (cs_CSVData.Districts i in csvData.m_CSVData)
        {
            Button butt = Instantiate(neighborhoodButton, neighborhoodList.transform);
            ugui = butt.GetComponentInChildren<TextMeshProUGUI>();
            ugui.text = i.m_districtName.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
