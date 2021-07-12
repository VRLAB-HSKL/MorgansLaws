using System.Collections;
using UnityEngine;

/// <summary>
/// Quits the Application with an animation
/// </summary>
public class QuitApplication : MonoBehaviour
{
    /// <summary>
    /// The animation that can be played before the application quits
    /// </summary>
    public Animator ExitAnimation;

    /// <summary>
    /// Determines, how long the animation is played, before quitting the application
    /// </summary>
    public float ExitAnimationTime = 1.0f;

    /// <summary>
    /// Starts quitting the application
    /// </summary>
    public void Quit()
    {
        StartCoroutine(ExitApplication());
    }

    /// <summary>
    /// Starts the animation if one is specified, then quits the application
    /// </summary>
    /// <returns></returns>
    private IEnumerator ExitApplication()
    {
        // Start the animation if it exists
        if (ExitAnimation != null)
        {
            ExitAnimation.SetTrigger("Start");
            yield return new WaitForSeconds(ExitAnimationTime);
        }

        // Quit the application, either on the VR headset or the unity player
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}