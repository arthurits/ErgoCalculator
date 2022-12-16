using System;
using System.Text;

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

public class TaskModel
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
    public TaskModel[] Tasks { get; set; } = Array.Empty<TaskModel>();

    public int NumberTasks { get; set; } = 0;

    //public override string ToString()
    //{
    //    string[] strLineD = new string[8];
    //    string[] strLineF = new string[9];

    //    strLineD[0] = string.Concat(System.Environment.NewLine, "Description", "\t");
    //    strLineD[1] = string.Concat(System.Environment.NewLine, "Air temperature (°C)");
    //    strLineD[2] = string.Concat(System.Environment.NewLine, "Radiant temperature (°C)");
    //    strLineD[3] = string.Concat(System.Environment.NewLine, "Air velocity (m/s)");
    //    strLineD[4] = string.Concat(System.Environment.NewLine, "Relative humidity (%)");
    //    strLineD[5] = string.Concat(System.Environment.NewLine, "Clothing (clo)", "\t");
    //    strLineD[6] = string.Concat(System.Environment.NewLine, "Metabolic rate (met)");
    //    strLineD[7] = string.Concat(System.Environment.NewLine, "External work (met)");

    //    strLineF[0] = string.Concat(System.Environment.NewLine, "Description", "\t");
    //    strLineF[1] = string.Concat(System.Environment.NewLine, "Heat loss diff. through skin");
    //    strLineF[2] = string.Concat(System.Environment.NewLine, "Heat loss by sweating", "\t");
    //    strLineF[3] = string.Concat(System.Environment.NewLine, "Latent respiration heat loss");
    //    strLineF[4] = string.Concat(System.Environment.NewLine, "Dry respiration heat loss", "\t");
    //    strLineF[5] = string.Concat(System.Environment.NewLine, "Heat loss by radiation", "\t");
    //    strLineF[6] = string.Concat(System.Environment.NewLine, "Heat loss by convection");

    //    strLineF[7] = string.Concat(System.Environment.NewLine, System.Environment.NewLine, "The PMV index");
    //    strLineF[8] = string.Concat(System.Environment.NewLine, "The PPD index", "\t");

    //    int i = 0;
    //    foreach (TaskModel task in Tasks)
    //    {
    //        strLineD[0] += string.Concat("\t\t", "Case ", ((char)('A' + i)).ToString());
    //        strLineD[1] += string.Concat("\t\t", task.Data.TempAir.ToString("0.####"));
    //        strLineD[2] += string.Concat("\t\t", task.Data.TempRad.ToString("0.####"));
    //        strLineD[3] += string.Concat("\t\t", task.Data.Velocity.ToString("0.####"));
    //        strLineD[4] += string.Concat("\t\t", task.Data.RelHumidity.ToString("0.####"));
    //        strLineD[5] += string.Concat("\t\t", task.Data.Clothing.ToString("0.####"));
    //        strLineD[6] += string.Concat("\t\t", task.Data.MetRate.ToString("0.####"));
    //        strLineD[7] += string.Concat("\t\t", task.Data.ExternalWork.ToString("0.####"));

    //        strLineF[0] += string.Concat("\t\t", "Case ", ((char)('A' + i)).ToString());
    //        strLineF[1] += string.Concat("\t", task.Variables.HL_Skin.ToString("0.####"));
    //        strLineF[2] += string.Concat("\t", task.Variables.HL_Sweating.ToString("0.####"));
    //        strLineF[3] += string.Concat("\t", task.Variables.HL_Latent.ToString("0.####"));
    //        strLineF[4] += string.Concat("\t", task.Variables.HL_Dry.ToString("0.####"));
    //        strLineF[5] += string.Concat("\t", task.Variables.HL_Radiation.ToString("0.####"));
    //        strLineF[6] += string.Concat("\t", task.Variables.HL_Convection.ToString("0.####"));

    //        strLineF[7] += string.Concat("\t\t", task.Variables.PMV.ToString("0.####"));
    //        strLineF[8] += string.Concat("\t\t", task.Variables.PPD.ToString("0.####"));

    //        i++;
    //    }

    //    return string.Concat("These are the results for the PMV and the PPD indexes according to ISO 7730:",
    //            System.Environment.NewLine,
    //            string.Concat(strLineD),
    //            System.Environment.NewLine,
    //            string.Concat(strLineF),
    //            System.Environment.NewLine);
    //}

    public string ToString(string[] strRows, System.Globalization.CultureInfo? culture = null)
    {
        StringBuilder strResult = new(2200);
        string[] strLineD = new string[8];
        string[] strLineF = new string[9];

        if (culture is null)
            culture = System.Globalization.CultureInfo.CurrentCulture;

        int i = 0;
        foreach (TaskModel task in Tasks)
        {
            strLineD[0] += $"\t{strRows[0]} {((char)('A' + i)).ToString(culture)}";
            strLineD[1] += $"\t{task.Data.TempAir.ToString("0.####", culture)}";
            strLineD[2] += $"\t{task.Data.TempRad.ToString("0.####", culture)}";
            strLineD[3] += $"\t{task.Data.Velocity.ToString("0.####", culture)}";
            strLineD[4] += $"\t{task.Data.RelHumidity.ToString("0.####", culture)}";
            strLineD[5] += $"\t{task.Data.Clothing.ToString("0.####", culture)}";
            strLineD[6] += $"\t{task.Data.MetRate.ToString("0.####", culture)}";
            strLineD[7] += $"\t{task.Data.ExternalWork.ToString("0.####", culture)}";

            strLineF[0] += $"\t{strRows[0]} {((char)('A' + i)).ToString(culture)}";
            strLineF[1] += $"\t{task.Variables.HL_Skin.ToString("0.####", culture)}";
            strLineF[2] += $"\t{task.Variables.HL_Sweating.ToString("0.####", culture)}";
            strLineF[3] += $"\t{task.Variables.HL_Latent.ToString("0.####", culture)}";
            strLineF[4] += $"\t{task.Variables.HL_Dry.ToString("0.####", culture)}";
            strLineF[5] += $"\t{task.Variables.HL_Radiation.ToString("0.####", culture)}";
            strLineF[6] += $"\t{task.Variables.HL_Convection.ToString("0.####", culture)}";

            strLineF[7] += $"\t{task.Variables.PMV.ToString("0.####", culture)}";
            strLineF[8] += $"\t{task.Variables.PPD.ToString("0.####", culture)}";

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
        strResult.Append(System.Environment.NewLine);

        // Scale factors
        strResult.Append(strRows[10] + strLineF[0] + System.Environment.NewLine);
        strResult.Append(strRows[11] + strLineF[1] + System.Environment.NewLine);
        strResult.Append(strRows[12] + strLineF[2] + System.Environment.NewLine);
        strResult.Append(strRows[13] + strLineF[3] + System.Environment.NewLine);
        strResult.Append(strRows[14] + strLineF[4] + System.Environment.NewLine);
        strResult.Append(strRows[15] + strLineF[5] + System.Environment.NewLine);
        strResult.Append(strRows[16] + strLineF[6] + System.Environment.NewLine);
        strResult.Append(System.Environment.NewLine);

        // Results: PMV and PPD indexes
        strResult.Append(strRows[17] + strLineF[7] + System.Environment.NewLine);
        strResult.Append(strRows[18] + strLineF[8] + System.Environment.NewLine);

        return strResult.ToString();
    }

    public override string ToString()
    {
        string[] strRows = new[]
        {
            "Case",
            "These are the results for the PMV and the PPD indexes according to ISO 7730:",
            "Initial data",
            "Air temperature (°C)",
            "Radiant temperature (°C)",
            "Air velocity (m/s)",
            "Relative humidity (%)",
            "Clothing (clo)",
            "Metabolic rate (met)",
            "External work (met)",
            "Heat loss factors",
            "Heat loss diff. through skin",
            "Heat loss by sweating",
            "Latent respiration heat loss",
            "Dry respiration heat loss",
            "Heat loss by radiation",
            "Heat loss by convection",
            "The PMV index is:",
            "The PPD index is:"
        };
        return ToString(strRows);
    }
}

public static class ThermalComfort
{
    /// <summary>
    /// https://github.com/CenterForTheBuiltEnvironment/comfort_tool/blob/master/static/js/comfortmodels.js
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static bool ComfortPMV(Job job)
    {
        bool result = true;
        foreach (TaskModel task in job.Tasks)
        {
            if (SingleComfortPMV(task) == false)
            {
                result = false;
                break;
            }
        }
        
        return result;
    }

    private static bool SingleComfortPMV(TaskModel task)
    {
        // Variable definition
        double pa = task.Data.RelHumidity * 10 * Math.Exp(16.6536 - 4030.183 / (task.Data.TempAir + 235));

        double dInsulation = 0.155 * task.Data.Clothing;   // Thermal insulation of the clothing in M2K/W
        double dMetRate = task.Data.MetRate * 58.15;       // Metabolic rate in W/M2
        double dExtWork = task.Data.ExternalWork * 58.15;  // External work in W/M2
        double dNeatHeat = dMetRate - dExtWork;             // Internal heat production in the human body
        double fcl;
        if (dInsulation <= 0.078)
            fcl = 1 + 1.29 * dInsulation;
        else
            fcl = 1.05 + 0.645 * dInsulation;


        // Heat transf. coeff. by forced convection
        double hcf = 12.1 * Math.Sqrt(task.Data.Velocity);
        double taa = task.Data.TempAir + 273;
        double tra = task.Data.TempRad + 273;
        // we have verified that using the equation below or this tcla = taa + (35.5 - ta) / (3.5 * (6.45 * icl + .1)) does not affect the PMV value
        double tcla = taa + (35.5 - task.Data.TempAir) / (3.5 * dInsulation + 0.1);

        // Intermediate variables
        double p1 = dInsulation * fcl;
        double p2 = p1 * 3.96;
        double p3 = p1 * 100;
        double p4 = p1 * taa;
        double p5 = 308.7 - 0.028 * dNeatHeat + p2 * Math.Pow(tra / 100, 4);
        double xn = tcla / 100;
        double xf = tcla / 50;
        double eps = 0.00015;

        // Iteration
        double hcn = 0;
        double hc = 0;
        int n = 0;
        while (Math.Abs(xn - xf) > eps)
        {
            xf = (xf + xn) / 2;
            hcn = 2.38 * Math.Pow(Math.Abs(100.0 * xf - taa), 0.25);
            if (hcf > hcn) hc = hcf;
            else hc = hcn;
            xn = (p5 + p4 * hc - p2 * Math.Pow(xf, 4)) / (100 + p3 * hc);
            ++n;
            if (n > 150)
            {
                //alert("Max iterations exceeded");
                return false;
            }
        }

        double tcl = 100 * xn - 273;


        // heat loss diff. through skin
        task.Variables.HL_Skin = 3.05 * 0.001 * (5733 - 6.99 * dNeatHeat - pa);

        // heat loss by sweating
        if (dNeatHeat > 58.15)
            task.Variables.HL_Sweating = 0.42 * (dNeatHeat - 58.15);
        else
            task.Variables.HL_Sweating = 0;

        // latent respiration heat loss
        task.Variables.HL_Latent = 1.7 * 0.00001 * dMetRate * (5867 - pa);

        // dry respiration heat loss
        task.Variables.HL_Dry = 0.0014 * dMetRate * (34 - task.Data.TempAir);

        // heat loss by radiation
        task.Variables.HL_Radiation = 3.96 * fcl * (Math.Pow(xn, 4) - Math.Pow(tra / 100, 4));

        // heat loss by convection
        task.Variables.HL_Convection = fcl * hc * (tcl - task.Data.TempAir);

        // PMV and PPD indexes
        task.Variables.PMV = 0.303 * Math.Exp(-0.036 * dMetRate) + 0.028;
        task.Variables.PMV *= (dNeatHeat - task.Variables.HL_Skin - task.Variables.HL_Sweating - task.Variables.HL_Latent - task.Variables.HL_Dry - task.Variables.HL_Radiation - task.Variables.HL_Convection);
        task.Variables.PPD = 100.0 - 95.0 * Math.Exp(-0.03353 * Math.Pow(task.Variables.PMV, 4.0) - 0.2179 * Math.Pow(task.Variables.PMV, 2.0));

        return true;
    }
}