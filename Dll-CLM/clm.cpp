// Dll-CLM.cpp: define las funciones exportadas de la aplicación DLL.
//
#pragma once

#include "stdafx.h"

#ifdef Clm_EXPORTS
#define Clm_API __declspec(dllexport)
#else
#define Clm_API __declspec(dllimport)
#endif

/* Definición de tipos */
	typedef struct dataCLM
	{
		int gender;     // 1: masculino    2: femenino
		double weight;
		double h;
		double v;
		double d;
		double f;
		double td;
		double t;
		int c;
		double hs;
		double ag;
		double bw;
	} dataCLM;

	typedef struct multipliersCLM
	{
		double fH;
		double fV;
		double fD;
		double fF;
		double fTD;
		double fT;
		double fC;
		double fHS;
		double fAG;
		double fBW;
	} multipliersCLM;

	typedef struct modelCLM
	{
		dataCLM data;
		multipliersCLM factors;
		double indexLSI;
	} modelCLM;


/* Prototipos de función a exportar*/
	extern "C" Clm_API void __stdcall CalculateLSI(modelCLM[], const int * const);

/* Prototipos de funciones internas*/
	double FactorH(const double * const, const int * const);
	double FactorV(const double * const, const int * const);
	double FactorD(const double * const, const int * const);
	double FactorF(const double * const, const int * const);
	double FactorTD(const double * const);
	double FactorT(const double * const);
	double FactorC(const int * const);
	double FactorHS(const double * const);
	double FactorAG(const double * const, const int * const);
	double FactorBW(const double * const, const int * const);
	double Porcentaje(const double * const, const int * const);
	double LSIindex(const int * const, const double * const, const multipliersCLM * const);

	double InterpolacionLineal(const double * const, const double * const, const double * const, const double * const, const double * const);
	int locate(double[], int, double);


/*Función que calcula los valores correspondienetes a los parámetros
del modelo CLM
Es la única función que se exporta desde la DLL*/
void __stdcall CalculateLSI(modelCLM modelo[], const int * const nSize)
{
	int i;  /* Indice para el bucle for*/

			/* Realizar los cálculos para cada tarea */
	for (i = 0; i < *nSize; i++)
	{
		modelo[i].factors.fH = FactorH(&modelo[i].data.h, &modelo[i].data.gender);
		modelo[i].factors.fV = FactorV(&modelo[i].data.v, &modelo[i].data.gender);
		modelo[i].factors.fD = FactorD(&modelo[i].data.d, &modelo[i].data.gender);
		modelo[i].factors.fF = FactorF(&modelo[i].data.f, &modelo[i].data.gender);
		modelo[i].factors.fTD = FactorTD(&modelo[i].data.td);
		modelo[i].factors.fT = FactorT(&modelo[i].data.t);
		modelo[i].factors.fC = FactorC(&modelo[i].data.c);
		modelo[i].factors.fHS = FactorHS(&modelo[i].data.hs);
		modelo[i].factors.fAG = FactorAG(&modelo[i].data.ag, &modelo[i].data.gender);
		modelo[i].factors.fBW = FactorBW(&modelo[i].data.bw, &modelo[i].data.gender);

		modelo[i].indexLSI = LSIindex(&modelo[i].data.gender, &modelo[i].data.weight, &modelo[i].factors);
	}

	return;
}

/* Función interna para calcular el valor del factor H del modelo CLM*/
double FactorH(const double * const x, const int * const nSexo)
{
	// Definición de variables
	double resultado = 0.0;
	double fFactor[27] = { 25,30,35,40,45,50,55,60,65,
		1.00,0.87,0.79,0.73,0.70,0.68,0.63,0.52,0.43,
		1.00,0.84,0.73,0.66,0.64,0.65,0.61,0.50,0.41 };

	int nIndice = 0;
	int nLongitud = 9;

	nIndice = locate(fFactor, nLongitud, *x);

	// Si el valor pedido está fuera del rango de valore de la matriz
	// entonces se devuelve el valor del extremo
	if (nIndice == -1 || nIndice == (nLongitud - 1))
	{
		if (nIndice == -1) nIndice++;
		return fFactor[nIndice + (nLongitud * (*nSexo))];
	}

	// Hacer una interpolación lineal
	resultado = InterpolacionLineal(x,
		&fFactor[nIndice],
		&fFactor[nIndice + 1],
		&fFactor[nIndice + (nLongitud * (*nSexo))],
		&fFactor[nIndice + 1 + (nLongitud * (*nSexo))]);

	return resultado;

}

/* Función interna para calcular el valor del factor V del modelo CLM*/
double FactorV(const double * const x, const int * const nSexo)
{
	// Definición de variables
	double resultado = 0.0;
	double fFactor[108] = { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145, 150, 155, 160, 165, 170, 175,
		0.62, 0.64, 0.67, 0.69, 0.72, 0.75, 0.77, 0.80, 0.82, 0.85, 0.87, 0.90, 0.92, 0.95, 0.98, 1.00, 0.99, 0.98, 0.97, 0.96, 0.94, 0.93, 0.92, 0.91, 0.89, 0.88, 0.87, 0.84, 0.83, 0.82, 0.80, 0.79, 0.77, 0.76, 0.73, 0.71,
		0.74, 0.76, 0.77, 0.79, 0.81, 0.83, 0.84, 0.86, 0.88, 0.90, 0.91, 0.93, 0.95, 0.97, 0.98, 1.00, 0.99, 0.98, 0.96, 0.95, 0.93, 0.90, 0.88, 0.86, 0.83, 0.81, 0.78, 0.76, 0.72, 0.70, 0.67, 0.65, 0.62, 0.60, 0.56, 0.54 };

	int nIndice = 0;
	int nLongitud = 36;

	nIndice = locate(fFactor, nLongitud, *x);

	if (nIndice == -1 || nIndice == (nLongitud - 1))
	{
		if (nIndice == -1) nIndice++;
		return fFactor[nIndice + (nLongitud * (*nSexo))];
	}

	resultado = InterpolacionLineal(x,
		&fFactor[nIndice],
		&fFactor[nIndice + 1],
		&fFactor[nIndice + (nLongitud * (*nSexo))],
		&fFactor[nIndice + 1 + (nLongitud * (*nSexo))]);

	return resultado;

}

/* Función interna para calcular el valor del factor D del modelo CLM*/
double FactorD(const double * const x, const int * const nSexo)
{
	// Definición de variables
	double resultado = 0.0;
	double fFactor[90] = { 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145, 150, 155, 160, 165, 170,
		1, 0.97, 0.94, 0.91, 0.88, 0.86, 0.84, 0.83, 0.81, 0.80, 0.79, 0.78, 0.77, 0.76, 0.75, 0.73, 0.72, 0.71, 0.69, 0.68, 0.67, 0.66, 0.64, 0.63, 0.61, 0.59, 0.57, 0.55, 0.52, 0.49,
		1, 0.99, 0.97, 0.96, 0.95, 0.94, 0.92, 0.89, 0.87, 0.84, 0.82, 0.80, 0.79, 0.78, 0.77, 0.77, 0.76, 0.75, 0.75, 0.75, 0.74, 0.74, 0.74, 0.74, 0.74, 0.74, 0.73, 0.73, 0.73, 0.73 };

	int nIndice = 0;
	int nLongitud = 30;

	nIndice = locate(fFactor, nLongitud, *x);

	if (nIndice == -1 || nIndice == (nLongitud - 1))
	{
		if (nIndice == -1) nIndice++;
		return fFactor[nIndice + (nLongitud * (*nSexo))];
	}

	resultado = InterpolacionLineal(x,
		&fFactor[nIndice],
		&fFactor[nIndice + 1],
		&fFactor[nIndice + (nLongitud * (*nSexo))],
		&fFactor[nIndice + 1 + (nLongitud * (*nSexo))]);

	return resultado;

}

/* Función interna para calcular el valor del factor F del modelo CLM*/
double FactorF(const double * const x, const int * const nSexo)
{
	// Definición de variables
	double resultado = 0.0;
	double fFactor[54] = { 0.2, 0.5, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
		1, 0.99, 0.95, 0.89, 0.83, 0.78, 0.73, 0.69, 0.65, 0.62, 0.59, 0.56, 0.54, 0.52, 0.50, 0.49, 0.47, 0.46,
		1, 0.99, 0.91, 0.87, 0.84, 0.8 , 0.77, 0.74, 0.7 , 0.68, 0.66, 0.65, 0.64, 0.63, 0.63, 0.62, 0.61, 0.6 };

	int nIndice = 0;
	int nLongitud = 18;

	nIndice = locate(fFactor, nLongitud, *x);

	if (nIndice == -1 || nIndice == (nLongitud - 1))
	{
		if (nIndice == -1) nIndice++;
		return fFactor[nIndice + (nLongitud * (*nSexo))];
	}

	resultado = InterpolacionLineal(x,
		&fFactor[nIndice],
		&fFactor[nIndice + 1],
		&fFactor[nIndice + (nLongitud * (*nSexo))],
		&fFactor[nIndice + 1 + (nLongitud * (*nSexo))]);

	return resultado;
}

/* Función interna para calcular el valor del factor TD del modelo CLM*/
double FactorTD(const double * const x)
{
	// Definición de variables
	double resultado = 0.0;
	double fFactor[18] = { 0, 1, 2, 3, 4, 5, 6, 7, 8,
		1.00, 1.00, 0.76, 0.66, 0.60, 0.57, 0.54, 0.50, 0.45 };

	int nIndice = 0;
	int nLongitud = 9;

	nIndice = locate(fFactor, nLongitud, *x);

	if (nIndice == -1 || nIndice == (nLongitud - 1))
	{
		if (nIndice == -1) nIndice++;
		return fFactor[nIndice + nLongitud];
	}

	resultado = InterpolacionLineal(x,
		&fFactor[nIndice],
		&fFactor[nIndice + 1],
		&fFactor[nIndice + nLongitud],
		&fFactor[nIndice + 1 + nLongitud]);

	return resultado;
}

/* Función interna para calcular el valor del factor T del modelo CLM*/
double FactorT(const double * const x)
{
	// Definición de variables
	double resultado = 0.0;
	double fFactor[28] = { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130,
		1.00, 0.98, 0.95, 0.93, 0.90, 0.88, 0.86, 0.83, 0.81, 0.78, 0.76, 0.74, 0.71, 0.69 };

	int nIndice = 0;
	int nLongitud = 14;

	nIndice = locate(fFactor, nLongitud, *x);

	if (nIndice == -1 || nIndice == (nLongitud - 1))
	{
		if (nIndice == -1) nIndice++;
		return fFactor[nIndice + nLongitud];
	}

	resultado = InterpolacionLineal(x,
		&fFactor[nIndice],
		&fFactor[nIndice + 1],
		&fFactor[nIndice + nLongitud],
		&fFactor[nIndice + 1 + nLongitud]);

	return resultado;
}

/* Función interna para calcular el valor del factor C del modelo CLM*/
double FactorC(const int * const x)
{
	// Definición de variables
	double resultado = 0.0;

	switch (*x)
	{
	case 1:
		resultado = 1.0;
		break;
	case 2:
		resultado = 0.925;
		break;
	case 3:
		resultado = 0.850;
		break;
	default:
		resultado = 0.0;
	}

	return resultado;
}

/* Función interna para calcular el valor del factor HS del modelo CLM*/
double FactorHS(const double * const x)
{
	// Definición de variables
	double resultado = 0.0;
	double fFactor[44] = { 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
		1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 0.98, 0.95, 0.93, 0.90, 0.88, 0.86, 0.83, 0.81, 0.78, 0.76, 0.74, 0.71, 0.69 };

	int nIndice = 0;
	int nLongitud = 22;

	nIndice = locate(fFactor, nLongitud, *x);

	if (nIndice == -1 || nIndice == (nLongitud - 1))
	{
		if (nIndice == -1) nIndice++;
		return fFactor[nIndice + nLongitud];
	}

	resultado = InterpolacionLineal(x,
		&fFactor[nIndice],
		&fFactor[nIndice + 1],
		&fFactor[nIndice + nLongitud],
		&fFactor[nIndice + 1 + nLongitud]);

	return resultado;
}

/* Función interna para calcular el valor del factor AG del modelo CLM*/
double FactorAG(const double * const x, const int * const nSexo)
{
	// Definición de variables
	double resultado = 0.0;
	double fFactor[27] = { 20, 25, 30, 35, 40, 45, 50, 55, 60,
		1, 0.91, 0.88, 0.88, 0.86, 0.78, 0.69, 0.62, 0.59,
		1, 0.95, 0.90, 0.87, 0.82, 0.79, 0.72, 0.64, 0.49 };

	int nIndice = 0;
	int nLongitud = 9;

	nIndice = locate(fFactor, nLongitud, *x);

	if (nIndice == -1 || nIndice == (nLongitud - 1))
	{
		if (nIndice == -1) nIndice++;
		return fFactor[nIndice + (nLongitud * (*nSexo))];
	}

	resultado = InterpolacionLineal(x,
		&fFactor[nIndice],
		&fFactor[nIndice + 1],
		&fFactor[nIndice + (nLongitud * (*nSexo))],
		&fFactor[nIndice + 1 + (nLongitud * (*nSexo))]);

	return resultado;
}

/* Función interna para calcular el valor del factor BW del modelo CLM*/
double FactorBW(const double * const x, const int * const nSexo)
{
	// Definición de variables
	double resultado = 0.0;
	double fFactor[39] = { 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100,
		0.70, 0.70, 0.70, 0.70, 0.70, 0.80, 1.00, 1.20, 1.30, 1.41, 1.45, 1.45, 1.45,
		1.00, 1.00, 1.00, 1.00, 1.00, 1.20, 1.40, 1.68, 1.85, 1.98, 2.05, 2.05, 2.05 };

	int nIndice = 0;
	int nLongitud = 13;

	nIndice = locate(fFactor, nLongitud, *x);

	if (nIndice == -1 || nIndice == (nLongitud - 1))
	{
		if (nIndice == -1) nIndice++;
		return fFactor[nIndice + (nLongitud * (*nSexo))];
	}

	resultado = InterpolacionLineal(x,
		&fFactor[nIndice],
		&fFactor[nIndice + 1],
		&fFactor[nIndice + (nLongitud * (*nSexo))],
		&fFactor[nIndice + 1 + (nLongitud * (*nSexo))]);

	return resultado;
}

/* Función interna para calcular el porcentaje de la población protegida del modelo CLM*/
double Porcentaje(const double * const x, const int * const nSexo)
{
	// Definición de variables
	double resultado = 0.0;
	double fFactor[19] = { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95 };
	double fFactorH[19] = { 39.05, 37.54, 36.04, 34.53, 33.02, 31.51, 30.00, 28.50, 26.99, 25.47, 23.96, 22.45, 20.94, 19.44, 17.93, 16.42, 14.91, 13.41, 11.89 };
	double fFactorM[19] = { 22.75, 22.15, 21.54, 20.94, 20.34, 19.73, 19.13, 18.53, 17.92, 17.32, 16.71, 16.11, 15.51, 14.90, 14.30, 13.70, 13.09, 12.49, 11.89 };

	int nIndice = 0;
	int nLongitud = 19;

	if (*nSexo == 1) //Sexo masculino
	{
		nIndice = locate(fFactorH, nLongitud, *x);

		if (nIndice == -1 || nIndice == (nLongitud - 1))
		{
			if (nIndice == -1) nIndice++;
			return fFactor[nIndice];
		}

		resultado = InterpolacionLineal(x,
			&fFactorH[nIndice],
			&fFactorH[nIndice + 1],
			&fFactor[nIndice],
			&fFactor[nIndice + 1]);
	}
	else if (*nSexo == 2)    //Sexo femenino
	{
		nIndice = locate(fFactorM, nLongitud, *x);

		if (nIndice == -1 || nIndice == (nLongitud - 1))
		{
			if (nIndice == -1) nIndice++;
			return fFactor[nIndice];
		}

		resultado = InterpolacionLineal(x,
			&fFactorM[nIndice],
			&fFactorM[nIndice + 1],
			&fFactor[nIndice],
			&fFactor[nIndice + 1]);
	}

	return resultado;
}

/* Función interna para calcular el valor del índice LSI del modelo CLM*/
double LSIindex(const int * const sexo, const double * const peso, const multipliersCLM * const factores)
{
	double pesoBase = 0.0;
	double multiplicacion = 0.0;
	double porcentaje = 0.0;
	double indice = 0.0;

	multiplicacion = factores->fH *
		factores->fV *
		factores->fD *
		factores->fF *
		factores->fTD *
		factores->fT *
		factores->fC *
		factores->fHS *
		factores->fAG *
		factores->fBW;

	if (multiplicacion == 0)    // División entre 0
		indice = 0;
	else
	{
		pesoBase = *peso / multiplicacion;
		porcentaje = Porcentaje(&pesoBase, sexo);
		indice = 10 - porcentaje / 10;
	}

	return indice;
}


////////////////////////////////////////////////////////////////////////
//                                                                    //
// Funciones internas                                                 //
//                                                                    //
////////////////////////////////////////////////////////////////////////

/* Función interna que realiza una interpolación lineal*/
double InterpolacionLineal(const double * const x, const double * const x1, const double * const x2, const double * const y1, const double * const y2)
{
	double resultado = 0.0;

	if (*x == *x1)        // Si el valor pedido coincide con el límite inferior
		resultado = *y1;
	else if (*x == *x2)   // Si el valor pedido coincide con el límite superior
		resultado = *y2;
	else                // En cualquier otro caso, hacer la interpolación lineal
		resultado = (*x - *x1)*(*y2 - *y1) / (*x2 - *x1) + *y1;

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
