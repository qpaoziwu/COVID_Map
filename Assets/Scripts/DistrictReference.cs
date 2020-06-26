using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictReference : MonoBehaviour
{
    [SerializeField]
    private GameObject DistrictFolder; //Districts from scene
    [SerializeField]
    private GameObject BorderFolder; //Border from scene
    [SerializeField]
    public List<GameObject> Districts = new List<GameObject>(); //Stored Districts 
    [SerializeField]
    public List<GameObject> Borders = new List<GameObject>(); //Stored Borders 
    [SerializeField]
    public List<District> DistList = new List<District>(); //Stored List 

    [SerializeField, System.Serializable]
    public class District
    {
        public string distName;
        public GameObject Dist;
        public GameObject Border;
        public int cases;
    }

    void Awake()
    {
        if (DistrictFolder && BorderFolder)
        {
            //Storing Districts from scene
            foreach (Transform d in DistrictFolder.transform)
            {
                Districts.Add(d.gameObject);
            }
            //Storing Borders from scene
            foreach (Transform b in BorderFolder.transform)
            {
                Borders.Add(b.gameObject);
            }
            //Storing all the infos into one list
            for (int i = 0; i < Districts.Count; i++)
            {
                District d = new District();
                d.distName = Districts[i].name;
                d.Dist = Districts[i];
                d.Border = Borders[i];
                d.cases = 0;
                DistList.Add(d);
            }
        }
    }
}
//DistList = new District[name,Dist,Border,cases];

//Districts.GetComponent<Renderer>();
// [SerializeField]
// struct District(string name, GameObject Dist, GameObject Border, int cases)
// {
//     public string n = name;
//     public GameObject D = Dist;
//     public GameObject B = Border;
//     public int c = cases;
// }


//District Distlist = new District();