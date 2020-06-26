using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_District : MonoBehaviour
{
    public TMPro.TextMeshProUGUI districtName, districtCases;
    public void AssignMe(int p_caseCount, string p_districtName)
    {
        districtName.text = p_districtName;
        districtCases.text = p_caseCount.ToString();
    }
}