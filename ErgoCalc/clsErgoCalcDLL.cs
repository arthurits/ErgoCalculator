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

    namespace DLL
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

        namespace NIOSHModel
        {
            // Definición de tipos
            [StructLayout(LayoutKind.Sequential)]
            public struct dataNIOSH
            {
                public double weight;  // Peso que se manipula
                public double h;       // Distancia horizontal
                public double v;       // Distancia vertical del punto de agarre al suelo
                public double d;       // Recorrido vertical
                public double a;       // Ángulo de simetría
                public double f;       // Frecuencia del levantamiento
                public double fa;      // Frecuencia acumulada a
                public double fb;      // Frecuencia acumulada b
                public double td;      // Duración de la tarea
                public int c;          // Agarre
            };

            [StructLayout(LayoutKind.Sequential)]
            public struct multipliersNIOSH
            {
                public double LC;   // Constante de carga
                public double HM;   // Factor de distancia horizontal
                public double VM;   // Factor de distancia vertical
                public double DM;   // Factor de desplazamiento vertical
                public double AM;   // Factor de asimetría
                public double FM;   // Factor de frecuencia
                public double FMa;  // Factor de frecuencia acumulada a
                public double FMb;  // Factor de frecuencia acumulada b
                public double CM;   // Factor de agarre
            };

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct modelNIOSH
            {
                [MarshalAs(UnmanagedType.Struct)]
                public dataNIOSH data;
                [MarshalAs(UnmanagedType.Struct)]
                public multipliersNIOSH factors;
                public double indexIF;
                public double index;
            };

            // Definición de la clase que encapsula la llamada a la DLL
            public class cModelINSHT
            {
                [DllImport("dlls/niosh.dll", EntryPoint = "CalculateNIOSH")]
                private static extern double CalculateNIOSH_DLL([In, Out] modelNIOSH[] datos, int[] orden, ref int nSize);

                public double CalculateNIOSH(modelNIOSH[] datos, int[] orden, ref int nSize)
                {
                    return CalculateNIOSH_DLL(datos, orden, ref nSize);
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
                    string strCadena ="[Maximum holding time]: {0}";
                    strCadena = string.Format("[Maximum holding time]:\t{0:0.###}\r\n[Paso]:\t{1:0.###}\r\n[Número de ciclos]:\t{2:0.###}\r\n[Número de puntos]:\t{3:0.###}", dMHT, dPaso, nCiclos,nPuntos);
                    return strCadena;
                }
            }

            public struct datosWR
            {
                public double _dMVC;
                public double _dMHT;
                public double[][] _dTrabajoDescanso;
                public double[][] _dTrabajoDescansop;
                public double _dPaso;
                public byte _bCiclos;
                public int _nCurva;
                public int _nPuntos;

                public override string ToString()
                {
                    System.String strCadena;
                    System.String strTiempoT = "";
                    System.String strTiempoD = "";
                    char[] charEspacio = { ' ' };

                    foreach (double d in _dTrabajoDescanso[0])
                        strTiempoT += d.ToString() + " ";
                    strTiempoT = strTiempoT.TrimEnd(charEspacio);

                    foreach (double d in _dTrabajoDescanso[1])
                        strTiempoD += d.ToString() + " ";
                    strTiempoD = strTiempoD.TrimEnd(charEspacio);

                    strCadena = "[Maximum voluntary contraction]: " + _dMVC.ToString() + "\r\n";
                    strCadena += "[Maximum holding time (min)]: " + _dMHT.ToString() + "\r\n";
                    strCadena += "[Ciclos de trabajo (min)]: " + strTiempoT + "\r\n";
                    strCadena += "[Ciclos de descanso (min)]: " + strTiempoD + "\r\n";
                    strCadena += "[Número de ciclos]: " + _bCiclos.ToString() + "\r\n";
                    strCadena += "[Paso]: " + _dPaso.ToString() + "\r\n";
                    strCadena += "[Número de curva]: " + _nCurva.ToString();
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

                public double[][] Curva(datosWR datos)
                {
                    // Definición de variables
                    // La matriz arrayCurva se inicializa a "empty" porque la función puede devolver
                    //   una matriz "empty" si no ha podido realizar el cálculo.
                    Double[][] arrayCurva = new double[0][];
                    //Double[][] arrayIntermedia = new Double[2][];
                    IntPtr ptrTiempos = IntPtr.Zero;
                    IntPtr ptrResultado = IntPtr.Zero;

                    // Definición de las variables que se pasan a la función
                    int longitud = datos._dTrabajoDescanso[0].Length;
                    datosDLL structDatos;
                    structDatos.dMHT = datos._dMHT;
                    structDatos.dPaso = datos._dPaso;
                    structDatos.nCiclos = Convert.ToInt32(datos._bCiclos);
                    structDatos.nPuntos = datos._nPuntos;

                    try
                    {
                        // Pasar la matriz a la memoria no gestionada
                        ptrTiempos = marshalJuggedToC(datos._dTrabajoDescanso);

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
                        marshalFreeMemory(ptrTiempos, datos._dTrabajoDescanso.Length);

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


                public double Sjogaard(double dMVC)
                {
                    return Sjogaard_DLL(dMVC);
                }

                public System.IntPtr Curva (System.IntPtr array, int tamaño, ref datosDLL datos)
                {
                    return Curva_DLL(array, tamaño, ref datos);
                }

                public System.IntPtr FreeMemory (System.IntPtr ptr)
                {
                    return FreeMemory_DLL(ptr);
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
                    IntPtr v1 = IntPtr.Zero;
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
                    IntPtr v1 = IntPtr.Zero;
                    int sizeofPtr = Marshal.SizeOf(typeof(IntPtr));
                    double[][] retval = new double[nI][];
                    for (int i = 0; i < retval.Length; i++)
                    {
                        retval[i] = new double[nJ];
                        v1 = Marshal.ReadIntPtr(carray, i * sizeofPtr);
                        Marshal.Copy(v1, retval[i], 0, nJ);
                        // Marshal.FreeCoTaskMem(v1);    // Se libera este puntero creado en la rutina marshalJuggedToC
                    }

                    v1 = IntPtr.Zero;
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
                    IntPtr puntero = IntPtr.Zero;
                    Int32 sizeofPtr = Marshal.SizeOf(typeof(IntPtr));

                    // Se libera la memoria de cada una de las filas de la matriz
                    for (Int32 i = 0; i < nElementos; i++)
                    {
                        puntero = Marshal.ReadIntPtr(matriz, i * sizeofPtr);
                        Marshal.FreeCoTaskMem(puntero);
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

        namespace Strain
        {
            using System;

            public enum Index
            {
                RSI,    // 0
                COSI,   // 1
                CUSI    // 2
            }

            // Definición de tipos
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct dataRSI
            {
                public double i;       // Intensidad del esfuerzo
                public double e;       // Esfuerzos por minuto
                public double d;       // Duración del esfuerzo
                public double p;       // Posición de la mano
                public double h;       // Duración de la tarea
                public double ea;       // Esfuerzos acumulados a
                public double eb;       // Esfuerzos acumulados b
            };

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct multipliersRSI
            {
                public double IM;   // Factor de intensidad del esfuerzo [0, 1]
                public double EM;   // Factor de esfuerzos por minuto
                public double DM;   // Factor de duración del esfuerzo
                public double PM;   // Factor de posición de la mano
                public double HM;   // Factor de duración de la tarea
                public double EMa;  // Factor de esfuerzos acumulados a
                public double EMb;  // Factor de esfuerzos acumulados a
            };

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct modelSubTask
            {
                [MarshalAs(UnmanagedType.Struct)]
                public dataRSI data;                // Subtask data
                [MarshalAs(UnmanagedType.Struct)]
                public multipliersRSI factors;      // Subtask factors
                public double index;                // The RSI index for this subtask
            };

            // https://docs.microsoft.com/es-es/dotnet/standard/native-interop/customize-struct-marshaling
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct modelTask
            {
                //[MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_ARRAY)]
                public modelSubTask[] SubTasks; // Set of subtasks in the task
                //[MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_I4)]
                public int[] order;             // Reordering of the subtasks from lower RSI to higher RSI
                public int numberSubTasks;      // Number of subtasks in the tasks
                public double h;                // The total time (in hours) that the task is performed per day
                public double ha;               // Duración de la tarea acumulada a
                public double hb;               // Duración de la tarea acumulada b
                public double HM;               // Factor of the total time
                public double HMa;              // Factor de duración de la tarea acumulada a
                public double HMb;              // Factor de duración de la tarea acumulada b
                public double index;            // The COSI index for this task

                public override string ToString()
                {
                    int i;
                    int length = SubTasks.Length;

                    string[] strLineD = new string[6];
                    string[] strLineR = new string[8];
                    string[] strTasks = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O" };

                    strLineD[0] = string.Concat(System.Environment.NewLine, "Description", "\t");
                    strLineD[1] = string.Concat(System.Environment.NewLine, "Intensity of exertion");
                    strLineD[2] = string.Concat(System.Environment.NewLine, "Efforts per minute");
                    strLineD[3] = string.Concat(System.Environment.NewLine, "Duration per exertion (s)");
                    strLineD[4] = string.Concat(System.Environment.NewLine, "Hand/wrist posture (-fl +ex)");
                    strLineD[5] = string.Concat(System.Environment.NewLine, "Task duration of task per day (h)");

                    strLineR[0] = string.Concat(System.Environment.NewLine, System.Environment.NewLine, "Description", "\t");
                    strLineR[1] = string.Concat(System.Environment.NewLine, "Intensity multiplier");
                    strLineR[2] = string.Concat(System.Environment.NewLine, "Efforts multiplier", "\t");
                    strLineR[3] = string.Concat(System.Environment.NewLine, "Duration multiplier");
                    strLineR[4] = string.Concat(System.Environment.NewLine, "Hand/wrist posture multiplier");
                    strLineR[5] = string.Concat(System.Environment.NewLine, "Task multiplier", "\t");
                    strLineR[6] = string.Concat(System.Environment.NewLine, System.Environment.NewLine, "The RSI index is:");
                    strLineR[7] = string.Empty;

                    for (i = 0; i < length; i++)
                    {
                        strLineD[0] += "\t\t" + "Task " + strTasks[i];
                        strLineD[1] += "\t\t" + SubTasks[i].data.i.ToString();
                        strLineD[2] += "\t\t" + SubTasks[i].data.e.ToString();
                        strLineD[3] += "\t\t" + SubTasks[i].data.d.ToString();
                        strLineD[4] += "\t\t" + SubTasks[i].data.p.ToString();    //strLineD[4].TrimEnd(new char[] { '\t' });
                        strLineD[5] += "\t\t" + SubTasks[i].data.h.ToString();

                        strLineR[0] += "\t\t" + "Task " + strTasks[i];
                        strLineR[1] += "\t\t" + SubTasks[i].factors.IM.ToString("0.####");
                        strLineR[2] += "\t\t" + SubTasks[i].factors.EM.ToString("0.####");
                        strLineR[3] += "\t\t" + SubTasks[i].factors.DM.ToString("0.####");
                        strLineR[4] += "\t\t" + SubTasks[i].factors.PM.ToString("0.####");
                        strLineR[5] += "\t\t" + SubTasks[i].factors.HM.ToString("0.####");
                        strLineR[6] += "\t\t" + SubTasks[i].index.ToString("0.####");
                    }

                    if (index == -1)
                    {
                        strLineR[7] = string.Concat(System.Environment.NewLine, System.Environment.NewLine, "The COSI index is:");
                        strLineR[7] += "\t\t" + index.ToString("0.####");
                    }

                    return string.Concat("These are the results obtained from the Revised Strain Index:",
                                        System.Environment.NewLine,
                                        string.Concat(strLineD),
                                        string.Concat(strLineR));
                }
            };

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct modelJob
            {
                //[MarshalAs(UnmanagedType.SafeArray)]
                public modelTask[] JobTasks;    // Set of tasks in the job
                //[MarshalAs(UnmanagedType.SafeArray)]
                public int[] order;             // Reordering of the subtasks from lower COSI to higher COSI
                public int numberTasks;         // Number of tasks in the job
                public double index;            // The CUSI index for this job

                public override string ToString()
                {
                    string [] strTasks = new string[numberTasks];
                    for (int i=0; i<numberTasks;i++)
                    {
                        strTasks[i]=JobTasks[i].ToString(); ;
                    }
                    string strJob;
                    strJob = System.Environment.NewLine;
                    strJob += System.Environment.NewLine;
                    strJob += "The CUSI index is: " + index.ToString("0.####");
                    return string.Concat(string.Concat(strTasks), strJob);
                }
            };

            // Definición de la clase que encapsula la llamada a la DLL
            public class cModelStrain
            {
                private modelJob _job;
                private Index _index;

                public modelJob Job { get => _job; set => _job = value; }
                public Index Index { get => _index; set => _index = value; }

                /* https://docs.microsoft.com/en-us/dotnet/framework/interop/marshaling-different-types-of-arrays
                
                https://stackoverflow.com/questions/7309838/improper-marshaling-c-sharp-array-to-a-c-unmanaged-array
                https://stackoverflow.com/questions/29126836/marshaling-nested-structures-from-c-sharp-to-c

                https://stackoverflow.com/questions/18498452/how-do-i-write-a-custom-marshaler-which-allows-data-to-flow-from-native-to-manag

                https://limbioliong.wordpress.com/2013/11/23/example-custom-marshaler-the-long-marshaler/
                https://limbioliong.wordpress.com/2013/11/03/understanding-custom-marshaling-part-1/

                Struct to byte array
                https://stackoverflow.com/questions/3278827/how-to-convert-a-structure-to-a-byte-array-in-c
                */

                [DllImport("dlls/strain.dll", EntryPoint = "StrainIndex")]
                private static extern double RSI_index([In, Out] modelSubTask[] subtasks, ref int nSize);
                [DllImport("dlls/strain.dll", EntryPoint = "StrainIndexCOSI")]
                private static extern double COSI_index(IntPtr Task, ref int nSubTasks);
                [DllImport("dlls/strain.dll", EntryPoint = "StrainIndexCUSI")]
                private static extern double CUSI_index(ref modelJob Job, ref int nTasks);

                [DllImport("dlls/strain.dll", EntryPoint = "DummyTest")]
                private static extern double RSI_Dummy(IntPtr Task, ref int nTasks);

                public cModelStrain(modelJob job, Index index)
                {
                    _job = job;
                    _index = index;
                }

                /// <summary>
                /// Calculates the RSI, COSI or CUSI index
                /// </summary>
                /// <returns></returns>
                public double StrainIndex()
                {
                    switch (_index)
                    {
                        case Index.RSI:
                            RSI_index(_job.JobTasks[0].SubTasks, ref _job.JobTasks[0].numberSubTasks);
                            _job.JobTasks[0].index = -1;    // Indicate that we only want the RSI and not the COSI index in the ToString()
                            break;
                        case Index.COSI:
                            int nSubTasks = 0;
                            for (int i = 0; i < _job.JobTasks.Length; i++)
                            {
                                IntPtr task = TaskToMarshal2(_job.JobTasks[i]);
                                try
                                {
                                    nSubTasks = _job.JobTasks[i].SubTasks.Length;
                                    double resultadoTest = RSI_Dummy(task, ref nSubTasks);
                                    //_job.JobTasks[i].index = COSI_index(ref (_job.JobTasks[i]), ref nSubTasks);
                                    //_job.JobTasks[i].index = COSI_index(TaskToMarshal(_job.JobTasks[i]), ref nSubTasks);
                                    //_job.JobTasks[i].index = COSI_index(task, ref nSubTasks);
                                    _job.JobTasks[i] = TaskfromMarshal2(task, _job.JobTasks[i]);
                                }
                                finally
                                {
                                    TaskFreeMemory(task);
                                }
                            };
                            break;
                        case Index.CUSI:
                            double resultado = CUSI_index(ref _job, ref _job.numberTasks);
                            break;
                        default:
                            break;
                    }
                    //return RSI(datos, ref nSize);
                    return 0.0;
                }

                public override string ToString()
                {
                    switch (_index)
                    {
                        case Index.RSI:
                            return ToStringRSI();
                        case Index.COSI:
                            return string.Empty;
                        case Index.CUSI:
                            return string.Empty;
                        default:
                            return string.Empty;
                    }
                }
                private string ToStringRSI()
                {
                    return _job.JobTasks[0].ToString();
                }   // ToString()

                #region Marshalling private routines
                private IntPtr TaskToMarshal(modelTask task)
                {
                    Int32 sizePtr = Marshal.SizeOf(typeof(IntPtr));
                    Int32 sizeInt = Marshal.SizeOf(typeof(int));
                    Int32 sizeDouble = Marshal.SizeOf(typeof(double));

                    int subtask = Marshal.SizeOf(typeof(modelSubTask));
                    int SubTasksSize = task.SubTasks.Length * sizePtr;

                    // Marshal array of subtasks (pointers to subtasks)
                    IntPtr ptrSubTasks = Marshal.AllocCoTaskMem(SubTasksSize);
                    for (int i=0; i<task.SubTasks.Length; i++)
                    {
                        IntPtr ptr = Marshal.AllocHGlobal(subtask);
                        Marshal.StructureToPtr(task.SubTasks[i], ptr, false);
                        Marshal.WriteIntPtr(ptrSubTasks, i * sizePtr, ptr);
                    }
                    
                    // Marshal array of ints
                    int orderSize = task.order.Length * sizeInt;
                    IntPtr ptrOrder = Marshal.AllocCoTaskMem(orderSize);
                    Marshal.Copy(task.order, 0, ptrOrder, task.order.Length);

                    // Allocate memory for modelTask struct
                    IntPtr p1 = Marshal.AllocCoTaskMem(2 * sizePtr + sizeInt + 7 * sizeDouble);

                    // Construct the struct in unmanaged memory
                    int memoffset = 0;
                    Marshal.WriteIntPtr(p1, memoffset, ptrSubTasks);
                    memoffset += sizePtr;
                    Marshal.WriteIntPtr(p1, memoffset, ptrOrder);
                    memoffset += sizePtr;
                    Marshal.WriteInt32(p1, memoffset, task.numberSubTasks);
                    memoffset += sizeInt;
                    Marshal.WriteInt64(p1, memoffset, (long)task.h);
                    memoffset += sizeDouble;
                    Marshal.WriteInt64(p1, memoffset, (long)task.ha);
                    memoffset += sizeDouble;
                    Marshal.WriteInt64(p1, memoffset, (long)task.hb);
                    memoffset += sizeDouble;
                    Marshal.WriteInt64(p1, memoffset, (long)task.HM);
                    memoffset += sizeDouble;
                    Marshal.WriteInt64(p1, memoffset, (long)task.HMa);
                    memoffset += sizeDouble;
                    Marshal.WriteInt64(p1, memoffset, (long)task.HMb);
                    memoffset += sizeDouble;
                    Marshal.WriteInt64(p1, memoffset, (long)task.index);
                    memoffset += sizeDouble;

                    return p1;
                }

                private IntPtr TaskToMarshal2(modelTask task)
                {
                    Int32 sizePtr = Marshal.SizeOf(typeof(IntPtr));
                    Int32 sizeInt = Marshal.SizeOf(typeof(int));
                    Int32 sizeDouble = Marshal.SizeOf(typeof(double));

                    
                    int nSubT = Marshal.SizeOf(typeof(modelSubTask));
                    byte[] arrSubT = new byte[nSubT];
                    int SubTasksSize = task.SubTasks.Length * nSubT;

                    // Marshal array of subtasks (pointers to subtasks)
                    IntPtr ptrSubTasks = Marshal.AllocCoTaskMem(SubTasksSize);
                    for (int i = 0; i < task.SubTasks.Length; i++)
                    {
                        //IntPtr ptr = Marshal.AllocHGlobal(nSubT);
                        //Marshal.StructureToPtr(task.SubTasks[i], ptr, true);
                        Marshal.StructureToPtr(task.SubTasks[i], IntPtr.Add(ptrSubTasks, i * nSubT), true);
                        //Marshal.Copy(ptr, arrSubT, 0, nSubT);
                        //Marshal.Copy(arrSubT, 0, IntPtr.Add(ptrSubTasks, i * nSubT), nSubT);
                        //Marshal.WriteIntPtr(ptrSubTasks, i * sizePtr, ptr);
                        //Marshal.FreeHGlobal(ptr);
                    }

                    // Marshal array of ints
                    int orderSize = task.order.Length * sizeInt;
                    IntPtr ptrOrder = Marshal.AllocCoTaskMem(orderSize);
                    Marshal.Copy(task.order, 0, ptrOrder, task.order.Length);

                    // Allocate memory for modelTask struct
                    IntPtr p1 = Marshal.AllocCoTaskMem(2 * sizePtr + sizeInt + 7 * sizeDouble);

                    // Construct the struct in unmanaged memory
                    int memoffset = 0;
                    Marshal.WriteIntPtr(p1, memoffset, ptrSubTasks);
                    memoffset += sizePtr;
                    Marshal.WriteIntPtr(p1, memoffset, ptrOrder);
                    memoffset += sizePtr;
                    Marshal.WriteInt32(p1, memoffset, task.numberSubTasks);
                    memoffset += sizeInt;


                    byte[] byteDouble = BitConverter.GetBytes(task.h);
                    Marshal.Copy(byteDouble, 0, IntPtr.Add(p1, memoffset), byteDouble.Length);
                    memoffset += sizeDouble;
                    
                    byteDouble = BitConverter.GetBytes(task.ha);
                    Marshal.Copy(byteDouble, 0, IntPtr.Add(p1, memoffset), byteDouble.Length);
                    memoffset += sizeDouble;
                    
                    byteDouble = BitConverter.GetBytes(task.hb);
                    Marshal.Copy(byteDouble, 0, IntPtr.Add(p1, memoffset), byteDouble.Length);
                    memoffset += sizeDouble;

                    byteDouble = BitConverter.GetBytes(task.HM);
                    Marshal.Copy(byteDouble, 0, IntPtr.Add(p1, memoffset), byteDouble.Length);
                    memoffset += sizeDouble;

                    byteDouble = BitConverter.GetBytes(task.HMa);
                    Marshal.Copy(byteDouble, 0, IntPtr.Add(p1, memoffset), byteDouble.Length);
                    memoffset += sizeDouble;

                    byteDouble = BitConverter.GetBytes(task.HMb);
                    Marshal.Copy(byteDouble, 0, IntPtr.Add(p1, memoffset), byteDouble.Length);
                    memoffset += sizeDouble;

                    byteDouble = BitConverter.GetBytes(task.index);
                    Marshal.Copy(byteDouble, 0, IntPtr.Add(p1, memoffset), byteDouble.Length);
                    memoffset += sizeDouble;

                    /*
                    Marshal.WriteInt64(p1, memoffset, (long)task.h);
                    memoffset += sizeDouble;
                    Marshal.WriteInt64(p1, memoffset, (long)task.ha);
                    memoffset += sizeDouble;
                    Marshal.WriteInt64(p1, memoffset, (long)task.hb);
                    memoffset += sizeDouble;
                    Marshal.WriteInt64(p1, memoffset, (long)task.HM);
                    memoffset += sizeDouble;
                    Marshal.WriteInt64(p1, memoffset, (long)task.HMa);
                    memoffset += sizeDouble;
                    Marshal.WriteInt64(p1, memoffset, (long)task.HMb);
                    memoffset += sizeDouble;
                    Marshal.WriteInt64(p1, memoffset, (long)task.index);
                    memoffset += sizeDouble;
                    */

                    return p1;
                }

                private modelTask TaskfromMarshal(IntPtr ptrTask, modelTask task1)
                {
                    // Definición de variables
                    IntPtr ptrSubT;
                    int sizePtr = Marshal.SizeOf(typeof(IntPtr));
                    modelTask task = new modelTask();
                    int memoffset = 0;
                    int nSubT = Marshal.ReadInt32(ptrTask, 2 * sizePtr);

                    // Get subtasks array
                    ptrSubT = Marshal.ReadIntPtr(ptrTask, memoffset);
                    task.SubTasks = new modelSubTask[nSubT];
                    for (int i = 0; i < nSubT; i++)
                    {
                        IntPtr ptr = Marshal.ReadIntPtr(ptrSubT, i * sizePtr);
                        task.SubTasks[i] = Marshal.PtrToStructure<modelSubTask>(ptr);
                        //task.SubTasks[i] = Marshal.PtrToStructure<modelSubTask>(IntPtr.Add(ptrSubT, i * sizePtr));
                    }
                    memoffset += sizePtr;

                    // Get order array
                    task.order = new int[nSubT];
                    IntPtr ptrOrder = Marshal.ReadIntPtr(ptrTask, memoffset);
                    Marshal.Copy(ptrOrder, task.order, 0, nSubT);
                    memoffset += sizePtr;

                    // Fill in the "regular" struct members
                    //task.numberSubTasks = Marshal.ReadInt32(ptrTask, 2 * sizePtr);
                    task.numberSubTasks = nSubT;
                    memoffset += Marshal.SizeOf(typeof(Int32));
                    task.h = Marshal.ReadInt64(ptrTask, memoffset);
                    memoffset += Marshal.SizeOf(typeof(double));
                    task.ha = Marshal.ReadInt64(ptrTask, memoffset);
                    memoffset += Marshal.SizeOf(typeof(double));
                    task.hb = Marshal.ReadInt64(ptrTask, memoffset);
                    memoffset += Marshal.SizeOf(typeof(double));
                    task.HM = Marshal.ReadInt64(ptrTask, memoffset);
                    memoffset += Marshal.SizeOf(typeof(double));
                    task.HMa = Marshal.ReadInt64(ptrTask, memoffset);
                    memoffset += Marshal.SizeOf(typeof(double));
                    task.HMb = Marshal.ReadInt64(ptrTask, memoffset);
                    memoffset += Marshal.SizeOf(typeof(double));
                    task.index = Marshal.ReadInt64(ptrTask, memoffset);

                    return task;
                }

                private modelTask TaskfromMarshal2(IntPtr ptrTask, modelTask task1)
                {
                    // Definición de variables
                    IntPtr ptrSubT;
                    int sizePtr = Marshal.SizeOf(typeof(IntPtr));
                    modelTask task = new modelTask();
                    int memoffset = 0;
                    int nSubT = Marshal.ReadInt32(ptrTask, 2 * sizePtr);
                    int nSubTask = Marshal.SizeOf(typeof(modelSubTask));

                    // Get subtasks array
                    ptrSubT = Marshal.ReadIntPtr(ptrTask, memoffset);
                    task.SubTasks = new modelSubTask[nSubT];
                    for (int i = 0; i < nSubT; i++)
                    {
                        task.SubTasks[i] = Marshal.PtrToStructure<modelSubTask>(IntPtr.Add(ptrSubT, i * nSubTask));
                    }
                    memoffset += sizePtr;

                    // Get order array
                    task.order = new int[nSubT];
                    IntPtr ptrOrder = Marshal.ReadIntPtr(ptrTask, memoffset);
                    Marshal.Copy(ptrOrder, task.order, 0, nSubT);
                    memoffset += sizePtr;

                    // Fill in the "regular" struct members
                    //task.numberSubTasks = Marshal.ReadInt32(ptrTask, 2 * sizePtr);
                    task.numberSubTasks = nSubT;
                    memoffset += Marshal.SizeOf(typeof(Int32));
                    task.h = ReadDouble(IntPtr.Add(ptrTask, memoffset));
                    task.h = (double)Marshal.ReadInt64(ptrTask, memoffset);
                    memoffset += Marshal.SizeOf(typeof(double));
                    task.ha = (double)Marshal.ReadInt64(ptrTask, memoffset);
                    memoffset += Marshal.SizeOf(typeof(double));
                    task.hb = (double)Marshal.ReadInt64(ptrTask, memoffset);
                    memoffset += Marshal.SizeOf(typeof(double));
                    task.HM = (double)Marshal.ReadInt64(ptrTask, memoffset);
                    memoffset += Marshal.SizeOf(typeof(double));
                    task.HMa = (double)Marshal.ReadInt64(ptrTask, memoffset);
                    memoffset += Marshal.SizeOf(typeof(double));
                    task.HMb = (double)Marshal.ReadInt64(ptrTask, memoffset);
                    memoffset += Marshal.SizeOf(typeof(double));
                    task.index = (double)Marshal.ReadInt64(ptrTask, memoffset);

                    return task;
                }

                private void TaskFreeMemory(IntPtr task)
                {
                    // Definición de variables
                    IntPtr ptrSubT;
                    int sizePtr = Marshal.SizeOf(typeof(IntPtr));

                    // Delete subtasks array
                    ptrSubT = Marshal.ReadIntPtr(task, 0);
                    /*int numSubT = Marshal.ReadInt32(task, 2 * sizePtr);
                    for (int i = 0; i < numSubT; i++)
                    {
                        IntPtr subT = Marshal.ReadIntPtr(ptrSubT, i * sizePtr);
                        Marshal.FreeCoTaskMem(subT);
                    }*/
                    Marshal.FreeCoTaskMem(ptrSubT);
                    
                    // Delete order array
                    IntPtr ptrOrder = Marshal.ReadIntPtr(task, sizePtr);
                    Marshal.FreeCoTaskMem(ptrOrder);

                    // Se libera la memoria del struct
                    Marshal.FreeCoTaskMem(task);
                }

                private double ReadDouble(IntPtr mem1)
                {
                    if (mem1 != IntPtr.Zero)
                    {
                        byte[] ba = new byte[sizeof(double)];

                        for (int i = 0; i < ba.Length; i++)
                            ba[i] = Marshal.ReadByte(mem1, i);
                        double v = BitConverter.ToDouble(ba, 0);
                        return v;
                    }
                    return 0;
                }

                #endregion Marshalling private routines
            }   // class cModelStrain
        }   // namespace Strain
    }   // namespace DLL
}   // namespace ErgoCalc