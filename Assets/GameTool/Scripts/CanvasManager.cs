using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameTool
{
    public class CanvasManager : SingletonMonoBehavior<CanvasManager>
    {
        private List<List<BaseUI>> ListUI = new List<List<BaseUI>>();
        private List<Transform> ListMenu = new List<Transform>();

        protected override void Awake()
        {
            base.Awake();
            // Tạo các object chứa
            for (int i = 0; i < Enum.GetNames(typeof(eUIType)).Length; i++)
            {
                GameObject obj = new GameObject(Enum.GetNames(typeof(eUIType))[i], typeof(RectTransform));
                obj.transform.SetParent(transform);
                obj.transform.position = transform.position;
                obj.transform.localScale = Vector3.one;
                SetFullRect(obj);
                ListUI.Add(new List<BaseUI>());
                ListMenu.Add(obj.transform);
            }
        }

        public BaseUI Push(string identifier, object[] args = null)
        {
            var uiReturn = FindUI(identifier);
            if (uiReturn)
            {
                GotoTop(uiReturn);
                uiReturn.gameObject.SetActive(true);
                return uiReturn;
            }
            string path = GlobalUIInfo.DefaultPath + identifier;
            GameObject cachedObj = Resources.Load<GameObject>(path);
            BaseUI cachedUI = cachedObj.GetComponent<BaseUI>();

            uiReturn = Instantiate(cachedUI, ListMenu[(int) cachedUI.config.uiType]);
            ListUI[(int) cachedUI.config.uiType].Add(uiReturn);
            GotoTop(uiReturn);
            uiReturn.identifier = identifier;
            uiReturn.Init(args);
            return uiReturn;
        }

        public void Pop(string identifier)
        {
            BaseUI baseUI = FindUI(identifier);
            if (baseUI)
            {
                baseUI.Pop();
            }
        }

        private void GotoTop(BaseUI baseUi)
        {
            baseUi.transform.SetSiblingIndex(baseUi.transform.parent.childCount - 1);
        }

        private void SetFullRect(GameObject _obj)
        {
            RectTransform _rect = _obj.GetComponent<RectTransform>();

            _rect.anchorMin = Vector2.zero;
            _rect.anchorMax = Vector2.one;

            _rect.pivot = Vector2.one / 2;

            _rect.offsetMin = Vector2.zero;
            _rect.offsetMax = Vector2.zero;
        }

        public BaseUI FindUI(string identifier)
        {
            BaseUI baseUI = null;
            
            for (int i = 0; i < ListUI.Count; i++)
            {
                baseUI = ListUI[i].Find((a1)=>a1.identifier == identifier && !a1.gameObject.activeSelf);
                if (baseUI)
                {
                    break;
                }
            }
            return baseUI;
        }
    }
}