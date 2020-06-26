using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_DistrictManager : MonoBehaviour
{
    public static cs_DistrictManager Instance;

    //Somewhere to place all districts
    [SerializeField]
    public List<scr_District> Districts = new List<scr_District>();

    //the actual districts
    [SerializeField]
    public List<CityDistrict> m_cityDistricts = new List<CityDistrict>();

    //district specific information
    [SerializeField, System.Serializable]
    public class CityDistrict
    {
        public string m_districtName;
        public scr_District m_districtLocation;
        public int m_districtCases;
    }

    //assigns all actual districts from list
    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < Districts.Count; i++)
        {
            CityDistrict city = new CityDistrict();
            city.m_districtName = Districts[i].name;
            city.m_districtLocation = Districts[i];
            m_cityDistricts.Add(city);
        }
    }

    //fills each district with information
    public void AssignTexts()
    {
        foreach (CityDistrict item in m_cityDistricts)
        {
            item.m_districtLocation.AssignMe(item.m_districtCases, item.m_districtName);
        }
    }
}