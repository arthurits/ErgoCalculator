namespace ErgoCalc.Models.OCRA;

public class Data
{

}

public class Multipliers
{

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
	public static void OcraCheckIndex(SubTask[] subT)
    {

    }
}