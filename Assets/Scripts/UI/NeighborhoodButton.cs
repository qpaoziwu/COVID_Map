using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NeighborhoodButton : MonoBehaviour
{
    //[HideInInspector]
    public DistrictDetails m_detailPanel;

    private void Start()
    {
        m_detailPanel = FindObjectOfType<DistrictDetails>();
    }
    public void Clicked()
    {
        m_detailPanel.m_districtToBeCalled = name.ToString();
        m_detailPanel.UpdateDistrictDetails();
    }
}
