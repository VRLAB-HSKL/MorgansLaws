/// <summary>
/// Power units are used as basic static on and off switches for the cables connected to it.
/// </summary>
public class Power : Gate
{
    /// <summary>
    /// Determines, if the power unit is staticly enabled
    /// </summary>
    public bool value = false;

    // Dorward its value to its male ouput connector
    public override void OnCircuitChanged()
    {
        foreach (GateOutput Output in Outputs)
            Output.value = value;

        base.OnCircuitChanged();
    }
}
