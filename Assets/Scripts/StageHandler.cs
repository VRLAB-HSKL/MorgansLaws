using UnityEngine;

/// <summary>
/// Handles the actions, when the player enters and exits a stage of the current scene
/// </summary>
public class StageHandler : MonoBehaviour
{
    /// <summary>
    /// The option window of the current stage
    /// The window will show up, when the players enters the stage
    /// The window will hide, when the players exits the stage
    /// </summary>
    public GameObject window;
    /// <summary>
    /// The door of the current stage to shut down, when the player leaves the stage
    /// This prevents the player moving between the stage and using objects from the previous stage
    /// </summary>
    public GameObject door;

    /// <summary>
    /// Triggers the actions to take, when the player enters the stage
    /// Shows the option window of the current stage
    /// </summary>
    /// <param name="other">The collider that just entered the stage</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            // show the option window of the current stage
            window.GetComponent<Canvas>().enabled = true;
    }

    /// <summary>
    /// Triggers the actions to take, when the player exits the stage
    /// Hides the option window of the current stage
    /// Shuts down the door of the stage, when the player leaves
    /// </summary>
    /// <param name="other">The collider that just leaved the stage</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            // hides the option window of the stage
            window.GetComponent<Canvas>().enabled = false;
            // shuts down the door behind the player
            door.GetComponent<Door>().blocked = true;
        }
    }
}
