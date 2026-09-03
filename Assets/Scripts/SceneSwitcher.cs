using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadRecommendationScene()
    {
        SceneManager.LoadScene("RecommendationScene"); 
    }
}
