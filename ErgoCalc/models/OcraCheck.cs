namespace ErgoCalc.Models.OCRA;

public enum TaskType
{
    SingleTask = 0,
    MultiTask = 1
}

public enum TaskBreaks
{
    Ideal = 0,
    Half = 1,
    Less = 2,
    Few = 3,
    One = 4,
    None = 5
}

public enum TaskATD
{
    Slow = 0,
    NotFast = 1,
    FastWithBreaks = 2,
    FastFewBreaks = 3,
    FasterFewBreaks = 4,
    FasterNoBreaks = 5,
    HighFrequencyNoBreaks = 6
}

public enum TaskATE
{
    Short = 0,
    Long = 1
}

public class Data
{
    /// <summary>
    /// Shift duration (minutes)
    /// </summary>
    public double DurationShift { get; set; } = 480;

    /// <summary>
    /// Breaks duration (minutes)
    /// </summary>
    public double DurationBreaks { get; set; } = 30;

    /// <summary>
    /// Lunch duration (minutes)
    /// </summary>
    public double DurationLunch { get; set; } = 30;

    /// <summary>
    /// Duration of non repetitive tasks (minutes)
    /// </summary>
    public double DurationNonRepetitive { get; set; } = 240;

    /// <summary>
    /// Recovery
    /// </summary>
    public TaskBreaks Recovery { get; set; } = TaskBreaks.Ideal;

    /// <summary>
    /// Recovery for dynamic actions
    /// </summary>
    public TaskATD FrequencyATD { get; set; } = TaskATD.Slow;

    /// <summary>
    /// Recovery for static actions
    /// </summary>
    public TaskATE FrequencyATE { get; set; } = TaskATE.Short;

}

public class Multipliers
{
    /// <summary>
    /// Net repetitive work
    /// </summary>
    public double TNTR { get; set; } = 180;

    /// <summary>
    /// Net repetitive work cycle
    /// </summary>
    public double NetRepetitiveWork { get; set; } = 0;

    /// <summary>
    /// Recovery factor
    /// </summary>
    public double FactorR { get; set; } = 0;

    /// <summary>
    /// Frequency factor
    /// </summary>
    public double FactorF { get; set; } = 0;
}

public class SubTask
{

}

public class TaskModel
{
    public string ToString(string[] strRows, System.Globalization.CultureInfo? culture = null)
    {
        return string.Empty;
    }

    public override string ToString()
    {
        string[] strRows = new[]
        {
            ""
        };
        return ToString(strRows);
    }
}

public class Job
{
    /// <summary>
    /// Set of tasks in the job
    /// </summary>
    public TaskModel[] Tasks { get; set; } = Array.Empty<TaskModel>();

    public string ToString(string[] strRows, System.Globalization.CultureInfo? culture = null)
    {
        string str = string.Empty;

        foreach (TaskModel task in Tasks)
            str += task.ToString(strRows, culture) + Environment.NewLine + Environment.NewLine;

        return str;
    }

    public override string ToString()
    {
        string[] strRows = new[]
        {
            ""
        };

        return ToString(strRows);
    }

}

public static class OcraCheck
{
    /// <summary>
    /// Compute the OCRA check index
    /// </summary>
    /// <param name="subT">Array of independent subtasks whose Ocra check index is computed</param>
	public static void OcraCheckIndex(Task[] tasks)
    {
        foreach(Task task in tasks)
        {

        }
    }

    private static double FactorR(TaskBreaks breaks)
    {
        double factor = 0;

        if (breaks == TaskBreaks.Ideal)
            factor = 0;
        else if (breaks == TaskBreaks.Half)
            factor = 2;
        else if (breaks == TaskBreaks.Less)
            factor = 3;
        else if (breaks == TaskBreaks.Few)
            factor = 4;
        else if (breaks == TaskBreaks.One)
            factor = 6;
        else if (breaks == TaskBreaks.None)
            factor = 10;

        return factor;
    }

    private static double FactorF(TaskATD atd, TaskATE ate)
    {
        double factorATD = 0;
        double factorATE = 0;

        if (atd == TaskATD.Slow)
            factorATD = 0;
        else if (atd == TaskATD.NotFast)
            factorATD = 1;
        else if (atd == TaskATD.FastWithBreaks)
            factorATD = 3;
        else if (atd == TaskATD.FastFewBreaks)
            factorATD = 4;
        else if (atd == TaskATD.FasterFewBreaks)
            factorATD = 6;
        else if (atd == TaskATD.FasterNoBreaks)
            factorATD = 8;
        else if (atd == TaskATD.HighFrequencyNoBreaks)
            factorATD = 10;

        if (ate == TaskATE.Short)
            factorATD = 2.5;
        else if (ate == TaskATE.Long)
            factorATD = 4.5;

        return Math.Max(factorATD, factorATE);
    }

    private static double FactorD(double value, bool multiTask = false)
    {
        double factor = 0;

        if (value <= 120)
            factor = 0.5;
        else if (value <= 180)
            factor = 0.65;
        else if (value <= 240)
            factor = 0.75;
        else if (value <= 300)
            factor = 0.85;
        else if (value <= 360)
            factor = 0.925;
        else if (value <= 420)
            factor = 0.95;
        else if (value <= 480)
            factor = 1.0;
        else if (value <= 539)
            factor = 1.2;
        else if (value <= 599)
            factor = 1.5;
        else if (value <= 659)
            factor = 2.0;
        else if (value <= 719)
            factor = 2.8;
        else if (value >= 720)
            factor = 4.0;

        return factor;
    }
}