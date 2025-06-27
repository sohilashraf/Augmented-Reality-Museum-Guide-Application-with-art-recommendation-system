using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
using Unity.Collections;
using System.Net.Sockets;
using System;

public class YoloClient : MonoBehaviour
{
    public ARCameraManager cameraManager;
    public GameObject[] avatarPrefabs;

    public Vector3 spawnOffset = new Vector3(0f, -0.5f, 1.5f);
    public Vector3 modelRotation = new Vector3(0f, 180f, 0f);  

    private bool imageSent = true;
    private const float captureCooldown = 2f;
    private string serverIP = "197.161.196.254";
    private int serverPort = 5001;
    private HashSet<string> spawnedLabels = new HashSet<string>();

    public NarrationManager narrationManager; 


    void Start()
    {
        Debug.Log("🚀 YoloClient started.");
        if (cameraManager == null || cameraManager.descriptor == null || !cameraManager.descriptor.supportsCameraImage)
        {
            Debug.LogError("❌ ARCameraManager not properly configured.");
        }
        else
        {
            Debug.Log("✅ ARCameraManager supports CPU image access.");
            Invoke("EnableImageCapture", 3f);
        }
    }

    void EnableImageCapture()
    {
        Debug.Log("⏱️ Image capture enabled.");
        imageSent = false;
    }

    void Update()
    {
        if (!imageSent && cameraManager != null)
        {
            StartCoroutine(CaptureAndSendImage());
        }
    }

    IEnumerator CaptureAndSendImage()
    {
        imageSent = true;

        if (!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
        {
            Debug.LogWarning("⚠️ Could not acquire CPU image.");
            imageSent = false;
            yield break;
        }

        byte[] jpgBytes = null;

        using (image)
        {
            var conversionParams = new XRCpuImage.ConversionParams
            {
                inputRect = new RectInt(0, 0, image.width, image.height),
                outputDimensions = new Vector2Int(image.width, image.height),
                outputFormat = TextureFormat.RGB24,
                transformation = XRCpuImage.Transformation.MirrorY
            };

            var buffer = new NativeArray<byte>(image.width * image.height * 3, Allocator.Temp);
            image.Convert(conversionParams, buffer);

            Texture2D texture = new Texture2D(image.width, image.height, TextureFormat.RGB24, false);
            texture.LoadRawTextureData(buffer);
            texture.Apply();

            jpgBytes = texture.EncodeToJPG();
            Destroy(texture);
            buffer.Dispose();
        }

        if (jpgBytes != null)
        {
            yield return StartCoroutine(SendOverSocket(jpgBytes));
        }

        yield return new WaitForSeconds(captureCooldown);
        imageSent = false;
    }

    IEnumerator SendOverSocket(byte[] imageData)
    {
        TcpClient client = new TcpClient();

        try
        {
            client.Connect(serverIP, serverPort);
            NetworkStream stream = client.GetStream();

            byte[] lengthBytes = BitConverter.GetBytes(imageData.Length);
            if (BitConverter.IsLittleEndian) Array.Reverse(lengthBytes);

            stream.Write(lengthBytes, 0, 4);
            stream.Write(imageData, 0, imageData.Length);

            byte[] labelBytes = new byte[1024];
            int bytesRead = stream.Read(labelBytes, 0, labelBytes.Length);
            string label = System.Text.Encoding.UTF8.GetString(labelBytes, 0, bytesRead).Trim();

            Debug.Log("🟢 Detected label: " + label);
            SpawnAvatar(label);

            stream.Close();
            client.Close();
        }
        catch (Exception ex)
        {
            Debug.LogError("❌ Socket error: " + ex.Message);
        }

        yield return null;
    }

    void SpawnAvatar(string label)
{
    if (label != "Khedive_Ismail" && label != "Khonsu" && label != "Thutmose")
    {
        Debug.Log($"🔕 Skipping detection — '{label}' is not enabled.");
        return;
    }

    if (spawnedLabels.Contains(label))
    {
        Debug.Log($"⚠️ '{label}' already spawned.");
        return;
    }

    int index = GetLabelIndex(label);
    if (index < 0 || index >= avatarPrefabs.Length)
    {
        Debug.LogWarning("⚠️ Unknown or missing label: " + label);
        return;
    }

    Transform cam = Camera.main.transform;
    Vector3 position = cam.position + cam.TransformDirection(spawnOffset);
    Quaternion rotation = Quaternion.Euler(modelRotation);

    GameObject obj = Instantiate(avatarPrefabs[index], position, rotation);
    obj.AddComponent<TouchInteraction>();  

    Debug.Log($"✅ Spawned '{label}' at {position}");
    spawnedLabels.Add(label);

    narrationManager.SetDetectedLabel(label);
}

int GetLabelIndex(string label)
{
    switch (label.Trim())
    {
        case "Khedive_Ismail": return 0;
        case "Khonsu": return 1;
        case "Thutmose": return 2;
        default: return -1;
    }
}

}