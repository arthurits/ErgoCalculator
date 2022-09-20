using System;

namespace ErgoCalc.Models.WR;

public class DataWR
{
    /// <summary>
    /// Maximum voluntary contraction.
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

        strCadena = "[Maximum voluntary contraction]: " + MVC.ToString() + "\r\n";
        strCadena += "[Maximum holding time (min)]: " + MHT.ToString() + "\r\n";
        strCadena += "[Work times (min)]: " + strTiempoT + "\r\n";
        strCadena += "[Rest times (min)]: " + strTiempoD + "\r\n";
        strCadena += "[Number of cycles]: " + Cycles.ToString() + "\r\n";
        strCadena += "[Step]: " + PlotStep.ToString() + "\r\n";
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
    /// <param name="dMVC">Maximum voluntary contraction (MVC)</param>
    /// <returns>Devuelve el valor HT para cada MVC</returns>
    public static double Sjogaard(double dMVC)
    {
        // Sjogaard equation
        double result = 5710 / Math.Pow(dMVC, 2.14);

        return result;
    }


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


    /// <summary>
    /// Calcula los puntos de la curva según el modelo WR.
    /// </summary>
    /// <param name="datos">struct con los datos introducidos por el usuario</param>
    /// <returns>Devuelve una matriz de 2 dimensiones con datos dobles.</returns>
    //public static double[][] Curva(DataWR datos)
    //{
    //    // Definición de variables
    //    // Es más práctico crear una "jagged array" para extraer las columnas y
    //    //  pasarlas al gráfico
    //    double[][] arrayCurva = new double[2][];

    //    // Cálculo del número de puntos de la curva
    //    int nSize = datos.Cycles * datos._dTrabajoDescanso[0].Length + 1;
    //    foreach (double d in datos._dTrabajoDescanso[1])
    //        nSize += ((int)(d / datos.PlotStep)) * datos.Cycles;

    //    // Inicialización de la matriz
    //    arrayCurva[0] = new double[nSize];
    //    arrayCurva[1] = new double[nSize];
    //    arrayCurva[0][0] = 0;
    //    arrayCurva[1][0] = 100;

    //    // Llamar a la función recursiva para calcular los valores
    //    CurvaRecursiva(ref arrayCurva, datos, 0, 0);

    //    // Devolver los resultados
    //    return arrayCurva;
    //}

    /// <summary>
    /// Función privada que de forma recursiva calcula los valores del modelo WR
    /// </summary>
    /// <param name="matriz">Matriz de 2 dimensiones que contiene los datos</param>
    /// <param name="datos">Índice del elemento que está vacío y se va a rellenar</param>
    /// <param name="nIndice">Último índice utilizado en la matriz</param>
    /// <param name="nIteración">Número de iteración</param>
    //private static void CurvaRecursiva(ref double[][] matriz, DataWR datos, int nIndice, int nIteración)
    //{
    //    // Definición de variables
    //    int i;
    //    double dHT = datos._dTrabajoDescanso[0][nIteración];
    //    double dHTp = datos._dTrabajoDescansop[0][nIteración];
    //    double offsetX = matriz[0][nIndice];
    //    double offsetY = matriz[1][nIndice];

    //    // Datos durante el ciclo de trabajo
    //    matriz[0][nIndice + 1] = offsetX + dHT;
    //    matriz[1][nIndice + 1] = offsetY - dHTp;

    //    // Datos durante el ciclo de descanso
    //    for (i = 1; i <= (datos._dTrabajoDescanso[1][nIteración] / datos.PlotStep); i++)
    //    {
    //        matriz[0][nIndice + 1 + i] = (offsetX + dHT) + i * datos.PlotStep;
    //        //matriz[1][nIndice + 1 + i] = (offsetY - dHTp) + dHTp * Math.Exp(-0.164 * dHT / (i * datos._dPaso));
    //        matriz[1][nIndice + 1 + i] = (offsetY) * Math.Exp(-0.5 * (i * datos.PlotStep) / datos.MHT) + 100 * (1 - Math.Exp(-0.5 * (i * datos.PlotStep) / datos.MHT)) - dHTp * (1 - Math.Exp(-0.164 * dHT / (i * datos.PlotStep)));
    //    }

    //    // Recursividad
    //    if ((nIndice + i + 1) < matriz[0].GetLength(0))
    //    {
    //        nIteración = (nIteración + 1 < datos._dTrabajoDescanso[0].Length) ? nIteración : -1;
    //        CurvaRecursiva(ref matriz, datos, nIndice + i, nIteración + 1);
    //    }
    //    else
    //        return;
    //}
}
