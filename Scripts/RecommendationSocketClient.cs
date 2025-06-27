using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class RecommendationSocketClient : MonoBehaviour
{
    public Button recommendButton;
    public string serverIP = "192.168.1.4"; 
    public int port = 5051;
    public string userID = "user123";

    void Start()
    {
        recommendButton.onClick.AddListener(SendRequest);
    }

    void SendRequest()
    {
        Thread thread = new Thread(() => {
            try
            {
                using (TcpClient client = new TcpClient(serverIP, port))
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] data = Encoding.UTF8.GetBytes(userID);
                    stream.Write(data, 0, data.Length);

                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Debug.Log("🟢 Recommendations: " + response);
                }
            }
            catch (SocketException e)
            {
                Debug.LogError("❌ Socket error: " + e.Message);
            }
        });
        thread.Start();
    }
}
