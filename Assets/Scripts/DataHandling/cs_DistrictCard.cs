using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class cs_DistrictCard : MonoBehaviour
{
    public TextMeshProUGUI m_districtName;
    public TextMeshProUGUI m_districtCases;

    [HideInInspector]
    public DistrictReference myObject;

    private void Awake()
    {
        myObject = FindObjectOfType<DistrictReference>();
        name = transform.parent.name;

        foreach (DistrictReference.DistrictRef item in myObject.RefList)
        {
            if (item.distName == name)
            {
                m_districtCases.text = item.caseCount.ToString();
            }
        }
        m_districtName.text = name.ToString();
    }

    public void UpdateCases()
    {
        foreach (DistrictReference.DistrictRef item in myObject.RefList)
        {
            if (item.distName == name)
            {
                m_districtCases.text = item.caseCount.ToString();
            }
        }
    }

    private void Update()
    {
        #region Look at camera
        Vector3 v = Camera.main.transform.position - transform.position;

        v.x = v.z = 0.0f;
        transform.LookAt(Camera.main.transform.position - v);
        transform.rotation = (Camera.main.transform.rotation);
        #endregion
    }
}