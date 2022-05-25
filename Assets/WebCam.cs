using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class WebCam : MonoBehaviour
{
    WebCamTexture webCamTexture;
    [SerializeField]RawImage playerFace;
    string selectedCam;
    string frontFacing = " ";
    string backFacing = " ";
    // Start is called before the first frame update
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        
        for(int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing)
            {
                frontFacing = devices[i].name;
            }
            else
            {
                backFacing = devices[i].name;
            }
        }
        Debug.Log(frontFacing + "/n" + backFacing);
        selectedCam = frontFacing;
        StartCamera();
    }
    private void StartCamera()
    {
        webCamTexture = new WebCamTexture(selectedCam);
        GetComponent<Renderer>().material.mainTexture = webCamTexture;
        webCamTexture.Play();
    }
    private void StopCam()
    {
        webCamTexture.Stop();
    }
    public void TakePhoto()
    {
        StartCoroutine(Capture());
    }
    IEnumerator Capture()
    {
        yield return new WaitForEndOfFrame();
        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();
        playerFace.texture = photo;
        gameObject.SetActive(false);
    }
    public void FlipCamera()
    {
        if(selectedCam == frontFacing)
        {
            selectedCam = backFacing;
            StopCam();
            StartCamera();
        }
        else if (selectedCam == backFacing)
        {
            selectedCam = frontFacing;
            StopCam();
            StartCamera();
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
