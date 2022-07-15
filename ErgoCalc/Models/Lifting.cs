using System;
using System.Collections.Generic;

namespace ErgoCalc.Models.Lifting;

public enum IndexTypeNew
{
    IndexLI = 0,
    IndexCLI = 1,
    IndexSLI = 2,
    IndexVLI = 3,
}

public enum Coupling
{
    NoHandle = 0,
    Poor = 1,
    Good = 2
}

/// <summary>
/// Data for each subtask
/// </summary>
public class Data
{
    /// <summary>
    /// Weight in kg
    /// </summary>
    public double weight { get; set; } = 0;

    /// <summary>
    /// Horizontal distance in meters
    /// </summary>
    public double h { get; set; } = 0;

    /// <summary>
    /// Vertical distance from hand to floor in meters
    /// </summary>
    public double v { get; set; } = 0;

    /// <summary>
    /// Vertical traveling in meters
    /// </summary>
    public double d { get; set; } = 0;

    /// <summary>
    /// Asymmetry angle in degrees
    /// </summary>
    public double a { get; set; } = 0;

    /// <summary>
    /// Handling frequency
    /// </summary>
    public double f { get; set; } = 0;

    /// <summary>
    /// Accumulated frequency a
    /// </summary>
    public double fa { get; set; } = 0;

    /// <summary>
    /// Accumulated frequency b
    /// </summary>
    public double fb { get; set; } = 0;
    
    /// <summary>
    /// SubTask duration in hours
    /// </summary>
    public double td { get; set; } = 0;
    
    /// <summary>
    /// Coupling type
    /// </summary>
    public Coupling c { get; set; } = 0;
}

/// <summary>
/// Multipliers to be computed from the data
/// </summary>
public class Multipliers
{
    /// <summary>
    /// Load constant in kg
    /// </summary>
    public double LC { get; set; } = 0;

    /// <summary>
    /// Horizontal distance multiplier
    /// </summary>
    public double HM { get; set; } = 0;

    /// <summary>
    /// Vertical distance multiplier
    /// </summary>
    public double VM { get; set; } = 0;

    /// <summary>
    /// Distance multiplier
    /// </summary>
    public double DM { get; set; } = 0;

    /// <summary>
    /// Asymmetry multiplier
    /// </summary>
    public double AM { get; set; } = 0;

    /// <summary>
    /// Frequency multiplier
    /// </summary>
    public double FM { get; set; } = 0;

    /// <summary>
    /// Factor de frecuencia acumulada a
    /// </summary>
    public double FMa { get; set; } = 0;

    /// <summary>
    /// Factor de frecuencia acumulada b
    /// </summary>
    public double FMb { get; set; } = 0;

    /// <summary>
    /// Coupling multiplier
    /// </summary>
    public double CM { get; set; } = 0;
}

/// <summary>
/// Subtask definition
/// </summary>
public class SubTaskNew
{
    public Data data { get; set; } = new();
    public Multipliers factors { get; set; } = new();
    public double indexIF { get; set; } = 0;
    public double LI { get; set; } = 0;
    public int task { get; set; } = 0;
    public int order { get; set; } = 0;
    public int itemIndex { get; set; } = 0;
}

/// <summary>
/// Task definition
/// </summary>
public class TaskNew
{
    /// <summary>
    /// Set of subtasks in the task
    /// </summary>
    public SubTaskNew[] subTasks { get; set; } = Array.Empty<SubTaskNew>();

    /// <summary>
    /// Reordering of the subtasks from lower LI to higher LI
    /// </summary>
    public int[] order { get; set; } = Array.Empty<int>();

    /// <summary>
    /// The composite lifting index for this task
    /// </summary>
    public double CLI { get; set; } = 0;

    /// <summary>
    /// Number of subtasks in the task
    /// </summary>
    public int numberSubTasks { get; set; } = 0;
    //public int order;
    //public int job;

    public TaskNew()
    {
    }

    public override string ToString()
    {
        return base.ToString();
    }
}

public class JobNew
{
    /// <summary>
    /// Set of tasks in the job
    /// </summary>
    public TaskNew[] jobTasks { get; set; } = Array.Empty<TaskNew>();

    /// <summary>
    /// Reordering of the subtasks from lower CLI to higher CLI
    /// </summary>
    public int[] order { get; set; } = Array.Empty<int>();

    /// <summary>
    /// The global index for this job
    /// </summary>
    public double index { get; set; } = 0;

    /// <summary>
    /// 
    /// </summary>
    public IndexTypeNew model { get; set; } = IndexTypeNew.IndexLI;

    /// <summary>
    /// Number of tasks in the job
    /// </summary>
    public int numberTasks { get; set; } = 0;

    public JobNew()
    {
    }

    public override string ToString()
    {
        return base.ToString();
    }
}

public class NIOSHLifting
{
    public List<SubTaskNew> SubTasks { get; set; } = new();
    public List<TaskNew> Tasks { get; set; } = new();
    public List<JobNew> Jobs { get; set; } = new();

    public JobNew Job { get; set; }

    public NIOSHLifting(JobNew job)
    {
        Job = job;
    }

    public void Compute()
    {

    }

    private void ComputeLI()
    {

    }

    private void ComputeCLI()
    {

    }

    /// <summary>
    /// Computes the Horizontal Multiplier
    /// </summary>
    /// <param name="value">H value in meters</param>
    /// <returns>H multiplier</returns>
    private double ComputeHM(double value)
    {
        double multiplier = 0.0;

        // Compute the multiplier
        if (value < 25)
            multiplier = 1.0;
        else if (value > 63)
            multiplier = 0.0;
        else
            multiplier = 25 / value;

        // Return the multiplier value
        return multiplier;
    }

    /// <summary>
    /// Computes the Vertical Multiplier
    /// </summary>
    /// <param name="value">V value in meters</param>
    /// <returns>V multiplier</returns>
    double ComputeVM(double value)
    {
        double multiplier = 0.0;

        // Compute the multiplier
        if (value > 175)
            multiplier = 0.0;
        else
            multiplier = 1 - 0.003 * Math.Abs(value - 75);

        // Return the multiplier value
        return multiplier;
    }

    /// <summary>
    /// Computes the Distance Multiplier
    /// </summary>
    /// <param name="value">Distance in meters</param>
    /// <returns>D multiplier</returns>
    double FactorDM(double value)
    {
        double multiplier = 0.0;

        // Compute the multiplier
        if (value < 25)
            multiplier = 1.0;
        else if (value > 175)
            multiplier = 0.0;
        else
            multiplier = 0.82 + 4.5 / value;

        // Return the multiplier value
        return multiplier;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    double FactorAM(double value)
    {
        double multiplier = 0.0;

        // Compute the multiplier
        if (value > 135)
            multiplier = 0.0;
        else
            multiplier = 1 - 0.0032 * (value);

        // Return the multiplier value
        return multiplier;
    }

    double FactorFM(double frequency, double v, double td)
    {
        // Definición de variables
        int nIndice = 0;
        int nColumna = 0;
        int nLongitud = 18;
        double multiplier = 0.0;
        double[] freq = new double[] { 0.2, 0.5, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        double[][] fm = new double[][]
        {
            new double[] { 1, 0.97, 0.94, 0.91, 0.88, 0.84, 0.8, 0.75, 0.7, 0.6, 0.52, 0.45, 0.41, 0.37, 0, 0, 0, 0 },
            new double[] { 1, 0.97, 0.94, 0.91, 0.88, 0.84, 0.8, 0.75, 0.7, 0.6, 0.52, 0.45, 0.41, 0.37, 0.34, 0.31, 0.28, 0 },
            new double[] { 0.95, 0.92, 0.88, 0.84, 0.79, 0.72, 0.6, 0.5, 0.42, 0.35, 0.3, 0.26, 0, 0, 0, 0, 0, 0 },
            new double[] { 0.95, 0.92, 0.88, 0.84, 0.79, 0.72, 0.6, 0.5, 0.42, 0.35, 0.3, 0.26, 0.23, 0.21, 0, 0, 0, 0 },
            new double[] { 0.85, 0.81, 0.75, 0.65, 0.55, 0.45, 0.35, 0.27, 0.22, 0.18, 0, 0, 0, 0, 0, 0, 0, 0 },
            new double[] { 0.85, 0.81, 0.75, 0.65, 0.55, 0.45, 0.35, 0.27, 0.22, 0.18, 0.15, 0.13, 0, 0, 0, 0, 0, 0 }
        };

        if (td <= 1.0)
        {
            if (v < 75)
                nColumna = 0;
            else
                nColumna = 1;
        }
        else if (td <= 2.0)
        {
            if (v < 75)
                nColumna = 2;
            else
                nColumna = 3;
        }

        else if (td <= 8.0)
        {
            if (v < 75)
                nColumna = 4;
            else
                nColumna = 5;
        }

        // Devuelve un valor entre -1 (fuera de rango) y nLongitud
        nIndice = Locate(freq, frequency);

        //if (nIndice == -1 || nIndice == (nLongitud - 2))
        //    return fm[nColumna][++nIndice];

        if (nIndice == -1)
            return fm[nColumna][++nIndice];

        if (nIndice >= (nLongitud))
            return fm[nColumna][nIndice];

        multiplier = LinearInterpolation(frequency,
            freq[nIndice],
            freq[nIndice + 1],
            fm[nColumna][nIndice],
            fm[nColumna][nIndice + 1]);

        // Return the multiplier value
        return multiplier;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="agarre"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    double FactorCM(Coupling agarre, double v)
    {
        // Definición de variables
        double result = 0.0;

        // Compute the multiplier value
        switch (agarre)
        {
            case Coupling.NoHandle:
                result = 0.90;
                break;
            case Coupling.Poor:
                if (v < 75)
                    result = 0.95;
                else
                    result = 1.0;
                break;
            case Coupling.Good:
                result = 1.00;
                break;
            default:
                result = 0.0;
                break;
        }

        // Return the multiplier value
        return result;
    }

    /// <summary>
    /// Performs a linear interpolation between two points
    /// </summary>
    /// <param name="x">Abscissa value to be interpolated</param>
    /// <param name="x1">Point 1 abscissa value</param>
    /// <param name="x2">Point 1 ordinate value</param>
    /// <param name="y1">Point 2 abscissa value</param>
    /// <param name="y2">Point 2 ordinate value</param>
    /// <returns>The ordinate interpolated value</returns>
    private double LinearInterpolation(double x, double x1, double x2, double y1, double y2)
    {
        double result = 0.0;

        // Linear interpolation
        if (x == x1)        // Si el valor pedido coincide con el límite inferior
            result = y1;
        else if (x == x2)   // Si el valor pedido coincide con el límite superior
            result = y2;
        else                // En cualquier otro caso, hacer la interpolación lineal
            result = (x - x1) * (y2 - y1) / (x2 - x1) + y1;

        // Return the interpolated value
        return result;
    }

    /// <summary>
    /// Given an array xx[0..n-1], and given a value x, returns a value j such that x is between xx[j]
    /// and xx[j + 1]. xx must be monotonic, either increasing or decreasing.j=-1 or j = n is returned
    /// to indicate that x is out of range
    /// </summary>
    /// <param name="array"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    private int Locate(double[] array, double x)
    {
        // Definición de variables
        int upperBound;
        int lowerBound;
        int midPoint;
        bool ascnd;

        // Inicialización de variables
        lowerBound = array.GetLowerBound(0);    //Initialize lower
        upperBound = array.GetUpperBound(0);    //and upper limits.
        ascnd = (array[^1] >= array[0]);

        // Comprobar si el valor pedido está fuera de la matriz
        // Cuidado con el orden ascendente o descendente
        if (x <= array[0] && x <= array[^1])
            return ascnd ? lowerBound - 1 : upperBound + 1;
        if (x >= array[^1] && x >= array[0])
            return ascnd ? upperBound + 1 : lowerBound - 1;

        // Bisection procedure
        while (upperBound - lowerBound > 1)             // If we are not yet done,
        {                       
            midPoint = (upperBound + lowerBound) >> 1;  // compute a midpoint (divide by two),
            if ((x >= array[midPoint]) == ascnd)
                lowerBound = midPoint;                  // and replace either the lower limit
            else
                upperBound = midPoint;                  // or the upper limit, as appropriate.
        }                                               // Repeat until the test condition is satisfied.

        return lowerBound;
    }
}
