using UnityEngine;

/// <summary>
/// Sets if the gate is currently grabbed by the player
/// </summary>
public class GateControl : MonoBehaviour
{
    /// <summary>
    /// If the gate is currently grabbed by the player
    /// </summary>
    public bool isGrabbed = false;

    /// <summary>
    /// Flags the gate as grabbed
    /// </summary>
    public void GateAfterGrabbed()
    {
        isGrabbed = true;
    }

    /// <summary>
    /// Removes the flag, if the gate is grabbed
    /// </summary>
    public void GateOnDrop()
    {
        isGrabbed = false;
    }
}
