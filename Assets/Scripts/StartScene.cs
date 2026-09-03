using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public string sceneToLoad = "MuseumScene"; 

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
