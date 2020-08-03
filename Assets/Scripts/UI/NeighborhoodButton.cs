using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NeighborhoodButton : MonoBehaviour
{
    //[HideInInspector]
    public DistrictDetails m_detailPanel;
    public DistrictReference m_reference;
    public CameraControl m_cameraControlScript;
    public BorderVFXHandler m_borderVFXHandler;

    private void Start()
    {
        m_detailPanel = FindObjectOfType<DistrictDetails>();
        m_reference = FindObjectOfType<DistrictReference>();
        m_cameraControlScript = FindObjectOfType<CameraControl>();
        m_borderVFXHandler = FindObjectOfType<BorderVFXHandler>();

    }
    public void Clicked()       // copy this for selecting district in map
    {
        m_detailPanel.m_districtToBeCalled = name.ToString();
        m_detailPanel.UpdateDistrictDetails();

        //GameObject m_targetDistrict = null;
        for (int i = 0; i < m_reference.Districts.Count; i++)
        {
            if (m_reference.Districts[i].name == m_detailPanel.m_districtToBeCalled)
            {
                m_borderVFXHandler.LastClickedObject = m_reference.Districts[i];
                m_borderVFXHandler.SwitchDistrict(i);
                m_cameraControlScript.SwitchToZoom();// Swtich camera mode to zoomed
                break;
            }
        }
    }
}