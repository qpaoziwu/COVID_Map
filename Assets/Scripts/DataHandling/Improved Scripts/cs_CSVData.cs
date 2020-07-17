using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// All data is pulled at start and the last column of cases by date is automatically selected.
/// Requires a slider and TextMeshProUGUI
/// Use a slider to navigate the different cases by date.
/// 
/// >>> RICHARD
/// You will be referencing m_caseCountBySelectedDate
/// </summary>
public class cs_CSVData : MonoBehaviour
{
    [Header("Data")]

    [Tooltip("All of the ''Case Count MM/DD/YYYY'' columns")]
    public List<string> m_CSVDates;

    [Tooltip("All districts with their names, selected date, selected case count, and all of the ''Case Count MM/DD/YYYY''s specific to that district")]
    public List<Districts> m_CSVData;

    [Header("Setup Inputs")]

    [Tooltip("Insert the link to the csv here")]
    public string m_CSVUrl;

    [Tooltip("Insert the timeline Slider\nSet the ''Direction'' to ''Right To Left''\nSet the ''Min Value'' to 0\nCheck the ''Whole Numbers'' box\nUse the ''On Value Changed(Single)'' event on the slider to call the ''SelectedData()'' method from this script")]
    public Slider m_timelineSlider;

    [Tooltip("Insert the GameObject with the script cs_Timeline")]
    public cs_Timeline m_timeline;

    [Tooltip("Insert text field to display the selected date\nMust be Text Mesh Pro")]
    public TMPro.TextMeshProUGUI sliderText;

    private void Start()
    {
        StartCoroutine(GetData());
    }

    /// <summary>
    /// Reads all of the data in the csv, adding the districts and assigning the values found to each
    /// </summary>
    public IEnumerator GetData()
    {
        UnityWebRequest p_www = UnityWebRequest.Get(m_CSVUrl);      // get csv from server
        yield return p_www.SendWebRequest();        // return csv

        #region If Error
        /// <summary>
        /// if the link is dead
        /// </summary>
        if (p_www.isNetworkError)
        {
            //tell me if there's an error
            Debug.Log(p_www.error);
        }
        else
        #endregion
        {
            string[] p_data = p_www.downloadHandler.text.Split(new char[] { '\n' });        // create an array for each line (new column) from server csv
            string[] p_dataHeaders = p_data[0].Split(new char[] { ',' });       // split the headers of each column separately

            #region Get Data
            for (int i = 1; i < p_data.Length - 2; i++)     // go through all of the data except for the first (Neighbourhood Name) and last (missing data & empty) columns
            {
                string[] p_row = p_data[i].Split(new char[] { ',' });       // create an array for each comma (data cell / row)

                if (p_row[1] != "")     // if list item doesn't have a valid name then skip
                {
                    Districts p_newData = new Districts();
                    p_newData.m_caseCountBySelectedDate = new List<Cases>();
                    m_CSVDates = new List<string>();

                    for (int y = 0; y < p_row.Length; y++)
                    {
                        Cases p_newCases = new Cases();

                        if (p_dataHeaders[y] == "Neighbourhood Name")
                        {
                            p_newData.m_districtName = p_row[y];        // assign district names for every row under the "Neighbourhood Name" column
                        }

                        else       // the remaining columns are "Case Count MM/DD/YYYY"
                        {
                            m_CSVDates.Add(p_dataHeaders[y]);       // adding the "Case Count MM/DD/YYYY" columns to the m_CSVDates list

                            p_newCases.m_date = p_dataHeaders[y];       // assigning the "Case Count MM/DD/YYYY" columns to each district
                            int p_amount = 0;
                            int.TryParse(p_row[y], out p_amount);       // assigning an integer the case count

                            p_newCases.m_cases = p_amount;      // assigning the case count to each district
                            p_newData.m_caseCountBySelectedDate.Add(p_newCases);        // adding the district information to the nested list "Cases" in "Districts"

                            p_newData.m_dateSelected = p_newData.m_caseCountBySelectedDate[0].m_date;       // makes the selected date on open current, which right now is the very first [0] "Case Count MM/DD/YYY" column in the csv
                            p_newData.m_casesBySelectedDate = p_newData.m_caseCountBySelectedDate[0].m_cases;       // makes the selected cases on open current, which right now is the very first [0] "Case Count MM/DD/YYY" column in the csv
                        }

                    }
                    m_CSVData.Add(p_newData);       // add all of the district data to the "Districts" list
                    m_CSVData.Sort((x, y) => y.m_districtName.CompareTo(x.m_districtName));     //sorting the list in reverse alphabetical order
                }
            }
            #endregion
        }
        m_timelineSlider.maxValue = m_CSVDates.Count - 1;       //set the timeline value to the amount of "Case Count MM/DD/YYYY" columns. Always minus 1 because sliders don't recognize 0
        m_timeline.m_tickAmount = m_CSVDates.Count - 2;     // sets the amount of ticks on the timeline by amount of dates in CSV minus the two that are pre-placed
        m_timeline.SpawnTicks();      // calls the spawn ticks method

        sliderText.text = m_CSVDates[(int)m_timelineSlider.value]; int foundS1 = sliderText.text.IndexOf(" "); int foundS2 = sliderText.text.IndexOf(" ", foundS1 + 1); sliderText.text = sliderText.text.Remove(0, foundS2);       // very specific, finds the first and second space in the "Case Count MM/DD/YYYY" and deletes the string from the first character to the second space to only display the date string
    }

    /// <summary>
    /// Changes the selected date and cases of all districts by their element interger to match the sliders current integer.
    /// Use the On Value Changed(Single) event on Slider to call this method
    /// </summary>
    public void SelectedDate()
    {
        sliderText.text = m_CSVDates[(int)m_timelineSlider.value]; int foundS1 = sliderText.text.IndexOf(" "); int foundS2 = sliderText.text.IndexOf(" ", foundS1 + 1); sliderText.text = sliderText.text.Remove(0, foundS2);       // very specific, finds the first and second space in the "Case Count MM/DD/YYYY" and deletes the string from the first character to the second space to only display the date string

        foreach (Districts p_item in m_CSVData)
        {
            p_item.m_casesBySelectedDate = p_item.m_caseCountBySelectedDate[(int)m_timelineSlider.value].m_cases;       // updates the currently selected cases for all districts
            p_item.m_dateSelected = p_item.m_caseCountBySelectedDate[(int)m_timelineSlider.value].m_date;       // updates the currently selected date for all districts
        }
    }
}


/// <summary>
/// Holds all the city district's data
/// </summary>
[System.Serializable]
public class Districts
{
    public string m_districtName;       // this district's name
    public string m_dateSelected;       // the currently selected date
    public int m_casesBySelectedDate;       // the case count for the selected date in the district
    public List<Cases> m_caseCountBySelectedDate; // a list of all of the cases per date for this district
}

/// <summary>
/// Holds each district's seperate case count by date data
/// </summary>
[System.Serializable]
public class Cases
{
    public string m_date;       // the date of the data
    public int m_cases;     // the cases for this date in this district
}