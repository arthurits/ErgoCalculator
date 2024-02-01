using System.Text;

namespace ErgoCalc.Models.LibertyMutual;

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
    public ModelLiberty[] Tasks { get; set; } = [];
    public int NumberTasks { get; set; } = 0;

    public string ToString(string[] strRows, System.Globalization.CultureInfo? culture = null)
    {
        // https://stackoverflow.com/questions/26235759/how-to-format-a-double-with-fixed-number-of-significant-digits-regardless-of-th
        // https://stackoverflow.com/questions/33401356/formatting-text-with-padding-does-not-line-up-in-c-sharp
        StringBuilder strResult = new(2200);
        string[] strLineD = new String[9];
        string[] strLineF = new String[12];
        string[] strThresholds = new string[13];

        if (culture is null)
            culture = System.Globalization.CultureInfo.CurrentCulture;

        int i = 0;
        foreach (var data in Tasks)
        {
            strLineD[0] += $"\t{strRows[0]} {((char)('A' + i)).ToString(culture)}";
            //strLineD[1] += $"\t{data.Data.Type.ToString()}";
            //strLineD[8] += $"\t{data.Data.Gender.ToString()}";
            strLineD[1] += $"\t{strRows[38].Split(", ")[(int)data.Data.Type]}";
            strLineD[8] += $"\t{strRows[37].Split(", ")[(int)data.Data.Gender]}";

            strLineF[0] += $"\t{strRows[0]} {((char)('A' + i)).ToString(culture)}";

            strThresholds[0] += $"\t{strRows[0]} {((char)('A' + i)).ToString(culture)}";
            if (data.Data.Type == TaskType.Pulling || data.Data.Type == TaskType.Pushing)
            {
                strLineD[2] += $"\t{strRows[36]}";
                strLineD[3] += $"\t{strRows[36]}";
                // Convert.ToDouble(String.Format("{0:G3}", number)).ToString()
                //strLineD[4] += string.Concat("\t\t", data.Data.DistHorz.ToString("0.####"));
                strLineD[4] += $"\t{Convert.ToDouble(String.Format("{0:G5}", data.Data.DistHorz)).ToString(culture)}";
                strLineD[5] += $"\t{strRows[36]}";
                strLineD[6] += $"\t{data.Data.VertHeight.ToString("G5", culture)}";
                strLineD[7] += $"\t{data.Data.Freq.ToString("G5", culture)}";

                strLineF[1] += $"\t{data.Initial.RL.ToString("0.####", culture)}";
                strLineF[2] += $"\t{strRows[36]}";
                strLineF[3] += $"\t{strRows[36]}";
                strLineF[4] += $"\t{data.Initial.DH.ToString("0.####", culture)}";
                strLineF[5] += $"\t{strRows[36]}";
                strLineF[6] += $"\t{data.Initial.V.ToString("0.####", culture)}";
                strLineF[7] += $"\t{data.Initial.F.ToString("0.####", culture)}";

                strLineF[8] += $"\t{data.Sustained.RL.ToString("0.####", culture)}";
                strLineF[9] += $"\t{data.Sustained.DH.ToString("0.####", culture)}";
                strLineF[10] += $"\t{data.Sustained.V.ToString("0.####", culture)}";
                strLineF[11] += $"\t{data.Sustained.F.ToString("0.####", culture)}";

                strThresholds[1] += $"\t{data.Initial.CV.ToString("0.####", culture)}";
                strThresholds[2] += $"\t{Convert.ToDouble(String.Format("{0:G5}", data.Initial.MAL)).ToString("0.####", culture)}";
                strThresholds[3] += $"\t{Convert.ToDouble(String.Format("{0:G5}", data.Initial.MAL75)).ToString("0.####", culture)}";
                strThresholds[4] += $"\t{Convert.ToDouble(String.Format("{0:G5}", data.Initial.MAL90)).ToString("0.####", culture)}";
                strThresholds[5] += $"\t{data.Sustained.CV.ToString("0.####", culture)}";
                strThresholds[6] += $"\t{Convert.ToDouble(String.Format("{0:G5}", data.Sustained.MAL)).ToString("0.####", culture)}";
                strThresholds[7] += $"\t{Convert.ToDouble(String.Format("{0:G5}", data.Sustained.MAL75)).ToString("0.####", culture)}";
                strThresholds[8] += $"\t{Convert.ToDouble(String.Format("{0:G5}", data.Sustained.MAL90)).ToString("0.####", culture)}";
                strThresholds[9] += $"\t{strRows[36]}";
                strThresholds[10] += $"\t{strRows[36]}";
                strThresholds[11] += $"\t{strRows[36]}";
                strThresholds[12] += $"\t{strRows[36]}";
            }
            else // Lift, lower, and carry
            {
                if (data.Data.Type == TaskType.Carrying)
                {
                    strLineD[2] += $"\t{strRows[36]}";
                    strLineD[3] += $"\t{strRows[36]}";
                    strLineD[4] += $"\t{data.Data.DistHorz.ToString("0.####", culture)}";
                    strLineD[5] += $"\t{strRows[36]}";
                    strLineD[6] += $"\t{data.Data.VertHeight.ToString("0.####", culture)}";
                    strLineD[7] += $"\t{data.Data.Freq.ToString("0.####", culture)}";

                    strLineF[1] += $"\t{data.Initial.RL.ToString("0.####", culture)}";
                    strLineF[2] += $"\t{strRows[36]}";
                    strLineF[3] += $"\t{strRows[36]}";
                    strLineF[4] += $"\t{data.Initial.DV.ToString("0.####", culture)}";
                    strLineF[5] += $"\t{strRows[36]}";
                    strLineF[6] += $"\t{data.Initial.V.ToString("0.####", culture)}";
                    strLineF[7] += $"\t{data.Initial.F.ToString("0.####", culture)}";
                }
                else //lift and lower
                {
                    //strLineD[2] += string.Concat("\t\t", data.Data.HorzReach.ToString("0.####"));
                    strLineD[2] += $"\t{Convert.ToDouble(String.Format("{0:G5}", data.Data.HorzReach)).ToString(culture)}";
                    strLineD[3] += $"\t{data.Data.VertRangeM.ToString("0.####", culture)}";
                    strLineD[4] += $"\t{strRows[36]}";
                    strLineD[5] += $"\t{data.Data.DistVert.ToString("0.####", culture)}";
                    strLineD[6] += $"\t{strRows[36]}";
                    strLineD[7] += $"\t{data.Data.Freq.ToString("0.####", culture)}";

                    strLineF[1] += $"\t{data.Initial.RL.ToString("0.####", culture)}";
                    strLineF[2] += $"\t{data.Initial.H.ToString("0.####", culture)}";
                    strLineF[3] += $"\t{data.Initial.VRM.ToString("0.####", culture)}";
                    strLineF[4] += $"\t{strRows[36]}";
                    strLineF[5] += $"\t{data.Initial.DV.ToString("0.####", culture)}";
                    strLineF[6] += $"\t{strRows[36]}";
                    strLineF[7] += $"\t{data.Initial.F.ToString("0.####", culture)}";
                }

                strLineF[8] += $"\t{strRows[36]}";
                strLineF[9] += $"\t{strRows[36]}";
                strLineF[10] += $"\t{strRows[36]}";
                strLineF[11] += $"\t{strRows[36]}";

                strThresholds[1] += $"\t{strRows[36]}";
                strThresholds[2] += $"\t{strRows[36]}";
                strThresholds[3] += $"\t{strRows[36]}";
                strThresholds[4] += $"\t{strRows[36]}";
                strThresholds[5] += $"\t{strRows[36]}";
                strThresholds[6] += $"\t{strRows[36]}";
                strThresholds[7] += $"\t{strRows[36]}";
                strThresholds[8] += $"\t{strRows[36]}";
                strThresholds[9] += $"\t{data.Initial.CV.ToString("0.####", culture)}";
                //strThresholds[10] += string.Concat("\t\t", data.Initial.MAL.ToString("0.####"));
                strThresholds[10] += $"\t{Convert.ToDouble(String.Format("{0:G5}", data.Initial.MAL)).ToString(culture)}";
                strThresholds[11] += $"\t{Convert.ToDouble(String.Format("{0:G5}", data.Initial.MAL75)).ToString("0.####", culture)}";
                strThresholds[12] += $"\t{Convert.ToDouble(String.Format("{0:G5}", data.Initial.MAL90)).ToString("0.####", culture)}";
            }
            i++;
        }

        strResult.Append(strRows[1]);
        strResult.Append(System.Environment.NewLine);
        strResult.Append(System.Environment.NewLine);

        // Initial data
        strResult.Append(strRows[2] + strLineD[0] + System.Environment.NewLine);
        strResult.Append(strRows[3] + strLineD[1] + System.Environment.NewLine);
        strResult.Append(strRows[4] + strLineD[2] + System.Environment.NewLine);
        strResult.Append(strRows[5] + strLineD[3] + System.Environment.NewLine);
        strResult.Append(strRows[6] + strLineD[4] + System.Environment.NewLine);
        strResult.Append(strRows[7] + strLineD[5] + System.Environment.NewLine);
        strResult.Append(strRows[8] + strLineD[6] + System.Environment.NewLine);
        strResult.Append(strRows[9] + strLineD[7] + System.Environment.NewLine);
        strResult.Append(strRows[10] + strLineD[8] + System.Environment.NewLine);
        strResult.Append(System.Environment.NewLine);

        strResult.Append(strRows[11] + strLineF[0] + System.Environment.NewLine);
        strResult.Append(strRows[12] + strLineF[1] + System.Environment.NewLine);
        strResult.Append(strRows[13] + strLineF[2] + System.Environment.NewLine);
        strResult.Append(strRows[14] + strLineF[3] + System.Environment.NewLine);
        strResult.Append(strRows[15] + strLineF[4] + System.Environment.NewLine);
        strResult.Append(strRows[16] + strLineF[5] + System.Environment.NewLine);
        strResult.Append(strRows[17] + strLineF[6] + System.Environment.NewLine);
        strResult.Append(strRows[18] + strLineF[7] + System.Environment.NewLine);
        strResult.Append(System.Environment.NewLine);
        strResult.Append(strRows[19] + strLineF[8] + System.Environment.NewLine);
        strResult.Append(strRows[20] + strLineF[9] + System.Environment.NewLine);
        strResult.Append(strRows[21] + strLineF[10] + System.Environment.NewLine);
        strResult.Append(strRows[22] + strLineF[11] + System.Environment.NewLine);
        strResult.Append(System.Environment.NewLine);

        strResult.Append(strRows[23] + strThresholds[0] + System.Environment.NewLine);
        strResult.Append(strRows[24] + strThresholds[1] + System.Environment.NewLine);
        strResult.Append(strRows[25] + strThresholds[2] + System.Environment.NewLine);
        strResult.Append(strRows[26] + strThresholds[3] + System.Environment.NewLine);
        strResult.Append(strRows[27] + strThresholds[4] + System.Environment.NewLine);
        strResult.Append(strRows[28] + strThresholds[5] + System.Environment.NewLine);
        strResult.Append(strRows[29] + strThresholds[6] + System.Environment.NewLine);
        strResult.Append(strRows[30] + strThresholds[7] + System.Environment.NewLine);
        strResult.Append(strRows[31] + strThresholds[8] + System.Environment.NewLine);
        strResult.Append(strRows[32] + strThresholds[9] + System.Environment.NewLine);
        strResult.Append(strRows[33] + strThresholds[10] + System.Environment.NewLine);
        strResult.Append(strRows[34] + strThresholds[11] + System.Environment.NewLine);
        strResult.Append(strRows[35] + strThresholds[12] + System.Environment.NewLine);

        return strResult.ToString();
    }

    public override string ToString()
    {
        string[] strRows =
        [
            "Task",
            "These are the results from the Liberty Mutual manual materials handling equations",
            "Initial data",
            "Manual handling type",
            "Horizontal reach H (m)",
            "Vertical range VRM (m)",
            "Horizontal distance DH (m)",
            "Vertical distance DV (m)",
            "Vertical height V (m)",
            "Frequency (actions/min)",
            "Sex",
            "Scale factors",
            "Reference load (LC)",
            "Horizontal reach (H)",
            "Vertical range (VRM)",
            "Horizontal distance (DH)",
            "Vertical distance (DV)",
            "Vertical height (V)",
            "Frequency factor (F)",
            "Sustained LC factor",
            "Sustained DH factor",
            "Sustained V factor",
            "Sustained F factor",
            "Maximum acceptable limit",
            "CV initial force",
            "MAL initial (kgf) for 50%",
            "MAL initial (kgf) for 75%",
            "MAL initial (kgf) for 90%",
            "CV sustained force",
            "MAL sustained (kgf) for 50%",
            "MAL sustained (kgf) for 75%",
            "MAL sustained (kgf) for 90%",
            "CV weight",
            "MAL weight (kg) for 50%",
            "MAL weight (kg) for 75%",
            "MAL weight (kg) for 90%",
            "------",
            "Male, Female",
            "Carrying, Lifting, Lowering, Pulling, Pushing"
        ];
        return ToString(strRows);
    }
}

public static class LibertyMutual
{
    private static readonly double zscore75 = 0.6744897501960818; // https://planetcalc.com/7803/
    private static readonly double zscore90 = 1.2815515655446008;	// https://planetcalc.com/7803/

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
        //double DH = task.Data.DistHorz;
        double DV = task.Data.DistVert;
        //double V = task.Data.VertHeight;
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

        return;
    }

    private static void Lowering(ModelLiberty task)
    {
        double CoeffVar = -1.0;
        double H = task.Data.HorzReach;
        double VRM = task.Data.VertRangeM;
        //double DH = task.Data.DistHorz;
        double DV = task.Data.DistVert;
        //double V = task.Data.VertHeight;
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

        return;
    }

    private static void Pushing(ModelLiberty task)
    {
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
            task.Sustained.V = 2.2940 - V / 0.3345 + Math.Pow(V, 2) / 0.6887;
            task.Sustained.DH = 1.1035 - Math.Log(DH) / 7.170;
            task.Sustained.F = 0.4896 - Math.Log(F) / 10.20 - Math.Pow(Math.Log(F), 2) / 403.9;

            //resultS = 65.3 * (2.2940 - V / 0.3345 + Math.Pow(V, 2) / 0.6887) * (1.1035 - Math.Log(DH) / 7.170) * (0.4896 - Math.Log(F) / 10.20 - Math.Pow(Math.Log(F), 2) / 403.9);
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

        return;
    }

    private static void Pulling(ModelLiberty task)
    {
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

}