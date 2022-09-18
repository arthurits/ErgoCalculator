using System;
using System.Collections.Generic;

namespace ErgoCalc.Models.Lifting;

public enum IndexType
{
    IndexLI = 0,
    IndexCLI = 1,
    IndexSLI = 2,
    IndexVLI = 3
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
public class SubTask
{
    public Data data { get; set; } = new();
    public Multipliers factors { get; set; } = new();
    public double indexIF { get; set; } = 0;
    public double indexLI { get; set; } = 0;
    public int task { get; set; } = 0;
    public int order { get; set; } = 0;
    public int itemIndex { get; set; } = 0;

    public override string ToString()
    {
        return base.ToString();
    }
}

/// <summary>
/// Task definition
/// </summary>
public class Task
{
    /// <summary>
    /// Set of subtasks in the task
    /// </summary>
    public SubTask[] subTasks { get; set; } = Array.Empty<SubTask>();

    /// <summary>
    /// Reordering of the subtasks from lower LI to higher LI
    /// </summary>
    public int[] OrderCLI { get; set; } = Array.Empty<int>();

    /// <summary>
    /// The composite lifting index for this task
    /// </summary>
    public double CLI { get; set; } = 0;

    /// <summary>
    /// 
    /// </summary>
    public IndexType model { get; set; } = IndexType.IndexLI;

    /// <summary>
    /// Number of subtasks in the task
    /// </summary>
    public int numberSubTasks { get; set; } = 0;
    //public int order;
    //public int job;

    public Task()
    {
    }

    public override string ToString()
    {
        Int32 i, length = subTasks.Length;
        //Int32[] ordenacion = new Int32[length];
        var strResult = new System.Text.StringBuilder(2200);
        String strEquationT;
        String strEquationN;
        String[] strLineD = new String[11];
        String[] strLineR = new String[13];
        //String[] strTasks = new String[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        //String[] coupling = new String[] { "Good", "Poor", "No hndl" };

        //for (i = 0; i < length; i++) ordenacion[order[i]] = length - i;

        for (i = 0; i < length; i++)
        {
            strLineD[0] += "\t\t" + "Task " + ((char)('A' + subTasks[i].itemIndex)).ToString();
            strLineD[1] += "\t\t" + subTasks[i].data.weight.ToString();
            strLineD[2] += "\t\t" + subTasks[i].data.h.ToString();
            strLineD[3] += "\t\t" + subTasks[i].data.v.ToString();
            strLineD[4] += "\t\t" + subTasks[i].data.d.ToString();
            strLineD[5] += "\t\t" + subTasks[i].data.f.ToString();
            strLineD[6] += "\t\t" + subTasks[i].data.fa.ToString();
            strLineD[7] += "\t\t" + subTasks[i].data.fb.ToString();
            strLineD[8] += "\t\t" + subTasks[i].data.td.ToString();
            strLineD[9] += "\t\t" + subTasks[i].data.a.ToString();
            strLineD[10] += "\t" + subTasks[i].data.c;

            strLineR[0] += "\t\t" + "Task " + ((char)('A' + subTasks[i].itemIndex)).ToString();
            strLineR[1] += "\t\t" + subTasks[i].factors.LC.ToString("0.####");
            strLineR[2] += "\t\t" + subTasks[i].factors.HM.ToString("0.####");
            strLineR[3] += "\t\t" + subTasks[i].factors.VM.ToString("0.####");
            strLineR[4] += "\t\t" + subTasks[i].factors.DM.ToString("0.####");
            strLineR[5] += "\t\t" + subTasks[i].factors.FM.ToString("0.####");
            strLineR[6] += "\t\t" + subTasks[i].factors.FMa.ToString("0.####");
            strLineR[7] += "\t\t" + subTasks[i].factors.FMb.ToString("0.####");
            strLineR[8] += "\t\t" + subTasks[i].factors.AM.ToString("0.####");
            strLineR[9] += "\t\t" + subTasks[i].factors.CM.ToString("0.####");
            
            if (model == IndexType.IndexCLI)
            {
                strLineR[10] += "\t\t" + subTasks[i].indexIF.ToString("0.####");
                strLineR[11] += "\t";
                strLineR[12] += "\t\t" + (OrderCLI[i] + 1).ToString();
            }
            
            strLineR[11] += "\t" + subTasks[i].indexLI.ToString("0.####");
            
            //ordenacion[length - _order[i] - 1] = i;
            //ordenacion[_order[i]] = length - i - 1;
        }

        //for (i = 0; i < length; i++) OrderCLI[i] = OrderCLI[length - i - 1];

        //rtbShowResult.AcceptsTab = true;
        //rtbShowResult.SelectionTabs = new int[] { 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1100, 1200, 1300, 1400, 1500};

        strResult.Append("These are the results obtained from the NIOSH lifting model:");
        strResult.Append(System.Environment.NewLine);
        strResult.Append(System.Environment.NewLine);

        // Initial data
        strResult.Append("Initial data" + "\t\t" + strLineD[0] + "\n");
        strResult.Append("Weight lifted (kg):\t" + strLineD[1] + "\n");
        strResult.Append("Horizontal distance (cm):" + strLineD[2] + "\n");
        strResult.Append("Vertical distance (cm):" + strLineD[3] + "\n");
        strResult.Append("Vertical travel distance (cm):\t" + strLineD[4].TrimStart('\t') + "\n");
        strResult.Append("Lifting frequency (times/min):\t" + strLineD[5].TrimStart('\t') + "\n");
        if (length > 1 && model == IndexType.IndexCLI)
        {
            strResult.Append("Lifting frequency A (times/min):\t" + strLineD[6].TrimStart('\t') + "\n");
            strResult.Append("Lifting frequency B (times/min):\t" + strLineD[7].TrimStart('\t') + "\n");
        }
        strResult.Append("Task duration (hours):" + strLineD[8] + "\n");
        strResult.Append("Twisting angle (°):\t" + strLineD[9] + "\n");
        strResult.Append("Coupling:\t\t\t" + strLineD[10] + "\n\n");

        // Multipliers
        strResult.Append("Multipliers" + "\t\t" + strLineR[0] + "\n");
        strResult.Append("Lifting constant (LC):" + strLineR[1] + "\n");
        strResult.Append("Horizontal multiplier (HM):" + strLineR[2] + "\n");
        strResult.Append("Vertical multiplier (VM):" + strLineR[3] + "\n");
        strResult.Append("Distance multiplier(DM):" + strLineR[4] + "\n");
        strResult.Append("Frequency multiplier(FM):" + strLineR[5] + "\n");
        if (length > 1 && model == IndexType.IndexCLI)
        {
            strResult.Append("Frequency A multiplier (FMa):\t" + strLineR[6].TrimStart('\t') + "\n");
            strResult.Append("Frequency B multiplier (FMb):\t" + strLineR[7].TrimStart('\t') + "\n");
        }
        strResult.Append("Twisting angle multiplier (AM):\t" + strLineR[8].TrimStart('\t') + "\n");
        strResult.Append("Coupling multiplier (CM):" + strLineR[9] + "\n\n");

        if (length > 1)
        {
            if (model == IndexType.IndexCLI)
            {
                strResult.Append("Lifting index (IF):\t" + strLineR[10] + "\n");
                strResult.Append("Lifting index (LI):\t" + strLineR[11] + "\n");
                strResult.Append("Subtask order:\t" + strLineR[12] + "\n\n");
            }
            else
            {
                strResult.Append("Lifting index:\t\t" + strLineR[11] + "\n\n");
            }
        }

        // Print NIOSH final equation
        strResult.Append("The NIOSH lifting index is computed as follows:\n\n");
        if (length > 1)
        {
            if (model == IndexType.IndexCLI)
            {
                var strName = ((char)('A' + subTasks[OrderCLI[0]].itemIndex)).ToString();
                strEquationT = "CLI = Index(" + strName + ")"; //((char)('A' + i)).ToString()
                strEquationN = "CLI = " + subTasks[OrderCLI[0]].indexLI.ToString("0.####");
                for (i = 1; i < length; i++)
                {
                    strName = ((char)('A' + subTasks[OrderCLI[i]].itemIndex)).ToString();
                    strEquationT += " + IndexIF(" + strName + ") * (1/FMa(" + strName + ") - 1/FMb(" + strName + "))";
                    strEquationN += " + " + subTasks[OrderCLI[i]].indexIF.ToString("0.####") + " * (1/" + subTasks[OrderCLI[i]].factors.FMa.ToString("0.####") + " - 1/" + subTasks[OrderCLI[i]].factors.FMb.ToString("0.####") + ")";
                }
                strEquationN += " = " + CLI.ToString("0.####");
                strResult.Append(strEquationT + "\n");
                strResult.Append(strEquationN + "\n\n");
                strResult.Append("The NIOSH lifting index is: " + CLI.ToString("0.####") + "\n");
            }
            else
            {
                strEquationT = "LI = Weight / (LC * HM * VM * DM * FM * AM * CM)";
                strResult.Append(strEquationT + "\n");
                for (i = 0; i < length; i++)
                {
                    strEquationN = "LI = " + subTasks[i].data.weight.ToString("0.####") + " / (";
                    strEquationN += subTasks[i].factors.LC.ToString("0.####") + " * ";
                    strEquationN += subTasks[i].factors.HM.ToString("0.####") + " * ";
                    strEquationN += subTasks[i].factors.VM.ToString("0.####") + " * ";
                    strEquationN += subTasks[i].factors.DM.ToString("0.####") + " * ";
                    strEquationN += subTasks[i].factors.FM.ToString("0.####") + " * ";
                    strEquationN += subTasks[i].factors.AM.ToString("0.####") + " * ";
                    strEquationN += subTasks[i].factors.CM.ToString("0.####") + ") = ";
                    strEquationN += subTasks[i].indexLI.ToString("0.####");
                    strResult.Append(strEquationN + "\n");
                }
                strResult.Append("\n");
            }
        }
        else
        {
            strEquationT = "LI = Weight / (LC * HM * VM * DM * FM * AM * CM)";
            strEquationN = "LI = " + subTasks[0].data.weight.ToString("0.####") + " / (";
            strEquationN += subTasks[0].factors.LC.ToString("0.####") + " * ";
            strEquationN += subTasks[0].factors.HM.ToString("0.####") + " * ";
            strEquationN += subTasks[0].factors.VM.ToString("0.####") + " * ";
            strEquationN += subTasks[0].factors.DM.ToString("0.####") + " * ";
            strEquationN += subTasks[0].factors.FM.ToString("0.####") + " * ";
            strEquationN += subTasks[0].factors.AM.ToString("0.####") + " * ";
            strEquationN += subTasks[0].factors.CM.ToString("0.####") + ") = ";
            strEquationN += subTasks[0].indexLI.ToString("0.####");

            strResult.Append(strEquationT + "\n");
            strResult.Append(strEquationN + "\n\n");
            strResult.Append("The NIOSH lifting index is: " + subTasks[0].indexLI.ToString("0.####") + "\n");
        };

        return strResult.ToString();
    }
}

/// <summary>
/// Job definition
/// </summary>
public class Job
{
    /// <summary>
    /// Set of tasks in the job
    /// </summary>
    public Task[] jobTasks { get; set; } = Array.Empty<Task>();

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
    public IndexType model { get; set; } = IndexType.IndexLI;

    /// <summary>
    /// Number of tasks in the job
    /// </summary>
    public int numberTasks { get; set; } = 0;

    public Job()
    {
    }

    public override string ToString()
    {
        string str = string.Empty;

        foreach (Task task in jobTasks)
            str += task.ToString() + Environment.NewLine + Environment.NewLine;

        return str;
    }
}

public static class NIOSHLifting
{

    public static void ComputeLI(SubTask[] subT)
    {
        //double[] pIndex = new double[subT.Length];

        for (int i = 0; i < subT.Length; i++)
        {
            if (subT[i].factors.LC == 0) subT[i].factors.LC = 23.0;
            subT[i].factors.HM = FactorHM(subT[i].data.h);
            subT[i].factors.VM = FactorVM(subT[i].data.v);
            subT[i].factors.DM = FactorDM(subT[i].data.d);
            subT[i].factors.AM = FactorAM(subT[i].data.a);
            subT[i].factors.FM = FactorFM(subT[i].data.f, subT[i].data.v, subT[i].data.td);
            subT[i].factors.CM = FactorCM(subT[i].data.c, subT[i].data.v);

            subT[i].indexLI = MultiplyFactors(subT[i].data.weight, subT[i].factors);
            subT[i].indexIF = subT[i].indexLI * subT[i].factors.FM;

            //pIndex[i] = subT[i].LI;
        }
    }

    public static double ComputeCLI(Task task)
    {
        // First compute the LI index for each subtask
        ComputeLI(task.subTasks);

        // 2nd step: Sort the LI indexes from highest to lowest
        double[] indexOrder = new double[task.subTasks.Length];
        int[] values = new int[task.subTasks.Length];
        for (int i = 0; i < task.subTasks.Length; i++)
        {
            indexOrder[i] = task.subTasks[i].indexLI;
            values[i] = i;
        }
        Array.Sort(indexOrder, values);
        Array.Reverse(values);
        //for (int i = 0; i < values.Length; i++) task.OrderCLI[i] = values[i];
        Array.Copy(values, task.OrderCLI, values.Length);

        // 3rd step: Compute the cumulative index
        double result = task.subTasks[values[0]].indexLI;
        task.subTasks[values[0]].data.fa = task.subTasks[values[0]].data.fb = task.subTasks[values[0]].data.f;
        task.subTasks[values[0]].order = 0;

        for (int i = 1; i < task.subTasks.Length; i++)
        {
            task.subTasks[values[i]].data.fa = task.subTasks[values[i -1]].data.fa + task.subTasks[values[i]].data.f;
            task.subTasks[values[i]].data.fb = task.subTasks[values[i -1 ]].data.fa;
            task.subTasks[values[i]].factors.FMa = FactorFM(task.subTasks[values[i]].data.fa, task.subTasks[values[i]].data.v, task.subTasks[values[i]].data.td);
            task.subTasks[values[i]].factors.FMb = FactorFM(task.subTasks[values[i]].data.fb, task.subTasks[values[i]].data.v, task.subTasks[values[i]].data.td);
            task.subTasks[values[i]].order = i;
            result += task.subTasks[values[i]].indexIF * (1 / task.subTasks[values[i]].factors.FMa - 1 / task.subTasks[values[i]].factors.FMb);
        }

        task.CLI = result;

        // Finally, return the index
        return result;
    }
    
    private static double MultiplyFactors(double weight, Multipliers factors)
    {
        double product = 0.0;
        double result = 0.0;

        product = factors.LC *
            factors.HM *
            factors.VM *
            factors.DM *
            factors.AM *
            factors.FM *
            factors.CM;

        if (product == 0)    // División entre 0
            result = 0;
        else
            result = weight / product;

        return result;
    }

    /// <summary>
    /// Computes the Horizontal Multiplier
    /// </summary>
    /// <param name="value">H value in meters</param>
    /// <returns>H multiplier</returns>
    private static double FactorHM(double value)
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
    private static double FactorVM(double value)
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
    private static double FactorDM(double value)
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
    private static double FactorAM(double value)
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frequency"></param>
    /// <param name="v"></param>
    /// <param name="td"></param>
    /// <returns></returns>
    private static double FactorFM(double frequency, double v, double td)
    {
        // Definición de variables
        int nIndice = 0;
        int nColumna = 0;
        //int nLongitud = 18; // freq.Length
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
            return fm[nColumna][0];

        if (nIndice >= freq.Length - 1)
            return fm[nColumna][freq.Length - 1];

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
    private static double FactorCM(Coupling agarre, double v)
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
    private static double LinearInterpolation(double x, double x1, double x2, double y1, double y2)
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
    private static int Locate(double[] array, double x)
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
