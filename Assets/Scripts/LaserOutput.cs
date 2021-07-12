using UnityEngine;

/// <summary>
/// An input serving as the start point for the laser inside the sockets
/// </summary>
public class LaserOutput : MonoBehaviour
{
    /// <summary>
    /// Determines, if the laser is enabled
    /// </summary>
    public bool value;

    /// <summary>
    /// End point of the laser
    /// </summary>
    public LaserInput Target;

    /// <summary>
    /// Activates or Deactivates the laser and sets its positions
    /// </summary>
    private void Update()
    {
        LineRenderer line = GetComponent<LineRenderer>();

        if (value && line != null && Target != null)
        {
            // Actuvate the laser and set its positions
            line.enabled = true;
            line.positionCount = 2;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, Target.transform.position);
        }
        else
        {
            // Deactuvate the laser and remove its positions
            line.enabled = false;
            line.positionCount = 0;
        }
    }

    /// <summary>
    /// Enables the laser
    /// </summary>
    /// <param name="Target">Target where the laser is aiming to</param>
    public void Enable(LaserInput Target)
    {
        value = enabled = GetComponent<LineRenderer>().enabled = true;
        this.Target = Target;
    }

    /// <summary>
    /// Disables the laser abnd removes its target
    /// </summary>
    public void Disable()
    {
        value = enabled = GetComponent<LineRenderer>().enabled = false;
        this.Target = null;
    }

}
