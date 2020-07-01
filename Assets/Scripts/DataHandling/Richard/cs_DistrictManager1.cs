using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class cs_DistrictManager1 : MonoBehaviour
{
    public static cs_DistrictManager1 Instance;

    //Somewhere to place all districts
    [SerializeField]
    public List<scr_District> Districts = new List<scr_District>();

    //the actual districts
    [SerializeField]
    public List<CityDistrict1> m_cityDistricts = new List<CityDistrict1>();

    //district specific information
    [SerializeField, System.Serializable]
    public class CityDistrict1
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
            CityDistrict1 city = new CityDistrict1();
            city.m_districtName = Districts[i].name;
            city.m_districtLocation = Districts[i];
            m_cityDistricts.Add(city);
        }
        m_cityDistricts.Sort((x, y) => y.m_districtName.CompareTo(x.m_districtName));
    }

    //fills each district with information
    public void AssignTexts()
    {
        foreach (CityDistrict1 item in m_cityDistricts)
        {
            item.m_districtLocation.AssignMe(item.m_districtCases, item.m_districtName);
        }
    }
}