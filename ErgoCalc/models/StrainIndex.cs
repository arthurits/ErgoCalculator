using System;

namespace ErgoCalc.Models.StrainIndex;

public enum IndexType
{
	RSI = 0,
	COSI = 1,
	CUSI = 2
}

public class Data
{
    public double i { get; set; } = 0;       // Intensidad del esfuerzo
    public double e { get; set; } = 0;       // Esfuerzos por minuto
    public double d { get; set; } = 0;       // Duración del esfuerzo
    public double p { get; set; } = 0;       // Posición de la mano
    public double h { get; set; } = 0;       // Duración de la tarea
    public double ea { get; set; } = 0;      // Esfuerzos acumulados a
    public double eb { get; set; } = 0;      // Esfuerzos acumulados b

};

public class Multipliers
{
    public double IM { get; set; } = 0;   // Factor de intensidad del esfuerzo [0, 1]
    public double EM { get; set; } = 0;   // Factor de esfuerzos por minuto
    public double DM { get; set; } = 0;   // Factor de duración del esfuerzo
    public double PM { get; set; } = 0;   // Factor de posición de la mano
    public double HM { get; set; } = 0;   // Factor de duración de la tarea
    public double EMa { get; set; } = 0;  // Factor de esfuerzos acumulados a
    public double EMb { get; set; } = 0;  // Factor de esfuerzos acumulados b
};

public class SubTask
{
	public Data Data { get; set; } = new();				// Subtask data
	public Multipliers Factors { get; set; } = new(); // Subtask factors
    public double IndexRSI { get; set; } = 0;          // The RSI index for this subtask
    public int ItemIndex { get; set; } = 0;
};

public class TaskModel
{
    public SubTask[] SubTasks { get; set; } = Array.Empty<SubTask>(); // Set of subtasks in the job
    public int[] Order { get; set; } = Array.Empty<int>();     // Reordering of the subtasks from lower RSI to higher RSI
    public double h { get; set; } = 0;       // The total time (in hours) that the task is performed per day
    public double ha { get; set; } = 0;      // Duración de la tarea acumulada a
    public double hb { get; set; } = 0;      // Duración de la tarea acumulada b
    public double HM { get; set; } = 0;      // Factor of the total time
    public double HMa { get; set; } = 0;     // Factor de duración de la tarea acumulada a
    public double HMb { get; set; } = 0;     // Factor de duración de la tarea acumulada b
    public double IndexCOSI { get; set; } = 0;   // The COSI index for this task

    /// <summary>
    /// Number of sub-tasks in the task
    /// </summary>
    public int NumberSubTasks { get; set; } = 0;  // Number of subtasks in the task

    public override string ToString()
    {
        int i;
        int subLength = SubTasks.Length;
        String strName = IndexCOSI == -1 ? "Task " : "SubT ";

        string[] strLineD = new string[8];
        string[] strLineF = new string[10];
        string strEqT;
        string strEqR;
        //string[] strTasks = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O" };

        strLineD[0] = string.Concat(System.Environment.NewLine, "Description", "\t");
        strLineD[1] = string.Concat(System.Environment.NewLine, "Intensity of exertion");
        strLineD[2] = string.Concat(System.Environment.NewLine, "Efforts per minute");
        if (IndexCOSI != -1)
        {
            strLineD[3] = string.Concat(System.Environment.NewLine, "Efforts per minute A");
            strLineD[4] = string.Concat(System.Environment.NewLine, "Efforts per minute B");
        }
        strLineD[5] = string.Concat(System.Environment.NewLine, "Duration per exertion (s)");
        strLineD[6] = string.Concat(System.Environment.NewLine, "Hand/wrist posture (-f +e)");
        strLineD[7] = string.Concat(System.Environment.NewLine, strName, "duration per day (h)");

        strLineF[0] = string.Concat(System.Environment.NewLine, System.Environment.NewLine, "Description", "\t");
        strLineF[1] = string.Concat(System.Environment.NewLine, "Intensity multiplier");
        strLineF[2] = string.Concat(System.Environment.NewLine, "Efforts multiplier", "\t");
        if (IndexCOSI != -1)
        {
            strLineF[3] = string.Concat(System.Environment.NewLine, "Efforts A multiplier");
            strLineF[4] = string.Concat(System.Environment.NewLine, "Efforts B multiplier");
        }
        strLineF[5] = string.Concat(System.Environment.NewLine, "Duration multiplier");
        strLineF[6] = string.Concat(System.Environment.NewLine, "Hand/wrist posture mult.");
        strLineF[7] = string.Concat(System.Environment.NewLine, strName, "duration multiplier");

        if (IndexCOSI == -1)
        {
            strLineF[8] = string.Concat(System.Environment.NewLine, System.Environment.NewLine, "The RSI index is:");
        }
        else
        {
            strLineF[8] = string.Concat(System.Environment.NewLine, System.Environment.NewLine, "Subtask RSI index:");
            strLineF[9] = string.Concat(System.Environment.NewLine, "Subtasks order:", "\t");
        }

        for (i = 0; i < subLength; i++)
        {
            // "SubTask " + ((char)('A' + SubTasks[i].ItemIndex)).ToString()
            //strLineD[0] += "\t\t" + strName + strTasks[SubTasks[i].ItemIndex];
            strLineD[0] += "\t\t" + strName + ((char)('A' + SubTasks[i].ItemIndex)).ToString();
            strLineD[1] += "\t\t" + SubTasks[i].Data.i.ToString();
            strLineD[2] += "\t\t" + SubTasks[i].Data.e.ToString();
            if (IndexCOSI != -1)
            {
                strLineD[3] += "\t\t" + SubTasks[i].Data.ea.ToString();
                strLineD[4] += "\t\t" + SubTasks[i].Data.eb.ToString();
            }
            strLineD[5] += "\t\t" + SubTasks[i].Data.d.ToString();
            strLineD[6] += "\t" + SubTasks[i].Data.p.ToString() + "\t";
            strLineD[7] += "\t" + SubTasks[i].Data.h.ToString() + "\t";

            strLineF[0] += "\t\t" + strName + ((char)('A' + SubTasks[i].ItemIndex)).ToString();
            strLineF[1] += "\t\t" + SubTasks[i].Factors.IM.ToString("0.####");
            strLineF[2] += "\t\t" + SubTasks[i].Factors.EM.ToString("0.####");
            if (IndexCOSI != -1)
            {
                strLineF[3] += "\t\t" + SubTasks[i].Factors.EMa.ToString();
                strLineF[4] += "\t\t" + SubTasks[i].Factors.EMb.ToString();
            }
            strLineF[5] += "\t\t" + SubTasks[i].Factors.DM.ToString("0.####");
            strLineF[6] += "\t" + SubTasks[i].Factors.PM.ToString("0.####") + "\t";
            strLineF[7] += "\t\t" + SubTasks[i].Factors.HM.ToString("0.####");
            strLineF[8] += "\t\t" + SubTasks[i].IndexRSI.ToString("0.####");
            if (IndexCOSI != -1)
                strLineF[9] += "\t\t" + (Order[i] + 1).ToString("0.####");
        }

        strLineD[6] = strLineD[6].TrimEnd();    // Also strLineD[6].TrimEnd(new char[] { '\t' })
        strLineD[7] = strLineD[7].TrimEnd();
        strLineF[6] = strLineF[6].TrimEnd();

        strEqT = string.Concat(System.Environment.NewLine, System.Environment.NewLine);
        strEqR = string.Empty;
        if (IndexCOSI == -1)
        {
            strEqT += string.Concat("The RSI index is computed as follows:", System.Environment.NewLine, System.Environment.NewLine);
            strEqT += "RSI = IM * EM * DM * PM * HM";

            strEqR = string.Empty;
            for (i = 0; i < subLength; i++)
            {
                strEqR += string.Concat(System.Environment.NewLine, "RSI (", ((char)('A' + i)).ToString(), ") = ");
                strEqR += string.Concat(SubTasks[i].Factors.IM.ToString("0.####"), " * ");
                strEqR += string.Concat(SubTasks[i].Factors.EM.ToString("0.####"), " * ");
                strEqR += string.Concat(SubTasks[i].Factors.DM.ToString("0.####"), " * ");
                strEqR += string.Concat(SubTasks[i].Factors.PM.ToString("0.####"), " * ");
                strEqR += string.Concat(SubTasks[i].Factors.HM.ToString("0.####"), " = ");
                strEqR += SubTasks[i].IndexRSI.ToString("0.####");
            }

        }
        else
        {
            strEqT += string.Concat("The COSI index is computed as follows:", System.Environment.NewLine, System.Environment.NewLine);
            strEqT += string.Concat("RSI = IM * EM * DM * PM * HM", System.Environment.NewLine);
            strEqT += string.Concat("COSI = ", "RSI(", ((char)('A' + SubTasks[Order[0]].ItemIndex)).ToString(), ")");
            strEqR += string.Concat("COSI = ", SubTasks[Order[0]].IndexRSI.ToString("0.####"));

            string strLetter;
            for (i = 1; i < subLength; i++)
            {
                strLetter = ((char)('A' + SubTasks[Order[i]].ItemIndex)).ToString();
                strEqT += string.Concat(" + ", "RSI(", strLetter, ") * (EMa(", strLetter, ") - EMb(", strLetter, ")) / (EM(", strLetter, "))");
                strEqR += string.Concat(" + ", SubTasks[Order[i]].IndexRSI.ToString("0.####"), " * (", SubTasks[Order[i]].Factors.EMa.ToString("0.####"), " - ", SubTasks[Order[i]].Factors.EMb.ToString("0.####"), ") / ", SubTasks[Order[i]].Factors.EM.ToString("0.####"));
            }
            strEqR += string.Concat(" = ", IndexCOSI.ToString("0.####"));

            strEqR += string.Concat(System.Environment.NewLine, System.Environment.NewLine, "The COSI index is: ", IndexCOSI.ToString("0.####"));

            //strEqT += string.Concat(System.Environment.NewLine, System.Environment.NewLine, "The COSI index is:");
            //strEqT += "\t\t" + index.ToString("0.####");
        }

        return string.Concat("These are the results obtained from the Revised Strain Index model:",
                            System.Environment.NewLine,
                            string.Concat(strLineD),
                            string.Concat(strLineF),
                            strEqT,
                            System.Environment.NewLine,
                            strEqR,
                            System.Environment.NewLine);
    }
};

public class Job
{
    public TaskModel[] Tasks { get; set; } = Array.Empty<TaskModel>(); // Set of tasks in the job
    public int[] Order { get; set; } = Array.Empty<int>();      // Reordering of the subtasks from lower COSI to higher COSI
    
    /// <summary>
    /// CUSI index value
    /// </summary>
    public double IndexCUSI { get; set; } = 0;

    /// <summary>
    /// Number of tasks in the job
    /// </summary>
    public int NumberTasks { get; set; } = 0;
    
    /// <summary>
    /// Number of sub-tasks in the job
    /// </summary>
    public int NumberSubTasks { get; set; } = 0;
    
    
    public IndexType Model { get; set; } = IndexType.RSI;

    public override string ToString()
    {
        string strTasks = string.Empty;

        foreach (TaskModel task in Tasks)
            strTasks += task.ToString() + Environment.NewLine + Environment.NewLine;

        string strCUSI = string.Empty;
        if (Model == IndexType.CUSI)
        {
            string[] strLineD = new string[4];
            string[] strLineF = new string[6];

            strLineD[0] = string.Concat(System.Environment.NewLine, "Description", "\t");
            strLineD[1] = string.Concat(System.Environment.NewLine, "Duration per day (h)");
            strLineD[2] = string.Concat(System.Environment.NewLine, "Duration per day A (h)");
            strLineD[3] = string.Concat(System.Environment.NewLine, "Duration per day B (h)");

            strLineF[0] = string.Concat(System.Environment.NewLine, "Description", "\t");
            strLineF[1] = string.Concat(System.Environment.NewLine, "Duration multiplier");
            strLineF[2] = string.Concat(System.Environment.NewLine, "Duration A multiplier");
            strLineF[3] = string.Concat(System.Environment.NewLine, "Duration B multiplier");

            strLineF[4] = string.Concat(System.Environment.NewLine, System.Environment.NewLine, "Task COSI index");
            strLineF[5] = string.Concat(System.Environment.NewLine, "Task order");

            for (int i = 0; i < NumberTasks; i++)
            {
                strLineD[0] += string.Concat("\t\t", "Task ", ((char)('A' + i)).ToString());
                strLineD[1] += string.Concat("\t\t", Tasks[i].h.ToString("0.####"));
                strLineD[2] += string.Concat("\t\t", Tasks[i].ha.ToString("0.####"));
                strLineD[3] += string.Concat("\t\t", Tasks[i].hb.ToString("0.####"));

                strLineF[0] += string.Concat("\t\t", "Task ", ((char)('A' + i)).ToString());
                strLineF[1] += string.Concat("\t\t", Tasks[i].HM.ToString("0.####"));
                strLineF[2] += string.Concat("\t\t", Tasks[i].HMa.ToString("0.####"));
                strLineF[3] += string.Concat("\t\t", Tasks[i].HMb.ToString("0.####"));
                strLineF[4] += string.Concat("\t\t", Tasks[i].IndexCOSI.ToString("0.####"));
                strLineF[5] += string.Concat("\t\t", (Order[i] + 1).ToString("0.####"));
            }

            string strEqT = string.Concat(System.Environment.NewLine, System.Environment.NewLine);
            string strEqR = string.Empty;

            strEqT += string.Concat("The CUSI index is computed as follows:", System.Environment.NewLine, System.Environment.NewLine);
            strEqT += string.Concat("CUSI = ", "COSI(", ((char)('A' + Order[0])).ToString(), ")");
            strEqR += string.Concat("CUSI = ", Tasks[Order[0]].IndexCOSI.ToString("0.####"));

            string strLetter;
            for (int i = 1; i < NumberTasks; i++)
            {
                strLetter = ((char)('A' + Order[i])).ToString();
                strEqT += string.Concat(" + ", "COSI(", strLetter, ") * (HMa(", strLetter, ") - HMb(", strLetter, ")) / (HM(", strLetter, "))");
                strEqR += string.Concat(" + ", Tasks[Order[i]].IndexCOSI.ToString("0.####"), " * (", Tasks[Order[i]].HMa.ToString("0.####"), " - ", Tasks[Order[i]].HMb.ToString("0.####"), ") / ", Tasks[Order[i]].HM.ToString("0.####"));
            }
            strEqR += string.Concat(" = ", IndexCUSI.ToString("0.####"));
            strEqR += string.Concat(System.Environment.NewLine, System.Environment.NewLine, "The CUSI index is: ", IndexCUSI.ToString("0.####"));

            strCUSI = string.Concat(
                System.Environment.NewLine,
                string.Concat(strLineD),
                System.Environment.NewLine,
                string.Concat(strLineF),
                System.Environment.NewLine,
                strEqT,
                System.Environment.NewLine,
                strEqR,
                System.Environment.NewLine);

        }

        return string.Concat(string.Concat(strTasks), strCUSI);
    }
};

public static class StrainIndex
{
    /// <summary>
    /// Compute the revised strain index
    /// </summary>
    /// <param name="subT">Array of independent subtasks whose RSI is computed</param>
	public static void IndexRSI(SubTask[] subT)
	{
        for (int i = 0; i < subT.Length; i++)
        {
            subT[i].Factors.IM = FactorIM(subT[i].Data.i);
            subT[i].Factors.EM = FactorEM(subT[i].Data.e);
            subT[i].Factors.DM = FactorDM(subT[i].Data.d);
            subT[i].Factors.PM = FactorPM(subT[i].Data.p);
            subT[i].Factors.HM = FactorHM(subT[i].Data.h);

            subT[i].IndexRSI = MultiplyFactors(subT[i].Factors);
        }
    }

    /// <summary>
    /// Compute the COSI index at the task level
    /// </summary>
    /// <param name="task">Task whose COSI index is computed</param>
	public static void IndexCOSI(TaskModel task)
	{
        // First compute the RSI index for each subtask
        IndexRSI(task.SubTasks);

        // 2nd step: Sort the RSI indexes from highest to lowest
        double[] indexOrder = new double[task.SubTasks.Length];
        int[] values = new int[task.SubTasks.Length];
        for (int i = 0; i < task.SubTasks.Length; i++)
        {
            indexOrder[i] = task.SubTasks[i].IndexRSI;
            values[i] = i;
        }
        Array.Sort(indexOrder, values);
        Array.Reverse(values);
        //for (int i = 0; i < values.Length; i++) task.order[i] = values[i];
        Array.Copy(values, task.Order, values.Length);

        // 3rd step: Compute the cumulative index
        task.IndexCOSI = task.SubTasks[values[0]].IndexRSI;
        task.SubTasks[values[0]].Data.ea = task.SubTasks[values[0]].Data.eb = task.SubTasks[values[0]].Data.e;
        task.h = task.SubTasks[values[0]].Data.h;
        //task.SubTasks[values[0]].order = 0;

        for (int i = 1; i < task.SubTasks.Length; i++)
        {
            task.SubTasks[values[i]].Data.ea = task.SubTasks[values[i - 1]].Data.ea + task.SubTasks[values[i]].Data.e;
            task.SubTasks[values[i]].Data.eb = task.SubTasks[values[i - 1]].Data.ea;
            task.SubTasks[values[i]].Factors.EMa = FactorEM(task.SubTasks[values[i]].Data.ea);
            task.SubTasks[values[i]].Factors.EMb = FactorEM(task.SubTasks[values[i]].Data.eb);
            task.IndexCOSI += (task.SubTasks[values[i]].IndexRSI / task.SubTasks[values[i]].Factors.EM) * (task.SubTasks[values[i]].Factors.EMa - task.SubTasks[values[i]].Factors.EMb);

            task.h += task.SubTasks[values[i]].Data.h; // All subtasks should share the same h
            //task.SubTasks[values[i]].order = i;
            //result += task.SubTasks[values[i]].indexIF * (1 / task.SubTasks[values[i]].factors.FMa - 1 / task.SubTasks[values[i]].factors.FMb);
        }

        task.h /= task.SubTasks.Length;    // In case they don't, the average h is computed
        task.HM = FactorHM(task.h);

        // return task.index;
    }

    /// <summary>
    /// Compute the CUSI index at the job level
    /// </summary>
    /// <param name="job">Job whose CUSI index is computed</param>
	public static void IndexCUSI(Job job)
	{
        // First compute the COSI index for each subtask
        double[] indexOrder = new double[job.Tasks.Length];
        int[] values = new int[job.Tasks.Length];
        for (int i = 0; i < job.Tasks.Length; i++)
        {
            IndexCOSI(job.Tasks[i]);
            indexOrder[i] = job.Tasks[i].IndexCOSI;
            values[i] = i;
        }

        // 2nd step: Sort the COSI indexes from highest to lowest
        Array.Sort(indexOrder, values);
        Array.Reverse(values);
        Array.Copy(values, job.Order, values.Length);

        /* 3er paso: calcular el sumatorio con los índices recalculados */
        job.IndexCUSI = job.Tasks[values[0]].IndexCOSI;
        job.Tasks[values[0]].ha = job.Tasks[values[0]].hb = job.Tasks[values[0]].h;

        for (int i = 1; i < job.Tasks.Length; i++)
        {
            job.Tasks[values[i]].ha = job.Tasks[values[i - 1]].ha + job.Tasks[values[i - 1]].h;
            job.Tasks[values[i]].hb = job.Tasks[values[i - 1]].ha;
            job.Tasks[values[i]].HMa = FactorHM(job.Tasks[values[i]].ha);
            job.Tasks[values[i]].HMb = FactorHM(job.Tasks[values[i - 1]].hb);
            job.IndexCUSI += (job.Tasks[values[i]].IndexCOSI / job.Tasks[values[i]].HM) * (job.Tasks[values[i]].HMa - job.Tasks[values[i]].HMb);
        }

        
        //return job.index;
    }

    /// <summary>
    /// Función para la multiplicación de los factores
    /// </summary>
    /// <param name="factors">Apuntador a los factores</param>
    /// <returns>Resultado de la multiplicación</returns>
    private static double MultiplyFactors(Multipliers factors)
    {
        // Definición de variables
        double result = factors.IM *
            factors.EM *
            factors.DM *
            factors.PM *
            factors.HM;

        return result;
    }

    /// <summary>
    /// Función para calcular el factor IM (intensidad del esfuerzo)
    /// </summary>
    /// <param name="value">Intensidad del esfuerzo en tanto por uno</param>
    /// <returns>Valor del factor IM</returns>
    private static double FactorIM(double value)
	{
        // Definición de variables
        double result = 0.0;

        // Calcular el factor
        if (value >= 0 && value <= 0.4)
            result = 30.0 * Math.Pow(value, 3) - 15.6 * Math.Pow(value, 2) + 13.0 * value + 0.4;
        else if (value > 0.4 && value <= 1.0)
            result = 36.0 * Math.Pow(value, 3) - 33.3 * Math.Pow(value, 2) + 24.77 * value - 1.86;

        // Devolver el resultado
        return result;
    }

    /// <summary>
    /// Función para calcular el factor EM (esfuerzos por minuto)
    /// </summary>
    /// <param name="value">Frecuencia de esfuerzos por minuto</param>
    /// <returns>Valor del factor EM</returns>
    private static double FactorEM(double value)
    {
        // Definición de variables
        double result = 0.0;

        // Calcular el factor
        if (value >= 0 && value <= 90)
            result = 0.10 + 0.25 * value;
        else if (value > 0.4 && value <= 1.0)
            result = 0.00334 * Math.Pow(value, 1.96);

        // Devolver el resultado
        return result;
    }

    /// <summary>
    /// Función para calcular el factor DM (duración del esfuerzo)
    /// </summary>
    /// <param name="value">Duración del esfuerzo en segundos</param>
    /// <returns>Valor del factor DM</returns>
    private static double FactorDM(double value)
    {
        // Definición de variables
        double result = 0.0;

        // Calcular el factor
        if (value >= 0 && value <= 60.0)
            result = 0.45 + 0.31 * value;
        else if (value > 60)
            result = 19.17 * Math.Log(value) - 59.44;

        // Devolver el resultado
        return result;
    }

    /// <summary>
    /// Función para calcular el factor PM (posición de la mano o muñeca)
    /// </summary>
    /// <param name="value">Grados de flexión (negativo) o extensión (positivo)</param>
    /// <returns>Valor del factor PM</returns>
    private static double FactorPM(double value)
    {
        // Definición de variables
        double result = 0.0;

        // Calcular el factor
        if (value < 0)
            result = 1.2 * Math.Exp(0.009 * (-value)) - 0.2;
        else if (value >= 0 && value <= 30)
            result = 1.0;
        else if (value > 30)
            result = 1.0 + 0.00028 * Math.Pow(value - 30, 2);

        // Devolver el resultado
        return result;
    }

    /// <summary>
    /// Función para calcular el factor HM (duración de la tarea)
    /// </summary>
    /// <param name="value">Tiempo en horas</param>
    /// <returns>Valor del factor HM</returns>
    private static double FactorHM(double value)
    {
        // Definición de variables
        double result = 0.0;

        if (value > 0 && value <= 0.05)
            result = 0.20;
        else if (value > 0.05)
            result = 0.042 * value + 0.090 * Math.Log(value) + 0.477;

        // Devolver el resultado
        return result;
    }

    /// <summary>
    /// Función para comprobar si la frecuencia y la duración están dentro del ciclo
    /// </summary>
    /// <param name="e">Frecuencia de esfuerzos por minuto</param>
    /// <param name="d">Duración del esfuerzo en segundos</param>
    /// <returns>1 si está OK, 0 si no se cumple la condición</returns>
    private static int CheckCicle(double e, double d)
    {
        // Definición de variables. Por defecto es "false"
        int result = 0;

        if (e * d / 60 <= 1)
            result = 1;

        return result;
    }

}
