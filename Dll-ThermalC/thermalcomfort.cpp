#include "pch.h"
#include <math.h>
#include <stdlib.h>
#include <cstdlib>
//#include <cmath>

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
/// https://github.com/CenterForTheBuiltEnvironment/comfort_tool/blob/master/static/js/comfortmodels.js
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


    // Heat transf. coeff. by forced convection
    double hcf = 12.1 * sqrt(data->data.Velocity);
    double taa = data->data.TempAir + 273;
    double tra = data->data.TempRad + 273;
    // we have verified that using the equation below or this tcla = taa + (35.5 - ta) / (3.5 * (6.45 * icl + .1)) does not affect the PMV value
    double tcla = taa + (35.5 - data->data.TempAir) / (3.5 * dInsulation + 0.1);

    // Intermediate variables
    double p1 = dInsulation * fcl;
    double p2 = p1 * 3.96;
    double p3 = p1 * 100;
    double p4 = p1 * taa;
    double p5 = 308.7 - 0.028 * dNeatHeat + p2 * pow(tra / 100, 4);
    double xn = tcla / 100;
    double xf = tcla / 50;
    double eps = 0.00015;
    
    // Iteration
    double hcn = 0;
    double hc = 0;
    int n = 0;
    while (abs(xn - xf) > eps) {
        xf = (xf + xn) / 2;
        hcn = 2.38 * pow(abs(100.0 * xf - taa), 0.25);
        if (hcf > hcn) hc = hcf;
        else hc = hcn;
        xn = (p5 + p4 * hc - p2 * pow(xf, 4)) / (100 + p3 * hc);
        ++n;
        if (n > 150) {
            //alert("Max iterations exceeded");
            return -1.0;
        }
    }

    double tcl = 100 * xn - 273;


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
    data->factors.HL_Dry = 0.0014 * dMetRate * (34 - data->data.TempAir);
    
    // heat loss by radiation
    data->factors.HL_Radiation = 3.96 * fcl * (pow(xn, 4) - pow(tra / 100, 4));
    
    // heat loss by convection
    data->factors.HL_Convection = fcl * hc * (tcl - data->data.TempAir);

	return 1.0;
}