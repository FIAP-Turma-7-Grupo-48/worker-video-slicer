using Controller.Utils.Interfaces;

namespace Controller.Utils;

public class ControllerVariables : IControllerVariables
{
    public ControllerVariables()
    {
        VideoSlicerInterval = GetVariableAsTimeSpan("VIDEO_SLICE_INTERVAL_AS_SECOND");
    }
    public TimeSpan VideoSlicerInterval { get; }

    private TimeSpan GetVariableAsTimeSpan(string Name)
    {
        var stringValue = Environment.GetEnvironmentVariable(Name);

        var isInt = int.TryParse(stringValue, out var intValue);

        if (isInt is false)
        {
            //Todo implementar validação
        }

        return TimeSpan.FromSeconds(intValue);
    }
}
