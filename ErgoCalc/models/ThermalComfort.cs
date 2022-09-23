using System;

namespace ErgoCalc.Models.ThermalComfort;

public class Data
{
    /// <summary>
    /// Air temperature (°C)
    /// </summary>
    public double TempAir { get; set; } = 0;
    /// <summary>
    /// Radiant temperature(°C)
    /// </summary>
    public double TempRad { get; set; } = 0;
    /// <summary>
    /// Air velocity (m/s)
    /// </summary>
    public double Velocity { get; set; } = 0;
    /// <summary>
    /// Relative humidity (%)
    /// </summary>
    public double RelHumidity { get; set; } = 0;
    /// <summary>
    /// Clothing (clo)
    /// </summary>
    public double Clothing { get; set; } = 0;
    /// <summary>
    /// Metabolic rate (met)
    /// </summary>
    public double MetRate { get; set; } = 0;
    /// <summary>
    /// External work (met), generally around 0
    /// </summary>
    public double ExternalWork { get; set; } = 0;

    public override string ToString()
    {
        return string.Empty;
    }
}

public class Variables
{
    /// <summary>
    /// // Factor de intensidad del esfuerzo [0, 1]
    /// </summary>
    public double PMV { get; set; } = 0;
    /// <summary>
    /// Factor de esfuerzos por minuto
    /// </summary>
    public double PPD { get; set; } = 0;
    /// <summary>
    /// HL1 heat loss diff. through skin
    /// </summary>
    public double HL_Skin { get; set; } = 0;
    /// <summary>
    /// HL2 heat loss by sweating
    /// </summary>
    public double HL_Sweating { get; set; } = 0;
    /// <summary>
    /// HL3 latent respiration heat loss
    /// </summary>
    public double HL_Latent { get; set; } = 0;
    /// <summary>
    /// HL4 dry respiration heat loss
    /// </summary>
    public double HL_Dry { get; set; } = 0;
    /// <summary>
    /// HL5 heat loss by radiation
    /// </summary>
    public double HL_Radiation { get; set; } = 0;
    /// <summary>
    /// HL6 heat loss by convection
    /// </summary>
    public double HL_Convection { get; set; } = 0;

    public override string ToString()
    {
        return string.Empty;
    }
}

public class Task
{
    /// <summary>
    /// Model data
    /// </summary>
    public Data Data { get; set; } = new();
    /// <summary>
    /// Model variables
    /// </summary>
    public Variables Variables { get; set; } = new();
    /// <summary>
    /// The PMV index
    /// </summary>
    public double indexPMV { get; set; } = 0;
    /// <summary>
    /// The PPD index
    /// </summary>
    public double indexPPD { get; set; } = 0;

    public override string ToString()
    {
        return string.Empty;
    }
}

public class Job
{
    public Task[] Tasks { get; set; } = Array.Empty<Task>();

    public override string ToString()
    {
        string strTasks = string.Empty;

        foreach (Task task in Tasks)
            strTasks += task.ToString() + Environment.NewLine + Environment.NewLine;

        return strTasks;

    }
}

public static class ThermalComfort
{
}
