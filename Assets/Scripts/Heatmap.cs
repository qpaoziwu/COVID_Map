// Alan Zucconi
// www.alanzucconi.com
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine; //Needed for struct layour
public class Heatmap : MonoBehaviour
{

    [StructLayout(LayoutKind.Sequential), System.Serializable] //So bit stream is fixed for the GPU
    [SerializeField]
    private struct HeatData
    {
        public Vector3 position;
        [Range(0f, 1f)]
        public float radius;
        [Range(0f, 1f)]
        public float intensity;

        public static int size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(HeatData)); //Needed for compute buffer
    }

    [StructLayout(LayoutKind.Sequential), System.Serializable] //So bit stream is fixed for the GPU
    [SerializeField]
    private struct StoredHeatData
    {
        public Vector3 position;
        [Range(0f, 1f)]
        public float radius;
        [Range(0f, 200f)]
        public float intensity;
        public static int size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(StoredHeatData)); //Needed for compute buffer
    }

    public DistrictReference Reference;

    //Material for Shader
    public Material material;

    [SerializeField]
    [Range(0f, 5f)]
    private float radiusRatio = 2.5f; //Increase the radius of each point 

    [SerializeField]
    [Range(0f, 200f)]
    private float intensityRatio = 50f; //Increase the stepping intensity

    private ComputeBuffer buffer;

    /*
        This is pre allocated larger than we need so we dont need to reallocate on gpu/cpu, having to increase this array requires allocating on both PUs
    */
    //[SerializeField]
    //private HeatData[] data = new HeatData[140];
    //[SerializeField]
    //private StoredHeatData[] storedData = new StoredHeatData[140];
    [SerializeField]
    private HeatData[] data;
    [SerializeField]
    private StoredHeatData[] storedData;


    //Size of the data array
    public int count = 0;
    public bool randomizePoints;


    //Reading Data
    void Start()
    {
        data = new HeatData[Reference.generatedPoints];
        storedData = new StoredHeatData[Reference.generatedPoints];
        buffer = new ComputeBuffer(Reference.generatedPoints, HeatData.size); //We can change count later but for now 100 per buffer
        material.SetBuffer("_HeatData", buffer); //Sets it up on the shader
        count = Reference.generatedPoints;

        //Hardcoded random test set
        for (int i = 0; i < data.Length; i++)
        {
            //    //Vector3 position = new Vector3(Random.Range(-1f, 1f),0, Random.Range(-.5f, .5f));
            //    //Vector3 position = new Vector3(Random.Range(-transform.localScale.x * .5f, transform.localScale.x * .5f), 0f, Random.Range(-transform.localScale.y * .5f, transform.localScale.y * .5f));

            //data[i].position = Reference.RefList[i].worldPos;
            data[i].position = Reference.PointFolder.transform.GetChild(i).position;
            data[i].radius = Random.value * this.transform.localScale.z; //Small demo size
            data[i].intensity = Random.value * this.transform.localScale.z;

            storedData[i].position = data[i].position;
            storedData[i].radius = data[i].radius;
            storedData[i].intensity = data[i].intensity;
        }
        

        material.SetInt("_Count", count);

        buffer.SetData(data); //SendsToShader, ***SETDATA() to change data
    }
    void Update()
    {
        Randomize();
        //Update buffer data each frame
        for (int i = 0; i < data.Length; i++)
        {
            //Vector3 position = new Vector3(data[i].position.x,transform.position.y, data[i].position.z);
            //Vector3 position = new Vector3(Random.Range(-transform.localScale.x * .5f, transform.localScale.x * .5f), 0f, Random.Range(-transform.localScale.y * .5f, transform.localScale.y * .5f));
            //Vector3 position = new Vector3(storedData[i].position.x, transform.position.y, storedData[i].position.z);
            //data[i].position = position;
            data[i].position = Reference.PointFolder.transform.GetChild(i).position;
            data[i].radius = storedData[i].radius * radiusRatio*0.1f; //Small demo size
            data[i].intensity = storedData[i].intensity * intensityRatio * 0.0001f* Mathf.Abs( Mathf.Clamp(Mathf.Sin(Time.time), 0.2f, 1f*Mathf.PerlinNoise(1f,1f)));

        }

        buffer.SetData(data);

    }
    void ReadDisrticts()
    {
        for (int i = 0; i < data.Length; i++)
        {


        }

    }
    void ReadEveryPoints()
    {
        //Hardcoded random test set
        for (int i = 0; i < data.Length; i++)
        {
            //    //Vector3 position = new Vector3(Random.Range(-1f, 1f),0, Random.Range(-.5f, .5f));
            //    //Vector3 position = new Vector3(Random.Range(-transform.localScale.x * .5f, transform.localScale.x * .5f), 0f, Random.Range(-transform.localScale.y * .5f, transform.localScale.y * .5f));

            //data[i].position = Reference.RefList[i].worldPos;
            data[i].position = Reference.PointFolder.transform.GetChild(i).position;
            data[i].radius = Random.value * this.transform.localScale.z; //Small demo size
            data[i].intensity = Random.value * this.transform.localScale.z;

            storedData[i].position = data[i].position;
            storedData[i].radius = data[i].radius;
            storedData[i].intensity = data[i].intensity;
        }

    }

    //For Debugging
    void Randomize()
    {
        if (randomizePoints)
        {
            for (int i = 0; i < data.Length; i++)
            {
                //Vector3 position = new Vector3(Random.Range(-1f, 1f),0, Random.Range(-.5f, .5f));
                Vector3 position = new Vector3(Random.Range(-transform.localScale.x * .5f, transform.localScale.x * .5f), 0f, Random.Range(-transform.localScale.y * .5f, transform.localScale.y * .5f));

                data[i].position = position;
                data[i].radius = Random.value * this.transform.localScale.z;//Small demo size
                data[i].intensity = Random.value * this.transform.localScale.z;
                storedData[i].position = data[i].position;
                storedData[i].radius = data[i].radius;
                storedData[i].intensity = data[i].intensity;
            }
            randomizePoints = false;
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