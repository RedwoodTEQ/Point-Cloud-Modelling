using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Unity3DViewer.UI
{
    public class UIManager : MonoBehaviour
    {
        private const float MODEL_TOGGLE_HEIGHT = 55;
        public GameObject modelListEntryTogglePrefab;
        public Transform modelListParentTF;

        public UISlider pointSizeSlider;
        public UIButton modelLoadBtn;
        public UIButton quiteBtn;

        public UnityAction<float> onPointSizeSliderChange;
        public UnityAction onLoadModelBtnClick;

        private List<ModelListEntry> modelListEntries = new List<ModelListEntry>();

        // Start is called before the first frame update
        void Start()
        {
            if (pointSizeSlider != null)
            {
                pointSizeSlider.component.onValueChanged.AddListener(delegate { OnPointSizeValueChanges(); });
            }

            if(modelLoadBtn)
            {
                modelLoadBtn.component.onClick.AddListener(delegate { OnModelLoadBtnClick(); });
            }

            if(quiteBtn)
            {
                quiteBtn.component.onClick.AddListener(delegate {
                    Application.Quit();
                    Debug.Log("Click quite.");
                    });
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPointSizeValueChanges()
        {
            var slider = pointSizeSlider.component;
            var label = pointSizeSlider.label;
            var size = slider.value / 1000;
            if (onPointSizeSliderChange != null)
            {
                onPointSizeSliderChange(size);
                label.text = "Point Size: " + size.ToString();
            }
        }

        public void OnModelLoadBtnClick()
        {
            if(onLoadModelBtnClick != null)
            {
                onLoadModelBtnClick();
            }

        }

        public void UpdateModelList(List<Model> models)
        {
            if(modelListEntryTogglePrefab == null)
            {
                Debug.LogError("UIManager >> UpdateModelList >> modelListEntryTogglePrefab is null.");
                return;
            }
            if(modelListParentTF == null)
            {
                Debug.LogError("UIManager >> UpdateModelList >> modelListParentTF is null.");
                return;
            }

            modelListEntries.Clear();
            var posY = 0f;
            models.ForEach(model => {
                var go = GameObject.Instantiate(modelListEntryTogglePrefab, modelListParentTF);
                var uiToggle = go.GetComponent<UIToggle>();
                var entry = new ModelListEntry(model, uiToggle, posY, model.name);
                modelListEntries.Add(entry);
                posY -= MODEL_TOGGLE_HEIGHT;
            });
        }

        private class ModelListEntry 
        {
            private Model _model;
            private UIToggle _toggle;

            public ModelListEntry(Model model, UIToggle toggle, float posY, string name)
            {
                _model = model;
                _toggle = toggle;

                _toggle.component.onValueChanged.AddListener(
                    delegate {
                        if(_model != null && _model.gameObject != null)
                        {
                            _model.gameObject.SetActive(_toggle.component.isOn);
                        }
                    }
                );

                _toggle.component.GetComponent<RectTransform>().transform.localPosition += new Vector3(0, posY, 0);

                _toggle.label.text = name;
            }
        }
    }
}