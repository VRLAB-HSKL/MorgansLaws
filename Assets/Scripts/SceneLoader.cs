using System;
using UnityEngine;

/// <summary>
/// Loads a new scene after playing an animation
/// </summary>
[Obsolete("Not yet used currently for VR", true)]
public class SceneLoader : MonoBehaviour
{
    public GameObject Player;

    //public SceneAsset NextScene;

    public Animator transition;

    public float transitionTime = 1.0f;

    //public void LoadNextStage()
    //{
    //    if (NextScene != null)
    //        StartCoroutine(LoadStage(NextScene));
    //}

    //public IEnumerator LoadStage(SceneAsset scene)
    //{
    //    if (transition != null)
    //        transition.SetTrigger("Start");

    //    yield return new WaitForSeconds(transitionTime);

    //    SceneManager.LoadScene(scene.name);
    //    Player.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    //}
}
