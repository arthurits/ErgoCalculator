// WRmodel.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"

#pragma once
#ifdef WR_EXPORTS
#define WRmodel_API __declspec(dllexport)
#else
#define WRmodel_API __declspec(dllimport)
#endif

// Definición de tipos
	struct datos
	{
		double dMHT;
		double dPaso;
		int nCiclos;
		int nPuntos;
	};

// Prototipo de las funciones
	extern "C" WRmodel_API double __stdcall Sjogaard(double);
	extern "C" WRmodel_API double** __stdcall Curva(double**, int, struct datos*);
	extern "C" WRmodel_API void __stdcall FreeMemory(void*);
	/*extern "C" __declspec(dllexport) int __stdcall DataSize (int);*/
	int roundToInt(double);


/* Calcula el MHT mediante la ecuación de Sjogaard*/
	double __stdcall Sjogaard(double dMVC)
	{
		// Definición de variables
		double resultado = 0.0;
		resultado = 5710.0 / pow(dMVC, 2.14);

		// Devolver el resultado
		return resultado;
	}

// Calcula la matriz de datos
	double** __stdcall Curva(double** arrayTiempos, int arrayTamano, struct datos *ptrData)
	{
		// Definición de variables
		// Es más práctico crear una "jagged array" para extraer las columnas y pasarlas al gráfico
		double *arrayResultado = NULL;
		double **arrayPrueba = NULL;
		double fOffsetX = 0.0;
		double fOffsetY = 100.0;
		int nPoints = ptrData->nPuntos;

		//Contadores para los bucles
		int nContador = 0;
		int i = 0;
		int j = 0;
		int k = 0;

		// Cálculo del número de puntos de la curva
		/*nPoints = ptrData->nCiclos * arrayTamano + 1;
		for (i = 0; i < arrayTamano; i++)
		nPoints += ptrData->nCiclos * round(arrayTiempos[1][i]/ptrData->dPaso);*/

		//nPoints *= 2;

		// Reservar memoria para la matriz de resultados
		arrayResultado = (double*)malloc(2.0 * nPoints * sizeof(double));
		if (arrayResultado == NULL)
			return NULL;
		
		arrayPrueba = (double**)malloc(2 * sizeof(double *));
		if (arrayPrueba == NULL)
			return NULL;
		
		// Inicializar la "jagged array" para que apunte a la matriz de resultados
		*(arrayPrueba + 0) = arrayResultado;			// Abscisas
		*(arrayPrueba + 1) = arrayResultado + nPoints;	// Ordenadas

		// Inicialización de la matriz de resultados
		*(arrayResultado) = fOffsetX;			// Abscisas
		*(arrayResultado + nPoints) = fOffsetY;	// Ordenadas

		// Iterar para cada uno de los ciclos
		for (i = 0; i<(*ptrData).nCiclos; i++)
		{
			for (j = 0; j<arrayTamano; j++)
			{
				nContador++;
				// Ciclo de trabajo
				*(arrayResultado + nContador) = fOffsetX + arrayTiempos[0][j];
				*(arrayResultado + nPoints + nContador) = fOffsetY - 100.0*(arrayTiempos[0][j] / ptrData->dMHT);

				// Ciclo de descanso
				for (k = 1; k <= roundToInt(arrayTiempos[1][j] / ptrData->dPaso); k++)
				{
					nContador++;
					*(arrayResultado + nContador) = fOffsetX + arrayTiempos[0][j] + k * ptrData->dPaso;
					*(arrayResultado + nPoints + nContador) = fOffsetY * exp(-0.5*(k * ptrData->dPaso) / ptrData->dMHT);
					*(arrayResultado + nPoints + nContador) += 100.0*(1.0 - exp(-0.5*(k * ptrData->dPaso) / ptrData->dMHT));
					*(arrayResultado + nPoints + nContador) += -100.0*(arrayTiempos[0][j] / ptrData->dMHT) * (1 - exp(-0.164*(arrayTiempos[0][j]) / (k*ptrData->dPaso)));
				}
				fOffsetX = *(arrayResultado + nContador);
				fOffsetY = *(arrayResultado + nPoints + nContador);
			}
		}

		//Liberar las memorias reservadas
		/*freeMemory(arrayPrueba[0]);
		freeMemory(arrayPrueba);*/

		// Devolver los resultados
		return arrayPrueba;
	}

// Para que se pueda liberar la memoria reservada por la función "Curva"
	void __stdcall FreeMemory(void* ptr)
	{
		free(ptr);
		return;
	}

/*
extern "C" __declspec(dllexport) int __stdcall DataSize (int i)
{
	switch (i)
		{
		case 1:
			return (int) sizeof (int);
			break;
		case 2:
			return (int) sizeof (long);
			break;
		case 3:
			return (int) sizeof (float);
			break;
		case 4:
			return (int) sizeof (double);
			break;
		case 5:
			return (int) sizeof (__int32);
			break;
		}
}
*/

// Función interna de redondeo
int roundToInt(double a)
{
	return (int)floor(a + 0.00001);
}
