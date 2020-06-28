using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRootManager : MonoBehaviour
{
    public Transform rootTF;
    [SerializeField]
    private List<Model> models = new List<Model>();

    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        if(!rootTF)
        {
            rootTF = this.transform.Find("Root");
        }

        if(rootTF)
        {
            foreach(Transform child in this.rootTF)
            {
                var model = new Model(child.gameObject);
                models.Add(model);
            }
        }

        uiManager = FindObjectOfType<UIManager>();
        if(uiManager)
        {
            uiManager.onPointSizeSliderChange += models[0].ChangePointSize;
        }

        // if(this.models.Count > 0)
        // {
        //     foreach (var m in this.models)
        //     {
        //         m.ChangePointSize(0.005f);
        //     }
        // }
    }

    private void OnEnable() {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Model {
    GameObject gameObject;
    MeshRenderer meshRender;

    public Model(GameObject obj) 
    {
        gameObject = obj;
        meshRender = gameObject.GetComponent<MeshRenderer>();
    }

    public void ChangePointSize(float size) 
    {
        if(this.meshRender && this.meshRender.material)
        {
            this.meshRender.material.SetFloat("_PointSize", size);
        }
    }
}