/// <summary>
/// The And logic gate
/// </summary>
public class And : Gate
{
    /// <summary>
    /// Overrides the default behaviour of the gate with the logic of an And logic gate
    /// </summary>
    public override void OnCircuitChanged()
    {
        base.OnCircuitChanged();
        Outputs[0].value = Inputs[0].value && Inputs[1].value;
        Outputs[0].OnCircuitChanged();
    }
}
