using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgoCalc.Models.Pushing;

public enum IndexType
{
	RSI = 0,
	COSI = 1,
	CUSI = 2
}

public class Data
{
	double i;       // Intensidad del esfuerzo
	double e;       // Esfuerzos por minuto
	double d;       // Duración del esfuerzo
	double p;       // Posición de la mano
	double h;       // Duración de la tarea
	double ea;      // Esfuerzos acumulados a
	double eb;      // Esfuerzos acumulados b

};

public class Multipliers
{
	double IM;   // Factor de intensidad del esfuerzo [0, 1]
	double EM;   // Factor de esfuerzos por minuto
	double DM;   // Factor de duración del esfuerzo
	double PM;   // Factor de posición de la mano
	double HM;   // Factor de duración de la tarea
	double EMa;  // Factor de esfuerzos acumulados a
	double EMb;  // Factor de esfuerzos acumulados b
};

public class SubTask
{
	Data data;				// Subtask data
	Multipliers factors;	// Subtask factors
	double index;			// The RSI index for this subtask
	int ItemIndex;
};

public class Task
{
	SubTask[] SubTasks; // Set of subtasks in the job
	int[] order;     // Reordering of the subtasks from lower RSI to higher RSI
	double h;       // The total time (in hours) that the task is performed per day
	double ha;      // Duración de la tarea acumulada a
	double hb;      // Duración de la tarea acumulada b
	double HM;      // Factor of the total time
	double HMa;     // Factor de duración de la tarea acumulada a
	double HMb;     // Factor de duración de la tarea acumulada b
	double index;   // The COSI index for this task
	int nsubtasks;  // Number of subtasks in the tasks
};

public class Job
{
	Task[] JobTasks;    // Set of tasks in the job
	int[] order;             // Reordering of the subtasks from lower COSI to higher COSI
	double index;           // The CUSI index for this job
	int ntasks;             // Number of tasks in the job
	int IndexType;          // 0 for RSI, 1 for COSI, and 2 for CUSI
};

public class Pushing
{
}
