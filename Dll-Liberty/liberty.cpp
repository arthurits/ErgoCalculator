#include "pch.h"
//#include <cmath>
#include <math.h>	// required for exp() 	// #define _USE_MATH_DEFINES for M_SQRT1_2 and M_2_SQRTPI
#include <stdlib.h>
#include <cstdlib>


#pragma once
#ifdef LIBERTY_EXPORTS
#define LibertyMutual_API __declspec(dllexport)
#else
#define LibertyMutual_API __declspec(dllimport)
#endif

#define M_SQRT1_2  0.707106781186547524401  // 1/sqrt(2)
#define M_2_SQRTPI 1.12837916709551257390   // 2/sqrt(pi)
constexpr double zscore75 = 0.6744897501960818;	// https://planetcalc.com/7803/
constexpr double zscore90 = 1.2815515655446008;	// https://planetcalc.com/7803/

// Type definitions

enum MNType
{
	TypeCarrying = 0,
	TypeLifting = 1,
	TypeLowering = 2,
	TypePulling = 3,
	TypePushing = 4
};


enum MNGender
{
	Male = 0,
	Female = 1
};

struct DataLiberty
{
	double HorzReach;	// Horizontal reach distance (H) must range from 0.20 to 0.68 m for females and 0.25 to 0.73 m for males. If H changes during a lift or lower, the mean H or maximum H can be used
	double VertRangeM;	// Vertical range middle (m)
	double DistHorz;	// The distance travelled horizontally per push or pull (m)
	double DistVert;	// Distance travelled vertically (DV) per lift or lower must not be lower than 0.25 m or exceed arm reach for the anthropometry being used
	double VertHeight;	// The vertical height of the hands (m)
	double Freq;		// The frequency per minute. It must range from 1 per day (i.e. 1/480 = ?0.0021/min) to 20/min
};

struct ScaleFactors
{
	double RL;		// Reference load (in kg or kgf)
	double H;		// Horizontal reach factor
	double VRM;
	double DH;
	double DV;
	double V;
	double F;
	double CV;
	double MAL;      // Maximum acceptable load — Mean (in kg or kgf)
	double MAL75;    // Maximum acceptable load for 75% (in kg or kgf)
	double MAL90;    // Maximum acceptable load for 90% (in kg or kgf)
};

struct ResultsLiberty
{
	double IniCoeffV;
	double SusCoeffV;
	double IniForce;    // Maximum initial force in kgf
	double SusForce;	// Maximum sustained force in kgf
	double Weight;      // Maximum weight in kg
};

struct ModelLiberty
{
	DataLiberty data;			// Model data
	ResultsLiberty results;		// Model variables
	ScaleFactors Initial;
	ScaleFactors Sustained;
	MNType type;
	MNGender gender;
};

/* Prototipos de función a exportar*/
extern "C" LibertyMutual_API double __stdcall LibertyMutualMMH(ModelLiberty*, int);

/* Prototipos de funciones internas*/
void Lifting(ModelLiberty*);
void Lowering(ModelLiberty*);
void Pushing(ModelLiberty*);
void Pulling(ModelLiberty*);
void Carrying(ModelLiberty*);
double Gaussian_Distribution(double);
double Gaussian_Density(double);


double __stdcall LibertyMutualMMH(ModelLiberty* data, int nSize)
{

	for (int i = 0; i < nSize; i++)
	{
		switch (data[i].type)
		{
		case TypeCarrying:
			Carrying(data + i);
			break;
		case TypeLifting:
			Lifting(data + i);
			break;
		case TypeLowering:
			Lowering(data + i);
			break;
		case TypePulling:
			Pulling(data + i);
			break;
		case TypePushing:
			Pushing(data + i);
			break;
		}

		data[i].Initial.MAL75 = data[i].Initial.MAL * (1 - data[i].Initial.CV * zscore75);
		data[i].Initial.MAL90 = data[i].Initial.MAL * (1 - data[i].Initial.CV * zscore90);

		data[i].Sustained.MAL75 = data[i].Sustained.MAL * (1 - data[i].Sustained.CV * zscore75);
		data[i].Sustained.MAL90 = data[i].Sustained.MAL * (1 - data[i].Sustained.CV * zscore90);
		//SingleComfortPMV(&data[i]);
	}
	
	return 0.0;
}


void Lifting(ModelLiberty* data)
{
	double result = -1.0;
	double CoeffVar = -1.0;

	double H = data->data.HorzReach;
	double VRM = data->data.VertRangeM;
	double DH = data->data.DistHorz;
	double DV = data->data.DistVert;
	double V = data->data.VertHeight;
	double F = data->data.Freq;

	if (data->gender == Male)
	{
		// Lift – Male
		//result = 82.6 * (1.3532 - H / 0.7079) * (0.7746 + VRM / 1.912 - pow(VRM, 2) / 3.296) * (0.8695 - log(DV) / 10.62) * (0.6259 - log(F) / 9.092 - pow(log(F), 2) / 125.0);
		data->Initial.RL = 82.6;
		data->Initial.H = 1.3532 - H / 0.7079;
		data->Initial.VRM = 0.7746 + VRM / 1.912 - pow(VRM, 2) / 3.296;
		data->Initial.DV = 0.8695 - log(DV) / 10.62;
		data->Initial.F = 0.6259 - log(F) / 9.092 - pow(log(F), 2) / 125.0;

		result = 82.6;
		result *= 1.3532 - H / 0.7079;
		result *= 0.7746 + VRM / 1.912 - pow(VRM, 2) / 3.296;
		result *= 0.8695 - log(DV) / 10.62;
		result *= 0.6259 - log(F) / 9.092 - pow(log(F), 2) / 125.0;
		CoeffVar = 0.276;
	}
	else if (data->gender == Female)
	{
		// Lift – Female
		//result = 34.9 * (1.2602 - H / 0.7686) * (0.9877 + VRM / 13.69 - pow(VRM, 2) / 9.221) * (0.8199 - log(DV) / 7.696) * (0.6767 - log(F) / 12.59 - pow(log(F), 2) / 228.2);
		data->Initial.RL = 34.9;
		data->Initial.H = 1.2602 - H / 0.7686;
		data->Initial.VRM = 0.9877 + VRM / 13.69 - pow(VRM, 2) / 9.221;
		data->Initial.DV = 0.8199 - log(DV) / 7.696;
		data->Initial.F = 0.6767 - log(F) / 12.59 - pow(log(F), 2) / 228.2;
		
		result = 34.9;
		result *= 1.2602 - H / 0.7686;
		result *= 0.9877 + VRM / 13.69 - pow(VRM, 2) / 9.221;
		result *= 0.8199 - log(DV) / 7.696;
		result *= 0.6767 - log(F) / 12.59 - pow(log(F), 2) / 228.2;
		CoeffVar = 0.260;
	}
	
	data->Initial.MAL = data->Initial.RL * data->Initial.H * data->Initial.VRM * data->Initial.DV * data->Initial.F;
	data->Initial.CV = CoeffVar;

	data->results.Weight = result;
	data->results.IniCoeffV = CoeffVar;

	return;
}

void Lowering(ModelLiberty* data)
{
	double result = -1.0;
	double CoeffVar = -1.0;

	double H = data->data.HorzReach;
	double VRM = data->data.VertRangeM;
	double DH = data->data.DistHorz;
	double DV = data->data.DistVert;
	double V = data->data.VertHeight;
	double F = data->data.Freq;

	if (data->gender == Male)
	{
		// Lower – Male (note: only the RL, FSF, and CV values are different from the Lift – Male equation)
		//result = 95.9 * (1.3532 - H / 0.7079) * (0.7746 + VRM / 1.912 - pow(VRM, 2) / 3.296) * (0.8695 - log(DV) / 10.62) * (0.5773 - log(F) / 10.80 - pow(log(F), 2) / 255.9);
		data->Initial.RL = 95.9;
		data->Initial.H = 1.3532 - H / 0.7079;
		data->Initial.VRM = 0.7746 + VRM / 1.912 - pow(VRM, 2) / 3.296;
		data->Initial.DV = 0.8695 - log(DV) / 10.62;
		data->Initial.F = 0.5773 - log(F) / 10.80 - pow(log(F), 2) / 255.9;
		
		result = 95.9;
		result *= 1.3532 - H / 0.7079;
		result *= 0.7746 + VRM / 1.912 - pow(VRM, 2) / 3.296;
		result *= 0.8695 - log(DV) / 10.62;
		result *= 0.5773 - log(F) / 10.80 - pow(log(F), 2) / 255.9;

		CoeffVar = 0.304;
	}
	else if (data->gender == Female)
	{
		// Lower – Female (note: only the RL and CV values are different from the Lift – Female equation)
		//result = 37.0 * (1.2602 - H / 0.7686) * (0.9877 + VRM / 13.69 - pow(VRM, 2) / 9.221) * (0.8199 - log(DV) / 7.696) * (0.6767 - log(F) / 12.59 - pow(log(F), 2) / 228.2);
		data->Initial.RL = 37.0;
		data->Initial.H = 1.2602 - H / 0.7686;
		data->Initial.VRM = 0.9877 + VRM / 13.69 - pow(VRM, 2) / 9.221;
		data->Initial.DV = 0.8199 - log(DV) / 7.696;
		data->Initial.F = 0.6767 - log(F) / 12.59 - pow(log(F), 2) / 228.2;
		
		result = 37.0;
		result *= 1.2602 - H / 0.7686;
		result *= 0.9877 + VRM / 13.69 - pow(VRM, 2) / 9.221;
		result *= 0.8199 - log(DV) / 7.696;
		result *= 0.6767 - log(F) / 12.59 - pow(log(F), 2) / 228.2;

		CoeffVar = 0.307;
	}

	data->Initial.MAL = data->Initial.RL * data->Initial.H * data->Initial.VRM * data->Initial.DV * data->Initial.F;
	data->Initial.CV = CoeffVar;

	data->results.Weight = result;
	data->results.IniCoeffV = CoeffVar;

	return;
}

void Pushing(ModelLiberty* data)
{
	double resultI = -1.0;
	double resultS = -1.0;
	double cvI = -1.0;
	double cvS = -1.0;

	double V = data->data.VertHeight;
	double DH = data->data.DistHorz;
	double F = data->data.Freq;

	if (data->gender == Male)
	{
		// Push – Initial – Male
		data->Initial.RL = 70.3;
		data->Initial.V = 1.2737 - V / 1.335 + pow(V, 2) / 2.576;
		data->Initial.DH = 1.0790 - log(DH) / 9.392;
		data->Initial.F = 0.6281 - log(F) / 13.07 - pow(log(F), 2) / 379.5;

		resultI = 70.3 * (1.2737 - V / 1.335 + pow(V, 2) / 2.576) * (1.0790 - log(DH) / 9.392) * (0.6281 - log(F) / 13.07 - pow(log(F), 2) / 379.5);
		cvI = 0.231;

		// Push – Sustained – Male
		data->Sustained.RL = 65.3;
		data->Sustained.V = 2.2940 - V / 0.3345 + pow(V, 2) / 0.6687;
		data->Sustained.DH = 1.1035 - log(DH) / 7.170;
		data->Sustained.F = 0.4896 - log(F) / 10.20 - pow(log(F), 2) / 403.9;

		resultS = 65.3 * (2.2940 - V / 0.3345 + pow(V, 2) / 0.6687) * (1.1035 - log(DH) / 7.170) * (0.4896 - log(F) / 10.20 - pow(log(F), 2) / 403.9);
		cvS = 0.267;
	}
	else if (data->gender == Female)
	{
		// Push or Pull – Initial – Female
		data->Initial.RL = 36.9;
		data->Initial.V = -0.5304 + V / 0.3361 - pow(V, 2) / 0.6915;
		data->Initial.DH = 1.0286 - DH / 72.22 + pow(DH, 2) / 9782;
		data->Initial.F = 0.7251 - log(F) / 13.19 - pow(log(F), 2) / 197.3;

		resultI = 36.9 * (-0.5304 + V / 0.3361 - pow(V, 2) / 0.6915) * (1.0286 - DH / 72.22 + pow(DH, 2) / 9782) * (0.7251 - log(F) / 13.19 - pow(log(F), 2) / 197.3);

		cvI = 0.214;	// for Push – Initial – Female
		//CoeffVar = 0.234;	// for Pull – Initial – Female

		// Push or Pull – Sustained – Female
		data->Sustained.RL = 25.5;
		data->Sustained.V = -0.6539 + V / 0.2941 - pow(V, 2) / 0.5722;
		data->Sustained.DH = 1.0391 - DH / 52.91 + pow(DH, 2) / 7975;
		data->Sustained.F = 0.6086 - log(F) / 11.95 - pow(log(F), 2) / 304.4;

		resultS = 25.5 * (-0.6539 + V / 0.2941 - pow(V, 2) / 0.5722) * (1.0391 - DH / 52.91 + pow(DH, 2) / 7975) * (0.6086 - log(F) / 11.95 - pow(log(F), 2) / 304.4);

		cvS = 0.286;	// for Push – Sustained – Female;
		//CoeffVar = 0.298;	// for Pull – Sustained – Female;
	}

	data->Initial.MAL = data->Initial.RL * data->Initial.V * data->Initial.DH * data->Initial.F;
	data->Initial.CV = cvI;

	data->Sustained.MAL = data->Sustained.RL * data->Sustained.V * data->Sustained.DH * data->Sustained.F;
	data->Sustained.CV = cvS;

	data->results.IniForce = resultI;
	data->results.SusForce = resultS;

	data->results.IniCoeffV = cvI;
	data->results.SusCoeffV = cvS;

	return;
}

void Pulling(ModelLiberty* data)
{
	double resultI = -1.0;
	double resultS = -1.0;
	double cvI = -1.0;
	double cvS = -1.0;

	double V = data->data.VertHeight;
	double DH = data->data.DistHorz;
	double F = data->data.Freq;

	if (data->gender == Male)
	{
		// Pull – Initial – Male(note: only the RL, VSF, &CoeffVar values are different from the Push – Initiate – Male equation)
		data->Initial.RL = 69.8;
		data->Initial.V = 1.7186 - V / 0.6888 + pow(V, 2) / 2.025;
		data->Initial.DH = 1.0790 - log(DH) / 9.392;
		data->Initial.F = 0.6281 - log(F) / 13.07 - pow(log(F), 2) / 379.5;

		resultI = 69.8 * (1.7186 - V / 0.6888 + pow(V, 2) / 2.025) * (1.0790 - log(DH) / 9.392) * (0.6281 - log(F) / 13.07 - pow(log(F), 2) / 379.5);
		cvI = 0.238;

		// Pull – Sustained – Male(note: only the RL, VSF, &CoeffVar values are different from the Push – Sustain – Male equation)
		data->Sustained.RL = 61.0;
		data->Sustained.V = 2.1977 - V / 0.3850 + pow(V, 2) / 0.9047;
		data->Sustained.DH = 1.1035 - log(DH) / 7.170;
		data->Sustained.F = 0.4896 - log(F) / 10.20 - pow(log(F), 2) / 403.9;

		resultS = 61.0 * (2.1977 - V / 0.3850 + pow(V, 2) / 0.9047) * (1.1035 - log(DH) / 7.170) * (0.4896 - log(F) / 10.20 - pow(log(F), 2) / 403.9);
		cvS = 0.257;
	}
	else if (data->gender == Female)
	{
		// Push or Pull – Initial – Female
		data->Initial.RL = 36.9;
		data->Initial.V = -0.5304 + V / 0.3361 - pow(V, 2) / 0.6915;
		data->Initial.DH = 1.0286 - DH / 72.22 + pow(DH, 2) / 9782;
		data->Initial.F = 0.7251 - log(F) / 13.19 - pow(log(F), 2) / 197.3;

		resultI = 36.9 * (-0.5304 + V / 0.3361 - pow(V, 2) / 0.6915) * (1.0286 - DH / 72.22 + pow(DH, 2) / 9782) * (0.7251 - log(F) / 13.19 - pow(log(F), 2) / 197.3);

		//CoeffVar = 0.214;	// for Push – Initial – Female
		cvI = 0.234;	// for Pull – Initial – Female

		// Push or Pull – Sustained – Female
		data->Sustained.RL = 25.5;
		data->Sustained.V = -0.6539 + V / 0.2941 - pow(V, 2) / 0.5722;
		data->Sustained.DH = 1.0391 - DH / 52.91 + pow(DH, 2) / 7975;
		data->Sustained.F = 0.6086 - log(F) / 11.95 - pow(log(F), 2) / 304.4;

		resultS = 25.5 * (-0.6539 + V / 0.2941 - pow(V, 2) / 0.5722) * (1.0391 - DH / 52.91 + pow(DH, 2) / 7975) * (0.6086 - log(F) / 11.95 - pow(log(F), 2) / 304.4);

		//CoeffVar = 0.286;	// for Push – Sustained – Female;
		cvS = 0.298;	// for Pull – Sustained – Female;
	}

	data->Initial.MAL = data->Initial.RL * data->Initial.V * data->Initial.DH * data->Initial.F;
	data->Initial.CV = cvI;

	data->Sustained.MAL = data->Sustained.RL * data->Sustained.V * data->Sustained.DH * data->Sustained.F;
	data->Sustained.CV = cvS;

	data->results.IniForce = resultI;
	data->results.SusForce = resultS;

	data->results.IniCoeffV = cvI;
	data->results.SusCoeffV = cvS;

	return;
}

void Carrying(ModelLiberty* data)
{
	double result = -1.0;
	double CoeffVar = -1.0;

	double V = data->data.VertHeight;
	double DH = data->data.DistHorz;
	double F = data->data.Freq;

	if (data->gender == Male)
	{
		//Carry – Male
		//result = 74.9 * (1.5505 - V / 1.417) * (1.1172 - log(DH) / 6.332) * (0.5149 - log(F) / 7.958 - pow(log(F), 2) / 131.1);
		data->Initial.RL = 74.9;
		data->Initial.V = 1.5505 - V / 1.417;
		data->Initial.DH = 1.1172 - log(DH) / 6.332;
		data->Initial.F = 0.5149 - log(F) / 7.958 - pow(log(F), 2) / 131.1;

		result = 74.9;
		result *= 1.5505 - V / 1.417;
		result *= 1.1172 - log(DH) / 6.332;
		result *= 0.5149 - log(F) / 7.958 - pow(log(F), 2) / 131.1;
		CoeffVar = 0.278;
	}
	else if (data->gender == Female)
	{
		//Carry – Female
		//result = 28.6 * (1.1645 - V / 4.437) * (1.0101 - DH / 207.8) * (0.6224 - log(F) / 16.33);
		data->Initial.RL = 28.6;
		data->Initial.V = 1.1645 - V / 4.437;
		data->Initial.DH = 1.0101 - DH / 207.8;
		data->Initial.F = 0.6224 - log(F) / 16.33;

		result = 28.6;
		result *= 1.1645 - V / 4.437;
		result *= 1.0101 - DH / 207.8;
		result *= 0.6224 - log(F) / 16.33;
		CoeffVar = 0.231;
	}

	data->Initial.MAL = data->Initial.RL * data->Initial.V * data->Initial.DH * data->Initial.F;
	data->Initial.CV = CoeffVar;

	data->results.Weight = result;
	data->results.IniCoeffV = CoeffVar;

	return;
}

////////////////////////////////////////////////////////////////////////////////
// double Gaussian_Distribution( double x )                                   //
// http://www.mymathlib.com/functions/probability/gaussian_distribution.html  //
//  Description:                                                              //
//     This function returns the probability that a random variable with      //
//     a standard Normal (Gaussian) distribution has a value less than "x".   //
//                                                                            //
//  Arguments:                                                                //
//     double x   Argument of Pr[X < x] where X ~ N(0,1).                     //
//                                                                            //
//  Return Values:                                                            //
//     The probability of observing a value less than (or equal) to x assuming//
//     a normal (Gaussian) distribution with mean 0 and variance 1.           //
//                                                                            //
//  Example:                                                                  //
//     double x, pr;                                                          //
//                                                                            //
//    pr = Gaussian_Distribution(x);                                          //
////////////////////////////////////////////////////////////////////////////////
double Gaussian_Distribution(double x)
{
	return  0.5 * (1.0 + erf(M_SQRT1_2 * x));
}

////////////////////////////////////////////////////////////////////////////////
// double Gaussian_Density( double x )                                        //
// http://www.mymathlib.com/functions/probability/gaussian_distribution.html  //
//  Description:                                                              //
//     This function returns the value of the probability density at X = x    //
//     where X has a standard Normal (Gaussian) distribution.                 //
//                                                                            //
//  Arguments:                                                                //
//     double x   Argument of dPr[X < x]/dx where X ~ N(0,1).                 //
//                                                                            //
//  Return Values:                                                            //
//     The value of the probability density at X = x where X has a Normal     //
//     (Gaussian) distribution with mean 0 and variance 1.                    //
//                                                                            //
//  Example:                                                                  //
//     double x, pr;                                                          //
//                                                                            //
//     pr = Gaussian_Density(x);                                              //
////////////////////////////////////////////////////////////////////////////////
double Gaussian_Density(double x)
{
	return  0.5 * M_SQRT1_2 * M_2_SQRTPI * exp(-0.5 * x * x);
}

// https://github.com/antelopeusersgroup/antelope_contrib/blob/master/lib/location/libgenloc/erfinv.c
// https://github.com/lakshayg/erfinv/blob/master/erfinv.c