using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameTool
{
    public class CanvasFollowDevice : MonoBehaviour
    {
        [SerializeField] private RectTransform mainRect;
        [SerializeField] private CanvasScaler scaler;
        [SerializeField] private Camera cam;
        [SerializeField] private float aspect;
        [SerializeField] private float refAspect;

        [Space] [Header("CAM SIZE")] [SerializeField]
        private float ourCamSize = 8;

        [SerializeField] private bool changeCamSize;


        private void Awake()
        {
            if (!mainRect)
            {
                mainRect = GetComponent<RectTransform>();
            }

            if (!scaler)
            {
                scaler = GetComponent<CanvasScaler>();
            }

            if (!cam)
            {
                cam = Camera.main;
            }

            aspect = (float) Screen.width / Screen.height; // x / y
            refAspect = scaler.referenceResolution.x / scaler.referenceResolution.y;
            ChangeScaler();
            ChangeCamSize();
        }

#if UNITY_EDITOR
        private void Update()
        {
            aspect = (float) Screen.width / Screen.height; // x / y
            refAspect = scaler.referenceResolution.x / scaler.referenceResolution.y;
            ChangeScaler();
            ChangeCamSize();
        }
#endif

        public void ChangeScaler()
        {
            if (aspect < refAspect)
            {
                scaler.matchWidthOrHeight = 0;
            }
            else
            {
                scaler.matchWidthOrHeight = 1;
            }
        }

        public void ChangeCamSize()
        {
            if (changeCamSize)
            {
                cam.orthographicSize = ourCamSize / (aspect / refAspect);
            }
        }
    }
}