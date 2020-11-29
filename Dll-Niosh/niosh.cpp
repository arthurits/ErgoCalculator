// Dll-Niosh.cpp: define las funciones exportadas de la aplicación DLL.
//
#include "stdafx.h"
#include "niosh.h"


// https://docs.microsoft.com/en-us/cpp/build/walkthrough-creating-and-using-a-dynamic-link-library-cpp


////////////////////////////////////////////////////////////////////////
//                                                                    //
// Funciones para calcular la ecuación de NIOSH                       //
//                                                                    //
////////////////////////////////////////////////////////////////////////
extern "C" __declspec(dllexport) double __stdcall CalculateNIOSH(modelNIOSH modelo[], int nIndex[], const int* const nSize)
{
	/* 1er paso: calcular los índices de las tareas simples */
	int i;  /* Indice para el bucle for*/

	double* pIndex = NULL;   /* Matriz con los índices individuales para ordenar*/
	pIndex = (double*)malloc(*nSize * sizeof(double));
	if (pIndex == NULL) return 0.0;

	for (i = 0; i < *nSize; i++)
	{
		if (modelo[i].factors.LC == 0)	modelo[i].factors.LC = 23.0;
		modelo[i].factors.HM = FactorHM(&modelo[i].data.h);
		modelo[i].factors.VM = FactorVM(&modelo[i].data.v);
		modelo[i].factors.DM = FactorDM(&modelo[i].data.d);
		modelo[i].factors.AM = FactorAM(&modelo[i].data.a);
		modelo[i].factors.FM = FactorFM(&modelo[i].data.f, &modelo[i].data.v, &modelo[i].data.td);
		modelo[i].factors.CM = FactorCM(&modelo[i].data.c, &modelo[i].data.v);

		modelo[i].index = NIOSHindex(&modelo[i].data.weight, &modelo[i].factors);
		modelo[i].indexIF = modelo[i].index * modelo[i].factors.FM;

		pIndex[i] = modelo[i].index;
	}

	/* 2º paso: ordenar los índices */
	HeapSortIndex(pIndex, nIndex, nSize);

	/* 3er paso: calcular el sumatorio con los índices recalculados */
	double resultado = modelo[nIndex[*nSize - 1]].index;
	modelo[nIndex[*nSize - 1]].data.fa = modelo[nIndex[*nSize - 1]].data.fb = modelo[nIndex[*nSize - 1]].data.f;

	for (i = *nSize - 2; i >= 0; i--)
	{
		modelo[nIndex[i]].data.fa = modelo[nIndex[i + 1]].data.fa + modelo[nIndex[i]].data.f;
		modelo[nIndex[i]].data.fb = modelo[nIndex[i + 1]].data.fa;
		modelo[nIndex[i]].factors.FMa = FactorFM(&modelo[nIndex[i]].data.fa, &modelo[nIndex[i]].data.v, &modelo[nIndex[i]].data.td);
		modelo[nIndex[i]].factors.FMb = FactorFM(&modelo[nIndex[i]].data.fb, &modelo[nIndex[i]].data.v, &modelo[nIndex[i]].data.td);
		resultado += modelo[nIndex[i]].indexIF * (1 / modelo[nIndex[i]].factors.FMa - 1 / modelo[nIndex[i]].factors.FMb);
	}

	/* Liberar la memoria reservada*/
	free(pIndex);

	/* Finalizar*/
	return resultado;
}

double NIOSHindex(const double* const peso, const multipliersNIOSH* const factores)
{
	double multiplicacion = 0.0;
	double indice = 0.0;

	multiplicacion = factores->LC *
		factores->HM *
		factores->VM *
		factores->DM *
		factores->AM *
		factores->FM *
		factores->CM;

	if (multiplicacion == 0)    // División entre 0
		indice = 0;
	else
		indice = *peso / multiplicacion;

	return indice;
}

double FactorHM(const double* const x)
{
	// Definición de variables
	double resultado = 0.0;

	// Calcular el factor
	if (*x < 25)
		resultado = 1.0;
	else if (*x > 63)
		resultado = 0.0;
	else
		resultado = 25 / *x;

	// Devolver el resultado
	return resultado;
}

double FactorVM(const double* const x)
{
	// Definición de variables
	double resultado = 0.0;

	// Calcular el factor
	if (*x > 175)
		resultado = 0.0;
	else
		resultado = 1 - 0.003 * fabs(*x - 75);

	// Devolver el resultado
	return resultado;
}

double FactorDM(const double* const x)
{
	// Definición de variables
	double resultado = 0.0;

	// Calcular el factor
	if (*x < 25)
		resultado = 1.0;
	else if (*x > 175)
		resultado = 0.0;
	else
		resultado = 0.82 + 4.5 / (*x);

	// Devolver el resultado
	return resultado;
}

double FactorAM(const double* const x)
{
	// Definición de variables
	double resultado = 0.0;

	if (*x > 135)
		resultado = 0.0;
	else
		resultado = 1 - 0.0032 * (*x);

	// Devolver el resultado
	return resultado;
}

double FactorFM(const double* const frecuencia, const double* const v, const double* const td)
{
	// Definición de variables
	int nIndice = 0;
	int nColumna = 0;
	int nLongitud = 18;
	double resultado = 0.0;
	double frecuen[18] = { 0.2, 0.5, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
	double fm[6][18] = {
		{ 1, 0.97, 0.94, 0.91, 0.88, 0.84, 0.8, 0.75, 0.7, 0.6, 0.52, 0.45, 0.41, 0.37, 0, 0, 0, 0 },
		{ 1, 0.97, 0.94, 0.91, 0.88, 0.84, 0.8, 0.75, 0.7, 0.6, 0.52, 0.45, 0.41, 0.37, 0.34, 0.31, 0.28, 0 },
		{ 0.95, 0.92, 0.88, 0.84, 0.79, 0.72, 0.6, 0.5, 0.42, 0.35, 0.3, 0.26, 0, 0, 0, 0, 0, 0 },
		{ 0.95, 0.92, 0.88, 0.84, 0.79, 0.72, 0.6, 0.5, 0.42, 0.35, 0.3, 0.26, 0.23, 0.21, 0, 0, 0, 0 },
		{ 0.85, 0.81, 0.75, 0.65, 0.55, 0.45, 0.35, 0.27, 0.22, 0.18, 0, 0, 0, 0, 0, 0, 0, 0 },
		{ 0.85, 0.81, 0.75, 0.65, 0.55, 0.45, 0.35, 0.27, 0.22, 0.18, 0.15, 0.13, 0, 0, 0, 0, 0, 0 }
	};

	if (*td <= 1.0)
	{
		if (*v < 75)
			nColumna = 0;
		else
			nColumna = 1;
	}
	else if (*td <= 2.0)
	{
		if (*v < 75)
			nColumna = 2;
		else
			nColumna = 3;
	}
	else if (*td <= 8.0)
	{
		if (*v < 75)
			nColumna = 4;
		else
			nColumna = 5;
	}

	// Devuelve un valor entre -1 (fuera de rango) y nLongitud-1
	nIndice = locate(frecuen, nLongitud, *frecuencia);

	//if (nIndice == -1 || nIndice == (nLongitud - 2))
	//    return fm[nColumna][++nIndice];

	if (nIndice == -1)
		return fm[nColumna][++nIndice];

	if (nIndice >= (nLongitud - 2))
		return fm[nColumna][nIndice];


	resultado = InterpolacionLineal(frecuencia,
		&frecuen[nIndice],
		&frecuen[nIndice + 1],
		&fm[nColumna][nIndice],
		&fm[nColumna][nIndice + 1]);

	// Devolver el resultado
	return resultado;
}

double FactorCM(const int* const agarre, const double* const v)
{
	// Definición de variables
	double resultado = 0.0;

	switch (*agarre)
	{
	case 1:     // Agarre bueno
		resultado = 1.0;
		break;
	case 2:     // Agarre regular
		if (*v < 75)
			resultado = 0.95;
		else
			resultado = 1.0;
		break;
	case 3:     // Agarre malo
		resultado = 0.90;
		break;
	default:
		resultado = 0.0;
	}

	// Devolver el resultado
	return resultado;
}




	////////////////////////////////////////////////////////////////////////
	//                                                                    //
	// Funciones internas                                                 //
	//                                                                    //
	////////////////////////////////////////////////////////////////////////

	/* Función interna que realiza una interpolación lineal*/
double InterpolacionLineal(const double* const x, const double* const x1, const double* const x2, const double* const y1, const double* const y2)
{
	double resultado = 0.0;

	if (*x == *x1)        // Si el valor pedido coincide con el límite inferior
		resultado = *y1;
	else if (*x == *x2)   // Si el valor pedido coincide con el límite superior
		resultado = *y2;
	else                // En cualquier otro caso, hacer la interpolación lineal
		resultado = (*x - *x1) * (*y2 - *y1) / (*x2 - *x1) + *y1;

	return resultado;
}

	/* Función interna que localiza un valor en una matriz*/
int locate(double xx[], int n, double x)
/*Given an array xx[1..n], and given a value x, returns a value j such that x is between xx[j]
and xx[j+1]. xx must be monotonic, either increasing or decreasing. j=0 or j=n is returned
to indicate that x is out of range.*/
{
	// Definición de variables
	int ju, jm, jl, j;
	int ascnd;

	// Inicialización de variables
	j = 0;
	jl = 0;         //Initialize lower
	ju = n - 1;       //and upper limits.
	ascnd = (xx[n - 1] >= xx[0]);

	// Comprobar si el valor pedido está fuera de la matriz
	// Cuidado con el orden ascendente o descendente
	if (x <= xx[0] && x <= xx[n - 1])
		return ascnd == 1 ? -1 : n - 1;
	if (x >= xx[n - 1] && x >= xx[0])
		return ascnd == 1 ? n - 1 : -1;

	// Método de la bisección
	while (ju - jl > 1)
	{                       //If we are not yet done,
		jm = (ju + jl) >> 1;    //compute a midpoint,
		if (x >= xx[jm] == ascnd)
			jl = jm;          //and replace either the lower limit
		else
			ju = jm;          //or the upper limit, as appropriate.
	}                       //Repeat until the test condition is satisfied.

							//if (x <= xx[1]) j=0;   //Then set the output
							//else if(x >= xx[n]) j=n-1;
							//else j=jl;

	return jl;
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

