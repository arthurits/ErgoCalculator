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

// Type definitions
struct DataTC
{
	double HorzReach;	// Horizontal reach distance (H) must range from 0.20 to 0.68 m for females and 0.25 to 0.73 m for males. If H changes during a lift or lower, the mean H or maximum H can be used
	double VRM;      // Radiant temperature (C)
	double VertHeight;	// The vertical height of the hands (m)
	double DistVert;	// Distance travelled vertically (DV) per lift or lower must not be lower than 0.25 m or exceed arm reach for the anthropometry being used
	double DistHorz;	// The distance travelled horizontally per push or pull (m)
	double Freq;		// The frequency per minute. It must range from 1 per day (i.e. 1/480 = ?0.0021/min) to 20/min
};

/* Prototipos de función a exportar*/
extern "C" LibertyMutual_API double __stdcall LibertyMutualMMH(double, int);

/* Prototipos de funciones internas*/
double Gaussian_Distribution(double);
double Gaussian_Density(double);


double __stdcall LibertyMutualMMH(double x, int i)
{
	double result;
	double CoeffVar;

	double H = 0.0;
	double VRM = 0.0;
	double V = 0.0;
	double DV = 0.0;
	double DH = 0.0;
	double F = 0.0;
	
	// Lift – Male
	result = 82.6 * (1.3532 - H / 0.7079) * (0.7746 + VRM / 1.912 - pow(VRM, 2) / 3.296) * (0.8695 - log(DV) / 10.62) * (0.6259 - log(F) / 9.092 - pow(log(F), 2) / 125.0);
	CoeffVar = 0.276;
	
	// Lower – Male (note: only the RL, FSF, and CV values are different from the Lift – Male equation)
	result = 95.9 * (1.3532 - H / 0.7079) * (0.7746 + VRM / 1.912 - pow(VRM, 2) / 3.296) * (0.8695 - log(DV) / 10.62) * (0.5773 - log(F) / 10.80 - pow(log(F), 2) / 255.9);
	CoeffVar = 0.304;
	
	// Lift – Female
	result = 34.9 * (1.2602 - H / 0.7686) * (0.9877 + VRM / 13.69 - pow(VRM, 2) / 9.221) * (0.8199 - log(DV) / 7.696) * (0.6767 - log(F) / 12.59 - pow(log(F), 2) / 228.2);
	CoeffVar = 0.260;

	// Lower – Female (note: only the RL and CV values are different from the Lift – Female equation)
	result = 37.0 * (1.2602 - H / 0.7686) * (0.9877 + VRM / 13.69 - pow(VRM, 2) / 9.221) * (0.8199 - log(DV) / 7.696) * (0.6767 - log(F) / 12.59 - pow(log(F), 2) / 228.2);
	CoeffVar = 0.307;

	
	// Push or Pull – Initial – Female
	result = 36.9 * (-0.5304 + V / 0.3361 - pow(V, 2) / 0.6915) * (1.0286 - DH / 72.22 + pow(DH, 2) / 9782) * (0.7251 - log(F) / 13.19 - pow(log(F), 2) / 197.3);

	CoeffVar = 0.214;	// for Push – Initial – Female
	CoeffVar = 0.234;	// for Pull – Initial – Female

	// Push or Pull – Sustained – Female
	result = 25.5 * (-0.6539 + V / 0.2941 - pow(V, 2) / 0.5722) * (1.0391 - DH / 52.91 + pow(DH, 2) / 7975) * (0.6086 - log(F) / 11.95 - pow(log(F), 2) / 304.4);

	CoeffVar = 0.286;	// for Push – Sustained – Female;
	CoeffVar = 0.298;	// for Pull – Sustained – Female;

	// Push – Initial – Male
	result = 70.3 * (1.2737 - V / 1.335 + pow(V, 2) / 2.576) * (1.0790 - log(DH) / 9.392) * (0.6281 - log(F) / 13.07 - pow(log(F), 2) / 379.5);
	CoeffVar = 0.231;

	// Push – Sustained – Male
	result = 65.3 * (2.2940 - V / 0.3345 + pow(V, 2) / 0.6687) * (1.1035 - log(DH) / 7.170) * (0.4896 - log(F) / 10.20 - pow(log(F), 2) / 403.9);
	CoeffVar = 0.267;

	// Pull – Initial – Male(note: only the RL, VSF, &CoeffVar values are different from the Push – Initiate – Male equation)
	result = 69.8 * (1.7186 - V / 0.6888 + pow(V, 2) / 2.025) * (1.0790 - log(DH) / 9.392) * (0.6281 - log(F) / 13.07 - pow(log(F), 2) / 379.5);
	CoeffVar = 0.238;

	// Pull – Sustained – Male(note: only the RL, VSF, &CoeffVar values are different from the Push – Sustain – Male equation)
	result = 61.0 * (2.1977 - V / 0.3850 + pow(V, 2) / 0.9047) * (1.1035 - log(DH) / 7.170) * (0.4896 - log(F) / 10.20 - pow(log(F), 2) / 403.9);
	CoeffVar = 0.257;


	//Carry – Female
	result = 28.6 * (1.1645 - V/4.437) * (1.0101 - DH / 207.8) * (0.6224 - log(F) / 16.33);
	CoeffVar = 0.231;

	//Carry – Male
	result = 74.9 * (1.5505 - V / 1.417) * (1.1172 - log(DH) / 6.332) * (0.5149 - log(F) / 7.958 - pow(log(F), 2) / 131.1);
	CoeffVar = 0.278;

	return 0.0;
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