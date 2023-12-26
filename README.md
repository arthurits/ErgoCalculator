# <img src="ErgoCalc/images/logo@256.png?raw=true" height="28" width="28"> ErgoCalc
Software implementing ergonomics algorithms (revised strain index, NIOSH lifting index, Liberty Mutual equations, metabolic rate, etc.). Written in .NET 8 (C# WinForms).

<img alt="Software logo" src="ErgoCalc/images/splash.png?raw=true" height="240">

Copyright © 2009-2024 by Arthurits Ltd. No commercial nor profit use allowed. This software is provided only for personal and not-for-profit use.
Download latest release: [![GitHub release (latest by date)](https://img.shields.io/github/v/release/arthurits/ErgoCalculator)](https://github.com/arthurits/ErgoCalculator/releases)

Sponsor this project!

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/paypalme/ArthuritsLtd)

## Use and installation
This download is a portable software. Please follow this instructions:
* Dowload the zip file.
* Unzip the contents to your desired folder.
* Execute (or create a direct link to) `ErgoCalcLauncher.exe`.

## Functionalities
* Work-rest model developed by J. Dul, M. Douwes, M., and P Smitt in 1991 and suggested in [ISO 11226:2000 — Evaluation of static working postures](https://www.iso.org/standard/25573.html)
![WR model](/Media/WRmodel.png?raw=true "WR model")
* [Comprehensive lifting model (CLM)](https://doi.org/10.1080/001401397187748) by Jorge Hidalgo, Ashraf Genaidy, Waldemar Karwowski, Doran Christensen, Ronald Huston, and Jefferey Stambough
* NIOSH lifting equation, inluding LI, CLI, SLI (not yet implemented), and VLI (not yet implemented) indices as suggested in [ISO 11228-1:2003 Ergonomics — Manual handling — Part 1: Lifting and carrying](https://www.iso.org/standard/26520.html)
![NIOSH model](/Media/Niosh.png?raw=true "RSI model")
* Revised strain index, including [RSI](https://doi.org/10.1080/00140139.2016.1237678), [COSI](https://doi.org/10.1080/00140139.2016.1246675), and [CUSI](https://doi.org/10.1080/00140139.2016.1246675) indices
![RSI model](/Media/RevisedStrainIndex.png?raw=true "RSI model")
* OCRA checklist (not yet implemented)
* Metabolic rate (levels I and II) [ISO 8996:2004 Ergonomics of the thermal environment — Determination of metabolic rate](https://www.iso.org/standard/34251.html)
* Thermal comfort, including PMV and PPD according to [ISO 7730:2005 — Ergonomics of the thermal environment](https://www.iso.org/standard/39155.html)
![Thermal comfort](/Media/ThermalComfort.png?raw=true "Thermal comfort")
* Liberty Mutual manual materials handling equations [LM-MMH](https://doi.org/10.1080/00140139.2021.1891297) developed by Jim R. Potvin, Vincent M. Ciriello, Stover H. Snook, Wayne S. Maynard, and George E. Brogmus
![Liberty mutual](/Media/Liberty.png?raw=true "Liberty mutual")

## External dependencies
This project uses controls and routines from the following Gits:
* [ScottPlot](https://github.com/ScottPlot/ScottPlot)
* [CBE Thermal Comfort Tool](https://github.com/CenterForTheBuiltEnvironment/comfort_tool), routines ported from [comfortmodels.js](https://github.com/CenterForTheBuiltEnvironment/comfort_tool/blob/master/static/js/comfortmodels.js)
* [PsychroLib (version 2.5.0)](https://github.com/psychrometrics/psychrolib), [C# port](https://github.com/psychrometrics/psychrolib/blob/master/src/c_sharp/PsychroLib/psychrolib.cs)
