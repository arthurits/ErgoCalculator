#include "pch.h"
#include <math.h>
#include <stdlib.h>

#pragma once
#ifdef THERMALC_EXPORTS
#define ThermalComfort_API __declspec(dllexport)
#else
#define ThermalComfort_API __declspec(dllimport)
#endif

// Type definitions
struct DataTC
{
    double TempAir;      // Air temperature (C)
    double TempRad;      // Radiant temperature (C)
    double Velocity;     // Air velocity (m/s)
    double RelHumidity;  // Relative humidity (%)
    double Clothing;     // Clothing (clo)
    double MetRate;      // Metabolic rate (met)
    double ExternalWork; // External work (met), generally around 0
};

struct VarsTC
{
    double PMV;          // Factor de intensidad del esfuerzo [0, 1]
    double PPD;          // Factor de esfuerzos por minuto
    double HL_Skin;      // HL1 heat loss diff. through skin
    double HL_Sweating;  // HL2 heat loss by sweating
    double HL_Latent;    // HL3 latent respiration heat loss
    double HL_Dry;       // HL4 dry respiration heat loss
    double HL_Radiation; // HL5 heat loss by radiation
    double HL_Convection;// HL6 heat loss by convection
};

struct ModelTC
{
    DataTC data;         // Model data
    VarsTC factors;      // Model variables
    double indexPMV;     // PMV index
    double indexPPD;     // PPD index
};


/* Prototipos de función a exportar*/
extern "C" ThermalComfort_API double __stdcall ComfortPMV(ModelTC*);

/* Prototipos de funciones internas*/


/// <summary>
/// 
/// </summary>
/// <param name="data"></param>
/// <returns></returns>
double __stdcall ComfortPMV(ModelTC* data)
{
    // Variable definition
    double pa = data->data.RelHumidity * 10 * exp(16.6536 - 4030.183 / (data->data.TempAir + 235));

    double dInsulation = 0.155 * data->data.Clothing;   // Thermal insulation of the clothing in M2K/W
    double dMetRate = data->data.MetRate * 58.15;       // Metabolic rate in W/M2
    double dExtWork = data->data.ExternalWork * 58.15;  // External work in W/M2
    double dNeatHeat = dMetRate - dExtWork;             // Internal heat production in the human body
    double fcl;
    if (dInsulation <= 0.078)
        fcl = 1 + 1.29 * dInsulation;
    else
        fcl = 1.05 + 0.645 * dInsulation;


    // heat loss diff. through skin
    data->factors.HL_Skin = 3.05 * 0.001 * (5733 - 6.99 * dNeatHeat - pa);
    
    // heat loss by sweating
    if (dNeatHeat > 58.15)
        data->factors.HL_Sweating = 0.42 * (dNeatHeat - 58.15);
    else
        data->factors.HL_Sweating = 0;
    
    // latent respiration heat loss
    data->factors.HL_Latent = 1.7 * 0.00001 * dMetRate * (5867 - pa);
    
    // dry respiration heat loss
    data->factors.HL_Dry = 0.0014 * dMetRate * (34 - ta);
    
    // heat loss by radiation
    data->factors.HL_Radiation = 3.96 * fcl * (pow(xn, 4) - pow(tra / 100, 4));
    
    // heat loss by convection
    data->factors.HL_Convection = fcl * hc * (tcl - ta);

	return 0.0;
}