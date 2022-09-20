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
        string strTiempoT = "";
        string strTiempoD = "";

        foreach (double d in WorkingTimes)
            strTiempoT += d.ToString() + " ";
        strTiempoT = strTiempoT.TrimEnd();

        foreach (double d in RestingTimes)
            strTiempoD += d.ToString() + " ";
        strTiempoD = strTiempoD.TrimEnd();

        strCadena = "[Maximum voluntary contraction (%)]: " + MVC.ToString() + "\r\n";
        strCadena += "[Maximum holding time (min)]: " + MHT.ToString() + "\r\n";
        strCadena += "[Working times (min)]: " + strTiempoT + "\r\n";
        strCadena += "[Resting times (min)]: " + strTiempoD + "\r\n";
        strCadena += "[Number of cycles]: " + Cycles.ToString() + "\r\n";
        strCadena += "[Plot step]: " + PlotStep.ToString() + "\r\n";
        strCadena += "[Curve number]: " + PlotCurve.ToString();
        return strCadena;
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