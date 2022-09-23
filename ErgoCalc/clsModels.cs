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
