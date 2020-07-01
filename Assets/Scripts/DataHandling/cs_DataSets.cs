﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class cs_DataSets : MonoBehaviour
{
    public List<DataContainer> m_pulledData;
    public string m_csvURL;

    public cs_DateConfirm m_dateSelect;

    private void Start()
    {
        StartCoroutine(GetData());
    }

    //finds city districts that have the same name (string) and gives them case counts
    private void AssignCityDistrict()
    {
        foreach (cs_DistrictManager.CityDistrict city in cs_DistrictManager.Instance.m_cityDistricts)
        {
            foreach (DataContainer p_datapoint in m_pulledData)
            {
                if (p_datapoint.m_districtName == city.m_districtName)
                {
                    city.m_districtCases = p_datapoint.m_caseCount;
                    break;
                }
            }
        }
        cs_DistrictManager.Instance.AssignTexts();
    }

    public IEnumerator GetData()
    {
        //get csv from server
        UnityWebRequest p_www = UnityWebRequest.Get(m_csvURL);
        //return csv
        yield return p_www.SendWebRequest();

        #region If Error
        if (p_www.isNetworkError)
        {
            //tell me if there's an error
            Debug.Log(p_www.error);
        }
        else
        #endregion
        {
            //create an array for each line (new line) from server csv
            string[] p_data = p_www.downloadHandler.text.Split(new char[] { '\n' });
            string[] p_dataHeaders = p_data[0].Split(new char[] { ',' });

            //load the data except for the first and last (empty) lines
            #region Today's Date
            if (m_dateSelect.m_dateChosen == false)
            {
                for (int i = 1; i < p_data.Length - 1; i++)
                {
                    //create an array for each comma (data cells)
                    string[] p_row = p_data[i].Split(new char[] { ',' });

                    //if list item doesn't have a valid name then skip
                    if (p_row[1] != "")
                    {
                        DataContainer p_newData = new DataContainer();

                        for (int y = 0; y < p_row.Length; y++)
                        {
                            //string dataW = dataHeaders[y];
                            //print(dataW);
                            if (p_dataHeaders[y] == "Neighbourhood Name")
                            {
                                p_newData.m_districtName = p_row[0];
                            }
                            //to know if the last coloum check if the NEXT is blank
                            else if (p_dataHeaders[y] == "Case Count " + /*System.DateTime.Now.ToShortDateString()*/ "6/26/2020" | p_dataHeaders[y] ==  "Case Count " + /*System.DateTime.Now.ToShortDateString()*/ "6/26/2020\r")
                            {
                                int p_amount = 0;
                                int.TryParse(p_row[y], out p_amount);
                                p_newData.m_caseCount = p_amount;
                            }

                        }
                        //look for data, if none leave default, if is then fill out
                        //add all parsed data to list
                        m_pulledData.Add(p_newData);
                    }
                }
            }
            #endregion
            #region Chosen Date
            else
            {
                for (int i = 1; i < p_data.Length - 1; i++)
                {
                    //create an array for each comma (data cells)
                    string[] p_row = p_data[i].Split(new char[] { ',' });

                    //if list item doesn't have a valid name then skip
                    if (p_row[1] != "")
                    {
                        DataContainer p_newData = new DataContainer();

                        for (int y = 0; y < p_row.Length; y++)
                        {
                            //string dataW = dataHeaders[y];
                            //print(dataW);
                            if (p_dataHeaders[y] == "Neighbourhood Name")
                            {
                                p_newData.m_districtName = p_row[0];
                            }
                            //to know if the last coloum check if the NEXT is blank
                            else if (p_dataHeaders[y] == "Case Count " + m_dateSelect.m_dateSelected | p_dataHeaders[y] == "Case Count " + m_dateSelect.m_dateSelected + "\r")
                            {
                                int p_amount = 0;
                                int.TryParse(p_row[y], out p_amount);
                                p_newData.m_caseCount = p_amount;
                            }

                        }
                        //look for data, if none leave default, if is then fill out
                        //add all parsed data to list
                        m_pulledData.Add(p_newData);
                    }
                }
            }
            #endregion
        }
        //start assign city block
        AssignCityDistrict();
    }
}

/// holds the type of data
[System.Serializable]
public class DataContainer
{
    public string m_districtName;
    public int m_caseCount;
}