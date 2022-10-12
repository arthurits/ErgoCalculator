using System;

namespace ErgoCalc.Models.WR;

public class DataWR
{
    /// <summary>
    /// Maximum voluntary contraction (%).
    /// </summary>
    public double MVC { get; set; } = 0;
    /// <summary>
    /// Maximum holding time (in minutes) computed using Sjogaard's equation.
    /// </summary>
    public double MHT { get; set; } = 0;
    /// <summary>
    /// 1D array to be filled with the WR-curve abscissa (time) values.
    /// </summary>
    public double[] PointsX { get; set; } = Array.Empty<double>();
    /// <summary>
    /// 1D array to be filled with the WR-curve ordinate values.
    /// </summary>
    public double[] PointsY { get; set; } = Array.Empty<double>();
    /// <summary>
    /// 1D array with the working times in seconds.
    /// </summary>
    public double[] WorkingTimes { get; set; } = Array.Empty<double>();
    /// <summary>
    /// 1D array with the resting times in seconds.
    /// </summary>
    public double[] RestingTimes { get; set; } = Array.Empty<double>();
    /// <summary>
    /// Time increment between PointsX[] elements for the rest-phase computation.
    /// </summary>
    public double PlotStep { get; set; } = 0;
    /// <summary>
    /// Zero-based index of the selected curve.
    /// </summary>
    public int PlotCurve { get; set; } = 0;
    /// <summary>
    /// Size of the PointsX[] and PointsY[] arrays.
    /// </summary>
    public int Points { get; set; } = 0;
    /// <summary>
    /// Number of times the work-rest sequence is repeated.
    /// </summary>
    public int Cycles { get; set; } = 0;
    /// <summary>
    /// Plot legend.
    /// </summary>
    public string Legend { get; set; } = String.Empty;

    public override string ToString()
    {
        string strCadena;

        strCadena = "[Maximum voluntary contraction (%)]: " + MVC.ToString() + Environment.NewLine;
        strCadena += "[Maximum holding time (min)]: " + MHT.ToString() + Environment.NewLine;
        strCadena += "[Working times (min)]: " + String.Join(" ", WorkingTimes) + Environment.NewLine;
        strCadena += "[Resting times (min)]: " + String.Join(" ", RestingTimes) + Environment.NewLine;
        strCadena += "[Number of cycles]: " + Cycles.ToString() + Environment.NewLine;
        strCadena += "[Plot step]: " + PlotStep.ToString() + Environment.NewLine;
        strCadena += "[Curve number]: " + PlotCurve.ToString();
        return strCadena;
    }

}

public class Job
{
    public DataWR[] Tasks { get; set; } = Array.Empty<DataWR>();
    public int NumberTasks { get; set; } = 0;
    public override string ToString()
    {
        string[] results = new string[7]
        {
            "[Maximum voluntary contraction (%)]: ",
            "[Maximum holding time (min)]: ",
            "[Working times (min)]: ",
            "[Resting times (min)]: ",
            "[Number of cycles]: ",
            "[Plot step]: ",
            "[Curve number]: "
        };

        foreach (DataWR task in Tasks)
        {
            results[0] += "\t" + task.MVC.ToString();
            results[1] += "\t" + task.MHT.ToString();
            results[2] += "\t" + String.Join(" ", task.WorkingTimes);
            results[3] += "\t" + String.Join(" ", task.RestingTimes);
            results[4] += "\t" + task.Cycles.ToString();
            results[5] += "\t" + task.PlotStep.ToString();
            results[6] += "\t" + task.PlotCurve.ToString();
        }

        //for (int i=0; i<results.Length; i++)
        //    results[i] = results[i].TrimEnd();

        return String.Join(Environment.NewLine, results);
    }
}

public static class WorkRest
{

    /// <summary>
    /// Computes the Holding Time (in minutes) from the MVC (in %).
    /// It uses the equation suggested by Sjogaard.
    /// </summary>
    /// <param name="MVC">Maximum voluntary contraction (percentage)</param>
    /// <returns>Returns the HT (in minutes) associated with the given MVC</returns>
    public static double ComputeMHT(double MVC)
    {
        // Sjogaard equation
        double result = 5710 / Math.Pow(MVC, 2.14);

        return result;
    }

    /// <summary>
    /// Cumputes the WR curve
    /// </summary>
    /// <param name="data">Class containing the needed data and storing the results</param>
    public static void WRCurve(DataWR data)
    {
        //WRCurve(data.WorkingTimes, data.RestingTimes, data.WorkingTimes.Length, data.PointsX, data.PointsY, data.Points, data.Cycles, data.MHT, data.PlotStep);

        // Definición de variables
        double fOffsetX = 0.0;
        double fOffsetY = 100.0;

        //Contadores para los bucles
        int nContador = 0;

        // Inicialización de la matriz de resultados
        data.PointsX[0] = fOffsetX;      // Abscisas
        data.PointsY[0] = fOffsetY;      // Ordenadas

        for (int i = 0; i < data.Cycles; i++)
        {
            for (int j = 0; j < data.WorkingTimes.Length; j++)
            {
                nContador++;
                // Ciclo de trabajo
                data.PointsX[nContador] = fOffsetX + data.WorkingTimes[j];
                data.PointsY[nContador] = fOffsetY - 100.0 * (data.WorkingTimes[j] / data.MHT);

                // Ciclo de descanso
                for (int k = 1; k <= (int)Math.Floor(data.RestingTimes[j] / data.PlotStep); k++)
                {
                    nContador++;
                    data.PointsX[nContador] = fOffsetX + data.WorkingTimes[j] + k * data.PlotStep;
                    data.PointsY[nContador] = fOffsetY * Math.Exp(-0.5 * (k * data.PlotStep) / data.MHT);
                    data.PointsY[nContador] += 100.0 * (1.0 - Math.Exp(-0.5 * (k * data.PlotStep) / data.MHT));
                    data.PointsY[nContador] += -100.0 * (data.WorkingTimes[j] / data.MHT) * (1 - Math.Exp(-0.164 * (data.WorkingTimes[j]) / (k * data.PlotStep)));
                }
                fOffsetX = data.PointsX[nContador];
                fOffsetY = data.PointsY[nContador];
            }
        }
        return;

    }

    /// <summary>
    /// Cumputes the WR curve
    /// </summary>
    /// <param name="Work">1D array with the working times in seconds</param>
    /// <param name="Rest">1D array with the resting times in seconds</param>
    /// <param name="nWRsize">Size of the Work[] and Rest[] arrays</param>
    /// <param name="PointsX">1D array to be filled with the WR-curve abscissa (time) values. Array should be empty but space already allocated</param>
    /// <param name="PointsY">1D array to be filled with the WR-curve ordinate values. Array should be empty but space already allocated</param>
    /// <param name="nPoints">Size of the PointsX[] and PointsY[] arrays</param>
    /// <param name="nCycles">Number of times the work-rest sequence is repeated</param>
    /// <param name="MHT">Maximum holding time computed using Sjogaard's equation</param>
    /// <param name="Step">Time increment between PointsX[] elements for the rest-phase computation</param>
    public static void WRCurve(double[] Work, double[] Rest, int nWRsize, double[] PointsX, double[] PointsY, int nPoints, int nCycles, double MHT, double Step)
    {
        // Definición de variables
        double fOffsetX = 0.0;
        double fOffsetY = 100.0;

        //Contadores para los bucles
        int nContador = 0;

        // Inicialización de la matriz de resultados
        PointsX[0] = fOffsetX;      // Abscisas
        PointsY[0] = fOffsetY;      // Ordenadas

        for (int i = 0; i < nCycles; i++)
        {
            for (int j = 0; j < nWRsize; j++)
            {
                nContador++;
                // Ciclo de trabajo
                PointsX[nContador] = fOffsetX + Work[j];
                PointsY[nContador] = fOffsetY - 100.0 * (Work[j] / MHT);

                // Ciclo de descanso
                for (int k = 1; k <= (int)Math.Floor(Rest[j] / Step); k++)
                {
                    nContador++;
                    PointsX[nContador] = fOffsetX + Work[j] + k * Step;
                    PointsY[nContador] = fOffsetY * Math.Exp(-0.5 * (k * Step) / MHT);
                    PointsY[nContador] += 100.0 * (1.0 - Math.Exp(-0.5 * (k * Step) / MHT));
                    PointsY[nContador] += -100.0 * (Work[j] / MHT) * (1 - Math.Exp(-0.164 * (Work[j]) / (k * Step)));
                }
                fOffsetX = PointsX[nContador];
                fOffsetY = PointsY[nContador];
            }
        }
        return;
    }
}