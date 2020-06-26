// Crated from tutorial https://www.youtube.com/watch?v=mAeTRCT0qZg
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Networking;

public class scr_TestScript : MonoBehaviour
{
    public string csvUrl = "https://089d93ed-979f-46ae-a3d8-7fa7699115b6.filesusr.com/ugd/f3c206_be4332325bf346cdab8fe0dbafd0c3fe.csv?dn=Test_Sheet1.csv";

    //keep a list of data
    List<scr_TestData> testData = new List<scr_TestData>();

    void Start()
    {
        //start the coroutine to find csv
        StartCoroutine(GetText());
        #region Find From Resources Alternative Option
        //TextAsset testSheet = Resources.Load<TextAsset>("csv_TestSheet1");                  //loading the data from resources folder

        //string[] data = testSheet.text.Split(new char[] { '\n' });                  //create an array for each line (new line)

        //for (int i = 1; i < data.Length - 1; i++)                   //load the data except for the first and last (empty) lines
        //{
        //    string[] row = data[i].Split(new char[] { ',' });                   //create an array for each comma (data cells)

        //    if (row[1] != "")                   //if list item doesn't have a valid name then skip
        //    {
        //        scr_TestData td = new scr_TestData();

        //        int.TryParse(row[0], out td.id);                    //look for data, if none leave default, if is then fill out
        //        td.CityBlock = row[1];                  //fill with string
        //        int.TryParse(row[2], out td.Cases);

        //        testData.Add(td);                   //add all parsed data to list
        //    }

        //}

        //foreach (scr_TestData td in testData)                   //debug the info written
        //{
        //    Debug.Log(td.CityBlock + "," + td.Cases);
        //}
        #endregion
    }

    IEnumerator GetText()
    {
        //get csv from server
        UnityWebRequest www = UnityWebRequest.Get(csvUrl);
        //return csv
        yield return www.SendWebRequest();

        #region If Error
        if (www.isNetworkError)
        {
            //tell me if there's an error
            Debug.Log(www.error);
        }
        else
        #endregion
        {
            //create an array for each line (new line) from server csv
            string[] data = www.downloadHandler.text.Split(new char[] { '\n' });

            //load the data except for the first and last (empty) lines
            for (int i = 1; i < data.Length - 1; i++)
            {
                //create an array for each comma (data cells)
                string[] row = data[i].Split(new char[] { ',' });

                //if list item doesn't have a valid name then skip
                if (row[1] != "")
                {
                    scr_TestData td = new scr_TestData();

                    //look for data, if none leave default, if is then fill out
                    int.TryParse(row[0], out td.id);
                    //fill with string
                    td.CityBlock = row[1];
                    int.TryParse(row[2], out td.Cases);

                    //add all parsed data to list
                    testData.Add(td);
                }
            }

            // debug the info written
            foreach (scr_TestData td in testData)
            {
                //debug line confirming data
                Debug.Log(td.CityBlock + ", " + td.Cases + " Cases");
            }
        }
    }
}
//    private void Update()
//    {
//        Debug.Log(testData[2].id);
//    }
//}