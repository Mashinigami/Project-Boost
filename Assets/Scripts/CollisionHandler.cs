using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS
    [SerializeField] float delayNextLevelInSeconds = 1f;
    [SerializeField] float delayCrashSequenceInSeconds = 1f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem crashVFX;
    [SerializeField] ParticleSystem successVFX;

    // CACHE
    AudioSource audioSourceCache;
    BoxCollider[] boxColliderChildrenCache;

    // STATE
    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSourceCache = GetComponent<AudioSource>();
        boxColliderChildrenCache = GetComponentsInChildren<BoxCollider>();
    }

    void Update()
    {
        // ProcessAdvancedCommands();
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void ReloadLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
        isTransitioning = false;
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

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSourceCache.Stop();
        audioSourceCache.PlayOneShot(crashSFX, 0.5f);
        crashVFX.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayCrashSequenceInSeconds);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSourceCache.Stop();
        audioSourceCache.PlayOneShot(successSFX);
        successVFX.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayNextLevelInSeconds);
    }

    // void ProcessAdvancedCommands()
    // {
    //     if (Input.GetKeyDown(KeyCode.C))
    //     {
    //         DisableCollision();
    //     }

    // }

    // void DisableCollision()
    // {
    //     collisionDisabled = !collisionDisabled;

    //     for (int index = 0; index < boxColliderChildrenCache.Length; index++)
    //     {
    //         boxColliderChildrenCache[index].enabled = collisionDisabled;
    //     }
    // }
}
