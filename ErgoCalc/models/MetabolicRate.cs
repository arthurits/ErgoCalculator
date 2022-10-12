using System;

namespace ErgoCalc.Models.MetabolicRate;


public class Data
{
    public int Category { get; set; } = 0;
    public int Type { get; set; } = 0;
}

public class Variables
{
    public double A { get; set; } = 0;
    public double B { get; set; } = 0;
    public double C { get; set; } = 0;
    public double D { get; set; } = 0;
    public double E { get; set; } = 0;
}

public class Task
{
    public Data Data = new();
    public Variables Results = new();
}

public class Job
{
    public Task[] Tasks { get; set; } = Array.Empty<Task>();
    public int NumberTasks { get; set; }= 0;

    public override string ToString()
    {
        string str;
        // Escribir los resultados en la ventana
        str = "These are the metabolic rate results according to the norm UNE-EN ISO 8996:2004 criteria.";
        str += Environment.NewLine + Environment.NewLine;

        // Nivel 1A
        str += "Level 1.a computation for a" + Environment.NewLine;
        str += "Metabolic rate range: " + Tasks[0].Results.A.ToString("0") + " to " + Tasks[0].Results.B.ToString("0");
        str += Environment.NewLine + Environment.NewLine;

        // Nivel 1B
        str += "Level 1.b computation for a" + Environment.NewLine;
        str += "Mean metabolic rate: " + Tasks[0].Results.C.ToString("0") + Environment.NewLine;
        str += "Metabolic rate range: " + Tasks[0].Results.D.ToString("0") + " to " + Tasks[0].Results.E.ToString("0");
        str += Environment.NewLine + Environment.NewLine;

        return str;
    }

}

public static class MetabolicRate
{
    public static bool CalculateMetRate(Job job)
    {
        bool result = false;

        foreach (Task task in job.Tasks)
        {
            // Calculate the metabolic rate according to level 1A                    
            CalculateLevel1A(task);
            CalculateLevel1B(task);
        }
        
        return result;
    }

    private static void CalculateLevel1A(Task task)
    {
        // Variable definition
        double[] dInf = new double[] { 115.0, 85.0, 70.0, 75.0, 110.0, 105.0, 110.0, 90.0, 110.0, 100.0, 55.0, 140.0, 105.0, 140.0, 170.0, 125.0, 80.0, 90.0, 70.0, 75.0, 75.0, 115.0, 70.0, 110.0, 85.0, 100.0, 85.0, 70.0, 80.0, 70.0, 55.0, 75.0, 70.0, 80.0, 65.0 };
        double[] dSup = new double[] { 190.0, 110.0, 95.0, 100.0, 160.0, 140.0, 175.0, 125.0, 140.0, 130.0, 70.0, 240.0, 165.0, 240.0, 220.0, 145.0, 140.0, 200.0, 110.0, 125.0, 125.0, 175.0, 85.0, 110.0, 100.0, 120.0, 100.0, 85.0, 115.0, 100.0, 70.0, 125.0, 100.0, 115.0, 145.0 };

        // Calculate the result
        if (task.Data.Category >= 0 && task.Data.Category < 35)
        {
            task.Results.A = dInf[task.Data.Category];
            task.Results.B = dSup[task.Data.Category];
        }
        else
        {
            task.Results.A = -1.0;
            task.Results.B = -1.0;
        }

        // Return from function
        return;
    }

    private static void CalculateLevel1B(Task task)
    {
        // Variable definition
        double[] dMedio = new double[] { 65.0, 100.0, 165.0, 230.0, 290.0 };
        double[] dInf = new double[] { 55.0, 70.0, 130.0, 200.0, 260.0 };
        double[] dSup = new double[] { 70.0, 130.0, 200.0, 260.0, 10000.0 };

        // Calculate the result
        if (task.Data.Type >= 0 && task.Data.Type < 5)
        {
            task.Results.C = dMedio[task.Data.Type];
            task.Results.D = dInf[task.Data.Type];
            task.Results.E = dSup[task.Data.Type];
        }
        else
        {
            task.Results.C = -1.0;
            task.Results.D = -1.0;
            task.Results.E = -1.0;
        }

        // Return from function
        return;
    }
}
