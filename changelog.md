# ErgoCalculator Changelog

## ErgoCalculator 1.8.2
* Target .NET 9.
* Add custom culture context menu in form plots
* [System.Drawing.Control](https://github.com/dotnet/winforms) control has been updated to version 9.0.0.

## ErgoCalculator 1.8.1
* Splash screen launcher is removed from output directory.
* Implement user selected directory path in open and save dialogs.
* [ScottPlott](https://github.com/ScottPlot/ScottPlot) control has been updated to version 4.1.74.
* [System.Drawing.Control](https://github.com/dotnet/winforms) control has been updated to version 8.0.10.

## ErgoCalculator 1.8.0
* Update data adquisition code in lifting form.
* Allow and handle `NaN` and infinity values in lifting module.
* Update internal validation routines.
* Add unit tests.
* Improve internal code.

## ErgoCalculator 1.7.9
* Modify wrong pushing coefficient.
* Drop revision number in about form.
* Improve internal code.

## ErgoCalculator 1.7.8
* [ScottPlott](https://github.com/ScottPlot/ScottPlot) control has been updated to version 4.1.71.
* Correct minor bugs.
* Improve internal code.
* Improve translation.

## ErgoCalculator 1.7.7
* Improve window app initial position and size logic.
* Improve culture selection logic.
* Change child windows output on culture change.
* Modify and improve code related lifting and LM-MMH models.
* Update code incorporating new `.NET 8` features.
* Refactor and simplify internal code.

## ErgoCalculator 1.7.6
* [ScottPlott](https://github.com/ScottPlot/ScottPlot) control has been updated to version 4.1.70.
* Drop NIOSH lifting model.
* Fully implement ISO 11228-1:2021 model with additional multipliers and reference mass recommendations.

## ErgoCalculator 1.7.5
* Target .NET 8.
* [ScottPlott](https://github.com/ScottPlot/ScottPlot) control has been updated to version 4.1.69.
* Correct exception when entering data for just one task in NIOSH model.

## ErgoCalculator 1.7.4
* Multilanguage support added to Liberty Mutual model.
* Culture formatting to output results.
* Improved duplicate function.
* Bug corrected in zoom factor.

## ErgoCalculator 1.7.3
* Target .NET 7.
* Bug corrected when data was edited in composite indexes.
* Upgraded text format options.
* Tab and multilanguage applied to NIOSH and StrainIndex models.

## ErgoCalculator 1.7.2
* [ScottPlott](https://github.com/ScottPlot/ScottPlot) control has been updated to version 4.1.59.
* Delete dependency on DLLs models. Each model is now implemented as a single class.
* Basic and partial multilanguage UI support (English and Spanish).
* JSON format is used instead of XML to store and retrieve application settings.

## ErgoCalculator 1.7.1
* Update [ScottPlott](https://github.com/ScottPlot/ScottPlot) to version 4.1.58
* Reading the user-input data in the NIOSH model produced undesired effects in some particular cases.

## ErgoCalculator 1.7
* .NET updated to version 6
* [ScottPlott](https://github.com/ScottPlot/ScottPlot) control updated to version 4.1.51
* Updated splash screen

## ErgoCalculator 1.6
* [ScottPlot](https://github.com/ScottPlot/ScottPlot) control has been upgraded from version 4.0.48 to version 4.1.16.
* Fixed an issue in the Liberty Mutual model for sustained force MAL 75% and MAL 90%.

## ErgoCalculator 1.5
* This version extends the number of kernel models with respect to previous v1.0 and offers an improved user interface experience.
* A file format with extension ERGO has been defined for many models, so that output calculations can be stored and later retrieved.
* Serveral examples are included in the folder examples, showing how data for each model should be input.
* The results can be either saved to RTF and TXT format, or simply copied (Ctrl+C) and later pasted.

## ErgoCalculator 1.0
* Initial public compilation.
