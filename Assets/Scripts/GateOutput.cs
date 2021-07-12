/// <summary>
/// A gate connector that can only be used as an output (male) connector
/// </summary>
public class GateOutput : GateIO
{
    public override void OnCircuitChanged()
    {
        base.OnCircuitChanged();

        // here could be code for outputs
    }
}