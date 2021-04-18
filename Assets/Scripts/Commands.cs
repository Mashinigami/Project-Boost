using UnityEngine;
using UnityEngine.SceneManagement;

public class Commands : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int nextSceneIndex = currentScene.buildIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
