using System;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace ErgoCalc;

public static class StringResources
{
    /// <summary>
    /// Represents a resource manager that provides convinient access to culture-specific resources at run time
    /// </summary>
    public static System.Resources.ResourceManager StringRM { get; set; } = new("ErgoCalc.localization.strings", typeof(FrmMain).Assembly);

    /// <summary>
    /// Specific culture from which the string resources will be retrieved
    /// </summary>
    public static System.Globalization.CultureInfo Culture { get; set; } = System.Globalization.CultureInfo.CurrentCulture;

    public static string GetString(string StringName, System.Globalization.CultureInfo Culture) => StringRM.GetString(StringName, Culture) ?? string.Empty;

    public static string BtnAccept => StringRM.GetString("strBtnAccept", Culture) ?? "&Accept";
    public static string BtnCancel => StringRM.GetString("strBtnCancel", Culture) ?? "&Cancel";
    public static string BtnExample => StringRM.GetString("strBtnExample", Culture) ?? "&Example";
    public static string BtnReset => StringRM.GetString("strBtnReset", Culture) ?? "&Reset";


    public static string Task => StringRM.GetString("strTask", Culture) ?? "Task";
    public static string Subtask => StringRM.GetString("strSubtask", Culture) ?? "Subtask";

    public static string FormTitleUnion => StringRM.GetString("strFormTitleUnion", Culture) ?? " - ";
    public static string FormMainTitle => StringRM.GetString("strFormMainTitle", Culture) ?? "ErgoCalc";
    public static string FormLanguageTitle => StringRM.GetString("strFormLanguageTitle", Culture) ?? "Select culture";
    public static string FormNewTitle => StringRM.GetString("strFormNewTitle", Culture) ?? "New model";
    public static string FormResultsNIOSH => StringRM.GetString("strFormResultsNIOSH", Culture) ?? "NIOSH model results";
    public static string FormResultsCLM => StringRM.GetString("strFormResultsCLM", Culture) ?? "CLM model results";
    public static string FormResultsLiberty => StringRM.GetString("strFormResultsLiberty", Culture) ?? "LM-MMH results";
    public static string FormResultsStrainIndex => StringRM.GetString("strFormResultsStrainIndex", Culture) ?? "Strain Index results";
    public static string FormResultsTC => StringRM.GetString("strFormResultsTC", Culture) ?? "Thermal comfort results";
    public static string FormResultsWR => StringRM.GetString("strFormResultsWR", Culture) ?? "WR model results";
    public static string FormResultsMetabolic => StringRM.GetString("strFormResultsMetabolic", Culture) ?? "Metabolic rate results";
    public static string FormResultsOCRAchecklist => StringRM.GetString("strFormResultsOCRAcheck", Culture) ?? "OCRA checklist model results";

    public static string ToolStripAbout => StringRM.GetString("strToolStripAbout", Culture) ?? "About";
    public static string ToolStripExit => StringRM.GetString("strToolStripExit", Culture) ?? "Exit";
    public static string ToolStripOpen => StringRM.GetString("strToolStripOpen", Culture) ?? "Open";
    public static string ToolStripSave => StringRM.GetString("strToolStripSave", Culture) ?? "Save";
    public static string ToolStripSaveChart => StringRM.GetString("strToolStripSaveChart", Culture) ?? "Save chart";
    public static string ToolStripNew => StringRM.GetString("strToolStripNew", Culture) ?? "New";
    public static string ToolStripDuplicate => StringRM.GetString("strToolStripDuplicate", Culture) ?? "Duplicate";
    public static string ToolStripEdit => StringRM.GetString("strToolStripEdit", Culture) ?? "Edit data";
    public static string ToolStripSettings => StringRM.GetString("strToolStripSettings", Culture) ?? "Settings";
    public static string ToolStripAddLine => StringRM.GetString("strToolStripAddLine", Culture) ?? "Add line";
    public static string ToolStripRemoveLine => StringRM.GetString("strToolStripRemoveLine", Culture) ?? "Remove line";
    public static string ToolTipAbout => StringRM.GetString("strToolTipAbout", Culture) ?? "About this software";
    public static string ToolTipExit => StringRM.GetString("strToolTipExit", Culture) ?? "Exit the application";
    public static string ToolTipOpen => StringRM.GetString("strToolTipOpen", Culture) ?? "Open data-file model from disk";
    public static string ToolTipSave => StringRM.GetString("strToolTipSave", Culture) ?? "Save data and results to disk";
    public static string ToolTipSaveChart => StringRM.GetString("strToolTipSaveChart", Culture) ?? "Save chart picture to disk";
    public static string ToolTipNew => StringRM.GetString("strToolTipNew", Culture) ?? "Start a new ergonomics model";
    public static string ToolTipDuplicate => StringRM.GetString("strToolTipDuplicate", Culture) ?? "Duplicate the current model";
    public static string ToolTipEdit => StringRM.GetString("strToolTipEdit", Culture) ?? "Edit current model's data";
    public static string ToolTipSettings => StringRM.GetString("strToolTipSettings", Culture) ?? "Settings for models, data, and UI";
    public static string ToolTipAddLine => StringRM.GetString("strToolTipAddLine", Culture) ?? "Add line to the current plot";
    public static string ToolTipRemoveLine => StringRM.GetString("strToolTipRemoveLine", Culture) ?? "Remove line from the current plot";
    public static string ToolTipUILanguage => StringRM.GetString("strToolTipUILanguage", Culture) ?? "User interface language";
    public static string ToolTipFont => StringRM.GetString("strToolTipFont", Culture) ?? "Select font";
    public static string ToolTipFontColor => StringRM.GetString("strToolTipFontColor", Culture) ?? "Select font color";
    public static string ToolTipWordWrap => StringRM.GetString("strToolTipWordWrap", Culture) ?? "Wrap words to the beginning of the next line";
    public static string ToolTipBackColor => StringRM.GetString("strToolTipBackColor", Culture) ?? "Select text backcolor";
    public static string ToolTipZoom => StringRM.GetString("strToolTipZoom", Culture) ?? "Select zoom factor";

    public static string MenuMainFile => StringRM.GetString("strMenuMainFile", Culture) ?? "&File";
    public static string MenuMainFileNew => StringRM.GetString("strMenuMainFileNew", Culture) ?? "&New...";
    public static string MenuMainFileExit => StringRM.GetString("strMenuMainFileExit", Culture) ?? "&Exit...";
    public static string MenuMainWindow => StringRM.GetString("strMenuMainWindow", Culture) ?? "&Window";
    public static string MenuMainHelpAbout => StringRM.GetString("strMenuMainHelpAbout", Culture) ?? "&About...";
    public static string MenuMainHelp => StringRM.GetString("strMenuMainHelp", Culture) ?? "&Help";

    // Form Language
    public static string RadCurrentCulture => StringRM.GetString("strRadCurrentCulture", Culture) ?? "Current culture formatting";
    public static string RadInvariantCulture => StringRM.GetString("strRadInvariantCulture", Culture) ?? "Invariant culture formatting";
    public static string RadUserCulture => StringRM.GetString("strRadUserCulture", Culture) ?? "Select culture";

    // Form New
    public static string LblSelect => StringRM.GetString("strLblSelect", Culture) ?? "Please, select a model to start working with:";
    public static string RadModelWR => StringRM.GetString("strRadModelWR", Culture) ?? "WorkRest model (1991)";
    public static string RadModelCLM => StringRM.GetString("strRadModelCLM", Culture) ?? "Comprehensive lifting model (CLM)";
    public static string RadModelNIOSH => StringRM.GetString("strRadModelNIOSH", Culture) ?? "NIOSH lifting equation (LI, CLI, SLI, VLI)";
    public static string RadModelStrain => StringRM.GetString("strRadModelStrain", Culture) ?? "Revised strain index (RSI, COSI, CUSI)";
    public static string RadModelOCRA => StringRM.GetString("strRadModelOCRA", Culture) ?? "OCRA checklist";
    public static string RadModelMetabolic => StringRM.GetString("strRadModelMetabolic", Culture) ?? "Metabolic rate";
    public static string RadModelThermal => StringRM.GetString("strRadModelThermal", Culture) ?? "Thermal comfort (PMV, PPD)";
    public static string RadModelLiberty => StringRM.GetString("strRadModelLiberty", Culture) ?? "Liberty Mutual manual handling";

    // Form Settings
    public static string FrmSettings => StringRM.GetString("strFrmSettings", Culture) ?? "Settings";
    public static string DlgReset => StringRM.GetString("strDlgReset", Culture) ?? "Do you want to reset all fields" +
            Environment.NewLine + "to their default values?";
    public static string DlgResetTitle => StringRM.GetString("strDlgResetTitle", Culture) ?? "Reset settings?";
    public static string TabPlot => StringRM.GetString("strTabPlot", Culture) ?? "Plotting";
    public static string TabGUI => StringRM.GetString("strTabGUI", Culture) ?? "User interface";
    public static string GrpCulture => StringRM.GetString("strGrpCulture", Culture) ?? "Select language";
    public static string ChkDlgPath => StringRM.GetString("strChkDlgPath", Culture) ?? "Remember open/save dialog previous path";
    public static string LblDataFormat => StringRM.GetString("strLblDataFormat", Culture) ?? "Numeric data-formatting string";

    public static string LblDlgFont => StringRM.GetString("strLblDlgFont", Culture) ?? "Change current font";
    public static string BtnDlgFont => StringRM.GetString("strBtnDlgFont", Culture) ?? "Select";
    public static string GrpFont => StringRM.GetString("strGrpFont", Culture) ?? "Current font";
    public static string LblFontName => StringRM.GetString("strLblFontName", Culture) ?? "Font {0}, size {1}";
    public static string LblFontStyle => StringRM.GetString("strLblFontStyle", Culture) ?? "Font style: {0}";
    //public static string LblFontSize => StringRM.GetString("strLblFontSize", Culture) ?? "Font size: {0}";
    public static string LblFontColor => StringRM.GetString("strLblFontColor", Culture) ?? "Font color: {0}";
    public static string ChkWordWrap => StringRM.GetString("strChkWordWrap", Culture) ?? "Word wrap";
    public static string LblBackColor => StringRM.GetString("strLblBackColor", Culture) ?? "Back color: {0}";

    // Form Zoom
    public static string FormZoom => StringRM.GetString("strFormZoom", Culture) ?? "Zoom level";
    public static string LblZoomFactor => StringRM.GetString("strLblZoom", Culture) ?? "Set zoom level";

    // Message box
    public static string MsgBoxExit => StringRM.GetString("strMsgBoxExit", Culture) ?? "Are you sure you want to exit" + Environment.NewLine + "the application?";
    public static string MsgBoxExitTitle => StringRM.GetString("strMsgBoxExitTitle", Culture) ?? "Exit?";
}
