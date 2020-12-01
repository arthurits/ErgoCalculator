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
	//double weight;  // Peso que se manipula
	double i;       // Intensidad del esfuerzo
	double e;       // Esfuerzos por minuto
	double d;       // Duración del esfuerzo
	double p;       // Posición de la mano
	double h;       // Duración de la tarea
	double ea;       // Esfuerzos acumulados a
	double eb;       // Esfuerzos acumulados b

} dataStrain;

typedef struct multipliersStrain
{
	//double LC;   // Constante de carga
	double IM;   // Factor de intensidad del esfuerzo [0, 1]
	double EM;   // Factor de esfuerzos por minuto
	double DM;   // Factor de duración del esfuerzo
	double PM;   // Factor de posición de la mano
	double HM;   // Factor de duración de la tarea
	double EMa;  // Factor de esfuerzos acumulados a
	double EMb;  // Factor de esfuerzos acumulados a
} multipliersStrain;

typedef struct modelSubTask
{
	dataStrain data;
	multipliersStrain factors;
	//double indexIF;
	double index;
} modelStrain;

typedef struct modelTask
{
	int* subtasks;	// Set of subtasks in the job
	double ha;		// Duración de la tarea acumulada a
	double hb;		// Duración de la tarea acumulada b
	double HMa;		// Factor de duración de la tarea acumulada a
	double HMb;		// Factor de duración de la tarea acumulada b
	double index;	// The COSI index for this task
} modelTask;

/* Prototipos de función a exportar*/
extern "C" StrainIndex_API double __stdcall StrainIndex(modelSubTask[], const int * const);
extern "C" StrainIndex_API double __stdcall StrainIndexCOSI(modelSubTask[], modelTask, const int* , const int);
extern "C" StrainIndex_API double __stdcall StrainIndexCUSI(modelSubTask[], const int* const nSubTasks);

/* Prototipos de funciones internas*/
double CalculateIndex(const multipliersStrain * const);
double FactorIM(const double * const);
double FactorEM(const double * const);
double FactorDM(const double * const);
double FactorPM(const double * const);
double FactorHM(const double * const);
int CheckCicle(const double * const, const double * const);

//double InterpolacionLineal(const double * const, const double * const, const double * const, const double * const, const double * const);
//int locate(double[], int, double);
//void HeapSortIndex(double[], int[], const int * const);


/// <summary>
/// Función principal para el cálculo del Strain Index
/// </summary>
/// <param name="modelo[]">Datos para cada una de las tareas</param>
/// <param name="nIndex[]">Orden inicial de las tareas</param>
/// <param name="*nSize">Tamaño de los vectores anteriores</param>
/// <returns>RSI index</returns>
double __stdcall StrainIndex(modelSubTask modelo[], const int * const nSize)
{
	/* 1er paso: calcular los índices de las tareas simples */
	int i;  /* Indice para el bucle for*/

	double *pIndex = NULL;   /* Matriz con los índices individuales para ordenar*/
	pIndex = (double *)malloc(*nSize * sizeof(double));
	if (pIndex == NULL) return 0.0;

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

		pIndex[i] = modelo[i].index;
	}

	return pIndex[i-1];
}

/// <summary>
/// Función principal para el cálculo del Strain Index
/// </summary>
/// <param name="modelo[]">Datos para cada una de las tareas</param>
/// <param name="nIndex[]">Orden inicial de las tareas</param>
/// <param name="*nSize">Tamaño de los vectores anteriores</param>
/// <returns>Valor del factor IM</returns>
double __stdcall StrainIndexCOSI(modelSubTask AllSubTasks[], modelTask Task, const int* Orden, const int nSize)
{
	double* indexRSI = NULL;
	int* nIndex = NULL;
	int i;
	// Iterate through all the Tasks
	//for (int _nTasks = 0; _nTasks < nSize; _nTasks++)
	//{
		/* 1er paso: calcular los índices de las tareas simples */
		//int _nSubTasks = Tasks[_nTasks][0];
		indexRSI = (double*)malloc(nSize * sizeof(double));   /* Matriz con los índices individuales para ordenar*/
		//pOrden = (int*)malloc(_nSubTasks * sizeof(int));   /* Matriz con los índices individuales para ordenar*/
		nIndex = new int[nSize];

		if (indexRSI == NULL) return 0.0;

		for (int i = 0; i < nSize; i++)
		{
			
			//*(indexRSI + i * sizeof(double)) = StrainIndex(&AllSubTasks[Tasks.subtasks[i]], (int*)1);
			indexRSI[i] = StrainIndex(&AllSubTasks[Task.subtasks[i]], (int*)1);
			nIndex[i] = i + 1;
		}

		/* 2º paso: ordenar los índices */
		HeapSortIndex(indexRSI, nIndex, &nSize);

		/* 3er paso: calcular el sumatorio con los índices recalculados */
		Task.index = AllSubTasks[nIndex[nSize - 1]].index;
		AllSubTasks[nIndex[nSize - 1]].data.ea = AllSubTasks[nIndex[nSize - 1]].data.eb = AllSubTasks[nIndex[nSize - 1]].data.e;

		for (i = nSize - 2; i >= 0; i--)
		{
			AllSubTasks[nIndex[i]].data.ea = AllSubTasks[nIndex[i + 1]].data.ea + AllSubTasks[nIndex[i]].data.e;
			AllSubTasks[nIndex[i]].data.eb = AllSubTasks[nIndex[i + 1]].data.ea;
			AllSubTasks[nIndex[i]].factors.EMa = FactorEM(&AllSubTasks[nIndex[i]].data.fa, &AllSubTasks[nIndex[i]].data.v, &AllSubTasks[nIndex[i]].data.td);
			AllSubTasks[nIndex[i]].factors.EMb = FactorEM(&AllSubTasks[nIndex[i]].data.fb, &AllSubTasks[nIndex[i]].data.v, &AllSubTasks[nIndex[i]].data.td);
			Task.index += AllSubTasks[nIndex[i]].indexIF * (1 / modelo[nIndex[i]].factors.FMa - 1 / modelo[nIndex[i]].factors.FMb);
		}

		/* Liberar la memoria reservada*/
		free(indexRSI);
		//free(pOrden);
	//}

	/* Finalizar*/
	return 0;

}

/// <summary>
/// Función principal para el cálculo del Strain Index
/// </summary>
/// <param name="modelo[]">Datos para cada una de las tareas</param>
/// <param name="nIndex[]">Orden inicial de las tareas</param>
/// <param name="*nSize">Tamaño de los vectores anteriores</param>
/// <returns>Valor del factor IM</returns>
double __stdcall StrainIndexCUSI(modelStrain SubTasks[], const int* const nSubTasks)
{

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
