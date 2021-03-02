// TODO: mencionar aqu� los encabezados adicionales que el programa necesita
#include "pch.h"
#include <math.h>
#include <stdlib.h>

#pragma once
#ifdef OCRAINDEX_EXPORTS
#define OcraIndex_API __declspec(dllexport)
#else
#define OcraIndex_API __declspec(dllimport)
#endif

/* Definici�n de tipos */
typedef struct dataOcra
{
	double i;		// Intensidad del esfuerzo
	double e;       // Esfuerzos por minuto
	double d;       // Duraci�n del esfuerzo
	double p;       // Posici�n de la mano
	double h;       // Duraci�n de la tarea
	double ea;		// Esfuerzos acumulados a
	double eb;		// Esfuerzos acumulados b

};


/* Prototipos de funci�n a exportar*/
extern "C" OcraIndex_API double __stdcall OCRAchecklist(dataOcra*);

/* Prototipos de funciones internas*/


/// <summary>
/// 
/// </summary>
/// <param name="data"></param>
/// <returns></returns>
double __stdcall OCRAchecklist(dataOcra* data)
{
	
	return 0.0;
}