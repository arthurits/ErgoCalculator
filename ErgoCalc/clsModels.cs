/*
https://stackoverflow.com/questions/5107694/how-do-i-add-a-reference-to-an-unmanaged-c-project-called-by-a-c-sharp-project 
I would follow Slaks' second answer...
[...] you can add a link to the unmanaged DLL as a file in the C# project, and set Build Action to None and Copy to Output Directory to Copy If Newer.
... followed by my comment, to differentiate between Debug and Release builds (even if is a little bit "hackish", since it requires you to manually edit the C# project file)
open your C# project's csproj file with a text editor and search for all "YourNativeCppProject.dll" occurrences (without the ".dll" subfix, so if you added pdb files as a link too,
you'll find more than one occurrence), and change the Include path using macros, for example: Include="$(SolutionDir)$(ConfigurationName)\YourNativeCppProject.dll
PS: if you look at the properties (F4), VS shows you the Debug's path even if you switch to the Release configuration, but if you compile, you'll see that the dll copied to output is the release version
 
I would suspect you're trying to add reference to a file which is only possible to do with managed assemblies and some COM files.
Here's what you should do:
1. Compile your solution.
2. Right click on the managed project and select "Add/Existing Item". Do not use "Add Reference".
3. Navigate to your compiled native DLL and select it (adjust file types as needed).
4. Click on the down arrow in the "Add" split button and select "Add As Link" (which is what I meant by "adding as reference" - sorry I have not used VS 2008 in a while).
5. Right click on that freshly added file and select "Properties".
6. Make sure "Build Action" is "Content" and "Copy To Output Directory" is set to "Copy always" or "Copy if newer".
7. Right click on the managed project and select "Project Dependencies".
8. Check your native DLL in the list which would appear.
You now should be all set.
  
 */


namespace ErgoCalc
{

    namespace Models
    {
        // Para acceder a todos los servicios Interop
        using System.Runtime.InteropServices;

        namespace CLMmodel
        {
           

            /* Definición de tipos */
            [StructLayout(LayoutKind.Sequential)]
            public struct dataCLM
            {
                public int gender;     // 1: masculino    2: femenino
                public double weight;
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

            [StructLayout(LayoutKind.Sequential)]
            public struct multipliersCLM
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

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct modelCLM
            {
                [MarshalAs(UnmanagedType.Struct)]
                public dataCLM data;
                [MarshalAs(UnmanagedType.Struct)]
                public multipliersCLM factors;
                public double indexLSI;
            };

            // Definición de la clase que encapsula la llamada a la DLL
            public class cModelCLM
            {
                [DllImport("dlls/clm.dll", EntryPoint = "CalculateLSI")]
                private static extern void CalculateLSI_DLL([In, Out] modelCLM[] datos, ref int nSize);

                public void CalculateLSI(modelCLM[] datos, ref int nSize)
                {
                    CalculateLSI_DLL(datos, ref nSize);
                    return;
                }            
            }

        }

        namespace WRmodel
        {
            using System;

            // Definición de tipos
            [StructLayout(LayoutKind.Sequential)]
            public struct datosDLL
            {
                public double dMHT;
                public double dPaso;
                public int nCiclos;
                public int nPuntos;

                public datosDLL (double maxTime, double paso, int ciclos, int puntos)
                {
                    dMHT = maxTime;
                    dPaso = paso;
                    nCiclos = ciclos;
                    nPuntos = puntos;
                }

                public override string ToString()
                {
                    string strCadena = string.Format("[Maximum holding time]:\t{0:0.###}\r\n[Paso]:\t{1:0.###}\r\n[Número de ciclos]:\t{2:0.###}\r\n[Número de puntos]:\t{3:0.###}", dMHT, dPaso, nCiclos,nPuntos);
                    return strCadena;
                }
            }

            public struct datosWR
            {
                public double _dMVC;
                public double _dMHT;
                //public double[][] _dPoints;
                public double[] _dPointsX;
                public double[] _dPointsY;
                //public double[][] _dWorkRest;
                public double[] _dWork;
                public double[] _dRest;
                //public double[] _dWorkRestDrop;
                public double _dPaso;
                public int _nCurva;
                public int _nPuntos;
                public byte _bCiclos;
                public string _strLegend;

                public override string ToString()
                {
                    System.String strCadena;
                    System.String strTiempoT = "";
                    System.String strTiempoD = "";
                    char[] charEspacio = { ' ' };

                    foreach (double d in _dWork)
                        strTiempoT += d.ToString() + " ";
                    strTiempoT = strTiempoT.TrimEnd(charEspacio);

                    foreach (double d in _dRest)
                        strTiempoD += d.ToString() + " ";
                    strTiempoD = strTiempoD.TrimEnd(charEspacio);

                    strCadena = "[Maximum voluntary contraction]: " + _dMVC.ToString() + "\r\n";
                    strCadena += "[Maximum holding time (min)]: " + _dMHT.ToString() + "\r\n";
                    strCadena += "[Work times (min)]: " + strTiempoT + "\r\n";
                    strCadena += "[Rest times (min)]: " + strTiempoD + "\r\n";
                    strCadena += "[Number of cycles]: " + _bCiclos.ToString() + "\r\n";
                    strCadena += "[Step]: " + _dPaso.ToString() + "\r\n";
                    strCadena += "[Curve number]: " + _nCurva.ToString();
                    return strCadena;
                }

            }

            // Definición de la clase que encapsula la llamada a la DLL
            public class cWRmodel
            {

                #region DLL function declaration

                /// <summary>
                /// Calcula el MHT mediante la ecuación de Sjogaard
                /// </summary>
                /// <param name="dMVC">Maximum voluntary contraction</param>
                /// <returns></returns>
                [DllImport("dlls/wrmodel.dll", EntryPoint = "Sjogaard")]
                private static extern double Sjogaard_DLL(double dMVC);

                /// <summary>
                /// Calcula la curva de trabajo-descanso
                /// </summary>
                /// <param name="array"></param>
                /// <param name="tamaño"></param>
                /// <param name="datos"></param>
                /// <returns></returns>
                [DllImport("dlls/wrmodel.dll", EntryPoint = "Curva")]
                private static extern System.IntPtr Curva_DLL(System.IntPtr array, int tamaño, ref datosDLL datos);

                [DllImport("dlls/wrmodel.dll", EntryPoint = "WRCurve")]
                private static extern void WRCurve(double[] Work, double[] Rest, int nWR, [In, Out] double[] PointsX, [In, Out] double[] PointsY, int nPoints, int nCycles, double dMHT, double dStep);

                /// <summary>
                /// Libera la memoria reservada por la DLL
                /// </summary>
                /// <param name="ptr">Apuntador a la matriz que devuelve la función CurvaDLL</param>
                /// <returns></returns>
                [DllImport("dlls/wrmodel.dll", EntryPoint = "FreeMemory")]
                private static extern System.IntPtr FreeMemory_DLL(System.IntPtr ptr);

                #endregion
                
                /// <summary>
                /// Constructor de la clase.
                /// </summary>
                public cWRmodel()
                {
                }


                #region Class functions

                public bool Curva(datosWR datos)
                {
                    // Definición de variables
                    bool result = true;
                    
                    datos._dMHT = Sjogaard(datos._dMVC);
                    
                    try
                    {    
                        WRCurve(datos._dWork,
                            datos._dRest,
                            datos._dWork.Length,
                            datos._dPointsX,
                            datos._dPointsY,
                            datos._nPuntos,
                            datos._bCiclos,
                            datos._dMHT,
                            datos._dPaso);
                    }
                    catch(Exception)
                    {
                        System.Windows.Forms.MessageBox.Show("Error calling WRCurve function", "Error");
                        result = false;
                    }
 
                    // Finalizar
                    return result;
                }
                
                public double Sjogaard(double dMVC)
                {
                    double result = -1.0;
                    try
                    {
                        result = Sjogaard_DLL(dMVC);
                    }
                    catch(Exception)
                    {
                        System.Windows.Forms.MessageBox.Show("Error calling Sjogaard function", "Error");
                    }
                    return result;
                }

                /// <summary>
                /// Deprecated. Superseeded by Curva
                /// </summary>
                /// <param name="datos"></param>
                /// <returns></returns>
                public double[][] CurvaDeprecated(datosWR datos)
                {
                    // Definición de variables
                    // La matriz arrayCurva se inicializa a "empty" porque la función puede devolver
                    //   una matriz "empty" si no ha podido realizar el cálculo.
                    Double[][] arrayCurva = Array.Empty<double[]>();
                    IntPtr ptrTiempos = IntPtr.Zero;
                    IntPtr ptrResultado = IntPtr.Zero;

                    // Definición de las variables que se pasan a la función
                    int longitud = datos._dWork.Length;
                    datosDLL structDatos;
                    structDatos.dMHT = datos._dMHT;
                    structDatos.dPaso = datos._dPaso;
                    structDatos.nCiclos = Convert.ToInt32(datos._bCiclos);
                    structDatos.nPuntos = datos._nPuntos;

                    try
                    {
                        // Pasar la matriz a la memoria no gestionada
                        ptrTiempos = marshalJuggedToC(new double[][] { datos._dWork, datos._dRest });

                        // Llamar a la función de la DLL que devuelve la curva calculada
                        ptrResultado = Curva_DLL(ptrTiempos, longitud, ref structDatos);

                        // Si la función no ha devuelto nada
                        if (ptrResultado == IntPtr.Zero)
                        {
                            throw new MemoryAllocation("Could not allocate memory.");
                        }

                        // Pasar el resultado de la DLL a la memoria gestionada
                        arrayCurva = marshalJuggedFromC(ptrResultado, 2, datos._nPuntos);
                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show(
                            e.Message,
                            "Memory allocation",
                            System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Error);
                    }
                    finally
                    {
                        // Liberar la memoria reservada por la rutina marshalJuggedToC
                        //marshalFreeMemory(ptrTiempos, datos._dWorkRest.Length);
                        marshalFreeMemory(ptrTiempos, 2);

                        // Liberar la memoria reservada por la DLL (en caso de que la haya podido reservar)
                        if (ptrResultado != IntPtr.Zero)
                        {
                            FreeMemory_DLL(Marshal.ReadIntPtr(ptrResultado, 0));
                            FreeMemory_DLL(ptrResultado);
                        }
                    }

                    // Finalizar
                    return arrayCurva;
                }

                

                #endregion

                #region Marshalling private routines

                /// <summary>
                /// Preparar una matriz jagged para pasarla a una DLL
                /// http://social.msdn.microsoft.com/Forums/en-US/clr/thread/d421bf2a-5a74-46b4-bf07-363d1c058017/
                /// </summary>
                /// <param name="array">Matriz jagged</param>
                /// <returns>Puntero de punteros</returns>
                private IntPtr marshalJuggedToC(double[][] array)
                {
                    Int32 sizeofPtr = Marshal.SizeOf(typeof(IntPtr));
                    Int32 sizeofDouble = Marshal.SizeOf(typeof(double));

                    IntPtr p1 = Marshal.AllocCoTaskMem(array.Length * sizeofPtr);
                    IntPtr v1;
                    for (Int32 i = 0; i < array.Length; i++)
                    {
                        v1 = Marshal.AllocCoTaskMem(array[i].Length * sizeofDouble);
                        Marshal.Copy(array[i], 0, v1, array[i].Length);
                        Marshal.WriteIntPtr(p1, i * sizeofPtr, v1);
                    }

                    // Queda pendiente de liberar las memorias reservadas para v1
                    // También debe liberarse la memoria reservada para p1

                    return p1;
                }

                /// <summary>
                /// Toma una matriz jagged y la pasa a código managed
                /// </summary>
                /// <param name="carray">Puntero a punteros</param>
                /// <param name="nI">Tamaño de la primera dimensión</param>
                /// <param name="nJ">Tamaño de la segunda dimensión</param>
                /// <returns>Managed jagged matrix</returns>
                private double[][] marshalJuggedFromC(IntPtr carray, int nI, int nJ)
                {
                    IntPtr v1;
                    int sizeofPtr = Marshal.SizeOf(typeof(IntPtr));
                    double[][] retval = new double[nI][];
                    for (int i = 0; i < retval.Length; i++)
                    {
                        retval[i] = new double[nJ];
                        v1 = Marshal.ReadIntPtr(carray, i * sizeofPtr);
                        Marshal.Copy(v1, retval[i], 0, nJ);
                        // Marshal.FreeCoTaskMem(v1);    // Se libera este puntero creado en la rutina marshalJuggedToC
                    }

                    // Marshal.FreeCoTaskMem(carray);  // Se libera la memoria del puntero a punteros (opcional)
                    return retval;
                }

                /// <summary>
                /// Libera la memoria reservada por la rutina marshalJuggedToC
                /// </summary>
                /// <param name="matriz">es un puntero a punteros</param>
                /// <param name="nElementos">número de elementos</param>
                private void marshalFreeMemory(IntPtr matriz, Int32 nElementos)
                {
                    // Definición de variables
                    IntPtr ptr;
                    Int32 sizeofPtr = Marshal.SizeOf(typeof(IntPtr));

                    // Se libera la memoria de cada una de las filas de la matriz
                    for (Int32 i = 0; i < nElementos; i++)
                    {
                        ptr = Marshal.ReadIntPtr(matriz, i * sizeofPtr);
                        Marshal.FreeCoTaskMem(ptr);
                    }

                    // Se libera la memoria de la matriz
                    Marshal.FreeCoTaskMem(matriz);
                }

                #endregion

                #region Deprecated

                /*        
                /// <summary>
                /// Calcular el Holding Time (en min) a partir del MVC (en %).
                /// Se utiliza la ecuación propuesta por Sjonjard.
                /// </summary>
                /// <param name="d_mvc">Contracción máxima voluntaria (MVC)</param>
                /// <returns>Devuelve el valor HT para cada MVC</returns>
                public double Sjogaard(double dMVC)
                {
                    // Definición de variables
                    double resultado = 0.0;
                    resultado = 5710 / Math.Pow(dMVC, 2.14);
            
                    return resultado;
                }

                /// <summary>
                /// Calcula los puntos de la curva según el modelo WR.
                /// </summary>
                /// <param name="datos">struct con los datos introducidos por el usuario</param>
                /// <returns>Devuelve una matriz de 2 dimensiones con datos dobles.</returns>
                public double [][] Curva(datosWR datos)
                {
                    // Definición de variables
                    // Es más práctico crear una "jagged array" para extraer las columnas y
                    //  pasarlas al gráfico
                    double[][] arrayCurva= new double [2][];
            
                    // Cálculo del número de puntos de la curva
                    int nSize = datos._bCiclos * datos._dTrabajoDescanso[0].Length + 1;
                    foreach (double d in datos._dTrabajoDescanso [1]) 
                        nSize += ((int)(d / datos._dPaso ))*datos._bCiclos;
            
                    // Inicialización de la matriz
                    arrayCurva[0] = new double [nSize];
                    arrayCurva[1] = new double [nSize];
                    arrayCurva[0][0] = 0;
                    arrayCurva[1][0] = 100;
            
                    // Llamar a la función recursiva para calcular los valores
                    CurvaRecursiva(ref arrayCurva, datos, 0, 0);

                    // Devolver los resultados
                    return arrayCurva;
                }

                /// <summary>
                /// Función privada que de forma recursiva calcula los valores del modelo WR
                /// </summary>
                /// <param name="matriz">Matriz de 2 dimensiones que contiene los datos</param>
                /// <param name="datos">Índice del elemento que está vacío y se va a rellenar</param>
                /// <param name="nIndice">Último índice utilizado en la matriz</param>
                /// <param name="nIteración">Número de iteración</param>
                private void CurvaRecursiva(ref double [][] matriz, datosWR datos, int nIndice, int nIteración)
                {
                    // Definición de variables
                    int i;
                    double dHT=datos._dTrabajoDescanso[0][nIteración];
                    double dHTp=datos._dTrabajoDescansop[0][nIteración];
                    double offsetX = matriz[0][nIndice];
                    double offsetY = matriz[1][nIndice];

                    // Datos durante el ciclo de trabajo
                    matriz[0][nIndice + 1] = offsetX + dHT;
                    matriz[1][nIndice + 1] = offsetY - dHTp;

                    // Datos durante el ciclo de descanso
                    for(i=1; i<=(datos._dTrabajoDescanso[1][nIteración]/datos._dPaso); i++)
                    {
                        matriz[0][nIndice + 1 + i] = (offsetX + dHT) + i * datos._dPaso;
                        //matriz[1][nIndice + 1 + i] = (offsetY - dHTp) + dHTp * Math.Exp(-0.164 * dHT / (i * datos._dPaso));
                        matriz[1][nIndice + 1 + i] = (offsetY) * Math.Exp(-0.5 * (i * datos._dPaso) / datos._dMHT) + 100 * (1 - Math.Exp(-0.5 * (i * datos._dPaso) / datos._dMHT)) - dHTp * (1 - Math.Exp(-0.164 * dHT / (i * datos._dPaso)));
                    }

                    // Recursividad
                    if ((nIndice + i + 1) < matriz[0].GetLength(0))
                    {
                        nIteración = (nIteración+1 < datos._dTrabajoDescanso[0].Length) ? nIteración : -1;                
                        CurvaRecursiva(ref matriz, datos, nIndice + i, nIteración + 1);
                    }
                    else
                        return;
                }
                */

                #endregion

            }
        }

        namespace MetRate
        {
            public class cMetRate
            {
                unsafe public void CalculateMetRate(int* aData,double* aResults)
                {
                    // Calculate the metabolic rate in according to level 1 A                    
                    CalculateLevel1A(aData, aResults);
                    CalculateLevel1B(aData, aResults);
                    return;
                }

                unsafe private void CalculateLevel1A(int* pData, double* pResults)
                {
                    // Variable definition
                    double[] dInf = new double[] { 115.0,  85.0, 70.0,  75.0, 110.0, 105.0, 110.0,  90.0, 110.0, 100.0, 55.0, 140.0, 105.0, 140.0, 170.0, 125.0,  80.0,  90.0,  70.0,  75.0,  75.0, 115.0, 70.0, 110.0,  85.0, 100.0,  85.0, 70.0,  80.0,  70.0, 55.0,  75.0,  70.0,  80.0, 65.0};
                    double[] dSup = new double[] { 190.0, 110.0, 95.0, 100.0, 160.0, 140.0, 175.0, 125.0, 140.0, 130.0, 70.0, 240.0, 165.0, 240.0, 220.0, 145.0, 140.0, 200.0, 110.0, 125.0, 125.0, 175.0, 85.0, 110.0, 100.0, 120.0, 100.0, 85.0, 115.0, 100.0, 70.0, 125.0, 100.0, 115.0, 145.0};
                    
                    // Calculate the result
                    if (*pData >= 0 && *pData < 35)
                    {
                        *pResults = dInf[*pData];
                        *(pResults + 1) = dSup[*pData];
                    }
                    else
                    {
                        *pResults = -1.0;
                        *(pResults + 1) = -1.0;
                    }

                    // Return from function
                    return;
                }

                unsafe private void CalculateLevel1B(int* pData, double* pResults)
                {
                    // Variable definition
                    double[] dMedio = new double[] { 65.0, 100.0, 165.0, 230.0, 290.0};
                    double[] dInf = new double[] { 55.0, 70.0, 130.0, 200.0, 260.0};
                    double[] dSup = new double[] { 70.0, 130.0, 200.0, 260.0, 10000.0};

                    // Calculate the result
                    if (*(pData + 1) >= 0 && *(pData + 1) < 5)
                    {
                        *(pResults + 2) = dMedio[*(pData + 1)];
                        *(pResults + 3) = dInf[*(pData + 1)];
                        *(pResults + 4) = dSup[*(pData + 1)];
                    }
                    else
                    {
                        *(pResults + 2) = -1.0;
                        *(pResults + 3) = -1.0;
                        *(pResults + 4) = -1.0;
                    }

                    // Return from function
                    return;
                }

            }
        }

        namespace ThermalComfort
        {   // https://github.com/CenterForTheBuiltEnvironment/comfort_tool/blob/master/static/js/comfortmodels.js
            using System;
            using System.Collections.Generic;

            // Definición de tipos
            [StructLayout(LayoutKind.Sequential)]
            public struct DataTC
            {
                public double TempAir;      // Air temperature (C)
                public double TempRad;      // Radiant temperature (C)
                public double Velocity;     // Air velocity (m/s)
                public double RelHumidity;  // Relative humidity (%)
                public double Clothing;     // Clothing (clo)
                public double MetRate;      // Metabolic rate (met)
                public double ExternalWork; // External work (met), generally around 0
            };

            [StructLayout(LayoutKind.Sequential)]
            public struct VarsTC
            {
                public double PMV;          // Factor de intensidad del esfuerzo [0, 1]
                public double PPD;          // Factor de esfuerzos por minuto
                public double HL_Skin;      // HL1 heat loss diff. through skin
                public double HL_Sweating;  // HL2 heat loss by sweating
                public double HL_Latent;    // HL3 latent respiration heat loss
                public double HL_Dry;       // HL4 dry respiration heat loss
                public double HL_Radiation; // HL5 heat loss by radiation
                public double HL_Convection;// HL6 heat loss by convection
            };

            [StructLayout(LayoutKind.Sequential)]
            public struct ModelTC
            {
                [MarshalAs(UnmanagedType.Struct)]
                public DataTC data;         // Model data
                [MarshalAs(UnmanagedType.Struct)]
                public VarsTC factors;      // Model variables
                public double indexPMV;     // PMV index
                public double indexPPD;     // PPD index
            };

            public class CThermalModels
            {
                private List<ModelTC> _data;
                //private IndexType _index;

                public List<ModelTC> GetData { get => new(_data); set => _data = value; }


                [DllImport("dlls/comfort.dll", EntryPoint = "ComfortPMV")]
                private static extern void ComfortPMV([In, Out] ModelTC[] data, int Size);

                public CThermalModels()
                {

                }

                public CThermalModels(List<ModelTC> data)
                {
                    _data = data;
                }

                public CThermalModels(ModelTC[] data)
                {
                    _data = new(data);
                }

                public void ThermalComfort()
                {
                    var tempArray = _data.ToArray();
                    ComfortPMV(tempArray, tempArray.Length);
                    _data = new(tempArray);
                }

                public override string ToString()
                {
                    string strResult;
                    /*foreach (var data in _data)
                    {
                        strResult = String.Format("The PMV index is: {0:#.##} and the PPD index is: {1:#.##}", data.factors.PMV, data.factors.PPD);
                        strResult += System.Environment.NewLine;
                    }
                    */

                    string[] strLineD = new string[8];
                    string[] strLineF = new string[9];

                    strLineD[0] = string.Concat(System.Environment.NewLine, "Description", "\t");
                    strLineD[1] = string.Concat(System.Environment.NewLine, "Air temperature (C)");
                    strLineD[2] = string.Concat(System.Environment.NewLine, "Radiant temperature (C)");
                    strLineD[3] = string.Concat(System.Environment.NewLine, "Air velocity (m/s)");
                    strLineD[4] = string.Concat(System.Environment.NewLine, "Relative humidity (%)");
                    strLineD[5] = string.Concat(System.Environment.NewLine, "Clothing (clo)", "\t");
                    strLineD[6] = string.Concat(System.Environment.NewLine, "Metabolic rate (met)");
                    strLineD[7] = string.Concat(System.Environment.NewLine, "External work (met)");

                    strLineF[0] = string.Concat(System.Environment.NewLine, "Description", "\t");
                    strLineF[1] = string.Concat(System.Environment.NewLine, "Heat loss diff. through skin");
                    strLineF[2] = string.Concat(System.Environment.NewLine, "Heat loss by sweating", "\t");
                    strLineF[3] = string.Concat(System.Environment.NewLine, "Latent respiration heat loss");
                    strLineF[4] = string.Concat(System.Environment.NewLine, "Dry respiration heat loss", "\t");
                    strLineF[5] = string.Concat(System.Environment.NewLine, "Heat loss by radiation", "\t");
                    strLineF[6] = string.Concat(System.Environment.NewLine, "Heat loss by convection");
                    
                    strLineF[7] = string.Concat(System.Environment.NewLine, System.Environment.NewLine, "The PMV index");
                    strLineF[8] = string.Concat(System.Environment.NewLine, "The PPD index", "\t");

                    int i = 0;
                    foreach (var data in _data)
                    {
                        strLineD[0] += string.Concat("\t\t", "Case ", ((char)('A' + i)).ToString());
                        strLineD[1] += string.Concat("\t\t", data.data.TempAir.ToString("0.####"));
                        strLineD[2] += string.Concat("\t\t", data.data.TempRad.ToString("0.####"));
                        strLineD[3] += string.Concat("\t\t", data.data.Velocity.ToString("0.####"));
                        strLineD[4] += string.Concat("\t\t", data.data.RelHumidity.ToString("0.####"));
                        strLineD[5] += string.Concat("\t\t", data.data.Clothing.ToString("0.####"));
                        strLineD[6] += string.Concat("\t\t", data.data.MetRate.ToString("0.####"));
                        strLineD[7] += string.Concat("\t\t", data.data.ExternalWork.ToString("0.####"));

                        strLineF[0] += string.Concat("\t\t", "Case ", ((char)('A' + i)).ToString());
                        strLineF[1] += string.Concat("\t", data.factors.HL_Skin.ToString("0.####"));
                        strLineF[2] += string.Concat("\t", data.factors.HL_Sweating.ToString("0.####"));
                        strLineF[3] += string.Concat("\t", data.factors.HL_Latent.ToString("0.####"));
                        strLineF[4] += string.Concat("\t", data.factors.HL_Dry.ToString("0.####"));
                        strLineF[5] += string.Concat("\t", data.factors.HL_Radiation.ToString("0.####"));
                        strLineF[6] += string.Concat("\t", data.factors.HL_Convection.ToString("0.####"));

                        strLineF[7] += string.Concat("\t\t", data.factors.PMV.ToString("0.####"));
                        strLineF[8] += string.Concat("\t\t", data.factors.PPD.ToString("0.####"));

                        i++;
                    }
                    
                    //String.Format("{0,-10} | {1,-10} | {2,5}", "Bill", "Gates", 51));
                    strResult = string.Concat("These are the results for the PMV and the PPD indexes according to ISO 7730:",
                            System.Environment.NewLine,
                            string.Concat(strLineD),
                            System.Environment.NewLine,
                            string.Concat(strLineF),
                            System.Environment.NewLine);


                    return strResult;
                }
            }
        }

        namespace LibertyMutual
        {
            using System;
            using System.Collections.Generic;

            public enum MNType : int
            {
                Carrying = 0,
                Lifting = 1,
                Lowering = 2,
                Pulling = 3,
                Pushing = 4
            }

            public enum MNGender : int
            {
                Male = 0,
                Female = 1
            }

            // Definición de tipos
            [StructLayout(LayoutKind.Sequential)]
            public struct DataLiberty
            {
                public double HorzReach;   // Horizontal reach distance (H) must range from 0.20 to 0.68 m for females and 0.25 to 0.73 m for males. If H changes during a lift or lower, the mean H or maximum H can be used
                public double VertRangeM;  // Vertical range middle (m)
                public double DistHorz;    // The distance travelled horizontally per push or pull (m)
                public double DistVert;    // The distance travelled vertically (DV) per lift or lower must not be lower than 0.25 m or exceed arm reach for the anthropometry being used
                public double VertHeight;  // The vertical height of the hands (m)
                public double Freq;        // The frequency per minute. It must range from 1 per day (i.e. 1/480 = ?0.0021/min) to 20/min
                public MNType type;
                public MNGender gender;
            };

            [StructLayout(LayoutKind.Sequential)]
            public struct ScaleFactors
            {
                public double RF;       // Reference load (in kg or kgf)
                public double H;        // Horizontal reach factor
                public double VRM;      // Vertical range middle factor
                public double DH;       // Horizontal travel distance factor
                public double DV;       // Vertical travel distance factor
                public double V;        // Vertical height factor
                public double F;        // Frequency factor
                public double CV;       // Coefficient of variation
                public double MAL;      // Maximum acceptable load — Mean (in kg or kgf)
                public double MAL75;    // Maximum acceptable load for 75% (in kg or kgf)
                public double MAL90;    // Maximum acceptable load for 90% (in kg or kgf)
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct ResultsLiberty
            {
                public double IniCoeffV;   // Coefficient of variation
                public double SusCoeffV; // Coefficient of variation
                public double IniForce;    // Maximum initial force in kgf
                public double SusForce;  // Maximum sustained force in kgf
                public double Weight;      // Maximum weight in kg
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct ModelLiberty
            {
                [MarshalAs(UnmanagedType.Struct)]
                public DataLiberty data;         // Model data
                [MarshalAs(UnmanagedType.Struct)]
                //public ResultsLiberty results;      // Model variables
                public ScaleFactors Initial;
                public ScaleFactors Sustained;
            }

            public class CLibertyMutual
            {
                private List<ModelLiberty> _data;
                private string strGridHeader = "Task ";
                //private IndexType _index;

                public List<ModelLiberty> GetData { get => new(_data); set => _data = value; }
                public string TaskName { get => strGridHeader; set => strGridHeader = value; }

                [DllImport("dlls/liberty.dll", EntryPoint = "LibertyMutualMMH")]
                private static extern double LibertyMutualMMH([In, Out] ModelLiberty[] data, int Size);
                
                
                /// <summary>
                /// Default constructor
                /// </summary>
                public CLibertyMutual()
                {

                }

                /// <summary>
                /// Parametrized overloaded constructor
                /// </summary>
                /// <param name="data"></param>
                public CLibertyMutual(List<ModelLiberty> data)
                {
                    _data = data;
                }

                /// <summary>
                /// Computation function calling the DLL library
                /// </summary>
                public void ComputeMMH()
                {
                    var tempArray = _data.ToArray();
                    var result = LibertyMutualMMH(tempArray, tempArray.Length);
                    _data = new(tempArray);
                }
                
                /// <summary>
                /// ToString overloaded function converting the internal class data into a structured text output
                /// </summary>
                /// <returns>Structured text output</returns>
                public override string ToString()
                {
                    // https://stackoverflow.com/questions/26235759/how-to-format-a-double-with-fixed-number-of-significant-digits-regardless-of-th
                    // https://stackoverflow.com/questions/33401356/formatting-text-with-padding-does-not-line-up-in-c-sharp
                    string strResult;

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
                    foreach (var data in _data)
                    {
                        strLineD[0] += string.Concat("\t\t", strGridHeader, ((char)('A' + i)).ToString());
                        strLineD[1] += string.Concat("\t", data.data.type.ToString());
                        strLineD[8] += string.Concat("\t\t", data.data.gender.ToString());
                        
                        strLineF[0] += string.Concat("\t\t", strGridHeader, ((char)('A' + i)).ToString());
                        
                        strThresholds[0] += string.Concat(i == 0 ? "\t" : "\t\t", strGridHeader, ((char)('A' + i)).ToString());
                        if (data.data.type == MNType.Pulling || data.data.type == MNType.Pushing)
                        {
                            strLineD[2] += string.Concat("\t\t", "------");
                            strLineD[3] += string.Concat("\t\t", "------");
                            // Convert.ToDouble(String.Format("{0:G3}", number)).ToString()
                            //strLineD[4] += string.Concat("\t\t", data.data.DistHorz.ToString("0.####"));
                            strLineD[4] += string.Concat("\t\t", Convert.ToDouble(String.Format("{0:G5}", data.data.DistHorz)).ToString());
                            strLineD[5] += string.Concat("\t\t", "------");
                            strLineD[6] += string.Concat("\t\t", data.data.VertHeight.ToString("0.####"));
                            strLineD[7] += string.Concat("\t\t", data.data.Freq.ToString("0.####"));

                            strLineF[1] += string.Concat("\t\t", data.Initial.RF.ToString("0.####"));
                            strLineF[2] += string.Concat("\t\t", "------");
                            strLineF[3] += string.Concat("\t\t", "------");
                            strLineF[4] += string.Concat("\t\t", data.Initial.DH.ToString("0.####"));
                            strLineF[5] += string.Concat("\t\t", "------");
                            strLineF[6] += string.Concat("\t\t", data.Initial.V.ToString("0.####"));
                            strLineF[7] += string.Concat("\t\t", data.Initial.F.ToString("0.####"));

                            strLineF[8] += string.Concat("\t\t", data.Sustained.RF.ToString("0.####"));
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
                            if (data.data.type == MNType.Carrying)
                            {
                                strLineD[2] += string.Concat("\t\t", "------");
                                strLineD[3] += string.Concat("\t\t", "------");
                                strLineD[4] += string.Concat("\t\t", data.data.DistHorz.ToString("0.####"));
                                strLineD[5] += string.Concat("\t\t", "------");
                                strLineD[6] += string.Concat("\t\t", data.data.VertHeight.ToString("0.####"));
                                strLineD[7] += string.Concat("\t\t", data.data.Freq.ToString("0.####"));

                                strLineF[1] += string.Concat("\t\t", data.Initial.RF.ToString("0.####"));
                                strLineF[2] += string.Concat("\t\t", "------");
                                strLineF[3] += string.Concat("\t\t", "------");
                                strLineF[4] += string.Concat("\t\t", data.Initial.DV.ToString("0.####"));
                                strLineF[5] += string.Concat("\t\t", "------");
                                strLineF[6] += string.Concat("\t\t", data.Initial.V.ToString("0.####"));
                                strLineF[7] += string.Concat("\t\t", data.Initial.F.ToString("0.####"));
                            }
                            else //lift and lower
                            {
                                //strLineD[2] += string.Concat("\t\t", data.data.HorzReach.ToString("0.####"));
                                strLineD[2] += string.Concat("\t\t", Convert.ToDouble(String.Format("{0:G5}", data.data.HorzReach)).ToString());
                                strLineD[3] += string.Concat("\t\t", data.data.VertRangeM.ToString("0.####"));
                                strLineD[4] += string.Concat("\t\t", "------");
                                strLineD[5] += string.Concat("\t\t", data.data.DistVert.ToString("0.####"));
                                strLineD[6] += string.Concat("\t\t", "------");
                                strLineD[7] += string.Concat("\t\t", data.data.Freq.ToString("0.####"));

                                strLineF[1] += string.Concat("\t\t", data.Initial.RF.ToString("0.####"));
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
            
        }

    }   // namespace DLL

}   // namespace ErgoCalc