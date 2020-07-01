using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictReference : MonoBehaviour
{
    [SerializeField]
    public GameObject DistrictFolder; //Districts from scene
    [SerializeField]
    public GameObject BorderFolder; //Border from scene
    [SerializeField]
    public GameObject PointFolder; //Border from scene

    public int generatedPoints;

    public cs_DataSets1 dataset1;

    [SerializeField]
    public List<DistrictRef> RefList = new List<DistrictRef>(); //Stored List 
    [SerializeField]
    public List<GameObject> Districts = new List<GameObject>(); //Stored Districts 
    [SerializeField]
    public List<GameObject> Borders = new List<GameObject>(); //Stored Borders 


    [SerializeField, System.Serializable]
    public class DistrictRef
    {
        public string distName;
        public int caseCount;
        public Vector3 worldPos;
        public GameObject Dist;
        public GameObject Border;
        public List<Transform> Points = new List<Transform>();
    }

    public DistrictRef FindDistByName(string name)
    {
        for (int i = 0; i < RefList.Count; i++)
        {
            if (RefList[i].distName == name)
            {
                return RefList[i];
            }
            break;
        }
        return null;
    }

    public void UpdateCaseCount()
    {
        for (int c = 0; c < RefList.Count - 1; c++)
        {
            RefList[c].caseCount = dataset1.m_pulledData[c].m_caseCount;
        }
    }

    public List<Transform> FindPointsByName(string name)
    {
        for (int i = 0; i < RefList.Count; i++)
        {
            if (RefList[i].distName== name)
            {
                return RefList[i].Points;
            }
            break;
        }
        return null;
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
                DistrictRef d = new DistrictRef
                {
                    distName = Districts[i].name,
                    worldPos = Districts[i].transform.position,
                    Dist = Districts[i],
                    Border = Borders[i]
                };
                RefList.Add(d);
            }
            //Storing Points from scene
            foreach (DistrictRef a in RefList)
            {
                foreach (Transform t in a.Dist.transform)
                {
                    int c = a.Points.Count+1;
                    //Rename Points and Storing Points
                    t.gameObject.name = a.distName +" " + c;
                    a.Points.Add(t);
                }
                for (int i = 0; i < a.Points.Count; i++)
                {
                    //Moves Points to PointFolder
                    generatedPoints++;
                    a.Points[i].parent = PointFolder.transform;
                }
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