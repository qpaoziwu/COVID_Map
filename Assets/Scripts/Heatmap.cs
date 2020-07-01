using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine; //Needed for struct layour

public class Heatmap : MonoBehaviour
{
    public enum DisplayMode
    {
        RealtimeDataSet,
        TestDataSet
    };
    [SerializeField]
    public DisplayMode Mode = DisplayMode.RealtimeDataSet;

    public DistrictReference Reference;

    //Material for Shader
    public Material material;

    //Size of the total points
    public int count = 0;

    [SerializeField]
    [Range(0f, 5f)]
    private float radiusRatio = 2.5f;               //Increase the radius of each point 

    [SerializeField]
    [Range(0f, 20f)]
    private float intensityRatio = 50f;             //Increase the stepping intensity

    [StructLayout(LayoutKind.Sequential), System.Serializable] //So bit stream is fixed for the GPU
    [SerializeField]
    private struct HeatData                          //Data that are sent to the GPU
    {
        public Vector3 position;
        [Range(0f, 1f)]
        public float radius;
        [Range(0f, 200f)]
        public float intensity;
        public static int size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(HeatData)); //Needed for compute buffer
    }

    [System.Serializable, SerializeField]
    private struct StoredHeatData                     //Initial position of each point
    {
        public Vector3 position;
        [Range(0f, 1f)]
        public float radius;
        [Range(0f, 200f)]
        public float intensity;
    }

    [SerializeField]
    private HeatData[] data;                          //Data that are sent to the GPU

    [SerializeField]
    private StoredHeatData[] storedData;              //Initial position of each point

    int indexForPoints;
    int indexForDistricts;
    int intTracker;

    private ComputeBuffer buffer;

    /*
        This is pre allocated larger than we need so we dont need to reallocate on gpu/cpu, having to increase this array requires allocating on both PUs
    */

    //Reading Data
    void Start()
    {
        //Setting array size to grid size
        data = new HeatData[Reference.generatedPoints];
        storedData = new StoredHeatData[Reference.generatedPoints];
        buffer = new ComputeBuffer(Reference.generatedPoints, HeatData.size);
        count = Reference.generatedPoints;

        //Setting shader variables
        material.SetBuffer("_HeatData", buffer); //Sets it up on the shader
        material.SetInt("_Count", count);

        //Reading all positions from Reference
        for (int i = 0; i < data.Length; i++)
        {
            data[i].position = Reference.PointFolder.transform.GetChild(i).position;

            storedData[i].position = data[i].position;
            storedData[i].radius = data[i].radius;
            storedData[i].intensity = data[i].intensity;
        }
        //SyncPointsByDistrict();

        buffer.SetData(data); //SendsToShader, ***SETDATA() to change data
    }

    void Update()
    {
        SyncPointsByDistrict();
        buffer.SetData(data);
    }


    /*              Synchronize points by District
            To maximize performance, we loop thru all the points linearly;
          otherwise there will be 140 more reading loops to set all the points.
          Logic: Loop X amount of points where X is # of the DistList.Point.Count + previous counted points;
          After each loop, move to next district.
    */

    void SyncPointsByDistrict()
    {
        //Reset all index#
        indexForPoints = 0;
        indexForDistricts = 0;
        intTracker = Reference.RefList[indexForDistricts].Points.Count - 1;

        //Loop thru Districts, and move index# to next district-index# after every loop 
        for (int d = indexForDistricts; d < Reference.RefList.Count + intTracker; d++)
        {
            //Loop thru points, every new loop moves index# to next point-set
            for (int x = indexForPoints; x < intTracker; x++)
            {
                //Set points
                data[x].radius = CaseCount(indexForDistricts) * radiusRatio * 0.1f;
                data[x].intensity = 1 * intensityRatio * 0.1f;
                //data[x].intensity = 1 * intensityRatio * 0.01f * Mathf.Abs(Mathf.Clamp(Mathf.Sin(Time.time), 0.2f, 1f * Mathf.PerlinNoise(1f, 1f)));

                //if the index# is within the array size, keep counting
                if (indexForPoints < count)
                {
                    indexForPoints++;
                }
            }
            //if the index# is within the array size, keep counting
            if (indexForDistricts < Reference.RefList.Count - 1)
            {
                indexForDistricts++;
                intTracker += Reference.RefList[indexForDistricts].Points.Count;
            }
        }

    }

    private int CaseCount(int i)
    {
        if ((int)Mode == 0)
        {
            return Reference.RefList[i].caseCount;
        }
        if ((int)Mode == 1)
        {
            return indexForDistricts;
        }
        return 0;
    }

    
    //Statically Randomize Every Point 
    void ReadEveryPoints()
    {
        for (int i = 0; i < data.Length; i++)
        {
            //data[i].position = Reference.RefList[i].worldPos;
            data[i].position = Reference.PointFolder.transform.GetChild(i).position;
            data[i].radius = Random.value * this.transform.localScale.z;
            data[i].intensity = Random.value * this.transform.localScale.z;

            storedData[i].position = data[i].position;
            storedData[i].radius = data[i].radius;
            storedData[i].intensity = data[i].intensity;
        }
    }


    void OnDisable()
    {

        buffer.Dispose();

    }
    void OnApplicationQuit()
    {
        buffer.Dispose();
    }
}

//Update buffer data each frame
//for (int i = 0; i < data.Length; i++)
//{
//    //Vector3 position = new Vector3(data[i].position.x,transform.position.y, data[i].position.z);
//    //Vector3 position = new Vector3(Random.Range(-transform.localScale.x * .5f, transform.localScale.x * .5f), 0f, Random.Range(-transform.localScale.y * .5f, transform.localScale.y * .5f));
//    //Vector3 position = new Vector3(storedData[i].position.x, transform.position.y, storedData[i].position.z);

//    //data[i].position = storedData[i].position;
//    data[i].radius = storedData[i].radius * radiusRatio * 0.1f; //Small demo size
//    data[i].intensity = storedData[i].intensity * intensityRatio * 0.0001f * Mathf.Abs(Mathf.Clamp(Mathf.Sin(Time.time), 0.2f, 1f * Mathf.PerlinNoise(1f, 1f)));

//}
//For Debugging
//public bool randomizePoints;
//void Randomize()
//{
//    if (randomizePoints)
//    {
//        for (int i = 0; i < data.Length; i++)
//        {
//            //Vector3 position = new Vector3(Random.Range(-1f, 1f),0, Random.Range(-.5f, .5f));
//            Vector3 position = new Vector3(Random.Range(-transform.localScale.x * .5f, transform.localScale.x * .5f), 0f, Random.Range(-transform.localScale.y * .5f, transform.localScale.y * .5f));

//            data[i].position = position;
//            data[i].radius = Random.value * this.transform.localScale.z;//Small demo size
//            data[i].intensity = Random.value * this.transform.localScale.z;
//            storedData[i].position = data[i].position;
//            storedData[i].radius = data[i].radius;
//            storedData[i].intensity = data[i].intensity;
//        }
//        randomizePoints = false;
//    }

//}
