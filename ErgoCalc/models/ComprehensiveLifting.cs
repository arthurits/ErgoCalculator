namespace ErgoCalc.Models.CLM;

public enum Gender
{
    Male = 0,
    Female = 1
}

public class Data
{
    public Gender Gender;     // 1: masculino    2: femenino
    public double Weight;
    public double h;
    public double v;
    public double d;
    public double f;
    public double td;
    public double t;
    public int c;
    public double hs;
    public double ag;
    public double bw;
};

public class Multipliers
{
    public double fH;
    public double fV;
    public double fD;
    public double fF;
    public double fTD;
    public double fT;
    public double fC;
    public double fHS;
    public double fAG;
    public double fBW;
};


public class Model
{
    public Data Data;
    public Multipliers Factors;
    public double IndexLSI;
};

/// <summary>
/// Contains functions to compute the comprehensive lifting model (CLM).
/// </summary>
public static class ComprehensiveLifting
{
    /// <summary>
    /// Computes the factor values and the LSI index from the CLM model
    /// </summary>
    /// <param name="model"></param>
    public static void CalculateLSI(Model[] model)
    {
        /* Realizar los cálculos para cada tarea */
        for (int i = 0; i < model.Length; i++)
        {
            model[i].Factors.fH = FactorH(model[i].Data.h, model[i].Data.Gender);
            model[i].Factors.fV = FactorV(model[i].Data.v, model[i].Data.Gender);
            model[i].Factors.fD = FactorD(model[i].Data.d, model[i].Data.Gender);
            model[i].Factors.fF = FactorF(model[i].Data.f, model[i].Data.Gender);
            model[i].Factors.fTD = FactorTD(model[i].Data.td);
            model[i].Factors.fT = FactorT(model[i].Data.t);
            model[i].Factors.fC = FactorC(model[i].Data.c);
            model[i].Factors.fHS = FactorHS(model[i].Data.hs);
            model[i].Factors.fAG = FactorAG(model[i].Data.ag, model[i].Data.Gender);
            model[i].Factors.fBW = FactorBW(model[i].Data.bw, model[i].Data.Gender);

            model[i].IndexLSI = LSIindex(model[i].Data.Gender, model[i].Data.Weight, model[i].Factors);
        }

        return;
    }

    private static double Factor(double[][] data, double value, Gender gender = Gender.Male)
    {
        // Definición de variables
        int column = gender == Gender.Male ? 1 : 2;
        double result;

        int nIndice = Locate(data[0], value);

        // Si el valor pedido está fuera del rango de valores de la matriz
        // entonces se devuelve el valor del extremo
        if (nIndice == -1 || nIndice == (data[0].Length - 1))
        {
            if (nIndice == -1) nIndice++;
            return data[column][nIndice];
        }

        // Hacer una interpolación lineal
        result = InterpolacionLineal(value,
            data[0][nIndice],
            data[0][nIndice + 1],
            data[column][nIndice],
            data[column][nIndice + 1]);

        return result;
    }

    /// <summary>
    /// Computes the H factor
    /// </summary>
    /// <param name="value"></param>
    /// <param name="gender"></param>
    /// <returns>H factor</returns>
    private static double FactorH(double value, Gender gender)
    {
        // Definición de variables
        int column = gender == Gender.Male ? 1 : 2;
        double result;
        double[][] fFactor =
        {
            new double[] {25,30,35,40,45,50,55,60,65 },
            new double [] {1.00,0.87,0.79,0.73,0.70,0.68,0.63,0.52,0.43 },
            new double [] {1.00,0.84,0.73,0.66,0.64,0.65,0.61,0.50,0.41 }
        };
        var test = Factor(fFactor, value, gender);
        int nIndice = Locate(fFactor[0], value);

        // Si el valor pedido está fuera del rango de valores de la matriz
        // entonces se devuelve el valor del extremo
        if (nIndice == -1 || nIndice == (fFactor[0].Length - 1))
        {
            if (nIndice == -1) nIndice++;
            return fFactor[column][nIndice];
        }

        // Hacer una interpolación lineal
        result = InterpolacionLineal(value,
            fFactor[0][nIndice],
            fFactor[0][nIndice + 1],
            fFactor[column][nIndice],
            fFactor[column][nIndice + 1]);

        return result;

    }

    /// <summary>
    /// Computes the V factor
    /// </summary>
    /// <param name="value"></param>
    /// <param name="gender"></param>
    /// <returns>V factor</returns>
    private static double FactorV(double value, Gender gender)
    {
        // Definición de variables
        int column = gender == Gender.Male ? 1 : 2;
        double result;
        double[][] fFactor =
        {
            new double [36] {0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145, 150, 155, 160, 165, 170, 175 },
            new double [36] {0.62, 0.64, 0.67, 0.69, 0.72, 0.75, 0.77, 0.80, 0.82, 0.85, 0.87, 0.90, 0.92, 0.95, 0.98, 1.00, 0.99, 0.98, 0.97, 0.96, 0.94, 0.93, 0.92, 0.91, 0.89, 0.88, 0.87, 0.84, 0.83, 0.82, 0.80, 0.79, 0.77, 0.76, 0.73, 0.71 },
            new double [36] {0.74, 0.76, 0.77, 0.79, 0.81, 0.83, 0.84, 0.86, 0.88, 0.90, 0.91, 0.93, 0.95, 0.97, 0.98, 1.00, 0.99, 0.98, 0.96, 0.95, 0.93, 0.90, 0.88, 0.86, 0.83, 0.81, 0.78, 0.76, 0.72, 0.70, 0.67, 0.65, 0.62, 0.60, 0.56, 0.54 }
        };

        int nIndice = Locate(fFactor[0], value);

        if (nIndice == -1 || nIndice == (fFactor[0].Length - 1))
        {
            if (nIndice == -1) nIndice++;
            return fFactor[column][nIndice];
        }

        result = InterpolacionLineal(value,
            fFactor[0][nIndice],
            fFactor[0][nIndice + 1],
            fFactor[column][nIndice],
            fFactor[column][nIndice + 1]);

        return result;
    }

    /// <summary>
    /// Internal function to compute the D factor
    /// </summary>
    /// <param name="value"></param>
    /// <param name="gender"></param>
    /// <returns>D factor</returns>
    private static double FactorD(double value, Gender gender)
    {
        // Definición de variables
        int column = gender == Gender.Male ? 1 : 2;
        double result;
        double[][] fFactor =
        {
            new double[30] {25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145, 150, 155, 160, 165, 170 },
            new double[30] {1, 0.97, 0.94, 0.91, 0.88, 0.86, 0.84, 0.83, 0.81, 0.80, 0.79, 0.78, 0.77, 0.76, 0.75, 0.73, 0.72, 0.71, 0.69, 0.68, 0.67, 0.66, 0.64, 0.63, 0.61, 0.59, 0.57, 0.55, 0.52, 0.49 },
            new double[30] {1, 0.99, 0.97, 0.96, 0.95, 0.94, 0.92, 0.89, 0.87, 0.84, 0.82, 0.80, 0.79, 0.78, 0.77, 0.77, 0.76, 0.75, 0.75, 0.75, 0.74, 0.74, 0.74, 0.74, 0.74, 0.74, 0.73, 0.73, 0.73, 0.73 }
        };

        int nIndice = Locate(fFactor[0], value);

        if (nIndice == -1 || nIndice == (fFactor[0].Length - 1))
        {
            if (nIndice == -1) nIndice++;
            return fFactor[column][nIndice];
        }

        result = InterpolacionLineal(value,
            fFactor[0][nIndice],
            fFactor[0][nIndice + 1],
            fFactor[column][nIndice],
            fFactor[column][nIndice + 1]);

        return result;

    }

    /// <summary>
    /// Internal function to compute the F (frequency) factor
    /// </summary>
    /// <param name="value"></param>
    /// <param name="gender"></param>
    /// <returns>F factor</returns>
    private static double FactorF(double value, Gender gender)
    {
        // Definición de variables
        int column = gender == Gender.Male ? 1 : 2;
        double result;
        double[][] fFactor =
        {
            new double [18] {0.2, 0.5, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 },
            new double [18] {1, 0.99, 0.95, 0.89, 0.83, 0.78, 0.73, 0.69, 0.65, 0.62, 0.59, 0.56, 0.54, 0.52, 0.50, 0.49, 0.47, 0.46 },
            new double [18] {1, 0.99, 0.91, 0.87, 0.84, 0.8 , 0.77, 0.74, 0.7 , 0.68, 0.66, 0.65, 0.64, 0.63, 0.63, 0.62, 0.61, 0.6 }
        };

        int nIndice = Locate(fFactor[0], value);

        if (nIndice == -1 || nIndice == (fFactor[0].Length - 1))
        {
            if (nIndice == -1) nIndice++;
            return fFactor[column][nIndice];
        }

        result = InterpolacionLineal(value,
            fFactor[0][nIndice],
            fFactor[0][nIndice + 1],
            fFactor[column][nIndice],
            fFactor[column][nIndice + 1]);

        return result;
    }

    /// <summary>
    /// Internal function to compute the TD (task duration) factor
    /// </summary>
    /// <param name="value"></param>
    /// <returns>TD factor</returns>
    private static double FactorTD(double value)
    {
        // Definición de variables
        int column = 1;
        double result;
        double[][] fFactor =
        {
            new double [] {0, 1, 2, 3, 4, 5, 6, 7, 8 },
            new double [] {1.00, 1.00, 0.76, 0.66, 0.60, 0.57, 0.54, 0.50, 0.45 }
        };

        int nIndice = Locate(fFactor[0], value);

        if (nIndice == -1 || nIndice == (fFactor[0].Length - 1))
        {
            if (nIndice == -1) nIndice++;
            return fFactor[column][nIndice];
        }

        result = InterpolacionLineal(value,
            fFactor[0][nIndice],
            fFactor[0][nIndice + 1],
            fFactor[column][nIndice],
            fFactor[column][nIndice + 1]);

        return result;
    }

    /// <summary>
    /// Internal function to compute the T factor
    /// </summary>
    /// <param name="value"></param>
    /// <returns>T factor</returns>
    private static double FactorT(double value)
    {
        // Definición de variables
        int column = 1;
        double result;
        double[][] fFactor =
        {
            new double[] {0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130 },
            new double[] {1.00, 0.98, 0.95, 0.93, 0.90, 0.88, 0.86, 0.83, 0.81, 0.78, 0.76, 0.74, 0.71, 0.69 }
        };

        int nIndice = Locate(fFactor[0], value);

        if (nIndice == -1 || nIndice == (fFactor[0].Length - 1))
        {
            if (nIndice == -1) nIndice++;
            return fFactor[column][nIndice];
        }

        result = InterpolacionLineal(value,
            fFactor[0][nIndice],
            fFactor[0][nIndice + 1],
            fFactor[column][nIndice],
            fFactor[column][nIndice + 1]);

        return result;
    }

    /// <summary>
    /// Internal function to compute the C (coupling) factor
    /// </summary>
    /// <param name="value"></param>
    /// <returns>C factor</returns>
    private static double FactorC(int value)
    {
        // Definición de variables
        double resultado =  value switch
        {
            1 => 1.0,
            2 => 0.925,
            3 => 0.850,
            _ => 0.0,
        };
        return resultado;
    }

    /// <summary>
    /// Internal function to compute the HS factor
    /// </summary>
    /// <param name="value"></param>
    /// <returns>HS factor</returns>
    private static double FactorHS(double value)
    {
        // Definición de variables
        int column = 1;
        double result;
        double[][] fFactor =
        {
            new double[] { 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 },
            new double[] {1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 0.98, 0.95, 0.93, 0.90, 0.88, 0.86, 0.83, 0.81, 0.78, 0.76, 0.74, 0.71, 0.69 }
        };

        int nIndice = Locate(fFactor[0], value);

        if (nIndice == -1 || nIndice == (fFactor[0].Length - 1))
        {
            if (nIndice == -1) nIndice++;
            return fFactor[column][nIndice];
        }

        result = InterpolacionLineal(value,
            fFactor[0][nIndice],
            fFactor[0][nIndice + 1],
            fFactor[column][nIndice],
            fFactor[column][nIndice + 1]);

        return result;
    }

    /// <summary>
    /// Internal function to compute the AG factor
    /// </summary>
    /// <param name="value"></param>
    /// <param name="gender"></param>
    /// <returns>AG factor</returns>
    private static double FactorAG(double value, Gender gender)
    {
        // Definición de variables
        int column = gender == Gender.Male ? 1 : 2;
        double result;
        double[][] fFactor =
        {
            new double [] {20, 25, 30, 35, 40, 45, 50, 55, 60 },
            new double [] {1, 0.91, 0.88, 0.88, 0.86, 0.78, 0.69, 0.62, 0.59 },
            new double [] {1, 0.95, 0.90, 0.87, 0.82, 0.79, 0.72, 0.64, 0.49 }
        };

        int nIndice = Locate(fFactor[0], value);

        if (nIndice == -1 || nIndice == (fFactor[0].Length - 1))
        {
            if (nIndice == -1) nIndice++;
            return fFactor[column][nIndice];
        }

        result = InterpolacionLineal(value,
            fFactor[0][nIndice],
            fFactor[0][nIndice + 1],
            fFactor[column][nIndice],
            fFactor[column][nIndice + 1]);

        return result;
    }

    /// <summary>
    /// Internal function to compute the BW factor
    /// </summary>
    /// <param name="value"></param>
    /// <param name="gender"></param>
    /// <returns>BW factor</returns>
    private static double FactorBW(double value, Gender gender)
    {
        // Definición de variables
        int column = gender == Gender.Male ? 1 : 2;
        double result;
        double[][] fFactor =
        {
            new double [] {40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100 },
            new double [] {0.70, 0.70, 0.70, 0.70, 0.70, 0.80, 1.00, 1.20, 1.30, 1.41, 1.45, 1.45, 1.45 },
            new double [] {1.00, 1.00, 1.00, 1.00, 1.00, 1.20, 1.40, 1.68, 1.85, 1.98, 2.05, 2.05, 2.05 }
        };

        int nIndice = Locate(fFactor[0], value);

        if (nIndice == -1 || nIndice == (fFactor[0].Length - 1))
        {
            if (nIndice == -1) nIndice++;
            return fFactor[column][nIndice];
        }

        result = InterpolacionLineal(value,
            fFactor[0][nIndice],
            fFactor[0][nIndice + 1],
            fFactor[column][nIndice],
            fFactor[column][nIndice + 1]);

        return result;
    }

    /// <summary>
    /// Computes the percentage of population protected by the CLM model
    /// </summary>
    /// <param name="value"></param>
    /// <param name="gender"></param>
    /// <returns>Percentage of population</returns>
    private static double Porcentaje(double value, Gender gender)
    {
        // Definición de variables
        int column = gender == Gender.Male ? 1 : 2;
        double result;
        double[][] fFactor =
        {
            new double[] {5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95 },
            new double[] { 39.05, 37.54, 36.04, 34.53, 33.02, 31.51, 30.00, 28.50, 26.99, 25.47, 23.96, 22.45, 20.94, 19.44, 17.93, 16.42, 14.91, 13.41, 11.89 },
            new double[] { 22.75, 22.15, 21.54, 20.94, 20.34, 19.73, 19.13, 18.53, 17.92, 17.32, 16.71, 16.11, 15.51, 14.90, 14.30, 13.70, 13.09, 12.49, 11.89 }
        };

        int nIndice = Locate(fFactor[column], value);

        if (nIndice == -1 || nIndice == (fFactor[0].Length - 1))
        {
            if (nIndice == -1) nIndice++;
            return fFactor[0][nIndice];
        }

        result = InterpolacionLineal(value,
            fFactor[column][nIndice],
            fFactor[column][nIndice + 1],
            fFactor[0][nIndice],
            fFactor[0][nIndice + 1]);

        return result;
    }

    /// <summary>
    /// Computes the LSI index in the CLM model
    /// </summary>
    /// <param name="sexo"></param>
    /// <param name="peso"></param>
    /// <param name="factores"></param>
    /// <returns>LSI index</returns>
    private static double LSIindex(Gender gender, double peso, Multipliers factores)
    {
        double pesoBase;
        double multiplicacion;
        double porcentaje;
        double indice;

        multiplicacion = factores.fH *
            factores.fV *
            factores.fD *
            factores.fF *
            factores.fTD *
            factores.fT *
            factores.fC *
            factores.fHS *
            factores.fAG *
            factores.fBW;

        if (multiplicacion == 0)    // División entre 0
            indice = 0;
        else
        {
            pesoBase = peso / multiplicacion;
            porcentaje = Porcentaje(pesoBase, gender);
            indice = 10 - porcentaje / 10;
        }

        return indice;
    }


    /// <summary>
    /// Lineal interpolation 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="y1"></param>
    /// <param name="y2"></param>
    /// <returns>The interpolated value</returns>
    private static double InterpolacionLineal(double value, double x1, double x2, double y1, double y2)
    {
        double result;

        if (value == x1)        // Si el valor pedido coincide con el límite inferior
            result = y1;
        else if (value == x2)   // Si el valor pedido coincide con el límite superior
            result = y2;
        else if (x2 == x1)
            result = (y1 + y2) / 2;
        else                // En cualquier otro caso, hacer la interpolación lineal
            result = (value - x1) * (y2 - y1) / (x2 - x1) + y1;

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
