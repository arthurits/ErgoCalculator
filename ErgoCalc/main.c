#include <stdio.h>
//#include <stdlib.h>
#include <math.h>
#include <malloc.h>


/* Definición de tipos */
    typedef struct
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

    typedef struct
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

    typedef struct
    {
        dataCLM data;
        multipliersCLM factors;
        double indexLSI;
    } modelCLM;



    typedef struct
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
        int c;          // Agarre
    } dataNIOSH;

    typedef struct
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
    } multipliersNIOSH;

    typedef struct
    {
        dataNIOSH data;
        multipliersNIOSH factors;
        double indexIF;
        double index;
    } modelNIOSH;




/* Prototipos de función*/

    double FactorH (const double * const, const int * const);
    double FactorV (const double * const, const int * const);
    double FactorD (const double * const, const int * const);
    double FactorF (const double * const, const int * const);
    double FactorTD (const double * const);
    double FactorT (const double * const);
    double FactorC (const int * const);
    double FactorHS (const double * const);
    double FactorAG (const double * const, const int * const);
    double FactorBW (const double * const, const int * const);
    double Porcentaje (const double * const, const int * const);
    double LSIindex (const int * const sexo, const double * const peso, const multipliersCLM * const factores);

    double InterpolacionLineal (const double * const, const double * const, const double * const, const double * const, const double * const);
    int locate (double [], int, double);

    void PresentacionInicial (void);
    void PresentacionFinal (void);
    void IntroducirDatos (modelCLM * const);
    void MostrarResultados (const modelCLM * const);
    void CalculateLSI (modelCLM [], const int * const);

    double round (double);
    void freeMemory (void*);
    void hpsort(int, double []);
    void HeapSortIndex (double[], int[], const int * const);


    double CalculateNIOSH (modelNIOSH [], int [], const int * const);
    double NIOSHindex (const double * const, const multipliersNIOSH * const);
    double FactorHM (const double * const);
    double FactorVM (const double * const);
    double FactorDM (const double * const);
    double FactorAM (const double * const);
    double FactorFM (const double * const, const double * const, const double * const);
    double FactorCM (const int * const, const double * const);

int main()
{
    /* Definición e inicialización de variables */

    //dataCLM datos;
    //multipliersCLM factores;
    //datos.gender = 1;
    //datos.td = 0;
    //factores.fTD = 1;
    //resultado = FactorH(39.9, 1);

    modelCLM modelo[1];
    int nTama = 1;

    modelo[0].data.gender = 1; // 1=masculino
    modelo[0].data.weight = 5;
    modelo[0].data.h = 50;
    modelo[0].data.v = 70;
    modelo[0].data.d = 55;
    modelo[0].data.f = 2;
    modelo[0].data.td = 1;     // por defecto no se contempla
    modelo[0].data.t = 45;
    modelo[0].data.c = 2;
    modelo[0].data.hs = 27;
    modelo[0].data.ag = 50;
    modelo[0].data.bw = 70;
    modelo[0].factors.fTD = 1; // para que no penalice

    /* Llamar a la subrutina encargada de la presentación inicial*/
    //PresentacionInicial();

    /* Introducir los datos para hacer los cálculos*/
    //IntroducirDatos(&modelo);

    /* Realizar los cálculos*/
    CalculateLSI(modelo, &nTama);

    /*Llamar a la subrutina encargada de presentar los resultados en pantalla*/
    //MostrarResultados(&modelo);

    modelNIOSH mode[3];
    int nIndex [3] = {0, 1, 2};
    int nSize = 3;

    mode[0].data.weight = 20.0;
    mode[0].data.h = 25;
    mode[0].data.v = 75;
    mode[0].data.d = 5;
    mode[0].data.a = 0;
    mode[0].data.f = 1;
    mode[0].data.td = 2;
    mode[0].data.c = 2;


    mode[1].data.weight = 25.0;
    mode[1].data.h = 30;
    mode[1].data.v = 75;
    mode[1].data.d = 5;
    mode[1].data.a = 0;
    mode[1].data.f = 2;
    mode[1].data.td = 2;
    mode[1].data.c = 3;

    mode[2].data.weight = 15.0;
    mode[2].data.h = 30;
    mode[2].data.v = 75;
    mode[2].data.d = 5;
    mode[2].data.a = 45;
    mode[2].data.f = 2;
    mode[2].data.td = 2;
    mode[2].data.c = 2;

    double resultado = 0.0;
    resultado = CalculateNIOSH(mode, nIndex, &nSize);

    /* Comprobar el funcionamiento de la rutina de ordenación
    double mat[6]={14.0, 8.0, 32.0, 7.0, 3.0, 15.0};
    int index[6]={0,1,2,3,4,5};
    int n = 6;
    HeapSortIndex(mat, index, n);*/


    /* Llamar a la subrutina encargada de la presentación final*/
    //PresentacionFinal();

    return 0;
}


double FactorH(const double * const x , const int * const nSexo)
{
    // Definición de variables
    double resultado = 0.0;
    double fFactor[27] = {25,30,35,40,45,50,55,60,65,
                            1.00,0.87,0.79,0.73,0.70,0.68,0.63,0.52,0.43,
                            1.00,0.84,0.73,0.66,0.64,0.65,0.61,0.50,0.41};

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

double FactorV(const double * const x , const int * const nSexo)
{
    // Definición de variables
    double resultado = 0.0;
    double fFactor[108] = {0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145, 150, 155, 160, 165, 170, 175,
                        0.62, 0.64, 0.67, 0.69, 0.72, 0.75, 0.77, 0.80, 0.82, 0.85, 0.87, 0.90, 0.92, 0.95, 0.98, 1.00, 0.99, 0.98, 0.97, 0.96, 0.94, 0.93, 0.92, 0.91, 0.89, 0.88, 0.87, 0.84, 0.83, 0.82, 0.80, 0.79, 0.77, 0.76, 0.73, 0.71,
                        0.74, 0.76, 0.77, 0.79, 0.81, 0.83, 0.84, 0.86, 0.88, 0.90, 0.91, 0.93, 0.95, 0.97, 0.98, 1.00, 0.99, 0.98, 0.96, 0.95, 0.93, 0.90, 0.88, 0.86, 0.83, 0.81, 0.78, 0.76, 0.72, 0.70, 0.67, 0.65, 0.62, 0.60, 0.56, 0.54};

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

double FactorD(const double * const x , const int * const nSexo)
{
    // Definición de variables
    double resultado = 0.0;
    double fFactor[90] = {25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145, 150, 155, 160, 165, 170,
                        1, 0.97, 0.94, 0.91, 0.88, 0.86, 0.84, 0.83, 0.81, 0.80, 0.79, 0.78, 0.77, 0.76, 0.75, 0.73, 0.72, 0.71, 0.69, 0.68, 0.67, 0.66, 0.64, 0.63, 0.61, 0.59, 0.57, 0.55, 0.52, 0.49,
                        1, 0.99, 0.97, 0.96, 0.95, 0.94, 0.92, 0.89, 0.87, 0.84, 0.82, 0.80, 0.79, 0.78, 0.77, 0.77, 0.76, 0.75, 0.75, 0.75, 0.74, 0.74, 0.74, 0.74, 0.74, 0.74, 0.73, 0.73, 0.73, 0.73};

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

double FactorF(const double * const x , const int * const nSexo)
{
    // Definición de variables
    double resultado = 0.0;
    double fFactor[54] = {0.2, 0.5, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
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

double FactorTD(const double * const x)
{
    // Definición de variables
    double resultado = 0.0;
    double fFactor[18] = {0, 1, 2, 3, 4, 5, 6, 7, 8,
                        1.00, 1.00, 0.76, 0.66, 0.60, 0.57, 0.54, 0.50, 0.45};

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

double FactorT(const double * const x)
{
    // Definición de variables
    double resultado = 0.0;
    double fFactor[28] = {0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130,
                        1.00, 0.98, 0.95, 0.93, 0.90, 0.88, 0.86, 0.83, 0.81, 0.78, 0.76, 0.74, 0.71, 0.69};

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

double FactorC(const int * const x)
{
    // Definición de variables
    double resultado = 0.0;

    switch(*x)
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

double FactorHS(const double * const x)
{
    // Definición de variables
    double resultado = 0.0;
    double fFactor[44] = {19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
                        1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 0.98, 0.95, 0.93, 0.90, 0.88, 0.86, 0.83, 0.81, 0.78, 0.76, 0.74, 0.71, 0.69};

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

double FactorAG(const double * const x , const int * const nSexo)
{
    // Definición de variables
    double resultado = 0.0;
    double fFactor[27] = {20, 25, 30, 35, 40, 45, 50, 55, 60,
                        1, 0.91, 0.88, 0.88, 0.86, 0.78, 0.69, 0.62, 0.59,
                        1, 0.95, 0.90, 0.87, 0.82, 0.79, 0.72, 0.64, 0.49};

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

double FactorBW(const double * const x , const int * const nSexo)
{
    // Definición de variables
    double resultado = 0.0;
    double fFactor[39] = {40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100,
                        0.70, 0.70, 0.70, 0.70, 0.70, 0.80, 1.00, 1.20, 1.30, 1.41, 1.45, 1.45, 1.45,
                        1.00, 1.00, 1.00, 1.00, 1.00, 1.20, 1.40, 1.68, 1.85, 1.98, 2.05, 2.05, 2.05};

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

double Porcentaje(const double * const x , const int * const nSexo)
{
    // Definición de variables
    double resultado = 0.0;
    double fFactor[19] = {5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95};
    double fFactorH[19] = {39.05, 37.54, 36.04, 34.53, 33.02, 31.51, 30.00, 28.50, 26.99, 25.47, 23.96, 22.45, 20.94, 19.44, 17.93, 16.42, 14.91, 13.41, 11.89};
    double fFactorM[19] = {22.75, 22.15, 21.54, 20.94, 20.34, 19.73, 19.13, 18.53, 17.92, 17.32, 16.71, 16.11, 15.51, 14.90, 14.30, 13.70, 13.09, 12.49, 11.89};

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

double LSIindex (const int * const sexo, const double * const peso, const multipliersCLM * const factores)
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
        porcentaje = Porcentaje (&pesoBase, sexo);
        indice = 10 - porcentaje/10;
    }

    return indice;
}

double InterpolacionLineal(const double * const x, const double * const x1, const double * const x2, const double * const y1, const double * const y2)
{
    double resultado = 0.0;

    if (*x == *x1)        // Si el valor pedido coincide con el límite inferior
        resultado = *y1;
    else if (*x == *x2)   // Si el valor pedido coincide con el límite superior
        resultado = *y2;
    else                // En cualquier otro caso, hacer la interpolación lineal
        resultado = (*x-*x1)*(*y2-*y1)/(*x2-*x1)+*y1;

    return resultado;
}

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
    ju = n-1;       //and upper limits.
    ascnd = (xx[n-1] >= xx[0]);

    // Comprobar si el valor pedido está fuera de la matriz
    // Cuidado con el orden ascendente o descendente
    if (x <= xx[0] && x <= xx[n-1])
        return ascnd == 1 ? -1 : n-1;
    if (x >= xx[n-1] && x >= xx[0])
        return ascnd == 1 ? n-1 : -1;

    // Método de la bisección
    while (ju-jl > 1)
    {                       //If we are not yet done,
        jm=(ju+jl) >> 1;    //compute a midpoint,
        if (x >= xx[jm] == ascnd)
            jl=jm;          //and replace either the lower limit
        else
            ju=jm;          //or the upper limit, as appropriate.
    }                       //Repeat until the test condition is satisfied.

    //if (x <= xx[1]) j=0;   //Then set the output
    //else if(x >= xx[n]) j=n-1;
    //else j=jl;

    return jl;
}

/* Rutina que realiza una pequeña presentación preliminar */
void PresentacionInicial (void) {

	int i;

	printf("\n\n\n\n");

    printf("\n%18s","-");

    for (i=1;i<=41;i++)
    	printf("%s","-");

    printf("\n%62s","|  INSTITUTO NACIONAL DE SEGURIDAD E HIGIENE EN EL TRABAJO  |");

    printf("\n%18s","-");

    for (i=1;i<=41;i++)
    	printf("%s","-");

    printf("\n\n\n\n\n\n%42s","CLM equation");
    printf("\n%42s", "Alpha version");

    printf("\n\n\n\n\n\n\n\n\n%s","(Presionar cualquier tecla para continuar... )");

    while ( (getchar() ) != '\n');

    system("cls");
}

/*Rutina de presentación final*/
void PresentacionFinal (void) {

	system("cls");
	printf("\n\n\n\n\nPrograma finalizado correctamente");
	printf("\n\n\n\n\n\n\n\n%10s","Copyright © 2009 by Alfredo Alvarez. All Rights Reserved");
	printf("\n%s\n\n\n", "November 30th, 2009");

    printf("\n\n\n%s","(Presionar cualquier tecla para continuar... )");

    while ( (getchar() ) != '\n');

    system("cls");
}

/*Rutina de introducción de datos*/
void IntroducirDatos (modelCLM * const modelo)
{
    /* Entrada de datos */
        printf("Enter gender (1=male, 2=female): ");
        scanf("%d", &modelo->data.gender);

        printf("Enter weight lifted (kg): ");
        scanf("%lf", &modelo->data.weight);

        printf("Enter horizontal distance (cm): ");
        scanf("%lf", &modelo->data.h);

        printf("Enter vertical distance (cm): ");
        scanf("%lf", &modelo->data.v);

        printf("Enter vertical travel distance (cm): ");
        scanf("%lf", &modelo->data.d);

        printf("Enter frequency lift (times/min): ");
        scanf("%lf", &modelo->data.f);

        if (modelo->data.f >= 4)
        {
            printf("Enter task duration (hours): ");
            scanf("%lf", &modelo->data.td);
        }

        printf("Enter twisting angle (degrees): ");
        scanf("%lf", &modelo->data.t);

        printf("Enter coupling (1 = good, 2 = poor, 3 = no handle): ");
        scanf("%d", &modelo->data.c);

        printf("Enter heat stress WBGT (ºC): ");
        scanf("%lf", &modelo->data.hs);

        printf("Enter age (years): ");
        scanf("%lf", &modelo->data.ag);

        printf("Enter body weight (kg): ");
        scanf("%lf", &modelo->data.bw);

        //Recoger el último retorno de carro
        getchar();

    printf("\n\n");

    return;
}

void MostrarResultados (const modelCLM * const modelo)
{
    printf("FactorH's value is: %lf \n", modelo->factors.fH);
    printf("FactorV's value is: %lf \n", modelo->factors.fV);
    printf("FactorD's value is: %lf \n", modelo->factors.fD);
    printf("FactorF's value is: %lf \n", modelo->factors.fF);
    if (modelo->data.f >= 4)
        printf("FactorTD's value is: %lf \n", modelo->factors.fTD);
    printf("FactorT's value is: %lf \n", modelo->factors.fT);
    printf("FactorC's value is: %lf \n",modelo->factors.fC);
    printf("FactorHS's value is: %lf \n", modelo->factors.fHS);
    printf("FactorAG's value is: %lf \n", modelo->factors.fAG);
    printf("FactorBW's value is: %lf \n", modelo->factors.fBW);

    printf("\n\nThe lifting safety index (LSI) is: %lf\n", modelo->indexLSI );

    printf("\n\n%s","(Presionar cualquier tecla para continuar... )");
    getchar();
    //while ( (getchar() ) != '\n');
}

void CalculateLSI (modelCLM modelo[], const int * const nSize)
{
    int i;  /* Indice para el bucle for*/

    /* Realizar los cálculos para cada tarea */
    for (i=0; i < *nSize; i++)
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

/* ***************************************************** */
/*                                                       */
/* Funciones para la ecuación de NIOSH                   */
/*                                                       */
/* ***************************************************** */

double CalculateNIOSH (modelNIOSH modelo[], int nIndex[], const int * const nSize)
{
    /* 1er paso: calcular los índices de las tareas simples */
        int i;  /* Indice para el bucle for*/

        double *pIndex = NULL;   /* Matriz con los índices individuales para ordenar*/
        pIndex = malloc(*nSize * sizeof(double));
        if (pIndex == NULL) return 0.0;

        for (i=0; i < *nSize; i++)
        {
            modelo[i].factors.LC = 23.0;
            modelo[i].factors.HM = FactorHM(&modelo[i].data.h);
            modelo[i].factors.VM = FactorVM(&modelo[i].data.v);
            modelo[i].factors.DM = FactorDM(&modelo[i].data.d);
            modelo[i].factors.AM = FactorAM(&modelo[i].data.a);
            modelo[i].factors.FM = FactorFM(&modelo[i].data.f, &modelo[i].data.v, &modelo[i].data.td);
            modelo[i].factors.CM = FactorCM(&modelo[i].data.c, &modelo[i].data.v);

            modelo[i].index = NIOSHindex(&modelo[i].data.weight, &modelo[i].factors);
            modelo[i].indexIF = modelo[i].index * modelo[i].factors.FM;

            pIndex[i]=modelo[i].index;
        }

    /* 2º paso: ordenar los índices */
        HeapSortIndex(pIndex, nIndex, nSize);

    /* 3er paso: calcular el sumatorio con los índices recalculados */
        double resultado = modelo[nIndex[*nSize - 1]].index;
        modelo[nIndex[*nSize - 1]].data.fa = modelo[nIndex[*nSize - 1]].data.fb = modelo[nIndex[*nSize - 1]].data.f;

        for (i=*nSize - 2; i >= 0; i--)
        {
            modelo[nIndex[i]].data.fa = modelo[nIndex[i+1]].data.fa + modelo[nIndex[i]].data.f;
            modelo[nIndex[i]].data.fb = modelo[nIndex[i+1]].data.fa;
            modelo[nIndex[i]].factors.FMa = FactorFM(&modelo[nIndex[i]].data.fa, &modelo[nIndex[i]].data.v, &modelo[nIndex[i]].data.td);
            modelo[nIndex[i]].factors.FMb = FactorFM(&modelo[nIndex[i]].data.fb, &modelo[nIndex[i]].data.v, &modelo[nIndex[i]].data.td);
            resultado += modelo[nIndex[i]].indexIF * ( 1/modelo[nIndex[i]].factors.FMa - 1/modelo[nIndex[i]].factors.FMb );
        }

    /* Liberar la memoria reservada*/
    free(pIndex);

    /* Finalizar*/
    return resultado;
}

double NIOSHindex (const double * const peso, const multipliersNIOSH * const factores)
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

double FactorHM (const double * const x)
{
    // Definición de variables
    double resultado = 0.0;

    // Calcular el factor
    if ( *x < 25)
        resultado = 1.0;
    else if ( *x > 63)
        resultado = 0.0;
    else
        resultado = 25 / *x;

    // Devolver el resultado
    return resultado;
}

double FactorVM (const double * const x)
{
    // Definición de variables
    double resultado = 0.0;

    // Calcular el factor
    if ( *x > 175 )
        resultado = 0.0;
    else
        resultado = 1-0.003*fabs( *x - 75);

    // Devolver el resultado
    return resultado;
}

double FactorDM (const double * const x)
{
    // Definición de variables
    double resultado = 0.0;

    // Calcular el factor
    if ( *x < 25 )
        resultado = 1.0;
    else if ( *x > 175 )
        resultado = 0.0;
    else
        resultado = 0.82 + 4.5/(*x);

    // Devolver el resultado
    return resultado;
}

double FactorAM (const double * const x)
{
    // Definición de variables
    double resultado = 0.0;

    if ( *x > 135 )
        resultado = 0.0;
    else
        resultado = 1 - 0.0032*(*x);

    // Devolver el resultado
    return resultado;
}

double FactorFM (const double * const frecuencia, const double * const v, const double * const td)
{
    // Definición de variables
    int nIndice = 0;
    int nColumna = 0;
    int nLongitud = 18;
    double resultado = 0.0;
    double frecuen[18] = {0.2, 0.5, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16};
    double fm [6][18] = {
        {1, 0.97, 0.94, 0.91, 0.88, 0.84, 0.8, 0.75, 0.7, 0.6, 0.52, 0.45, 0.41, 0.37, 0, 0, 0, 0},
        {1, 0.97, 0.94, 0.91, 0.88, 0.84, 0.8, 0.75, 0.7, 0.6, 0.52, 0.45, 0.41, 0.37, 0.34, 0.31, 0.28, 0},
        {0.95, 0.92, 0.88, 0.84, 0.79, 0.72, 0.6, 0.5, 0.42, 0.35, 0.3, 0.26, 0, 0, 0, 0, 0, 0},
        {0.95, 0.92, 0.88, 0.84, 0.79, 0.72, 0.6, 0.5, 0.42, 0.35, 0.3, 0.26, 0.23, 0.21, 0, 0, 0, 0},
        {0.85, 0.81, 0.75, 0.65, 0.55, 0.45, 0.35, 0.27, 0.22, 0.18, 0, 0, 0, 0, 0, 0, 0, 0},
        {0.85, 0.81, 0.75, 0.65, 0.55, 0.45, 0.35, 0.27, 0.22, 0.18, 0.15, 0.13, 0, 0, 0, 0, 0, 0}
        };

    if ( *td <= 1.0 )
    {
        if ( *v < 75 )
            nColumna = 0;
        else
            nColumna = 1;
    }
    else if ( *td <= 2.0)
    {
        if ( *v < 75 )
            nColumna = 2;
        else
            nColumna = 3;
    }
    else if ( *td <= 8.0)
    {
        if ( *v < 75 )
            nColumna = 4;
        else
            nColumna = 5;
    }

    nIndice = locate(frecuen, nLongitud, *frecuencia);

    if (nIndice == -1 || nIndice == (nLongitud - 2))
        return fm[nColumna][++nIndice];

    resultado = InterpolacionLineal(frecuencia,
                                    &frecuen[nIndice],
                                    &frecuen[nIndice + 1],
                                    &fm[nColumna][nIndice],
                                    &fm[nColumna][nIndice + 1]);

    // Devolver el resultado
    return resultado;
}

double FactorCM (const int * const agarre, const double * const v)
{
    // Definición de variables
    double resultado = 0.0;

    switch(*agarre)
    {
        case 1:     // Agarre bueno
            resultado = 1.0;
            break;
        case 2:     // Agarre regular
            if ( *v < 75 )
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




/* ***************************************************** */
/*                                                       */
/* Funciones auxiliares                                  */
/*                                                       */
/* ***************************************************** */

double round (double a)
{
    return floor(a+0.00001);
}

void freeMemory (void*ptr)
{
    free(ptr);
    return;
}



void hpsort(int n, double ra[])
/*Sorts an array ra[1..n] into ascending numerical order using the Heapsort algorithm. n is
input; ra is replaced on output by its sorted rearrangement.
Numerical Recipes in C, chapter 8.3, page 337*/
{
    int i,ir,j,l;
    double rra;

    if (n < 0) return;  /* Added to check positive value*/
    if (n < 2) return;
    l = (n >> 1);
    ir = n;

    /*The index l will be decremented from its initial value down to 1 during the “hiring” (heap
    creation) phase. Once it reaches 1, the index ir will be decremented from its initial value
    down to 1 during the “retirement-and-promotion” (heap selection) phase.*/
    for (;;)
    {
        if (l > 0)  /* Still in hiring phase.*/
            rra=ra[--l];
        else        /* In retirement-and-promotion phase.*/
        {
            if (--ir == 0) return;  /* Done with the last promotion.*/
            rra=ra[ir];     /* Clear a space at end of array.*/
            ra[ir]=ra[0];   /* Retire the top of the heap into it.*/
        }

        /*Whether in the hiring phase or promotion phase, we
        here set up to sift down element rra to its proper
        level.*/
        i=l;
        j=l+l+1;

        while (j < ir)
        {
            if ( (j+1) < ir && ra[j] < ra[j+1]) j++; /*Compare to the better underling.*/
            if (rra < ra[j])    /* Demote rra.*/
            {
                ra[i]=ra[j];
                i=j;
                j <<= 1;
                j++;
            } else break;   /* Found rra’s level. Terminate the sift-down.*/
        }
        ra[i]=rra;  /* Put rra into its slot.*/
    }
}



void HeapSortIndex(double ra[], int ind[], const int * const n)
/*Sorts an array ra[1..n] into ascending numerical order using the Heapsort algorithm. n is
input; ra is replaced on output by its sorted rearrangement.
This routine's been modified for sorting an index table.
Numerical Recipes in C, chapter 8.3, page 337*/
{
    int i,ir,j,l,iind;  /* Indices */
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
            rra=ra[ind[iind]];  /* Get the comparision value */
            irra=ind[iind];
        }
        else        /* In retirement-and-promotion phase.*/
        {
            if (--ir == 0) return;  /* Done with the last promotion.*/
            iind = ir;
            rra=ra[ind[ir]];    /* Get the comparision value */
            irra = ind[ir];     /* Clear a space at end of array.*/
            ind[ir]=ind[0];     /* Retire the top of the heap into it.*/
        }

        /*Whether in the hiring phase or promotion phase, we
        here set up to sift down element rra to its proper
        level.*/
        i=l;
        j=l+l+1;

        while (j < ir)
        {
            if ( (j+1) < ir && ra[ind[j]] < ra[ind[j+1]])
                j++; /*Compare to the better underling.*/
            if (rra < ra[ind[j]])    /* Demote rra.*/
            {
                ind[i]=ind[j];
                i=j;
                j <<= 1;
                j++;
            } else break;   /* Found rra’s level. Terminate the sift-down.*/
        }
        ind[i]=irra;  /* Put rra into its slot.*/
    }
}
