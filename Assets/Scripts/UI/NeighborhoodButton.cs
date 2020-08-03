using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NeighborhoodButton : MonoBehaviour
{
    //[HideInInspector]
    public DistrictDetails m_detailPanel;
    public CameraControl m_cameraControlScript;

    private void Start()
    {
        m_detailPanel = FindObjectOfType<DistrictDetails>();
        m_cameraControlScript = FindObjectOfType<CameraControl>();
    }
    public void Clicked()       // copy this for selecting district in map
    {
        m_detailPanel.m_districtToBeCalled = name.ToString();
        m_detailPanel.UpdateDistrictDetails();

        m_cameraControlScript.SwitchToZoom();
    }
}