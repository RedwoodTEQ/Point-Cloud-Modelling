using Siccity.GLTFUtility;
using UnityEngine;

namespace Unity3DViewer
{
    public static class ModelLoader {
        public static GameObject LoadFromDisk(string path) {
            GameObject modelObj = Importer.LoadFromFile(path);
            return modelObj;
        }
    }
}