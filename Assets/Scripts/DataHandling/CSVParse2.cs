using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CSVParse2 : MonoBehaviour
{
    /// <summary>
    /// The csv file can be dragged throughthe inspector.
    /// </summary>
    public string csvUrl = "https://089d93ed-979f-46ae-a3d8-7fa7699115b6.filesusr.com/ugd/f3c206_be4332325bf346cdab8fe0dbafd0c3fe.csv?dn=Test_Sheet1.csv";

    /// <summary>
    /// The grid in which the CSV File would be parsed.
    /// </summary>
    string[,] grid;

    void Start()
    {
        StartCoroutine(GetCSV());
    }

    IEnumerator GetCSV()
    {
        //get csv from server
        UnityWebRequest www = UnityWebRequest.Get(csvUrl);
        www.downloadHandler = new DownloadHandlerBuffer();
        //return csv
        yield return www.SendWebRequest();

        grid = getCSVGrid(www.downloadHandler.text);
    }

    /// <summary>
    /// splits a CSV file into a 2D string array
    /// </summary>
    /// <returns> 2 day array of the csv file.</returns>
    /// <param name="csvText">the CSV data as string</param>
    static public string[,] getCSVGrid(string csvText)
    {
        //split the data on split line character
        string[] lines = csvText.Split("\n"[0]);

        // find the max number of columns
        int totalColumns = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string[] row = lines[i].Split(',');
            totalColumns = Mathf.Max(totalColumns, row.Length);
        }

        // creates new 2D string grid to output to
        string[,] outputGrid = new string[totalColumns + 1, lines.Length + 1];
        for (int y = 0; y < lines.Length; y++)
        {
            string[] row = lines[y].Split(',');
            for (int x = 0; x < row.Length; x++)
            {
                outputGrid[x, y] = row[x];
            }
        }

        return outputGrid;
    }

    /// <summary>
    /// Gets the value from the CSV File at index(row,col).
    /// </summary>
    /// <param name="row">Row.</param>
    /// <param name="col">Col.</param>
    void getValueAtIndex(int row, int col)
    {
        Debug.Log(grid[row, col]);
    }

    /// <summary>
    /// outputs the content of a 2D array.
    /// </summary>
    /// <param name="grid">2D array , here the CSV grid.</param>
    static public void DisplayGrid(string[,] grid)
    {
        string textOutput = "";
        for (int y = 0; y < grid.GetUpperBound(1); y++)
        {
            for (int x = 0; x < grid.GetUpperBound(0); x++)
            {

                textOutput += grid[x, y];
                textOutput += ",";
            }
            textOutput += "\n";
        }
        Debug.Log(textOutput);
    }
    public void DoTheThing()
    {
        getValueAtIndex(0, 0);
    }
}
