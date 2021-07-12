/// <summary>
/// A gate connector that can only be used as an input (female) connector
/// </summary>
public class GateInput : GateIO
{
    public override void OnCircuitChanged()
    {
        base.OnCircuitChanged();

        // here could be code for inputs
    }
}
