// Niosh.h - Contains declarations of Niosh lifting index functions
#pragma once

#ifdef NIOSH_EXPORTS
#define Niosh_API __declspec(dllexport)
#else
#define Niosh_API __declspec(dllimport)
#endif


/* Definición de tipos */
enum MNCoupling
{
	NoHandle = 0,
	Poor = 1,
	Good = 2
};

typedef struct data
{
	double weight;  // Peso que se manipula
	double h;       // Distancia horizontal
	double v;       // Distancia vertical del punto de agarre al suelo
	double d;       // Recorrido vertical
	double a;       // Ángulo de simetría
	double f;       // Frecuencia del levantamiento
	double fa;      // Frecuencia acumulada a
	double fb;      // Frecuencia acumulada b
	double td;      // Duración de la tarea
	MNCoupling c;          // Agarre
};

typedef struct factors
{
	double LC;   // Constante de carga
	double HM;   // Factor de distancia horizontal
	double VM;   // Factor de distancia vertical
	double DM;   // Factor de desplazamiento vertical
	double AM;   // Factor de asimetría
	double FM;   // Factor de frecuencia
	double FMa;  // Factor de frecuencia acumulada a
	double FMb;  // Factor de frecuencia acumulada b
	double CM;   // Factor de agarre
};

typedef struct SubTask
{
	data data;
	factors factors;
	double indexIF;
	double LI;
	int task;
	int order;
};

typedef struct Task
{
	double CLI;
	int order;
	int nSubTasks;
	int job;
};

typedef struct Job
{
	int nTasks;
};

/* Prototipos de función a exportar*/
extern "C" Niosh_API double __stdcall CalculateNIOSH (SubTask [], int [], const int * const);

/* Prototipos de funciones internas*/
double NIOSHindex(const double * const, const factors * const);
double FactorHM(const double * const);
double FactorVM(const double * const);
double FactorDM(const double * const);
double FactorAM(const double * const);
double FactorFM(const double * const, const double * const, const double * const);
double FactorCM(int, const double * const);

double InterpolacionLineal(const double * const, const double * const, const double * const, const double * const, const double * const);
int locate(double[], int, double);
void HeapSortIndex(double[], int[], const int * const);
