/// <summary>
/// The Or logic gate
/// </summary>
public class Or : Gate
{
    /// <summary>
    /// Overrides the default behaviour of the gate with the logic of an Or logic gate
    /// </summary>
    public override void OnCircuitChanged()
    {
        base.OnCircuitChanged();
        Outputs[0].value = Inputs[0].value || Inputs[1].value;
        Outputs[0].OnCircuitChanged();
    }
}