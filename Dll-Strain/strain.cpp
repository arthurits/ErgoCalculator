// $nombredeproyecto$.cpp: define las funciones exportadas de la aplicación DLL.
//

#include "stdafx.h"

#pragma once
#ifdef STRAININDEX_EXPORTS
#define StrainIndex_API __declspec(dllexport)
#else
#define StrainIndex_API __declspec(dllimport)
#endif

/* Definición de tipos */
typedef struct dataStrain
{
	double i;		// Intensidad del esfuerzo
	double e;       // Esfuerzos por minuto
	double d;       // Duración del esfuerzo
	double p;       // Posición de la mano
	double h;       // Duración de la tarea
	double ea;		// Esfuerzos acumulados a
	double eb;		// Esfuerzos acumulados b

};

typedef struct multipliersStrain
{
	double IM;   // Factor de intensidad del esfuerzo [0, 1]
	double EM;   // Factor de esfuerzos por minuto
	double DM;   // Factor de duración del esfuerzo
	double PM;   // Factor de posición de la mano
	double HM;   // Factor de duración de la tarea
	double EMa;  // Factor de esfuerzos acumulados a
	double EMb;  // Factor de esfuerzos acumulados b
};

typedef struct modelSubTask
{
	dataStrain data;			// Subtask data
	multipliersStrain factors;	// Subtask factors
	double index;				// The RSI index for this subtask
	int ItemIndex;
};

typedef struct modelTask
{
	modelSubTask* subtasks;	// Set of subtasks in the job
	int* order;		// Reordering of the subtasks from lower RSI to higher RSI
	double h;		// The total time (in hours) that the task is performed per day
	double ha;		// Duración de la tarea acumulada a
	double hb;		// Duración de la tarea acumulada b
	double HM;		// Factor of the total time
	double HMa;		// Factor de duración de la tarea acumulada a
	double HMb;		// Factor de duración de la tarea acumulada b
	double index;	// The COSI index for this task
	int nsubtasks;	// Number of subtasks in the tasks
};

typedef struct modelJob
{
	modelTask* JobTasks;	// Set of tasks in the job
	int* order;				// Reordering of the subtasks from lower COSI to higher COSI
	double index;			// The CUSI index for this job
	int ntasks;				// Number of tasks in the job
	int IndexType;			// 0 for RSI, 1 for COSI, and 2 for CUSI
};


/* Prototipos de función a exportar*/
extern "C" StrainIndex_API void __stdcall StrainIndex(modelJob*);
extern "C" StrainIndex_API double __stdcall StrainIndexRSI(modelSubTask[], const int * const);
extern "C" StrainIndex_API double __stdcall StrainIndexCOSI(modelTask*, const int* const);
extern "C" StrainIndex_API double __stdcall StrainIndexCUSI(modelJob*, const int* const);

/* Prototipos de funciones internas*/
double CalculateIndex(const multipliersStrain * const);
double FactorIM(const double * const);
double FactorEM(const double * const);
double FactorDM(const double * const);
double FactorPM(const double * const);
double FactorHM(const double * const);
int CheckCicle(const double * const, const double * const);
void HeapSortIndex(double[], int[], const int* const);

//double InterpolacionLineal(const double * const, const double * const, const double * const, const double * const, const double * const);
//int locate(double[], int, double);
//void HeapSortIndex(double[], int[], const int * const);

/// <summary>
/// Función principal para el cálculo del Strain Index
/// </summary>
/// <param name="Job"></param>
/// <param name="nTasks"></param>
/// <returns></returns>
void __stdcall StrainIndex(modelJob* Job)
{
	switch ((*Job).IndexType)
	{
	case 0:
		for (int i = 0; i < ((*Job).JobTasks[0].nsubtasks); i++)
		{
			(*Job).JobTasks[0].subtasks[i].index = StrainIndexRSI(&(*Job).JobTasks[0].subtasks[i], &(*Job).JobTasks[0].nsubtasks);
		}
		break;
	case 1:
		for (int i = 0; i < (*Job).ntasks; i++)
		{
			(*Job).JobTasks[i].index = StrainIndexCOSI(&(*Job).JobTasks[i], &(*Job).JobTasks[i].nsubtasks);
		}
		break;
	case 2:
		(*Job).index = StrainIndexCUSI(Job, &(*Job).ntasks);
		break;
	}
	return;
}


/// <summary>
/// Función principal para el cálculo del Strain Index
/// </summary>
/// <param name="modelo[]">Datos para cada una de las tareas</param>
/// <param name="nIndex[]">Orden inicial de las tareas</param>
/// <param name="*nSize">Tamaño de los vectores anteriores</param>
/// <returns>RSI index</returns>
double __stdcall StrainIndexRSI(modelSubTask modelo[], const int * const nSize)
{
	/* 1er paso: calcular los índices de las tareas simples */
	int i;  /* Indice para el bucle for*/

	// double *pIndex = NULL;   /* Matriz con los índices individuales para ordenar*/
	// pIndex = (double*)malloc(*nSize * sizeof(double));
	// if (pIndex == NULL) return 0.0;

	for (i = 0; i < *nSize; i++)
	{
		modelo[i].factors.IM = FactorIM(&modelo[i].data.i);
		modelo[i].factors.EM = FactorEM(&modelo[i].data.e);
		modelo[i].factors.DM = FactorDM(&modelo[i].data.d);
		modelo[i].factors.PM = FactorPM(&modelo[i].data.p);
		modelo[i].factors.HM = FactorHM(&modelo[i].data.h);
		//modelo[i].factors.CM = FactorCM(&modelo[i].data.c, &modelo[i].data.v);

		modelo[i].index = CalculateIndex(&modelo[i].factors);
		//modelo[i].indexIF = modelo[i].index * modelo[i].factors.FM;

		// pIndex[i] = modelo[i].index;
	}

	return modelo[i - 1].index;
}

/// <summary>
/// Función principal para el cálculo del Strain Index
/// </summary>
/// <param name="modelo[]">Datos para cada una de las tareas</param>
/// <param name="nIndex[]">Orden inicial de las tareas</param>
/// <param name="*nSize">Tamaño de los vectores anteriores</param>
/// <returns>Valor del factor IM</returns>
double __stdcall StrainIndexCOSI(modelTask* Task, const int* const nSubT)
{
	double* indexRSI = NULL;
	//int* nIndex = NULL;
	int i;
	int size = 1;

	/* 1er paso: calcular los índices de las tareas simples */
	//int _nSubTasks = Tasks[_nTasks][0];
	indexRSI = (double*)malloc(*nSubT * sizeof(double));   /* Matriz con los índices individuales para ordenar */
	//pOrden = (int*)malloc(_nSubTasks * sizeof(int));   /* Matriz con los índices individuales para ordenar */
	//nIndex = new int[*nSize];

	if (indexRSI == NULL) return 0.0;

	for (i = 0; i < *nSubT; i++)
	{		
		//*(indexRSI + i * sizeof(double)) = StrainIndex(&AllSubTasks[Tasks.subtasks[i]], (int*)1);
		indexRSI[i] = StrainIndexRSI(&(*Task).subtasks[i], &size);
		(*Task).order[i] = i;
		//nIndex[i] = i + 1;
	}

	/* 2º paso: ordenar los índices */
	HeapSortIndex(indexRSI, (*Task).order, nSubT);

	/* 3er paso: calcular el sumatorio con los índices recalculados */
	(*Task).index = (*Task).subtasks[(*Task).order[*nSubT - 1]].index;
	(*Task).subtasks[(*Task).order[*nSubT - 1]].data.ea = (*Task).subtasks[(*Task).order[*nSubT - 1]].data.eb = (*Task).subtasks[(*Task).order[*nSubT - 1]].data.e;
	(*Task).h = (*Task).subtasks[(*Task).order[*nSubT - 1]].data.h;

	for (i = *nSubT - 2; i >= 0; i--)
	{
		(*Task).subtasks[(*Task).order[i]].data.ea = (*Task).subtasks[(*Task).order[i + 1]].data.ea + (*Task).subtasks[(*Task).order[i]].data.e;
		(*Task).subtasks[(*Task).order[i]].data.eb = (*Task).subtasks[(*Task).order[i + 1]].data.ea;
		(*Task).subtasks[(*Task).order[i]].factors.EMa = FactorEM(&(*Task).subtasks[(*Task).order[i]].data.ea);
		(*Task).subtasks[(*Task).order[i]].factors.EMb = FactorEM(&(*Task).subtasks[(*Task).order[i]].data.eb);
		(*Task).index += ((*Task).subtasks[(*Task).order[i]].index / (*Task).subtasks[(*Task).order[i]].factors.EM) * ((*Task).subtasks[(*Task).order[i]].factors.EMa - (*Task).subtasks[(*Task).order[i]].factors.EMb);

		(*Task).h += (*Task).subtasks[(*Task).order[i]].data.h;	// All subtasks should share the same h
	}
	(*Task).h /= *nSubT;	// In case they don't, the average h is computed
	(*Task).HM = FactorHM(&(*Task).h);

	/* Liberar la memoria reservada*/
	free(indexRSI);

	/* Finalizar*/
	return (*Task).index;
}

/// <summary>
/// Función principal para el cálculo del Strain Index
/// </summary>
/// <param name="Job"></param>
/// <param name="nTasks"></param>
/// <returns></returns>
double __stdcall StrainIndexCUSI(modelJob* Job, const int* const nTasks)
{
	double* indexCOSI = NULL;
	int i;

	/* 1er paso: calcular los índices COSI de las tareas */
	indexCOSI = (double*)malloc(*nTasks * sizeof(double));   /* Matriz con los índices COSI para ordenar */

	for (i = 0; i < *nTasks; i++)
	{
		indexCOSI[i] = StrainIndexCOSI(&(*Job).JobTasks[i], &(*Job).JobTasks[i].nsubtasks);
		(*Job).order[i] = i;
		//(*Job).JobTasks[i].h = (*Job).JobTasks[i].subtasks[0].data.h;
		//(*Job).JobTasks[i].HM = (*Job).JobTasks[i].subtasks[0].factors.HM;
	}
	
	/* 2º paso: ordenar los índices */
	HeapSortIndex(indexCOSI, (*Job).order, nTasks);

	/* 3er paso: calcular el sumatorio con los índices recalculados */
	(*Job).index = (*Job).JobTasks[(*Job).order[*nTasks - 1]].index;
	(*Job).JobTasks[(*Job).order[*nTasks - 1]].ha = (*Job).JobTasks[(*Job).order[*nTasks - 1]].hb = (*Job).JobTasks[(*Job).order[*nTasks - 1]].h;
	
	for (i = *nTasks - 2; i >= 0; i--)
	{
		(*Job).JobTasks[(*Job).order[i]].ha = (*Job).JobTasks[(*Job).order[i + 1]].ha + (*Job).JobTasks[(*Job).order[i]].h;
		(*Job).JobTasks[(*Job).order[i]].hb = (*Job).JobTasks[(*Job).order[i + 1]].ha;
		(*Job).JobTasks[(*Job).order[i]].HMa = FactorHM(&(*Job).JobTasks[(*Job).order[i]].ha);
		(*Job).JobTasks[(*Job).order[i]].HMb = FactorHM(&(*Job).JobTasks[(*Job).order[i]].hb);
		(*Job).index += ((*Job).JobTasks[(*Job).order[i]].index / (*Job).JobTasks[(*Job).order[i]].HM) * ((*Job).JobTasks[(*Job).order[i]].HMa - (*Job).JobTasks[(*Job).order[i]].HMb);
	}

	/* Liberar la memoria reservada*/
	free(indexCOSI);

	return (*Job).index;
}

/// <summary>
/// Función para la multiplicación de los factores
/// </summary>
/// <param name="*factores">Apuntador a los factores</param>
/// <returns>Resultado de la multiplicación</returns>
double CalculateIndex(const multipliersStrain * const factores)
{
	// Definición de variables
	double multiplicacion = 0.0;
	//double indice = 0.0;

	multiplicacion = factores->IM *
		factores->EM *
		factores->DM *
		factores->PM *
		factores->HM;

	return multiplicacion;
}

/// <summary>
/// Función para calcular el factor IM (intensidad del esfuerzo)
/// </summary>
/// <param name="x">Intensidad del esfuerzo en tanto por uno</param>
/// <returns>Valor del factor IM</returns>
double FactorIM(const double * const x)
{
	// Definición de variables
	double resultado = 0.0;

	// Calcular el factor
	if (*x >= 0 && *x <= 0.4)
		resultado = 30.0*pow(*x, 3) - 15.6*pow(*x, 2) + 13.0*(*x) + 0.4;
	else if (*x > 0.4 && *x <= 1.0)
		resultado = 36.0*pow(*x, 3) - 33.3*pow(*x, 2) + 24.77*(*x) - 1.86;

	// Devolver el resultado
	return resultado;
}

/// <summary>
/// Función para calcular el factor EM (esfuerzos por minuto)
/// </summary>
/// <param name="x">Frecuencia de esfuerzos por minuto</param>
/// <returns>Valor del factor EM</returns>
double FactorEM(const double * const x)
{
	// Definición de variables
	double resultado = 0.0;

	// Calcular el factor
	if (*x >= 0 && *x <= 90)
		resultado = 0.10 + 0.25*(*x);
	else if (*x > 0.4 && *x <= 1.0)
		resultado = 0.00334*pow(*x, 1.96);

	// Devolver el resultado
	return resultado;
}

/// <summary>
/// Función para calcular el factor DM (duración del esfuerzo)
/// </summary>
/// <param name="x">Duración del esfuerzo en segundos</param>
/// <returns>Valor del factor DM</returns>
double FactorDM(const double * const x)
{
	// Definición de variables
	double resultado = 0.0;

	// Calcular el factor
	if (*x >= 0 && *x <= 60.0)
		resultado = 0.45 + 0.31*(*x);
	else if (*x > 60)
		resultado = 19.17*log(*x) - 59.44;

	// Devolver el resultado
	return resultado;
}

/// <summary>
/// Función para calcular el factor PM (posición de la mano o muñeca)
/// </summary>
/// <param name="x">Grados de flexión (negativo) o extensión (positivo)</param>
/// <returns>Valor del factor PM</returns>
double FactorPM(const double * const x)
{
	// Definición de variables
	double resultado = 0.0;

	// Calcular el factor
	if (*x < 0)
		resultado = 1.2*exp(0.009*(-*x)) - 0.2;
	else if (*x >= 0 && *x <= 30)
		resultado = 1.0;
	else if (*x > 30)
		resultado = 1.0 + 0.00028*pow(*x - 30, 2);

	// Devolver el resultado
	return resultado;
}

/// <summary>
/// Función para calcular el factor HM (duración de la tarea)
/// </summary>
/// <param name="x">Tiempo en horas</param>
/// <returns>Valor del factor HM</returns>
double FactorHM(const double * const x)
{
	// Definición de variables
	double resultado = 0.0;

	if (*x > 0 && *x <= 0.05)
		resultado = 0.20;
	else if (*x > 0.05)
		resultado = 0.042*(*x) + 0.090*log(*x) + 0.477;

	// Devolver el resultado
	return resultado;
}

/// <summary>
/// Función para comprobar si la frecuencia y la duración están dentro del ciclo
/// </summary>
/// <param name="e">Frecuencia de esfuerzos por minuto</param>
/// <param name="d">Duración del esfuerzo en segundos</param>
/// <returns>1 si está OK, 0 si no se cumple la condición</returns>
int CheckCicle(const double * const e, const double * const d)
{
	// Definición de variables. Por defecto es "false"
	int resultado = 0;

	if ((*e)*(*d) / 60 <= 1)
		resultado = 1;

	return resultado;
}

/* Ordenación Heapsort con una tabla índice */
void HeapSortIndex(double ra[], int ind[], const int* const n)
/*Sorts an array ra[1..n] into ascending numerical order using the Heapsort algorithm. n is
input; ra is replaced on output by its sorted rearrangement.
This routine's been modified for sorting an index table.
Numerical Recipes in C, chapter 8.3, page 337*/
{
	int i, ir, j, l, iind;  /* Indices */
	double rra;         /* Variables temporales */
	int irra;

	if (*n < 0) return;  /* Added to check positive value*/
	if (*n < 2) return;
	l = (*n >> 1);
	ir = *n;

	/*The index l will be decremented from its initial value down to 0 during the “hiring” (heap
	creation) phase. Once it reaches 0, the index ir will be decremented from its initial value
	down to 0 during the “retirement-and-promotion” (heap selection) phase.*/
	for (;;)
	{
		if (l > 0)  /* Still in hiring phase.*/
		{
			iind = --l;
			rra = ra[ind[iind]];  /* Get the comparision value */
			irra = ind[iind];
		}
		else        /* In retirement-and-promotion phase.*/
		{
			if (--ir == 0) return;  /* Done with the last promotion.*/
			iind = ir;
			rra = ra[ind[ir]];    /* Get the comparision value */
			irra = ind[ir];     /* Clear a space at end of array.*/
			ind[ir] = ind[0];     /* Retire the top of the heap into it.*/
		}

		/*Whether in the hiring phase or promotion phase, we
		here set up to sift down element rra to its proper
		level.*/
		i = l;
		j = l + l + 1;

		while (j < ir)
		{
			if ((j + 1) < ir && ra[ind[j]] < ra[ind[j + 1]])
				j++; /*Compare to the better underling.*/
			if (rra < ra[ind[j]])    /* Demote rra.*/
			{
				ind[i] = ind[j];
				i = j;
				j <<= 1;
				j++;
			}
			else break;   /* Found rra’s level. Terminate the sift-down.*/
		}
		ind[i] = irra;  /* Put rra into its slot.*/
	}
}
