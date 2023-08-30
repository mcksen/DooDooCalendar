using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour
{
    private bool isCamAvailable;
    private WebCamTexture backCamera;
    private Texture defaultBackground;
    [SerializeField] private RawImage background;
    [SerializeField] private AspectRatioFitter fitter;

    public RawImage Background => background;

    // private void Awake()
    // {
    //     EventManager.Instance.onCameraPressed += HandleCameraPressed;
    // }

    // public void Configure(string name)
    // {
    //     photoName = name;
    // }
    private void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            Debug.LogError("No camera detected");
            isCamAvailable = false;
            return;
        }
        for (int i = 0; i < devices.Length; i++)
        {
#if !UNITY_EDITOR
            if (!devices[i].isFrontFacing)
#endif
            {
                backCamera = new WebCamTexture(devices[i].name, Screen.width, Screen.height);

                isCamAvailable = true;
            }
        }
        if (backCamera == null)
        {
            Debug.Log("No back camera were found");
            return;
        }
        backCamera.Play();
        background.texture = backCamera;
    }


    private void Update()
    {
        if (!isCamAvailable)
        {
            return;
        }
        float ratio = (float)backCamera.width / (float)backCamera.height;
        fitter.aspectRatio = ratio;
        float scaleY = backCamera.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);
        int orientation = -backCamera.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }

    // private void Destroy()
    // {
    //     EventManager.Instance.onCameraPressed -= HandleCameraPressed;
    // }

    // private void HandleCameraPressed(string name)
    // {
    //     photoName = name;
    // }

    // public void TakePhoto()
    // {
    //     ScreenCapture.CaptureScreenshot(photoName);
    // }
}
