// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using PclSharp;

// public class Test : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Start()
//     {
//         // PclTest.Load();
//         Load();
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     public static PointCloudOfXYZ cloud = new PointCloudOfXYZ();

//     public static PointCloudOfXYZ Load()
//     {
//         Debug.Log("Load()");
//         var file = "Assets/Resources/SampleModels/test.txt";
//         var reader = new PclSharp.IO.PCDReader();
//         reader.Read(file, cloud);
//         return cloud;
//     }
// }
