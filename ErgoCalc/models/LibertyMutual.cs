using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ErgoCalc.Models.LibertyMutual2;

public enum TaskType : int
{
    Carrying,
    Lifting,
    Lowering,
    Pulling,
    Pushing
}

public enum Gender : int
{
    Male,
    Female
}

public class Data
{
    /// <summary>
    /// Horizontal reach distance (H) must range from 0.20 to 0.68 m for females and 0.25 to 0.73 m for males. If H changes during a lift or lower, the mean H or maximum H can be used
    /// </summary>
    public double HorzReach { get; set; } = 0;
    /// <summary>
    /// Vertical range middle (m)
    /// </summary>
    public double VertRangeM { get; set; } = 0;
    /// <summary>
    /// The distance travelled horizontally per push or pull (m)
    /// </summary>
    public double DistHorz { get; set; } = 0;
    /// <summary>
    /// The distance travelled vertically (DV) per lift or lower must not be lower than 0.25 m or exceed arm reach for the anthropometry being used
    /// </summary>
    public double DistVert { get; set; } = 0;
    /// <summary>
    /// The vertical height of the hands (m)
    /// </summary>
    public double VertHeight { get; set; } = 0;
    /// <summary>
    /// The frequency per minute. It must range from 1 per day (i.e. 1/480 = ?0.0021/min) to 20/min
    /// </summary>
    public double Freq { get; set; } = 0;
    public TaskType Type { get; set; } = TaskType.Carrying;
    public Gender Gender { get; set; } = Gender.Male;
};

public class ScaleFactors
{
    /// <summary>
    /// Reference load (in kg or kgf)
    /// </summary>
    public double RL { get; set; } = 0;
    /// <summary>
    /// Horizontal reach factor
    /// </summary>
    public double H { get; set; } = 0;
    /// <summary>
    /// Vertical range middle factor
    /// </summary>
    public double VRM { get; set; } = 0;
    /// <summary>
    /// Horizontal travel distance factor
    /// </summary>
    public double DH { get; set; } = 0;
    /// <summary>
    /// Vertical travel distance factor
    /// </summary>
    public double DV { get; set; } = 0;
    /// <summary>
    /// Vertical height factor
    /// </summary>
    public double V { get; set; } = 0;
    /// <summary>
    /// Frequency factor
    /// </summary>
    public double F { get; set; } = 0;
    /// <summary>
    /// Coefficient of variation
    /// </summary>
    public double CV { get; set; } = 0;
    /// <summary>
    /// Maximum acceptable load — Mean (in kg or kgf)
    /// </summary>
    public double MAL { get; set; } = 0;
    /// <summary>
    /// Maximum acceptable load for 75% (in kg or kgf)
    /// </summary>
    public double MAL75 { get; set; } = 0;
    /// <summary>
    /// Maximum acceptable load for 90% (in kg or kgf)
    /// </summary>
    public double MAL90 { get; set; } = 0;
};

public class Results
{
    /// <summary>
    /// Coefficient of variation
    /// </summary>
    public double IniCoeffV { get; set; } = 0;
    /// <summary>
    /// Coefficient of variation
    /// </summary>
    public double SusCoeffV { get; set; } = 0;
    /// <summary>
    /// Maximum initial force in kgf
    /// </summary>
    public double IniForce { get; set; } = 0;
    /// <summary>
    /// Maximum sustained force in kgf
    /// </summary>
    public double SusForce { get; set; } = 0;
    /// <summary>
    /// Maximum weight in kg
    /// </summary>
    public double Weight { get; set; } = 0;
};

public class ModelLiberty
{
    /// <summary>
    /// Model data
    /// </summary>
    public Data Data { get; set; } = new();
    /// <summary>
    /// Model variables
    /// </summary>
    public Results Results { get; set; } = new();
    public ScaleFactors Initial { get; set; } = new();
    public ScaleFactors Sustained { get; set; } = new();

    
};

public class Job
{
    public ModelLiberty[] Tasks { get; set; } = Array.Empty<ModelLiberty>();
    public int NumberTasks { get; set; } = 0;

    public override string ToString()
    {
        // https://stackoverflow.com/questions/26235759/how-to-format-a-double-with-fixed-number-of-significant-digits-regardless-of-th
        // https://stackoverflow.com/questions/33401356/formatting-text-with-padding-does-not-line-up-in-c-sharp
        string strResult;

        string strGridHeader = "Task ";
        string[] strLineD = new string[9];
        string[] strLineF = new string[12];
        string[] strThresholds = new string[13];

        strLineD[0] = string.Concat(System.Environment.NewLine, "Initial data", "\t\t");
        strLineD[1] = string.Concat(System.Environment.NewLine, "Manual handling type", "\t\t");
        strLineD[2] = string.Concat(System.Environment.NewLine, "Horizontal reach H (m)");
        strLineD[3] = string.Concat(System.Environment.NewLine, "Vertical range VRM (m)");
        strLineD[4] = string.Concat(System.Environment.NewLine, "Horizontal distance DH (m)");
        strLineD[5] = string.Concat(System.Environment.NewLine, "Vertical distance DV (m)");
        strLineD[6] = string.Concat(System.Environment.NewLine, "Vertical height V (m)");
        strLineD[7] = string.Concat(System.Environment.NewLine, "Frequency F (actions/min)");
        strLineD[8] = string.Concat(System.Environment.NewLine, "Gender", "\t\t\t");

        strLineF[0] = string.Concat(System.Environment.NewLine, "Scale factors", "\t\t");
        strLineF[1] = string.Concat(System.Environment.NewLine, "Reference load", "\t");
        strLineF[2] = string.Concat(System.Environment.NewLine, "H factor", "\t\t");
        strLineF[3] = string.Concat(System.Environment.NewLine, "VRM factor", "\t\t");
        strLineF[4] = string.Concat(System.Environment.NewLine, "DH factor", "\t\t");
        strLineF[5] = string.Concat(System.Environment.NewLine, "DV factor", "\t\t");
        strLineF[6] = string.Concat(System.Environment.NewLine, "V factor", "\t\t");
        strLineF[7] = string.Concat(System.Environment.NewLine, "F factor", "\t\t");

        strLineF[8] = string.Concat(System.Environment.NewLine, System.Environment.NewLine, "Reference load", "\t");
        strLineF[9] = string.Concat(System.Environment.NewLine, "Sustained DH factor", "\t");
        strLineF[10] = string.Concat(System.Environment.NewLine, "Sustained V factor", "\t");
        strLineF[11] = string.Concat(System.Environment.NewLine, "Sustained F factor", "\t");


        strThresholds[0] = string.Concat(System.Environment.NewLine, "Maximum acceptable limit", "\t");
        strThresholds[1] = string.Concat(System.Environment.NewLine, "CV initial force", "\t");
        strThresholds[2] = string.Concat(System.Environment.NewLine, "MAL initial (kgf) for 50%");
        strThresholds[3] = string.Concat(System.Environment.NewLine, "MAL initial (kgf) for 75%");
        strThresholds[4] = string.Concat(System.Environment.NewLine, "MAL initial (kgf) for 90%");
        strThresholds[5] = string.Concat(System.Environment.NewLine, "CV sustained force", "\t");
        strThresholds[6] = string.Concat(System.Environment.NewLine, "MAL sustained (kgf) for 50%");
        strThresholds[7] = string.Concat(System.Environment.NewLine, "MAL sustained (kgf) for 75%");
        strThresholds[8] = string.Concat(System.Environment.NewLine, "MAL sustained (kgf) for 90%");
        strThresholds[9] = string.Concat(System.Environment.NewLine, "CV weight", "\t\t");
        strThresholds[10] = string.Concat(System.Environment.NewLine, "MAL weight (kg) for 50%");
        strThresholds[11] = string.Concat(System.Environment.NewLine, "MAL weight (kg) for 75%");
        strThresholds[12] = string.Concat(System.Environment.NewLine, "MAL weight (kg) for 90%");

        int i = 0;
        foreach (var data in Tasks)
        {
            strLineD[0] += string.Concat("\t\t", strGridHeader, ((char)('A' + i)).ToString());
            strLineD[1] += string.Concat("\t", data.Data.Type.ToString());
            strLineD[8] += string.Concat("\t\t", data.Data.Gender.ToString());

            strLineF[0] += string.Concat("\t\t", strGridHeader, ((char)('A' + i)).ToString());

            strThresholds[0] += string.Concat(i == 0 ? "\t" : "\t\t", strGridHeader, ((char)('A' + i)).ToString());
            if (data.Data.Type == TaskType.Pulling || data.Data.Type == TaskType.Pushing)
            {
                strLineD[2] += string.Concat("\t\t", "------");
                strLineD[3] += string.Concat("\t\t", "------");
                // Convert.ToDouble(String.Format("{0:G3}", number)).ToString()
                //strLineD[4] += string.Concat("\t\t", data.Data.DistHorz.ToString("0.####"));
                strLineD[4] += string.Concat("\t\t", Convert.ToDouble(String.Format("{0:G5}", data.Data.DistHorz)).ToString());
                strLineD[5] += string.Concat("\t\t", "------");
                strLineD[6] += string.Concat("\t\t", data.Data.VertHeight.ToString("0.####"));
                strLineD[7] += string.Concat("\t\t", data.Data.Freq.ToString("0.####"));

                strLineF[1] += string.Concat("\t\t", data.Initial.RL.ToString("0.####"));
                strLineF[2] += string.Concat("\t\t", "------");
                strLineF[3] += string.Concat("\t\t", "------");
                strLineF[4] += string.Concat("\t\t", data.Initial.DH.ToString("0.####"));
                strLineF[5] += string.Concat("\t\t", "------");
                strLineF[6] += string.Concat("\t\t", data.Initial.V.ToString("0.####"));
                strLineF[7] += string.Concat("\t\t", data.Initial.F.ToString("0.####"));

                strLineF[8] += string.Concat("\t\t", data.Sustained.RL.ToString("0.####"));
                strLineF[9] += string.Concat("\t\t", data.Sustained.DH.ToString("0.####"));
                strLineF[10] += string.Concat("\t\t", data.Sustained.V.ToString("0.####"));
                strLineF[11] += string.Concat("\t\t", data.Sustained.F.ToString("0.####"));

                strThresholds[1] += string.Concat("\t\t", data.Initial.CV.ToString("0.####"));
                strThresholds[2] += string.Concat("\t\t", Convert.ToDouble(String.Format("{0:G5}", data.Initial.MAL)).ToString("0.####"));
                strThresholds[3] += string.Concat("\t\t", Convert.ToDouble(String.Format("{0:G5}", data.Initial.MAL75)).ToString("0.####"));
                strThresholds[4] += string.Concat("\t\t", Convert.ToDouble(String.Format("{0:G5}", data.Initial.MAL90)).ToString("0.####"));
                strThresholds[5] += string.Concat("\t\t", data.Sustained.CV.ToString("0.####"));
                strThresholds[6] += string.Concat("\t\t", Convert.ToDouble(String.Format("{0:G5}", data.Sustained.MAL)).ToString("0.####"));
                strThresholds[7] += string.Concat("\t\t", Convert.ToDouble(String.Format("{0:G5}", data.Sustained.MAL75)).ToString("0.####"));
                strThresholds[8] += string.Concat("\t\t", Convert.ToDouble(String.Format("{0:G5}", data.Sustained.MAL90)).ToString("0.####"));
                strThresholds[9] += string.Concat("\t\t", "------");
                strThresholds[10] += string.Concat("\t\t", "------");
                strThresholds[11] += string.Concat("\t\t", "------");
                strThresholds[12] += string.Concat("\t\t", "------");
            }
            else // Lift, lower, and carry
            {
                if (data.Data.Type == TaskType.Carrying)
                {
                    strLineD[2] += string.Concat("\t\t", "------");
                    strLineD[3] += string.Concat("\t\t", "------");
                    strLineD[4] += string.Concat("\t\t", data.Data.DistHorz.ToString("0.####"));
                    strLineD[5] += string.Concat("\t\t", "------");
                    strLineD[6] += string.Concat("\t\t", data.Data.VertHeight.ToString("0.####"));
                    strLineD[7] += string.Concat("\t\t", data.Data.Freq.ToString("0.####"));

                    strLineF[1] += string.Concat("\t\t", data.Initial.RL.ToString("0.####"));
                    strLineF[2] += string.Concat("\t\t", "------");
                    strLineF[3] += string.Concat("\t\t", "------");
                    strLineF[4] += string.Concat("\t\t", data.Initial.DV.ToString("0.####"));
                    strLineF[5] += string.Concat("\t\t", "------");
                    strLineF[6] += string.Concat("\t\t", data.Initial.V.ToString("0.####"));
                    strLineF[7] += string.Concat("\t\t", data.Initial.F.ToString("0.####"));
                }
                else //lift and lower
                {
                    //strLineD[2] += string.Concat("\t\t", data.Data.HorzReach.ToString("0.####"));
                    strLineD[2] += string.Concat("\t\t", Convert.ToDouble(String.Format("{0:G5}", data.Data.HorzReach)).ToString());
                    strLineD[3] += string.Concat("\t\t", data.Data.VertRangeM.ToString("0.####"));
                    strLineD[4] += string.Concat("\t\t", "------");
                    strLineD[5] += string.Concat("\t\t", data.Data.DistVert.ToString("0.####"));
                    strLineD[6] += string.Concat("\t\t", "------");
                    strLineD[7] += string.Concat("\t\t", data.Data.Freq.ToString("0.####"));

                    strLineF[1] += string.Concat("\t\t", data.Initial.RL.ToString("0.####"));
                    strLineF[2] += string.Concat("\t\t", data.Initial.H.ToString("0.####"));
                    strLineF[3] += string.Concat("\t\t", data.Initial.VRM.ToString("0.####"));
                    strLineF[4] += string.Concat("\t\t", "------");
                    strLineF[5] += string.Concat("\t\t", data.Initial.DV.ToString("0.####"));
                    strLineF[6] += string.Concat("\t\t", "------");
                    strLineF[7] += string.Concat("\t\t", data.Initial.F.ToString("0.####"));
                }

                strLineF[8] += string.Concat("\t\t", "------");
                strLineF[9] += string.Concat("\t\t", "------");
                strLineF[10] += string.Concat("\t\t", "------");
                strLineF[11] += string.Concat("\t\t", "------");

                strThresholds[1] += string.Concat("\t\t", "------");
                strThresholds[2] += string.Concat("\t\t", "------");
                strThresholds[3] += string.Concat("\t\t", "------");
                strThresholds[4] += string.Concat("\t\t", "------");
                strThresholds[5] += string.Concat("\t\t", "------");
                strThresholds[6] += string.Concat("\t\t", "------");
                strThresholds[7] += string.Concat("\t\t", "------");
                strThresholds[8] += string.Concat("\t\t", "------");
                strThresholds[9] += string.Concat("\t\t", data.Initial.CV.ToString("0.####"));
                //strThresholds[10] += string.Concat("\t\t", data.Initial.MAL.ToString("0.####"));
                strThresholds[10] += string.Concat("\t\t", Convert.ToDouble(String.Format("{0:G5}", data.Initial.MAL)).ToString());
                strThresholds[11] += string.Concat("\t\t", Convert.ToDouble(String.Format("{0:G5}", data.Initial.MAL75)).ToString("0.####"));
                strThresholds[12] += string.Concat("\t\t", Convert.ToDouble(String.Format("{0:G5}", data.Initial.MAL90)).ToString("0.####"));
            }

            i++;
        }

        strResult = string.Concat("These are the results from the Liberty Mutual manual materials handling equations",
                System.Environment.NewLine,
                string.Concat(strLineD),
                System.Environment.NewLine,
                string.Concat(strLineF),
                System.Environment.NewLine,
                string.Concat(strThresholds),
                System.Environment.NewLine);

        return strResult;
    }
}

public static class LibertyMutual
{
    private static double M_SQRT1_2 = 0.707106781186547524401;  // 1/sqrt(2)
    private static double M_2_SQRTPI = 1.12837916709551257390;   // 2/sqrt(pi)
    private static double zscore75 = 0.6744897501960818; // https://planetcalc.com/7803/
    private static double zscore90 = 1.2815515655446008;	// https://planetcalc.com/7803/

    public static bool LibertyMutualMMH(Job job)
    {
        bool result = true;

        foreach (ModelLiberty task in job.Tasks)
        {
            switch (task.Data.Type)
            {
                case TaskType.Carrying:
                    Carrying(task);
                    break;
                case TaskType.Lifting:
                    Lifting(task);
                    break;
                case TaskType.Lowering:
                    Lowering(task);
                    break;
                case TaskType.Pulling:
                    Pulling(task);
                    break;
                case TaskType.Pushing:
                    Pushing(task);
                    break;
            }

            task.Initial.MAL75 = task.Initial.MAL * (1 - task.Initial.CV * zscore75);
            task.Initial.MAL90 = task.Initial.MAL * (1 - task.Initial.CV * zscore90);

            task.Sustained.MAL75 = task.Sustained.MAL * (1 - task.Sustained.CV * zscore75);
            task.Sustained.MAL90 = task.Sustained.MAL * (1 - task.Sustained.CV * zscore90);
        }

        return result;
    }


    private static void Lifting(ModelLiberty task)
    {
        double CoeffVar = -1.0;
        double H = task.Data.HorzReach;
        double VRM = task.Data.VertRangeM;
        double DH = task.Data.DistHorz;
        double DV = task.Data.DistVert;
        double V = task.Data.VertHeight;
        double F = task.Data.Freq;

        if (task.Data.Gender == Gender.Male)
        {
            // Lift – Male
            //result = 82.6 * (1.3532 - H / 0.7079) * (0.7746 + VRM / 1.912 - Math.Pow(VRM, 2) / 3.296) * (0.8695 - Math.Log(DV) / 10.62) * (0.6259 - Math.Log(F) / 9.092 - Math.Pow(Math.Log(F), 2) / 125.0);
            task.Initial.RL = 82.6;
            task.Initial.H = 1.3532 - H / 0.7079;
            task.Initial.VRM = 0.7746 + VRM / 1.912 - Math.Pow(VRM, 2) / 3.296;
            task.Initial.DV = 0.8695 - Math.Log(DV) / 10.62;
            task.Initial.F = 0.6259 - Math.Log(F) / 9.092 - Math.Pow(Math.Log(F), 2) / 125.0;

            CoeffVar = 0.276;
        }
        else if (task.Data.Gender == Gender.Female)
        {
            // Lift – Female
            //result = 34.9 * (1.2602 - H / 0.7686) * (0.9877 + VRM / 13.69 - Math.Pow(VRM, 2) / 9.221) * (0.8199 - Math.Log(DV) / 7.696) * (0.6767 - Math.Log(F) / 12.59 - Math.Pow(Math.Log(F), 2) / 228.2);
            task.Initial.RL = 34.9;
            task.Initial.H = 1.2602 - H / 0.7686;
            task.Initial.VRM = 0.9877 + VRM / 13.69 - Math.Pow(VRM, 2) / 9.221;
            task.Initial.DV = 0.8199 - Math.Log(DV) / 7.696;
            task.Initial.F = 0.6767 - Math.Log(F) / 12.59 - Math.Pow(Math.Log(F), 2) / 228.2;

            CoeffVar = 0.260;
        }

        task.Initial.MAL = task.Initial.RL * task.Initial.H * task.Initial.VRM * task.Initial.DV * task.Initial.F;
        task.Initial.CV = CoeffVar;

        //data.results.Weight = result;
        //data.results.IniCoeffV = CoeffVar;

        return;
    }

    private static void Lowering(ModelLiberty task)
    {
        double CoeffVar = -1.0;
        double H = task.Data.HorzReach;
        double VRM = task.Data.VertRangeM;
        double DH = task.Data.DistHorz;
        double DV = task.Data.DistVert;
        double V = task.Data.VertHeight;
        double F = task.Data.Freq;

        if (task.Data.Gender == Gender.Male)
        {
            // Lower – Male (note: only the RL, FSF, and CV values are different from the Lift – Male equation)
            //result = 95.9 * (1.3532 - H / 0.7079) * (0.7746 + VRM / 1.912 - Math.Pow(VRM, 2) / 3.296) * (0.8695 - Math.Log(DV) / 10.62) * (0.5773 - Math.Log(F) / 10.80 - Math.Pow(Math.Log(F), 2) / 255.9);
            task.Initial.RL = 95.9;
            task.Initial.H = 1.3532 - H / 0.7079;
            task.Initial.VRM = 0.7746 + VRM / 1.912 - Math.Pow(VRM, 2) / 3.296;
            task.Initial.DV = 0.8695 - Math.Log(DV) / 10.62;
            task.Initial.F = 0.5773 - Math.Log(F) / 10.80 - Math.Pow(Math.Log(F), 2) / 255.9;

            CoeffVar = 0.304;
        }
        else if (task.Data.Gender == Gender.Female)
        {
            // Lower – Female (note: only the RL and CV values are different from the Lift – Female equation)
            //result = 37.0 * (1.2602 - H / 0.7686) * (0.9877 + VRM / 13.69 - Math.Pow(VRM, 2) / 9.221) * (0.8199 - Math.Log(DV) / 7.696) * (0.6767 - Math.Log(F) / 12.59 - Math.Pow(Math.Log(F), 2) / 228.2);
            task.Initial.RL = 37.0;
            task.Initial.H = 1.2602 - H / 0.7686;
            task.Initial.VRM = 0.9877 + VRM / 13.69 - Math.Pow(VRM, 2) / 9.221;
            task.Initial.DV = 0.8199 - Math.Log(DV) / 7.696;
            task.Initial.F = 0.6767 - Math.Log(F) / 12.59 - Math.Pow(Math.Log(F), 2) / 228.2;

            CoeffVar = 0.307;
        }

        task.Initial.MAL = task.Initial.RL * task.Initial.H * task.Initial.VRM * task.Initial.DV * task.Initial.F;
        task.Initial.CV = CoeffVar;

        //data.results.Weight = result;
        //data.results.IniCoeffV = CoeffVar;

        return;
    }

    private static void Pushing(ModelLiberty task)
    {
        //double resultI = -1.0;
        //double resultS = -1.0;
        double cvI = -1.0;
        double cvS = -1.0;

        double V = task.Data.VertHeight;
        double DH = task.Data.DistHorz;
        double F = task.Data.Freq;

        if (task.Data.Gender == Gender.Male)
        {
            // Push – Initial – Male
            task.Initial.RL = 70.3;
            task.Initial.V = 1.2737 - V / 1.335 + Math.Pow(V, 2) / 2.576;
            task.Initial.DH = 1.0790 - Math.Log(DH) / 9.392;
            task.Initial.F = 0.6281 - Math.Log(F) / 13.07 - Math.Pow(Math.Log(F), 2) / 379.5;

            //resultI = 70.3 * (1.2737 - V / 1.335 + Math.Pow(V, 2) / 2.576) * (1.0790 - Math.Log(DH) / 9.392) * (0.6281 - Math.Log(F) / 13.07 - Math.Pow(Math.Log(F), 2) / 379.5);
            cvI = 0.231;

            // Push – Sustained – Male
            task.Sustained.RL = 65.3;
            task.Sustained.V = 2.2940 - V / 0.3345 + Math.Pow(V, 2) / 0.6687;
            task.Sustained.DH = 1.1035 - Math.Log(DH) / 7.170;
            task.Sustained.F = 0.4896 - Math.Log(F) / 10.20 - Math.Pow(Math.Log(F), 2) / 403.9;

            //resultS = 65.3 * (2.2940 - V / 0.3345 + Math.Pow(V, 2) / 0.6687) * (1.1035 - Math.Log(DH) / 7.170) * (0.4896 - Math.Log(F) / 10.20 - Math.Pow(Math.Log(F), 2) / 403.9);
            cvS = 0.267;
        }
        else if (task.Data.Gender == Gender.Female)
        {
            // Push or Pull – Initial – Female
            task.Initial.RL = 36.9;
            task.Initial.V = -0.5304 + V / 0.3361 - Math.Pow(V, 2) / 0.6915;
            task.Initial.DH = 1.0286 - DH / 72.22 + Math.Pow(DH, 2) / 9782;
            task.Initial.F = 0.7251 - Math.Log(F) / 13.19 - Math.Pow(Math.Log(F), 2) / 197.3;

            //resultI = 36.9 * (-0.5304 + V / 0.3361 - Math.Pow(V, 2) / 0.6915) * (1.0286 - DH / 72.22 + Math.Pow(DH, 2) / 9782) * (0.7251 - Math.Log(F) / 13.19 - Math.Pow(Math.Log(F), 2) / 197.3);

            cvI = 0.214;    // for Push – Initial – Female
                            //CoeffVar = 0.234;	// for Pull – Initial – Female

            // Push or Pull – Sustained – Female
            task.Sustained.RL = 25.5;
            task.Sustained.V = -0.6539 + V / 0.2941 - Math.Pow(V, 2) / 0.5722;
            task.Sustained.DH = 1.0391 - DH / 52.91 + Math.Pow(DH, 2) / 7975;
            task.Sustained.F = 0.6086 - Math.Log(F) / 11.95 - Math.Pow(Math.Log(F), 2) / 304.4;

            //resultS = 25.5 * (-0.6539 + V / 0.2941 - Math.Pow(V, 2) / 0.5722) * (1.0391 - DH / 52.91 + Math.Pow(DH, 2) / 7975) * (0.6086 - Math.Log(F) / 11.95 - Math.Pow(Math.Log(F), 2) / 304.4);

            cvS = 0.286;    // for Push – Sustained – Female;
                            //CoeffVar = 0.298;	// for Pull – Sustained – Female;
        }

        task.Initial.MAL = task.Initial.RL * task.Initial.V * task.Initial.DH * task.Initial.F;
        task.Initial.CV = cvI;

        task.Sustained.MAL = task.Sustained.RL * task.Sustained.V * task.Sustained.DH * task.Sustained.F;
        task.Sustained.CV = cvS;

        //data.results.IniForce = resultI;
        //data.results.SusForce = resultS;

        //data.results.IniCoeffV = cvI;
        //data.results.SusCoeffV = cvS;

        return;
    }

    private static void Pulling(ModelLiberty task)
    {
        //double resultI = -1.0;
        //double resultS = -1.0;
        double cvI = -1.0;
        double cvS = -1.0;

        double V = task.Data.VertHeight;
        double DH = task.Data.DistHorz;
        double F = task.Data.Freq;

        if (task.Data.Gender == Gender.Male)
        {
            // Pull – Initial – Male(note: only the RL, VSF, &CoeffVar values are different from the Push – Initiate – Male equation)
            task.Initial.RL = 69.8;
            task.Initial.V = 1.7186 - V / 0.6888 + Math.Pow(V, 2) / 2.025;
            task.Initial.DH = 1.0790 - Math.Log(DH) / 9.392;
            task.Initial.F = 0.6281 - Math.Log(F) / 13.07 - Math.Pow(Math.Log(F), 2) / 379.5;

            //resultI = 69.8 * (1.7186 - V / 0.6888 + Math.Pow(V, 2) / 2.025) * (1.0790 - Math.Log(DH) / 9.392) * (0.6281 - Math.Log(F) / 13.07 - Math.Pow(Math.Log(F), 2) / 379.5);
            cvI = 0.238;

            // Pull – Sustained – Male(note: only the RL, VSF, &CoeffVar values are different from the Push – Sustain – Male equation)
            task.Sustained.RL = 61.0;
            task.Sustained.V = 2.1977 - V / 0.3850 + Math.Pow(V, 2) / 0.9047;
            task.Sustained.DH = 1.1035 - Math.Log(DH) / 7.170;
            task.Sustained.F = 0.4896 - Math.Log(F) / 10.20 - Math.Pow(Math.Log(F), 2) / 403.9;

            //resultS = 61.0 * (2.1977 - V / 0.3850 + Math.Pow(V, 2) / 0.9047) * (1.1035 - Math.Log(DH) / 7.170) * (0.4896 - Math.Log(F) / 10.20 - Math.Pow(Math.Log(F), 2) / 403.9);
            cvS = 0.257;
        }
        else if (task.Data.Gender == Gender.Female)
        {
            // Push or Pull – Initial – Female
            task.Initial.RL = 36.9;
            task.Initial.V = -0.5304 + V / 0.3361 - Math.Pow(V, 2) / 0.6915;
            task.Initial.DH = 1.0286 - DH / 72.22 + Math.Pow(DH, 2) / 9782;
            task.Initial.F = 0.7251 - Math.Log(F) / 13.19 - Math.Pow(Math.Log(F), 2) / 197.3;

            //resultI = 36.9 * (-0.5304 + V / 0.3361 - Math.Pow(V, 2) / 0.6915) * (1.0286 - DH / 72.22 + Math.Pow(DH, 2) / 9782) * (0.7251 - Math.Log(F) / 13.19 - Math.Pow(Math.Log(F), 2) / 197.3);

            //CoeffVar = 0.214;	// for Push – Initial – Female
            cvI = 0.234;    // for Pull – Initial – Female

            // Push or Pull – Sustained – Female
            task.Sustained.RL = 25.5;
            task.Sustained.V = -0.6539 + V / 0.2941 - Math.Pow(V, 2) / 0.5722;
            task.Sustained.DH = 1.0391 - DH / 52.91 + Math.Pow(DH, 2) / 7975;
            task.Sustained.F = 0.6086 - Math.Log(F) / 11.95 - Math.Pow(Math.Log(F), 2) / 304.4;

            //resultS = 25.5 * (-0.6539 + V / 0.2941 - Math.Pow(V, 2) / 0.5722) * (1.0391 - DH / 52.91 + Math.Pow(DH, 2) / 7975) * (0.6086 - Math.Log(F) / 11.95 - Math.Pow(Math.Log(F), 2) / 304.4);

            //CoeffVar = 0.286;	// for Push – Sustained – Female;
            cvS = 0.298;    // for Pull – Sustained – Female;
        }

        task.Initial.MAL = task.Initial.RL * task.Initial.V * task.Initial.DH * task.Initial.F;
        task.Initial.CV = cvI;

        task.Sustained.MAL = task.Sustained.RL * task.Sustained.V * task.Sustained.DH * task.Sustained.F;
        task.Sustained.CV = cvS;

        //data.results.IniForce = resultI;
        //data.results.SusForce = resultS;

        //data.results.IniCoeffV = cvI;
        //data.results.SusCoeffV = cvS;

        return;
    }

    private static void Carrying(ModelLiberty task)
    {
        double CoeffVar = -1.0;
        double V = task.Data.VertHeight;
        double DH = task.Data.DistHorz;
        double F = task.Data.Freq;

        if (task.Data.Gender == Gender.Male)
        {
            //Carry – Male
            //result = 74.9 * (1.5505 - V / 1.417) * (1.1172 - Math.Log(DH) / 6.332) * (0.5149 - Math.Log(F) / 7.958 - Math.Pow(Math.Log(F), 2) / 131.1);
            task.Initial.RL = 74.9;
            task.Initial.V = 1.5505 - V / 1.417;
            task.Initial.DH = 1.1172 - Math.Log(DH) / 6.332;
            task.Initial.F = 0.5149 - Math.Log(F) / 7.958 - Math.Pow(Math.Log(F), 2) / 131.1;

            CoeffVar = 0.278;
        }
        else if (task.Data.Gender == Gender.Female)
        {
            //Carry – Female
            //result = 28.6 * (1.1645 - V / 4.437) * (1.0101 - DH / 207.8) * (0.6224 - Math.Log(F) / 16.33);
            task.Initial.RL = 28.6;
            task.Initial.V = 1.1645 - V / 4.437;
            task.Initial.DH = 1.0101 - DH / 207.8;
            task.Initial.F = 0.6224 - Math.Log(F) / 16.33;

            CoeffVar = 0.231;
        }

        task.Initial.MAL = task.Initial.RL * task.Initial.V * task.Initial.DH * task.Initial.F;
        task.Initial.CV = CoeffVar;

        //data.results.Weight = result;
        //data.results.IniCoeffV = CoeffVar;

        return;
    }

    ////////////////////////////////////////////////////////////////////////////////
    // double Gaussian_Distribution( double x )                                   //
    // http://www.mymathlib.com/functions/probability/gaussian_distribution.html  //
    //  Description:                                                              //
    //     This function returns the probability that a random variable with      //
    //     a standard Normal (Gaussian) distribution has a value less than "x".   //
    //                                                                            //
    //  Arguments:                                                                //
    //     double x   Argument of Pr[X < x] where X ~ N(0,1).                     //
    //                                                                            //
    //  Return Values:                                                            //
    //     The probability of observing a value less than (or equal) to x assuming//
    //     a normal (Gaussian) distribution with mean 0 and variance 1.           //
    //                                                                            //
    //  Example:                                                                  //
    //     double x, pr;                                                          //
    //                                                                            //
    //    pr = Gaussian_Distribution(x);                                          //
    ////////////////////////////////////////////////////////////////////////////////
    private static double Gaussian_Distribution(double x)
    {
        return 0.5 * (1.0 + Erf(M_SQRT1_2 * x));
    }

    ////////////////////////////////////////////////////////////////////////////////
    // double Gaussian_Density( double x )                                        //
    // http://www.mymathlib.com/functions/probability/gaussian_distribution.html  //
    //  Description:                                                              //
    //     This function returns the value of the probability density at X = x    //
    //     where X has a standard Normal (Gaussian) distribution.                 //
    //                                                                            //
    //  Arguments:                                                                //
    //     double x   Argument of dPr[X < x]/dx where X ~ N(0,1).                 //
    //                                                                            //
    //  Return Values:                                                            //
    //     The value of the probability density at X = x where X has a Normal     //
    //     (Gaussian) distribution with mean 0 and variance 1.                    //
    //                                                                            //
    //  Example:                                                                  //
    //     double x, pr;                                                          //
    //                                                                            //
    //     pr = Gaussian_Density(x);                                              //
    ////////////////////////////////////////////////////////////////////////////////
    private static double Gaussian_Density(double x)
    {
        return 0.5 * M_SQRT1_2 * M_2_SQRTPI * Math.Exp(-0.5 * x * x);
    }

    // https://github.com/antelopeusersgroup/antelope_contrib/blob/master/lib/location/libgenloc/erfinv.c
    // https://github.com/lakshayg/erfinv/blob/master/erfinv.c


    /// <summary>
    /// Returns the value of the gaussian error function at <paramref name="x"/>.
    /// https://stackoverflow.com/questions/22834998/what-reference-should-i-use-to-use-erf-erfc-function
    /// </summary>
    private static double Erf(double x)
    {
        /*
        Copyright (C) 1993 by Sun Microsystems, Inc. All rights reserved.
        *
        * Developed at SunPro, a Sun Microsystems, Inc. business.
        * Permission to use, copy, modify, and distribute this
        * software is freely granted, provided that this notice
        * is preserved.
        */

        #region Constants

        const double tiny = 1e-300;
        const double erx = 8.45062911510467529297e-01;

        // Coefficients for approximation to erf on [0, 0.84375]
        const double efx = 1.28379167095512586316e-01; /* 0x3FC06EBA; 0x8214DB69 */
        const double efx8 = 1.02703333676410069053e+00; /* 0x3FF06EBA; 0x8214DB69 */
        const double pp0 = 1.28379167095512558561e-01; /* 0x3FC06EBA; 0x8214DB68 */
        const double pp1 = -3.25042107247001499370e-01; /* 0xBFD4CD7D; 0x691CB913 */
        const double pp2 = -2.84817495755985104766e-02; /* 0xBF9D2A51; 0xDBD7194F */
        const double pp3 = -5.77027029648944159157e-03; /* 0xBF77A291; 0x236668E4 */
        const double pp4 = -2.37630166566501626084e-05; /* 0xBEF8EAD6; 0x120016AC */
        const double qq1 = 3.97917223959155352819e-01; /* 0x3FD97779; 0xCDDADC09 */
        const double qq2 = 6.50222499887672944485e-02; /* 0x3FB0A54C; 0x5536CEBA */
        const double qq3 = 5.08130628187576562776e-03; /* 0x3F74D022; 0xC4D36B0F */
        const double qq4 = 1.32494738004321644526e-04; /* 0x3F215DC9; 0x221C1A10 */
        const double qq5 = -3.96022827877536812320e-06; /* 0xBED09C43; 0x42A26120 */

        // Coefficients for approximation to erf in [0.84375, 1.25]
        const double pa0 = -2.36211856075265944077e-03; /* 0xBF6359B8; 0xBEF77538 */
        const double pa1 = 4.14856118683748331666e-01; /* 0x3FDA8D00; 0xAD92B34D */
        const double pa2 = -3.72207876035701323847e-01; /* 0xBFD7D240; 0xFBB8C3F1 */
        const double pa3 = 3.18346619901161753674e-01; /* 0x3FD45FCA; 0x805120E4 */
        const double pa4 = -1.10894694282396677476e-01; /* 0xBFBC6398; 0x3D3E28EC */
        const double pa5 = 3.54783043256182359371e-02; /* 0x3FA22A36; 0x599795EB */
        const double pa6 = -2.16637559486879084300e-03; /* 0xBF61BF38; 0x0A96073F */
        const double qa1 = 1.06420880400844228286e-01; /* 0x3FBB3E66; 0x18EEE323 */
        const double qa2 = 5.40397917702171048937e-01; /* 0x3FE14AF0; 0x92EB6F33 */
        const double qa3 = 7.18286544141962662868e-02; /* 0x3FB2635C; 0xD99FE9A7 */
        const double qa4 = 1.26171219808761642112e-01; /* 0x3FC02660; 0xE763351F */
        const double qa5 = 1.36370839120290507362e-02; /* 0x3F8BEDC2; 0x6B51DD1C */
        const double qa6 = 1.19844998467991074170e-02; /* 0x3F888B54; 0x5735151D */

        // Coefficients for approximation to erfc in [1.25, 1/0.35]
        const double ra0 = -9.86494403484714822705e-03; /* 0xBF843412; 0x600D6435 */
        const double ra1 = -6.93858572707181764372e-01; /* 0xBFE63416; 0xE4BA7360 */
        const double ra2 = -1.05586262253232909814e+01; /* 0xC0251E04; 0x41B0E726 */
        const double ra3 = -6.23753324503260060396e+01; /* 0xC04F300A; 0xE4CBA38D */
        const double ra4 = -1.62396669462573470355e+02; /* 0xC0644CB1; 0x84282266 */
        const double ra5 = -1.84605092906711035994e+02; /* 0xC067135C; 0xEBCCABB2 */
        const double ra6 = -8.12874355063065934246e+01; /* 0xC0545265; 0x57E4D2F2 */
        const double ra7 = -9.81432934416914548592e+00; /* 0xC023A0EF; 0xC69AC25C */
        const double sa1 = 1.96512716674392571292e+01; /* 0x4033A6B9; 0xBD707687 */
        const double sa2 = 1.37657754143519042600e+02; /* 0x4061350C; 0x526AE721 */
        const double sa3 = 4.34565877475229228821e+02; /* 0x407B290D; 0xD58A1A71 */
        const double sa4 = 6.45387271733267880336e+02; /* 0x40842B19; 0x21EC2868 */
        const double sa5 = 4.29008140027567833386e+02; /* 0x407AD021; 0x57700314 */
        const double sa6 = 1.08635005541779435134e+02; /* 0x405B28A3; 0xEE48AE2C */
        const double sa7 = 6.57024977031928170135e+00; /* 0x401A47EF; 0x8E484A93 */
        const double sa8 = -6.04244152148580987438e-02; /* 0xBFAEEFF2; 0xEE749A62 */

        // Coefficients for approximation to erfc in [1/0.35, 28]
        const double rb0 = -9.86494292470009928597e-03; /* 0xBF843412; 0x39E86F4A */
        const double rb1 = -7.99283237680523006574e-01; /* 0xBFE993BA; 0x70C285DE */
        const double rb2 = -1.77579549177547519889e+01; /* 0xC031C209; 0x555F995A */
        const double rb3 = -1.60636384855821916062e+02; /* 0xC064145D; 0x43C5ED98 */
        const double rb4 = -6.37566443368389627722e+02; /* 0xC083EC88; 0x1375F228 */
        const double rb5 = -1.02509513161107724954e+03; /* 0xC0900461; 0x6A2E5992 */
        const double rb6 = -4.83519191608651397019e+02; /* 0xC07E384E; 0x9BDC383F */
        const double sb1 = 3.03380607434824582924e+01; /* 0x403E568B; 0x261D5190 */
        const double sb2 = 3.25792512996573918826e+02; /* 0x40745CAE; 0x221B9F0A */
        const double sb3 = 1.53672958608443695994e+03; /* 0x409802EB; 0x189D5118 */
        const double sb4 = 3.19985821950859553908e+03; /* 0x40A8FFB7; 0x688C246A */
        const double sb5 = 2.55305040643316442583e+03; /* 0x40A3F219; 0xCEDF3BE6 */
        const double sb6 = 4.74528541206955367215e+02; /* 0x407DA874; 0xE79FE763 */
        const double sb7 = -2.24409524465858183362e+01; /* 0xC03670E2; 0x42712D62 */

        #endregion

        if (double.IsNaN(x))
            return double.NaN;

        if (double.IsNegativeInfinity(x))
            return -1.0;

        if (double.IsPositiveInfinity(x))
            return 1.0;

        int n0, hx, ix, i;
        double R, S, P, Q, s, y, z, r;
        unsafe
        {
            double one = 1.0;
            n0 = ((*(int*)&one) >> 29) ^ 1;
            hx = *(n0 + (int*)&x);
        }
        ix = hx & 0x7FFFFFFF;

        if (ix < 0x3FEB0000) // |x| < 0.84375
        {
            if (ix < 0x3E300000) // |x| < 2**-28
            {
                if (ix < 0x00800000)
                    return 0.125 * (8.0 * x + efx8 * x); // avoid underflow
                return x + efx * x;
            }
            z = x * x;
            r = pp0 + z * (pp1 + z * (pp2 + z * (pp3 + z * pp4)));
            s = 1.0 + z * (qq1 + z * (qq2 + z * (qq3 + z * (qq4 + z * qq5))));
            y = r / s;
            return x + x * y;
        }
        if (ix < 0x3FF40000) // 0.84375 <= |x| < 1.25
        {
            s = Math.Abs(x) - 1.0;
            P = pa0 + s * (pa1 + s * (pa2 + s * (pa3 + s * (pa4 + s * (pa5 + s * pa6)))));
            Q = 1.0 + s * (qa1 + s * (qa2 + s * (qa3 + s * (qa4 + s * (qa5 + s * qa6)))));
            if (hx >= 0)
                return erx + P / Q;
            else
                return -erx - P / Q;
        }
        if (ix >= 0x40180000) // inf > |x| >= 6
        {
            if (hx >= 0)
                return 1.0 - tiny;
            else
                return tiny - 1.0;
        }
        x = Math.Abs(x);
        s = 1.0 / (x * x);
        if (ix < 0x4006DB6E) // |x| < 1/0.35
        {
            R = ra0 + s * (ra1 + s * (ra2 + s * (ra3 + s * (ra4 + s * (ra5 + s * (ra6 + s * ra7))))));
            S = 1.0 + s * (sa1 + s * (sa2 + s * (sa3 + s * (sa4 + s * (sa5 + s * (sa6 + s * (sa7 + s * sa8)))))));
        }
        else // |x| >= 1/0.35
        {
            R = rb0 + s * (rb1 + s * (rb2 + s * (rb3 + s * (rb4 + s * (rb5 + s * rb6)))));
            S = 1.0 + s * (sb1 + s * (sb2 + s * (sb3 + s * (sb4 + s * (sb5 + s * (sb6 + s * sb7))))));
        }
        z = x;
        unsafe { *(1 - n0 + (int*)&z) = 0; }
        r = Math.Exp(-z * z - 0.5625) * Math.Exp((z - x) * (z + x) + R / S);
        if (hx >= 0)
            return 1.0 - r / x;
        else
            return r / x - 1.0;
    }

    /// <summary>
    /// Returns the value of the complementary error function at <paramref name="x"/>.
    /// </summary>
    private static double Erfc(double x)
    {
        /*
        Copyright (C) 1993 by Sun Microsystems, Inc. All rights reserved.
        *
        * Developed at SunPro, a Sun Microsystems, Inc. business.
        * Permission to use, copy, modify, and distribute this
        * software is freely granted, provided that this notice
        * is preserved.
        */

        #region Constants

        const double tiny = 1e-300;
        const double erx = 8.45062911510467529297e-01;

        // Coefficients for approximation to erf on [0, 0.84375]
        const double efx = 1.28379167095512586316e-01; /* 0x3FC06EBA; 0x8214DB69 */
        const double efx8 = 1.02703333676410069053e+00; /* 0x3FF06EBA; 0x8214DB69 */
        const double pp0 = 1.28379167095512558561e-01; /* 0x3FC06EBA; 0x8214DB68 */
        const double pp1 = -3.25042107247001499370e-01; /* 0xBFD4CD7D; 0x691CB913 */
        const double pp2 = -2.84817495755985104766e-02; /* 0xBF9D2A51; 0xDBD7194F */
        const double pp3 = -5.77027029648944159157e-03; /* 0xBF77A291; 0x236668E4 */
        const double pp4 = -2.37630166566501626084e-05; /* 0xBEF8EAD6; 0x120016AC */
        const double qq1 = 3.97917223959155352819e-01; /* 0x3FD97779; 0xCDDADC09 */
        const double qq2 = 6.50222499887672944485e-02; /* 0x3FB0A54C; 0x5536CEBA */
        const double qq3 = 5.08130628187576562776e-03; /* 0x3F74D022; 0xC4D36B0F */
        const double qq4 = 1.32494738004321644526e-04; /* 0x3F215DC9; 0x221C1A10 */
        const double qq5 = -3.96022827877536812320e-06; /* 0xBED09C43; 0x42A26120 */

        // Coefficients for approximation to erf in [0.84375, 1.25]
        const double pa0 = -2.36211856075265944077e-03; /* 0xBF6359B8; 0xBEF77538 */
        const double pa1 = 4.14856118683748331666e-01; /* 0x3FDA8D00; 0xAD92B34D */
        const double pa2 = -3.72207876035701323847e-01; /* 0xBFD7D240; 0xFBB8C3F1 */
        const double pa3 = 3.18346619901161753674e-01; /* 0x3FD45FCA; 0x805120E4 */
        const double pa4 = -1.10894694282396677476e-01; /* 0xBFBC6398; 0x3D3E28EC */
        const double pa5 = 3.54783043256182359371e-02; /* 0x3FA22A36; 0x599795EB */
        const double pa6 = -2.16637559486879084300e-03; /* 0xBF61BF38; 0x0A96073F */
        const double qa1 = 1.06420880400844228286e-01; /* 0x3FBB3E66; 0x18EEE323 */
        const double qa2 = 5.40397917702171048937e-01; /* 0x3FE14AF0; 0x92EB6F33 */
        const double qa3 = 7.18286544141962662868e-02; /* 0x3FB2635C; 0xD99FE9A7 */
        const double qa4 = 1.26171219808761642112e-01; /* 0x3FC02660; 0xE763351F */
        const double qa5 = 1.36370839120290507362e-02; /* 0x3F8BEDC2; 0x6B51DD1C */
        const double qa6 = 1.19844998467991074170e-02; /* 0x3F888B54; 0x5735151D */

        // Coefficients for approximation to erfc in [1.25, 1/0.35]
        const double ra0 = -9.86494403484714822705e-03; /* 0xBF843412; 0x600D6435 */
        const double ra1 = -6.93858572707181764372e-01; /* 0xBFE63416; 0xE4BA7360 */
        const double ra2 = -1.05586262253232909814e+01; /* 0xC0251E04; 0x41B0E726 */
        const double ra3 = -6.23753324503260060396e+01; /* 0xC04F300A; 0xE4CBA38D */
        const double ra4 = -1.62396669462573470355e+02; /* 0xC0644CB1; 0x84282266 */
        const double ra5 = -1.84605092906711035994e+02; /* 0xC067135C; 0xEBCCABB2 */
        const double ra6 = -8.12874355063065934246e+01; /* 0xC0545265; 0x57E4D2F2 */
        const double ra7 = -9.81432934416914548592e+00; /* 0xC023A0EF; 0xC69AC25C */
        const double sa1 = 1.96512716674392571292e+01; /* 0x4033A6B9; 0xBD707687 */
        const double sa2 = 1.37657754143519042600e+02; /* 0x4061350C; 0x526AE721 */
        const double sa3 = 4.34565877475229228821e+02; /* 0x407B290D; 0xD58A1A71 */
        const double sa4 = 6.45387271733267880336e+02; /* 0x40842B19; 0x21EC2868 */
        const double sa5 = 4.29008140027567833386e+02; /* 0x407AD021; 0x57700314 */
        const double sa6 = 1.08635005541779435134e+02; /* 0x405B28A3; 0xEE48AE2C */
        const double sa7 = 6.57024977031928170135e+00; /* 0x401A47EF; 0x8E484A93 */
        const double sa8 = -6.04244152148580987438e-02; /* 0xBFAEEFF2; 0xEE749A62 */

        // Coefficients for approximation to erfc in [1/0.35, 28]
        const double rb0 = -9.86494292470009928597e-03; /* 0xBF843412; 0x39E86F4A */
        const double rb1 = -7.99283237680523006574e-01; /* 0xBFE993BA; 0x70C285DE */
        const double rb2 = -1.77579549177547519889e+01; /* 0xC031C209; 0x555F995A */
        const double rb3 = -1.60636384855821916062e+02; /* 0xC064145D; 0x43C5ED98 */
        const double rb4 = -6.37566443368389627722e+02; /* 0xC083EC88; 0x1375F228 */
        const double rb5 = -1.02509513161107724954e+03; /* 0xC0900461; 0x6A2E5992 */
        const double rb6 = -4.83519191608651397019e+02; /* 0xC07E384E; 0x9BDC383F */
        const double sb1 = 3.03380607434824582924e+01; /* 0x403E568B; 0x261D5190 */
        const double sb2 = 3.25792512996573918826e+02; /* 0x40745CAE; 0x221B9F0A */
        const double sb3 = 1.53672958608443695994e+03; /* 0x409802EB; 0x189D5118 */
        const double sb4 = 3.19985821950859553908e+03; /* 0x40A8FFB7; 0x688C246A */
        const double sb5 = 2.55305040643316442583e+03; /* 0x40A3F219; 0xCEDF3BE6 */
        const double sb6 = 4.74528541206955367215e+02; /* 0x407DA874; 0xE79FE763 */
        const double sb7 = -2.24409524465858183362e+01; /* 0xC03670E2; 0x42712D62 */

        #endregion

        if (double.IsNaN(x))
            return double.NaN;

        if (double.IsNegativeInfinity(x))
            return 2.0;

        if (double.IsPositiveInfinity(x))
            return 0.0;

        int n0, hx, ix;
        double R, S, P, Q, s, y, z, r;
        unsafe
        {
            double one = 1.0;
            n0 = ((*(int*)&one) >> 29) ^ 1;
            hx = *(n0 + (int*)&x);
        }
        ix = hx & 0x7FFFFFFF;

        if (ix < 0x3FEB0000) // |x| < 0.84375
        {
            if (ix < 0x3C700000) // |x| < 2**-56
                return 1.0 - x;
            z = x * x;
            r = pp0 + z * (pp1 + z * (pp2 + z * (pp3 + z * pp4)));
            s = 1.0 + z * (qq1 + z * (qq2 + z * (qq3 + z * (qq4 + z * qq5))));
            y = r / s;
            if (hx < 0x3FD00000) // x < 1/4
                return 1.0 - (x + x * y);
            else
            {
                r = x * y;
                r += (x - 0.5);
                return 0.5 - r;
            }
        }
        if (ix < 0x3FF40000) // 0.84375 <= |x| < 1.25
        {
            s = Math.Abs(x) - 1.0;
            P = pa0 + s * (pa1 + s * (pa2 + s * (pa3 + s * (pa4 + s * (pa5 + s * pa6)))));
            Q = 1.0 + s * (qa1 + s * (qa2 + s * (qa3 + s * (qa4 + s * (qa5 + s * qa6)))));
            if (hx >= 0)
            {
                z = 1.0 - erx;
                return z - P / Q;
            }
            else
            {
                z = erx + P / Q;
                return 1.0 + z;
            }
        }
        if (ix < 0x403C0000) // |x| < 28
        {
            x = Math.Abs(x);
            s = 1.0 / (x * x);
            if (ix < 0x4006DB6D) // |x| < 1/.35 ~ 2.857143
            {
                R = ra0 + s * (ra1 + s * (ra2 + s * (ra3 + s * (ra4 + s * (ra5 + s * (ra6 + s * ra7))))));
                S = 1.0 + s * (sa1 + s * (sa2 + s * (sa3 + s * (sa4 + s * (sa5 + s * (sa6 + s * (sa7 + s * sa8)))))));
            }
            else // |x| >= 1/.35 ~ 2.857143
            {
                if (hx < 0 && ix >= 0x40180000)
                    return 2.0 - tiny; // x < -6
                R = rb0 + s * (rb1 + s * (rb2 + s * (rb3 + s * (rb4 + s * (rb5 + s * rb6)))));
                S = 1.0 + s * (sb1 + s * (sb2 + s * (sb3 + s * (sb4 + s * (sb5 + s * (sb6 + s * sb7))))));
            }
            z = x;
            unsafe { *(1 - n0 + (int*)&z) = 0; }
            r = Math.Exp(-z * z - 0.5625) *
            Math.Exp((z - x) * (z + x) + R / S);
            if (hx > 0)
                return r / x;
            else
                return 2.0 - r / x;
        }
        else
        {
            if (hx > 0)
                return tiny * tiny;
            else
                return 2.0 - tiny;
        }
    }
}