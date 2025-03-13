using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadSceneByName(string sceneNameToLoad)
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
