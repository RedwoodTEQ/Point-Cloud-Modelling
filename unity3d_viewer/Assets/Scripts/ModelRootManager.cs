using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity3DViewer.UI;

namespace Unity3DViewer
{
    public class ModelRootManager : MonoBehaviour
    {
        public Transform rootTF;
        [SerializeField]
        private List<Model> models = new List<Model>();

        private UIManager uiManager;

        // Start is called before the first frame update
        void Start()
        {
            if (!rootTF)
            {
                rootTF = this.transform.Find("Root");
            }

            uiManager = FindObjectOfType<UIManager>();
            if (uiManager)
            {
                uiManager.onPointSizeSliderChange += ChangePointSize;
                uiManager.onLoadModelBtnClick += LoadModelFromDisk;
            }

            UpdateModels();
        }

        private void LoadModelFromDisk() 
        {
            UnityAction<string> onSuccess = delegate(string path)
            {
                var modelObj = ModelLoader.LoadFromDisk(path);
                if(modelObj)
                {
                    var arr = path.Split('/');
                    var parent = new GameObject(arr[arr.Length - 1]);
                    modelObj.transform.parent = parent.transform;
                    parent.transform.parent = rootTF;

                    UpdateModels();
                }
            };

            StartCoroutine(FileLoader.LoadCoroutine(onSuccess));
        }

        private void ChangePointSize(float size)
        {
            if(models.Count > 0)
            {
                models[0].ChangePointSize(size);
            }
        }

        private void UpdateModels()
        {
            models.Clear();
            if (rootTF)
            {
                foreach (Transform child in this.rootTF)
                {
                    var model = new Model(child.gameObject);
                    models.Add(model);
                }
            }

            uiManager.UpdateModelList(models);
        }

        private void OnEnable()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    [System.Serializable]
    public class Model
    {
        public GameObject gameObject {get; private set;}
        public MeshRenderer meshRender {get; private set;}
        public string name {get; private set;}

        public Model(GameObject obj)
        {
            gameObject = obj;
            meshRender = gameObject.GetComponent<MeshRenderer>();
            name = gameObject.name;
        }

        public void ChangePointSize(float size)
        {
            if (this.meshRender && this.meshRender.material)
            {
                this.meshRender.material.SetFloat("_PointSize", size);
            }
        }
    }
}