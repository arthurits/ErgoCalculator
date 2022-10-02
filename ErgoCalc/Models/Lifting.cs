using System;

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
    /// Load constant in kg
    /// </summary>
    public double LC { get; set; } = 0;

    /// <summary>
    /// Weight in kg
    /// </summary>
    public double Weight { get; set; } = 0;

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
    public Data Data { get; set; } = new();
    public Multipliers Factors { get; set; } = new();
    public double IndexIF { get; set; } = 0;
    public double IndexLI { get; set; } = 0;
    public int Task { get; set; } = 0;
    public int Order { get; set; } = 0;
    public int ItemIndex { get; set; } = 0;

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
    public SubTask[] SubTasks { get; set; } = Array.Empty<SubTask>();

    /// <summary>
    /// Reordering of the subtasks from lower LI to higher LI
    /// </summary>
    public int[] OrderCLI { get; set; } = Array.Empty<int>();

    /// <summary>
    /// The composite lifting index for this task
    /// </summary>
    public double IndexCLI { get; set; } = 0;

    /// <summary>
    /// 
    /// </summary>
    public IndexType Model { get; set; } = IndexType.IndexLI;

    /// <summary>
    /// Number of sub-tasks in the task
    /// </summary>
    public int NumberSubTasks { get; set; } = 0;
    

    public Task()
    {
    }

    public override string ToString()
    {
        Int32 i, length = SubTasks.Length;
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
            strLineD[0] += "\t\t" + "Task " + ((char)('A' + SubTasks[i].ItemIndex)).ToString();
            strLineD[1] += "\t\t" + SubTasks[i].Data.Weight.ToString();
            strLineD[2] += "\t\t" + SubTasks[i].Data.h.ToString();
            strLineD[3] += "\t\t" + SubTasks[i].Data.v.ToString();
            strLineD[4] += "\t\t" + SubTasks[i].Data.d.ToString();
            strLineD[5] += "\t\t" + SubTasks[i].Data.f.ToString();
            strLineD[6] += "\t\t" + SubTasks[i].Data.fa.ToString();
            strLineD[7] += "\t\t" + SubTasks[i].Data.fb.ToString();
            strLineD[8] += "\t\t" + SubTasks[i].Data.td.ToString();
            strLineD[9] += "\t\t" + SubTasks[i].Data.a.ToString();
            strLineD[10] += "\t" + SubTasks[i].Data.c;

            strLineR[0] += "\t\t" + "Task " + ((char)('A' + SubTasks[i].ItemIndex)).ToString();
            strLineR[1] += "\t\t" + SubTasks[i].Factors.LC.ToString("0.####");
            strLineR[2] += "\t\t" + SubTasks[i].Factors.HM.ToString("0.####");
            strLineR[3] += "\t\t" + SubTasks[i].Factors.VM.ToString("0.####");
            strLineR[4] += "\t\t" + SubTasks[i].Factors.DM.ToString("0.####");
            strLineR[5] += "\t\t" + SubTasks[i].Factors.FM.ToString("0.####");
            strLineR[6] += "\t\t" + SubTasks[i].Factors.FMa.ToString("0.####");
            strLineR[7] += "\t\t" + SubTasks[i].Factors.FMb.ToString("0.####");
            strLineR[8] += "\t\t" + SubTasks[i].Factors.AM.ToString("0.####");
            strLineR[9] += "\t\t" + SubTasks[i].Factors.CM.ToString("0.####");
            
            if (Model == IndexType.IndexCLI)
            {
                strLineR[10] += "\t\t" + SubTasks[i].IndexIF.ToString("0.####");
                strLineR[11] += "\t";
                strLineR[12] += "\t\t" + (OrderCLI[i] + 1).ToString();
            }
            
            strLineR[11] += "\t" + SubTasks[i].IndexLI.ToString("0.####");
            
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
        if (length > 1 && Model == IndexType.IndexCLI)
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
        if (length > 1 && Model == IndexType.IndexCLI)
        {
            strResult.Append("Frequency A multiplier (FMa):\t" + strLineR[6].TrimStart('\t') + "\n");
            strResult.Append("Frequency B multiplier (FMb):\t" + strLineR[7].TrimStart('\t') + "\n");
        }
        strResult.Append("Twisting angle multiplier (AM):\t" + strLineR[8].TrimStart('\t') + "\n");
        strResult.Append("Coupling multiplier (CM):" + strLineR[9] + "\n\n");

        if (length > 1)
        {
            if (Model == IndexType.IndexCLI)
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
            if (Model == IndexType.IndexCLI)
            {
                var strName = ((char)('A' + SubTasks[OrderCLI[0]].ItemIndex)).ToString();
                strEquationT = "CLI = Index(" + strName + ")"; //((char)('A' + i)).ToString()
                strEquationN = "CLI = " + SubTasks[OrderCLI[0]].IndexLI.ToString("0.####");
                for (i = 1; i < length; i++)
                {
                    strName = ((char)('A' + SubTasks[OrderCLI[i]].ItemIndex)).ToString();
                    strEquationT += " + IndexIF(" + strName + ") * (1/FMa(" + strName + ") - 1/FMb(" + strName + "))";
                    strEquationN += " + " + SubTasks[OrderCLI[i]].IndexIF.ToString("0.####") + " * (1/" + SubTasks[OrderCLI[i]].Factors.FMa.ToString("0.####") + " - 1/" + SubTasks[OrderCLI[i]].Factors.FMb.ToString("0.####") + ")";
                }
                strEquationN += " = " + IndexCLI.ToString("0.####");
                strResult.Append(strEquationT + "\n");
                strResult.Append(strEquationN + "\n\n");
                strResult.Append("The NIOSH lifting index is: " + IndexCLI.ToString("0.####") + "\n");
            }
            else
            {
                strEquationT = "LI = Weight / (LC * HM * VM * DM * FM * AM * CM)";
                strResult.Append(strEquationT + "\n");
                for (i = 0; i < length; i++)
                {
                    strEquationN = "LI = " + SubTasks[i].Data.Weight.ToString("0.####") + " / (";
                    strEquationN += SubTasks[i].Factors.LC.ToString("0.####") + " * ";
                    strEquationN += SubTasks[i].Factors.HM.ToString("0.####") + " * ";
                    strEquationN += SubTasks[i].Factors.VM.ToString("0.####") + " * ";
                    strEquationN += SubTasks[i].Factors.DM.ToString("0.####") + " * ";
                    strEquationN += SubTasks[i].Factors.FM.ToString("0.####") + " * ";
                    strEquationN += SubTasks[i].Factors.AM.ToString("0.####") + " * ";
                    strEquationN += SubTasks[i].Factors.CM.ToString("0.####") + ") = ";
                    strEquationN += SubTasks[i].IndexLI.ToString("0.####");
                    strResult.Append(strEquationN + "\n");
                }
                strResult.Append("\n");
            }
        }
        else
        {
            strEquationT = "LI = Weight / (LC * HM * VM * DM * FM * AM * CM)";
            strEquationN = "LI = " + SubTasks[0].Data.Weight.ToString("0.####") + " / (";
            strEquationN += SubTasks[0].Factors.LC.ToString("0.####") + " * ";
            strEquationN += SubTasks[0].Factors.HM.ToString("0.####") + " * ";
            strEquationN += SubTasks[0].Factors.VM.ToString("0.####") + " * ";
            strEquationN += SubTasks[0].Factors.DM.ToString("0.####") + " * ";
            strEquationN += SubTasks[0].Factors.FM.ToString("0.####") + " * ";
            strEquationN += SubTasks[0].Factors.AM.ToString("0.####") + " * ";
            strEquationN += SubTasks[0].Factors.CM.ToString("0.####") + ") = ";
            strEquationN += SubTasks[0].IndexLI.ToString("0.####");

            strResult.Append(strEquationT + "\n");
            strResult.Append(strEquationN + "\n\n");
            strResult.Append("The NIOSH lifting index is: " + SubTasks[0].IndexLI.ToString("0.####") + "\n");
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
    public Task[] Tasks { get; set; } = Array.Empty<Task>();

    /// <summary>
    /// Reordering of the subtasks from lower CLI to higher CLI
    /// </summary>
    public int[] Order { get; set; } = Array.Empty<int>();

    /// <summary>
    /// The global index for this job
    /// </summary>
    public double Index { get; set; } = 0;

    /// <summary>
    /// Index type: LI, CLI, VLI, SLI
    /// </summary>
    public IndexType Model { get; set; } = IndexType.IndexLI;

    /// <summary>
    /// Number of tasks in the job
    /// </summary>
    public int NumberTasks { get; set; } = 0;

    /// <summary>
    /// Number of sub-tasks in the job
    /// </summary>
    public int NumberSubTasks { get; set; } = 0;

    public Job()
    {
    }

    public override string ToString()
    {
        string str = string.Empty;

        foreach (Task task in Tasks)
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
            //if (subT[i].Factors.LC == 0) subT[i].Factors.LC = 23.0;
            subT[i].Factors.HM = FactorHM(subT[i].Data.h);
            subT[i].Factors.VM = FactorVM(subT[i].Data.v);
            subT[i].Factors.DM = FactorDM(subT[i].Data.d);
            subT[i].Factors.AM = FactorAM(subT[i].Data.a);
            subT[i].Factors.FM = FactorFM(subT[i].Data.f, subT[i].Data.v, subT[i].Data.td);
            subT[i].Factors.CM = FactorCM(subT[i].Data.c, subT[i].Data.v);

            subT[i].IndexLI = MultiplyFactors(subT[i].Data.LC, subT[i].Data.Weight, subT[i].Factors);
            subT[i].IndexIF = subT[i].IndexLI * subT[i].Factors.FM;

            //pIndex[i] = subT[i].LI;
        }
    }

    public static double ComputeCLI(Task task)
    {
        // First compute the LI index for each subtask
        ComputeLI(task.SubTasks);

        // 2nd step: Sort the LI indexes from highest to lowest
        double[] indexOrder = new double[task.SubTasks.Length];
        int[] values = new int[task.SubTasks.Length];
        for (int i = 0; i < task.SubTasks.Length; i++)
        {
            indexOrder[i] = task.SubTasks[i].IndexLI;
            values[i] = i;
        }
        Array.Sort(indexOrder, values);
        Array.Reverse(values);
        //for (int i = 0; i < values.Length; i++) task.OrderCLI[i] = values[i];
        Array.Copy(values, task.OrderCLI, values.Length);

        // 3rd step: Compute the cumulative index
        double result = task.SubTasks[values[0]].IndexLI;
        task.SubTasks[values[0]].Data.fa = task.SubTasks[values[0]].Data.fb = task.SubTasks[values[0]].Data.f;
        task.SubTasks[values[0]].Order = 0;

        for (int i = 1; i < task.SubTasks.Length; i++)
        {
            task.SubTasks[values[i]].Data.fa = task.SubTasks[values[i -1]].Data.fa + task.SubTasks[values[i]].Data.f;
            task.SubTasks[values[i]].Data.fb = task.SubTasks[values[i -1 ]].Data.fa;
            task.SubTasks[values[i]].Factors.FMa = FactorFM(task.SubTasks[values[i]].Data.fa, task.SubTasks[values[i]].Data.v, task.SubTasks[values[i]].Data.td);
            task.SubTasks[values[i]].Factors.FMb = FactorFM(task.SubTasks[values[i]].Data.fb, task.SubTasks[values[i]].Data.v, task.SubTasks[values[i]].Data.td);
            task.SubTasks[values[i]].Order = i;
            result += task.SubTasks[values[i]].IndexIF * (1 / task.SubTasks[values[i]].Factors.FMa - 1 / task.SubTasks[values[i]].Factors.FMb);
        }

        task.IndexCLI = result;

        // Finally, return the index
        return result;
    }
    
    private static double MultiplyFactors(double loadConstant, double weight, Multipliers factors)
    {
        double product = 0.0;
        double result = 0.0;

        product = loadConstant *
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
