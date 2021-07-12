/// <summary>
/// The Not logic gate
/// </summary>
public class Not : Gate
{
    /// <summary>
    /// Overrides the default behaviour of the gate with the logic of an Not logic gate
    /// </summary>
    public override void OnCircuitChanged()
    {
        base.OnCircuitChanged();
        Outputs[0].value = !Inputs[0].value;
        Outputs[0].OnCircuitChanged();
    }
}
