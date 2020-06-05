using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Render : MonoBehaviour
{
    [SerializeField]
    private string _screenshotName;
    private void Start()
    {
        ScreenCapture.CaptureScreenshot(Path.Combine(Application.persistentDataPath, _screenshotName));
    }
}
