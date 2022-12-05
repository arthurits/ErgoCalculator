using ErgoCalc.Models.LibertyMutual;
using Microsoft.VisualBasic.Devices;
using ScottPlot.Drawing.Colormaps;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Windows.Forms;
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
    public static string Subtask => StringRM.GetString("strSubTask", Culture) ?? "Subtask";

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

    // NIOSH lifting model
    public static string NIOSH_AM => StringRM.GetString("strNIOSH_AM", Culture) ?? "Twisting angle multiplier (AM):";
    public static string NIOSH_Angle => StringRM.GetString("strNIOSH_Angle", Culture) ?? "Twisting angle (°):";
    public static string NIOSH_CM => StringRM.GetString("strNIOSH_CM", Culture) ?? "Coupling multiplier (CM):";
    public static string NIOSH_Coupling => StringRM.GetString("strNIOSH_Coupling", Culture) ?? "Coupling:";
    public static string NIOSH_Data => StringRM.GetString("strNIOSH_Data", Culture) ?? "Initial data";
    public static string NIOSH_DM => StringRM.GetString("strNIOSH_DM", Culture) ?? "Distance multiplier (DM):";
    public static string NIOSH_Duration => StringRM.GetString("strNIOSH_Duration", Culture) ?? "Task duration (hours):";
    public static string NIOSH_Equation => StringRM.GetString("strNIOSH_Equation", Culture) ?? "The NIOSH lifting index is computed as follows:";
    public static string NIOSH_EquationIndex => StringRM.GetString("strNIOSH_EquationIndex", Culture) ?? "Index";
    public static string NIOSH_EquationWeight => StringRM.GetString("strNIOSH_EquationWeight", Culture) ?? "Weight";
    public static string NIOSH_FM => StringRM.GetString("strNIOSH_FM", Culture) ?? "Frequency multiplier (FM):";
    public static string NIOSH_FMa => StringRM.GetString("strNIOSH_FMa", Culture) ?? "Frequency A multiplier (FMa):";
    public static string NIOSH_FMb => StringRM.GetString("strNIOSH_FMb", Culture) ?? "Frequency B multiplier (FMb):";
    public static string NIOSH_Frequency => StringRM.GetString("strNIOSH_Frequency", Culture) ?? "Lifting frequency (times / min):";
    public static string NIOSH_FrequencyA => StringRM.GetString("strNIOSH_FrequencyA", Culture) ?? "Lifting frequency A (times / min):";
    public static string NIOSH_FrequencyB => StringRM.GetString("strNIOSH_FrequencyB", Culture) ?? "Lifting frequency B (times / min):";
    public static string NIOSH_HM => StringRM.GetString("strNIOSH_HM", Culture) ?? "Horizontal multiplier (HM):";
    public static string NIOSH_HorizontalD => StringRM.GetString("strNIOSH_HorizontalD", Culture) ?? "Horizontal distance (cm):";
    public static string NIOSH_IF => StringRM.GetString("strNIOSH_IF", Culture) ?? "Lifting index (IF):";
    public static string NIOSH_Index => StringRM.GetString("strNIOSH_Index", Culture) ?? "The NIOSH lifting index is:";
    public static string NIOSH_LC => StringRM.GetString("strNIOSH_LC", Culture) ?? "Lifting constant (LC):";
    public static string NIOSH_LI => StringRM.GetString("strNIOSH_LI", Culture) ?? "Lifting index (LI):";
    public static string NIOSH_Multipliers => StringRM.GetString("strNIOSH_Multipliers", Culture) ?? "Multipliers";
    public static string NIOSH_SubtasksOrder => StringRM.GetString("strNIOSH_Order", Culture) ?? "Subtask order:";
    public static string NIOSH_Results => StringRM.GetString("strNIOSH_Results", Culture) ?? "These are the results obtained from the NIOSH lifting model:";
    public static string NIOSH_VerticalD => StringRM.GetString("strNIOSH_VerticalD", Culture) ?? "Vertical distance (cm):";
    public static string NIOSH_VerticalT => StringRM.GetString("strNIOSH_VerticalT", Culture) ?? "Vertical travel distance (cm):";
    public static string NIOSH_VM => StringRM.GetString("strNIOSH_VM", Culture) ?? "Vertical multiplier (VM):";
    public static string NIOSH_Weight => StringRM.GetString("strNIOSH_Weight", Culture) ?? "Weight lifted (kg):";
    public static string[] NIOSH_RowHeaders => new[] {
        NIOSH_Data,
        NIOSH_Weight,
        NIOSH_HorizontalD,
        NIOSH_VerticalD,
        NIOSH_VerticalT,
        NIOSH_Frequency,
        NIOSH_FrequencyA,
        NIOSH_FrequencyB,
        NIOSH_Duration,
        NIOSH_Angle,
        NIOSH_Coupling,
        NIOSH_Multipliers,
        NIOSH_LC,
        NIOSH_HM,
        NIOSH_VM,
        NIOSH_DM,
        NIOSH_FM,
        NIOSH_FMa,
        NIOSH_FMb,
        NIOSH_AM,
        NIOSH_CM,
        NIOSH_IF,
        NIOSH_LI,
        NIOSH_SubtasksOrder
    };
    public static string[] NIOSH_ColumnHeaders => new[]
    {
        Task,
        Subtask
    };
    public static string[] NIOSH_ResultsHeaders => new[]
    {
        Task,
        Subtask,
        NIOSH_Results,
        NIOSH_Data,
        NIOSH_Weight,
        NIOSH_HorizontalD,
        NIOSH_VerticalD,
        NIOSH_VerticalT,
        NIOSH_Frequency,
        NIOSH_FrequencyA,
        NIOSH_FrequencyB,
        NIOSH_Duration,
        NIOSH_Angle,
        NIOSH_Coupling,
        NIOSH_Multipliers,
        NIOSH_LC,
        NIOSH_HM,
        NIOSH_VM,
        NIOSH_DM,
        NIOSH_FM,
        NIOSH_FMa,
        NIOSH_FMb,
        NIOSH_AM,
        NIOSH_CM,
        NIOSH_IF,
        NIOSH_LI,
        NIOSH_SubtasksOrder,
        NIOSH_Equation,
        NIOSH_EquationIndex,
        NIOSH_EquationWeight,
        NIOSH_Index
    };

    // Strain index
    public static string StrainIndex_COSI => StringRM.GetString("strStrainIndex_COSI", Culture) ?? "The COSI index is:";
    public static string StrainIndex_COSIequation => StringRM.GetString("strStrainIndex_COSIequation", Culture) ?? "The COSI index is computed as follows:";
    public static string StrainIndex_CUSI => StringRM.GetString("strStrainIndex_CUSI", Culture) ?? "The CUSI index is:";
    public static string StrainIndex_CUSIequation => StringRM.GetString("strStrainIndex_CUSIequation", Culture) ?? "The CUSI index is computed as follows:";
    public static string StrainIndex_Data => StringRM.GetString("strStrainIndex_Data", Culture) ?? "Initial data";
    public static string StrainIndex_DM => StringRM.GetString("strStrainIndex_DM", Culture) ?? "Duration multiplier";
    public static string StrainIndex_Duration => StringRM.GetString("strStrainIndex_Duration", Culture) ?? "Duration per exertion (s)";
    public static string StrainIndex_Efforts => StringRM.GetString("strStrainIndex_Efforts", Culture) ?? "Efforts per minute";
    public static string StrainIndex_EffortsA => StringRM.GetString("strStrainIndex_EffortsA", Culture) ?? "Efforts per minute A";
    public static string StrainIndex_EffortsB => StringRM.GetString("strStrainIndex_EffortsB", Culture) ?? "Efforts per minute B";
    public static string StrainIndex_EM => StringRM.GetString("strStrainIndex_EM", Culture) ?? "Efforts multiplier";
    public static string StrainIndex_EMa => StringRM.GetString("strStrainIndex_EMa", Culture) ?? "Efforts A multiplier";
    public static string StrainIndex_EMb => StringRM.GetString("strStrainIndex_EMb", Culture) ?? "Efforts B multiplier";
    public static string StrainIndex_HM => StringRM.GetString("strStrainIndex_HM", Culture) ?? "Subtask duration multiplier";
    public static string StrainIndex_HMaTask => StringRM.GetString("strStrainIndex_HMaTask", Culture) ?? "Task duration A multiplier";
    public static string StrainIndex_HMbTask => StringRM.GetString("strStrainIndex_HMbTask", Culture) ?? "Task duration B multiplier";
    public static string StrainIndex_HMTask => StringRM.GetString("strStrainIndex_HMTask", Culture) ?? "Task duration multiplier";
    public static string StrainIndex_Hours => StringRM.GetString("strStrainIndex_Hours", Culture) ?? "Subtask duration per day (h)";
    public static string StrainIndex_HoursTask => StringRM.GetString("strStrainIndex_HoursTask", Culture) ?? "Task duration per day (h)";
    public static string StrainIndex_HoursTaskA => StringRM.GetString("strStrainIndex_HoursTaskA", Culture) ?? "Task duration per day A (h)";
    public static string StrainIndex_HoursTaskB => StringRM.GetString("strStrainIndex_HoursTaskB", Culture) ?? "Task duration per day B (h)";
    public static string StrainIndex_IM => StringRM.GetString("strStrainIndex_IM", Culture) ?? "Intensity multiplier";
    public static string StrainIndex_Intensity => StringRM.GetString("strStrainIndex_Intensity", Culture) ?? "Intensity of exertion";
    public static string StrainIndex_Multipliers => StringRM.GetString("strStrainIndex_Multipliers", Culture) ?? "Multipliers";
    public static string StrainIndex_PM => StringRM.GetString("strStrainIndex_PM", Culture) ?? "Hand/wrist posture mult.";
    public static string StrainIndex_Posture => StringRM.GetString("strStrainIndex_Posture", Culture) ?? "Hand/wrist posture (-f +e)";
    public static string StrainIndex_Results => StringRM.GetString("strStrainIndex_Results", Culture) ?? "These are the results obtained from the Revised Strain Index model:";
    public static string StrainIndex_RSI => StringRM.GetString("strStrainIndex_RSI", Culture) ?? "The RSI index is:";
    public static string StrainIndex_RSIequation => StringRM.GetString("strStrainIndex_RSIequation", Culture) ?? "The RSI index is computed as follows:";
    public static string StrainIndex_SubtasksOrder => StringRM.GetString("strStrainIndex_SubtasksOrder", Culture) ?? "Subtasks order:";
    public static string StrainIndex_SubtasksRSI => StringRM.GetString("strStrainIndex_SubtasksRSI", Culture) ?? "Subtask RSI index:";
    public static string StrainIndex_TasksRSI => StringRM.GetString("strStrainIndex_TasksRSI", Culture) ?? "Task RSI index:";
    public static string StrainIndex_TasksCOSI => StringRM.GetString("strStrainIndex_TasksCOSI", Culture) ?? "Task COSI index:";
    public static string StrainIndex_TasksOrder => StringRM.GetString("strStrainIndex_TasksOrder", Culture) ?? "Task order:";

    public static string[] StrainIndex_RowHeaders => new[]
    {
        StrainIndex_Data,
        StrainIndex_Intensity,
        StrainIndex_Efforts,
        StrainIndex_EffortsA,
        StrainIndex_EffortsB,
        StrainIndex_Duration,
        StrainIndex_Posture,
        StrainIndex_Hours,
        StrainIndex_Multipliers,
        StrainIndex_IM,
        StrainIndex_EM,
        StrainIndex_EMa,
        StrainIndex_EMb,
        StrainIndex_DM,
        StrainIndex_PM,
        StrainIndex_HM,
        StrainIndex_TasksRSI,
        StrainIndex_SubtasksRSI,
        StrainIndex_SubtasksOrder,
        StrainIndex_HoursTask,
        StrainIndex_HoursTaskA,
        StrainIndex_HoursTaskB,
        StrainIndex_HMTask,
        StrainIndex_HMaTask,
        StrainIndex_HMbTask,
        StrainIndex_TasksCOSI,
        StrainIndex_TasksOrder
    };
    public static string[] StrainIndex_ColumnHeaders => new[]
    {
        Task,
        Subtask
    };
    public static string[] StrainIndex_ResultsHeaders => new[]
    {
        Task,                       // 0
        Subtask,
        StrainIndex_Results,
        StrainIndex_Data,
        StrainIndex_Intensity,
        StrainIndex_Efforts,        // 5
        StrainIndex_EffortsA,
        StrainIndex_EffortsB,
        StrainIndex_Duration,
        StrainIndex_Posture,
        StrainIndex_Hours,          // 10
        StrainIndex_Multipliers,
        StrainIndex_IM,
        StrainIndex_EM,
        StrainIndex_EMa,
        StrainIndex_EMb,            // 15
        StrainIndex_DM,
        StrainIndex_PM,
        StrainIndex_HM,
        StrainIndex_TasksRSI,
        StrainIndex_SubtasksRSI,    // 20
        StrainIndex_SubtasksOrder,
        StrainIndex_COSIequation,
        StrainIndex_COSI,
        StrainIndex_RSIequation,
        StrainIndex_RSI,            // 25
        StrainIndex_Data,
        StrainIndex_HoursTask,
        StrainIndex_HoursTaskA,
        StrainIndex_HoursTaskB,
        StrainIndex_Multipliers,    // 30
        StrainIndex_HMTask,
        StrainIndex_HMaTask,
        StrainIndex_HMbTask,
        StrainIndex_TasksCOSI,
        StrainIndex_TasksOrder,     // 35
        StrainIndex_CUSIequation,
        StrainIndex_CUSI
    };
}
