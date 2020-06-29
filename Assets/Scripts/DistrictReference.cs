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
    private GameObject PointFolder; //Border from scene

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
        public List<Transform> Points = new List<Transform>();
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
                DistList.Add(d);
            }
            //
            foreach (District a in DistList)
            {
                foreach (Transform t in a.Dist.transform)
                {

                    t.gameObject.name = a.Points.Count + " " + a.distName;
                    a.Points.Add(t);

                }
                for (int i = 0; i < a.Points.Count; i++)
                {
                    a.Points[i].parent = PointFolder.transform;
                }
            }
        }
    }

    //AOE Effect

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