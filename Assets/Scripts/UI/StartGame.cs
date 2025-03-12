using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void LoadScene(string sceneNameToLoad)
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
