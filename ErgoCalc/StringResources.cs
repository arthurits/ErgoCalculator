﻿namespace ErgoCalc;

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

    // Document type saving
    public static string DocumentTypeLifting => "Lifting model";
    public static string DocumentTypeCLM => "Comprehensive lifting model";
    public static string DocumentTypeLiberty => "LM-MMH model";
    public static string DocumentTypeStrainIndex => "Strain index";
    public static string DocumentTypeTC => "Thermal comfort model";
    public static string DocumentTypeWR => "Work-Rest model";

    public static string Case => StringRM.GetString("strCase", Culture) ?? "Case";
    public static string NumberOfCases => StringRM.GetString("strNumberCases", Culture) ?? "Number of cases";
    public static string Task => StringRM.GetString("strTask", Culture) ?? "Task";
    public static string Tasks => StringRM.GetString("strTasks", Culture) ?? "Tasks";
    public static string Subtask => StringRM.GetString("strSubTask", Culture) ?? "Subtask";
    public static string Subtasks => StringRM.GetString("strSubTasks", Culture) ?? "Subtasks";
    public static string NumberOfTasks => StringRM.GetString("strNumberTasks", Culture) ?? "Number of tasks";
    public static string NumberOfSubtasks => StringRM.GetString("strNumberSubtasks", Culture) ?? "Number of subtasks";
    public static string Gender => StringRM.GetString("strGender", Culture) ?? "Male, Female";
    public static string IndexType => StringRM.GetString("strIndexType", Culture) ?? "Index type";

    public static string FormTitleUnion => StringRM.GetString("strFormTitleUnion", Culture) ?? " - ";
    public static string FormMainTitle => StringRM.GetString("strFormMainTitle", Culture) ?? "ErgoCalc";
    public static string FormLanguageTitle => StringRM.GetString("strFormLanguageTitle", Culture) ?? "Select culture";
    public static string FormNewTitle => StringRM.GetString("strFormNewTitle", Culture) ?? "New model";

    public static string FormDataLifting => StringRM.GetString("strFormDataLifting", Culture) ?? "Lifting and lowering model parameters";
    public static string FormDataCLM => StringRM.GetString("strFormDataCLM", Culture) ?? "CLM model parameters";
    public static string FormDataLiberty => StringRM.GetString("strFormDataLiberty", Culture) ?? "LM-MMH model parameters";
    public static string FormDataStrainIndex => StringRM.GetString("strFormDataStrainIndex", Culture) ?? "Strain Index model parameters";
    public static string FormDataTC => StringRM.GetString("strFormDataTC", Culture) ?? "Thermal comfort model parameters";
    public static string FormDataWR => StringRM.GetString("strFormDataWR", Culture) ?? "WR model parameters";
    public static string FormDataMetabolic => StringRM.GetString("strFormDataMetabolic", Culture) ?? "Metabolic rate model parameters";
    public static string FormDataOCRAchecklist => StringRM.GetString("strFormDataOCRAcheck", Culture) ?? "OCRA checklist model parameters";
    public static string FormResultsLifting => StringRM.GetString("strFormResultsLifting", Culture) ?? "Lifting model results";
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
    public static string RadModelLifting => StringRM.GetString("strRadModelLifting", Culture) ?? "Lifting equation (LI, CLI, SLI, VLI)";
    public static string RadModelStrain => StringRM.GetString("strRadModelStrain", Culture) ?? "Revised strain index (RSI, COSI, CUSI)";
    public static string RadModelOCRA => StringRM.GetString("strRadModelOCRA", Culture) ?? "OCRA checklist";
    public static string RadModelMetabolic => StringRM.GetString("strRadModelMetabolic", Culture) ?? "Metabolic rate";
    public static string RadModelThermal => StringRM.GetString("strRadModelThermal", Culture) ?? "Thermal comfort (PMV, PPD)";
    public static string RadModelLiberty => StringRM.GetString("strRadModelLiberty", Culture) ?? "Liberty Mutual manual materials handling";

    // Form Settings
    public static string FrmSettings => StringRM.GetString("strFrmSettings", Culture) ?? "Settings";
    public static string DlgReset => StringRM.GetString("strDlgReset", Culture) ?? "Do you want to reset all fields" +
            Environment.NewLine + "to their default values?";
    public static string DlgResetTitle => StringRM.GetString("strDlgResetTitle", Culture) ?? "Reset settings?";
    public static string TabPlot => StringRM.GetString("strTabPlot", Culture) ?? "Plotting";
    public static string TabGUI => StringRM.GetString("strTabGUI", Culture) ?? "User interface";
    public static string GrpCulture => StringRM.GetString("strGrpCulture", Culture) ?? "Select language";
    public static string ChkDlgPath => StringRM.GetString("strChkDlgPath", Culture) ?? "Remember open/save dialog previous path";
    public static string ChkWindowPos => StringRM.GetString("strChkWindowPos", Culture) ?? "Remember window position and size on startup";
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

    // Lifting model
    public static string Lifting_AM => StringRM.GetString("strLifting_AM", Culture) ?? "Twisting angle multiplier (AM):";
    public static string Lifting_Angle => StringRM.GetString("strLifting_Angle", Culture) ?? "Twisting angle (°):";
    public static string Lifting_CM => StringRM.GetString("strLifting_CM", Culture) ?? "Coupling multiplier (CM):";
    public static string Lifting_Coupling => StringRM.GetString("strLifting_Coupling", Culture) ?? "Coupling:";
    public static string Lifting_CouplingType => StringRM.GetString("strLifting_CouplingType", Culture) ?? "Poor, Fair, Good";
    public static string Lifting_Data => StringRM.GetString("strLifting_Data", Culture) ?? "Initial data";
    public static string Lifting_DM => StringRM.GetString("strLifting_DM", Culture) ?? "Distance multiplier (DM):";
    public static string Lifting_Duration => StringRM.GetString("strLifting_Duration", Culture) ?? "Task duration (hours):";
    public static string Lifting_Equation => StringRM.GetString("strLifting_Equation", Culture) ?? "The lifting index (LI) is computed as follows:";
    public static string Lifting_EquationIndex => StringRM.GetString("strLifting_EquationIndex", Culture) ?? "Index";
    public static string Lifting_EquationWeight => StringRM.GetString("strLifting_EquationWeight", Culture) ?? "Weight";
    public static string Lifting_FM => StringRM.GetString("strLifting_FM", Culture) ?? "Frequency multiplier (FM):";
    public static string Lifting_FMa => StringRM.GetString("strLifting_FMa", Culture) ?? "Frequency A multiplier (FMa):";
    public static string Lifting_FMb => StringRM.GetString("strLifting_FMb", Culture) ?? "Frequency B multiplier (FMb):";
    public static string Lifting_Frequency => StringRM.GetString("strLifting_Frequency", Culture) ?? "Lifting frequency (times / min):";
    public static string Lifting_FrequencyA => StringRM.GetString("strLifting_FrequencyA", Culture) ?? "Lifting frequency A (times / min):";
    public static string Lifting_FrequencyB => StringRM.GetString("strLifting_FrequencyB", Culture) ?? "Lifting frequency B (times / min):";
    public static string Lifting_HM => StringRM.GetString("strLifting_HM", Culture) ?? "Horizontal multiplier (HM):";
    public static string Lifting_HorizontalD => StringRM.GetString("strLifting_HorizontalD", Culture) ?? "Horizontal distance (cm):";
    public static string Lifting_IF => StringRM.GetString("strLifting_IF", Culture) ?? "Freq-indep lifting index (FILI):";
    public static string Lifting_Index => StringRM.GetString("strLifting_Index", Culture) ?? "The lifting index is:";
    public static string Lifting_MassRef => StringRM.GetString("strLifting_MassRef", Culture) ?? "Mass reference (kg):";
    public static string Lifting_LI => StringRM.GetString("strLifting_LI", Culture) ?? "Lifting index (LI):";
    public static string Lifting_Multipliers => StringRM.GetString("strLifting_Multipliers", Culture) ?? "Multipliers";
    public static string Lifting_SubtasksOrder => StringRM.GetString("strLifting_Order", Culture) ?? "Subtask order:";
    public static string Lifting_Results => StringRM.GetString("strLifting_Results", Culture) ?? "These are the results obtained from the ISO 11228-1:2021 norm model:";
    public static string Lifting_VerticalD => StringRM.GetString("strLifting_VerticalD", Culture) ?? "Vertical distance (cm):";
    public static string Lifting_VerticalT => StringRM.GetString("strLifting_VerticalT", Culture) ?? "Vertical travel distance (cm):";
    public static string Lifting_VM => StringRM.GetString("strLifting_VM", Culture) ?? "Vertical multiplier (VM):";
    public static string Lifting_Weight => StringRM.GetString("strLifting_Weight", Culture) ?? "Weight lifted (kg):";
    
    public static string Lifting_OneHanded => StringRM.GetString("strLifting_OneHanded", Culture) ?? "One-handed?";
    public static string Lifting_OneHandedValue => StringRM.GetString("strLifting_OneHandedValue", Culture) ?? "false, true";
    public static string Lifting_OM => StringRM.GetString("strLifting_OM", Culture) ?? "One-handed multiplier (OM):";
    public static string Lifting_TwoPerson => StringRM.GetString("strLifting_TwoPerson", Culture) ?? "Two person?";
    public static string Lifting_PM => StringRM.GetString("strLifting_PM", Culture) ?? "Two person multiplier (PM):";
    public static string Lifting_EM => StringRM.GetString("strLifting_EM", Culture) ?? "Extended time multiplier (EM):";
    public static string Lifting_GenderType => Gender;
    public static string Lifting_Gender => StringRM.GetString("strLifting_Gender", Culture) ?? "Gender:";
    public static string Lifting_Age => StringRM.GetString("strLifting_Age", Culture) ?? "Age:";
    public static string[] Lifting_RowHeaders => [
        Lifting_Data,
        Lifting_Gender,
        Lifting_Age,
        Lifting_Weight,
        Lifting_HorizontalD,
        Lifting_VerticalD,
        Lifting_VerticalT,
        Lifting_Frequency,
        Lifting_FrequencyA,
        Lifting_FrequencyB,
        Lifting_Duration,
        Lifting_Angle,
        Lifting_Coupling,
        Lifting_OneHanded,
        Lifting_TwoPerson,
        Lifting_Multipliers,
        Lifting_MassRef,
        Lifting_HM,
        Lifting_VM,
        Lifting_DM,
        Lifting_FM,
        Lifting_FMa,
        Lifting_FMb,
        Lifting_AM,
        Lifting_CM,
        Lifting_OM,
        Lifting_PM,
        Lifting_EM,
        Lifting_IF,
        Lifting_LI,
        Lifting_SubtasksOrder
    ];
    public static string[] Lifting_ColumnHeaders => [
        $"{Task} A",
        $"{Subtask} A",
        Lifting_CouplingType.Split(", ")[0],
        Lifting_CouplingType.Split(", ")[1],
        Lifting_CouplingType.Split(", ")[2],
        Lifting_OneHandedValue.Split(", ")[0],
        Lifting_OneHandedValue.Split(", ")[1],
        Lifting_GenderType.Split(", ")[0],
        Lifting_GenderType.Split(", ")[1]
    ];
    public static string[] Lifting_ResultsHeaders => [
        Task,                   // 0
        Subtask,
        Lifting_Results,
        Lifting_Data,
        Lifting_Weight,
        Lifting_HorizontalD,      // 5
        Lifting_VerticalD,
        Lifting_VerticalT,
        Lifting_Frequency,
        Lifting_FrequencyA,
        Lifting_FrequencyB,       // 10
        Lifting_Duration,
        Lifting_Angle,
        Lifting_Coupling,
        Lifting_Multipliers,
        Lifting_MassRef,          // 15
        Lifting_HM,
        Lifting_VM,
        Lifting_DM,
        Lifting_FM,
        Lifting_FMa,              // 20
        Lifting_FMb,
        Lifting_AM,
        Lifting_CM,
        Lifting_IF,
        Lifting_LI,               // 25
        Lifting_SubtasksOrder,
        Lifting_Equation,
        Lifting_EquationIndex,
        Lifting_EquationWeight,
        Lifting_Index,            // 30
        Lifting_CouplingType,
        Lifting_OneHanded,
        Lifting_TwoPerson,
        Lifting_OM,
        Lifting_PM,               // 35
        Lifting_EM,
        Lifting_OneHandedValue,
        Lifting_GenderType,
        Lifting_Gender,
        Lifting_Age               // 40
    ];
    public static string[] Lifting_DataInputHeaders => [
        Lifting_Gender,
        Lifting_Age,
        Lifting_Weight,
        Lifting_HorizontalD,
        Lifting_VerticalD,
        Lifting_VerticalT,
        Lifting_Frequency,
        Lifting_Duration,
        Lifting_Angle,
        Lifting_Coupling,
        Lifting_OneHanded,
        Lifting_TwoPerson
    ];

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
    public static string[] StrainIndex_RowHeaders => [
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
    ];
    public static string[] StrainIndex_ColumnHeaders => [
        $"{Task} A",
        $"{Subtask} A"
    ];
    public static string[] StrainIndex_ResultsHeaders => [
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
    ];
    public static string[] StrainIndex_DataInputHeaders => [
        StrainIndex_Intensity,
        StrainIndex_Efforts,
        StrainIndex_Duration,
        StrainIndex_Posture,
        StrainIndex_HoursTask
    ];

    // Liberty Mutual
    public static string LibertyMutual_Data => StringRM.GetString("strLibertyMutual_Data", Culture) ?? "Initial data";
    public static string LibertyMutual_DH => StringRM.GetString("strLibertyMutual_DH", Culture) ?? "Horizontal distance (DH)";
    public static string LibertyMutual_DV => StringRM.GetString("strLibertyMutual_DV", Culture) ?? "Vertical distance (DV)";
    public static string LibertyMutual_F => StringRM.GetString("strLibertyMutual_F", Culture) ?? "Frequency factor (F)";
    public static string LibertyMutual_Frequency => StringRM.GetString("strLibertyMutual_Frequency", Culture) ?? "Frequency (actions/min)";
    public static string LibertyMutual_H => StringRM.GetString("strLibertyMutual_H", Culture) ?? "Horizontal reach (H)";
    public static string LibertyMutual_HandlingType => StringRM.GetString("strLibertyMutual_HandlingType", Culture) ?? "Manual handling type";
    public static string LibertyMutual_HorizontalDistance => StringRM.GetString("strLibertyMutual_HorizontalDistance", Culture) ?? "Horizontal distance DH (m)";
    public static string LibertyMutual_HorizontalReach => StringRM.GetString("strLibertyMutual_HorizontalReach", Culture) ?? "Horizontal reach H (m)";
    public static string LibertyMutual_InitialCV => StringRM.GetString("strLibertyMutual_InitialCV", Culture) ?? "CV initial force";
    public static string LibertyMutual_InitialMAL50 => StringRM.GetString("strLibertyMutual_InitialMAL50", Culture) ?? "MAL initial (kgf) for 50%";
    public static string LibertyMutual_InitialMAL75 => StringRM.GetString("strLibertyMutual_InitialMAL75", Culture) ?? "MAL initial (kgf) for 75%";
    public static string LibertyMutual_InitialMAL90 => StringRM.GetString("strLibertyMutual_InitialMAL90", Culture) ?? "MAL initial (kgf) for 90%";
    public static string LibertyMutual_RL => StringRM.GetString("strLibertyMutual_RL", Culture) ?? "Reference load (RL)";
    public static string LibertyMutual_MAL => StringRM.GetString("strLibertyMutual_MAL", Culture) ?? "Maximum acceptable limit";
    public static string LibertyMutual_Multipliers => StringRM.GetString("strLibertyMutual_Multipliers", Culture) ?? "Scale factors";
    public static string LibertyMutual_Results => StringRM.GetString("strLibertyMutual_Results", Culture) ?? "These are the results from the Liberty Mutual manual materials handling equations";
    public static string LibertyMutual_Gender => StringRM.GetString("strLibertyMutual_Sex", Culture) ?? "Gender";
    public static string LibertyMutual_SustainedCV => StringRM.GetString("strLibertyMutual_SustainedCV", Culture) ?? "CV sustained force";
    public static string LibertyMutual_SustainedDH => StringRM.GetString("strLibertyMutual_SustainedDH", Culture) ?? "Sustained DH factor";
    public static string LibertyMutual_SustainedF => StringRM.GetString("strLibertyMutual_SustainedF", Culture) ?? "Sustained F factor";
    public static string LibertyMutual_SustainedRL => StringRM.GetString("strLibertyMutual_SustainedRL", Culture) ?? "Sustained RL factor";
    public static string LibertyMutual_SustainedMAL50 => StringRM.GetString("strLibertyMutual_SustainedMAL50", Culture) ?? "MAL sustained (kgf) for 50%";
    public static string LibertyMutual_SustainedMAL75 => StringRM.GetString("strLibertyMutual_SustainedMAL75", Culture) ?? "MAL sustained (kgf) for 75%";
    public static string LibertyMutual_SustainedMAL90 => StringRM.GetString("strLibertyMutual_SustainedMAL90", Culture) ?? "MAL sustained (kgf) for 90%";
    public static string LibertyMutual_SustainedV => StringRM.GetString("strLibertyMutual_SustainedV", Culture) ?? "Sustained V factor";
    public static string LibertyMutual_V => StringRM.GetString("strLibertyMutual_V", Culture) ?? "Vertical height (V)";
    public static string LibertyMutual_VerticalDistance => StringRM.GetString("strLibertyMutual_VerticalDistance", Culture) ?? "Vertical distance DV (m)";
    public static string LibertyMutual_VerticalHeight => StringRM.GetString("strLibertyMutual_VerticalHeight", Culture) ?? "Vertical height V (m)";
    public static string LibertyMutual_VerticalRange => StringRM.GetString("strLibertyMutual_VerticalRange", Culture) ?? "Vertical range VRM (m)";
    public static string LibertyMutual_VRM => StringRM.GetString("strLibertyMutual_VRM", Culture) ?? "Vertical range (VRM)";
    public static string LibertyMutual_WeightCV => StringRM.GetString("strLibertyMutual_WeightCV", Culture) ?? "CV weight";
    public static string LibertyMutual_WeightMAL50 => StringRM.GetString("strLibertyMutual_WeightMAL50", Culture) ?? "MAL weight (kg) for 50%";
    public static string LibertyMutual_WeightMAL75 => StringRM.GetString("strLibertyMutual_WeightMAL75", Culture) ?? "MAL weight (kg) for 75%";
    public static string LibertyMutual_WeightMAL90 => StringRM.GetString("strLibertyMutual_WeightMAL90", Culture) ?? "MAL weight (kg) for 90%";
    public static string LibertyMutual_Empty => StringRM.GetString("strLibertyMutual_Empty", Culture) ?? "------";
    public static string LibertyMutual_TaskType => StringRM.GetString("strLibertyMutual_TaskType", Culture) ?? "Carrying, Lifting, Lowering, Pulling, Pushing";
    public static string LibertyMutual_GenderType => Gender;
    public static string LibertyMutual_PlotInitialF => StringRM.GetString("strLibertyMutual_PlotInitialF", Culture) ?? "Initial force / kg-f";
    public static string LibertyMutual_PlotSustainedF => StringRM.GetString("strLibertyMutual_PlotSustainedF", Culture) ?? "Sustained force / kg-f";
    public static string LibertyMutual_PlotWeight => StringRM.GetString("strLibertyMutual_PlotWeight", Culture) ?? "Weight / kg";
    public static string[] LibertyMutual_RowHeaders => [
        LibertyMutual_Data,
        LibertyMutual_HandlingType,
        LibertyMutual_HorizontalReach,
        LibertyMutual_VerticalRange,
        LibertyMutual_HorizontalDistance,
        LibertyMutual_VerticalDistance,
        LibertyMutual_VerticalHeight,
        LibertyMutual_Frequency,
        LibertyMutual_Gender,
        LibertyMutual_Multipliers,
        LibertyMutual_RL,
        LibertyMutual_H,
        LibertyMutual_VRM,
        LibertyMutual_DH,
        LibertyMutual_DV,
        LibertyMutual_V,
        LibertyMutual_F,
        LibertyMutual_SustainedRL,
        LibertyMutual_SustainedDH,
        LibertyMutual_SustainedV,
        LibertyMutual_SustainedF,
        LibertyMutual_MAL,
        LibertyMutual_InitialCV,
        LibertyMutual_InitialMAL50,
        LibertyMutual_InitialMAL75,
        LibertyMutual_InitialMAL90,
        LibertyMutual_SustainedCV,
        LibertyMutual_SustainedMAL50,
        LibertyMutual_SustainedMAL75,
        LibertyMutual_SustainedMAL90,
        LibertyMutual_WeightCV,
        LibertyMutual_WeightMAL50,
        LibertyMutual_WeightMAL75,
        LibertyMutual_WeightMAL90
    ];
    public static string[] LibertyMutual_ColumnHeaders => [
        $"{Task} A",
        LibertyMutual_TaskType.Split(", ")[0],
        LibertyMutual_TaskType.Split(", ")[1],
        LibertyMutual_TaskType.Split(", ")[2],
        LibertyMutual_TaskType.Split(", ")[3],
        LibertyMutual_TaskType.Split(", ")[4],
        LibertyMutual_GenderType.Split(", ")[0],
        LibertyMutual_GenderType.Split(", ")[1]
    ];
    public static string[] LibertyMutual_ResultsHeaders => [
        Task,                               // 0
        LibertyMutual_Results,
        LibertyMutual_Data,
        LibertyMutual_HandlingType,
        LibertyMutual_HorizontalReach,
        LibertyMutual_VerticalRange,        // 5
        LibertyMutual_HorizontalDistance,
        LibertyMutual_VerticalDistance,
        LibertyMutual_VerticalHeight,
        LibertyMutual_Frequency,
        LibertyMutual_Gender,                  // 10
        LibertyMutual_Multipliers,
        LibertyMutual_RL,
        LibertyMutual_H,
        LibertyMutual_VRM,
        LibertyMutual_DH,                   // 15
        LibertyMutual_DV,
        LibertyMutual_V,
        LibertyMutual_F,
        LibertyMutual_SustainedRL,
        LibertyMutual_SustainedDH,          // 20
        LibertyMutual_SustainedV,
        LibertyMutual_SustainedF,
        LibertyMutual_MAL,
        LibertyMutual_InitialCV,
        LibertyMutual_InitialMAL50,         // 25
        LibertyMutual_InitialMAL75,
        LibertyMutual_InitialMAL90,
        LibertyMutual_SustainedCV,
        LibertyMutual_SustainedMAL50,
        LibertyMutual_SustainedMAL75,       // 30
        LibertyMutual_SustainedMAL90,
        LibertyMutual_WeightCV,
        LibertyMutual_WeightMAL50,
        LibertyMutual_WeightMAL75,
        LibertyMutual_WeightMAL90,          // 35
        LibertyMutual_Empty,
        LibertyMutual_GenderType,
        LibertyMutual_TaskType
    ];
    public static string[] LibertyMutual_DataInputHeaders => [
        LibertyMutual_HandlingType,
        LibertyMutual_HorizontalReach,
        LibertyMutual_VerticalRange,
        LibertyMutual_HorizontalDistance,
        LibertyMutual_VerticalDistance,
        LibertyMutual_VerticalHeight,
        LibertyMutual_Frequency,
        LibertyMutual_Gender
    ];

    // Thermal comfort
    public static string ThermalComfort_AirTemp => StringRM.GetString("strThermalComfort_AirTemp", Culture) ?? "Air temperature (°C)";
    public static string ThermalComfort_AirVel => StringRM.GetString("strThermalComfort_AirVel", Culture) ?? "Air velocity (m/s)";
    public static string ThermalComfort_Clothing => StringRM.GetString("strThermalComfort_Clothing", Culture) ?? "Clothing (clo)";
    public static string ThermalComfort_Data => StringRM.GetString("strThermalComfort_Data", Culture) ?? "Initial data";
    public static string ThermalComfort_ExternalWork => StringRM.GetString("strThermalComfort_ExternalWork", Culture) ?? "External work (met)";
    public static string ThermalComfort_LossConvection => StringRM.GetString("strThermalComfort_LossConvection", Culture) ?? "Heat loss by convection";
    public static string ThermalComfort_LossDry => StringRM.GetString("strThermalComfort_LossDry", Culture) ?? "Dry respiration heat loss";
    public static string ThermalComfort_LossRadiation => StringRM.GetString("strThermalComfort_LossRadiation", Culture) ?? "Heat loss by radiation";
    public static string ThermalComfort_LossRespiration => StringRM.GetString("strThermalComfort_LossRespiration", Culture) ?? "Latent respiration heat loss";
    public static string ThermalComfort_LossSkin => StringRM.GetString("strThermalComfort_LossSkin", Culture) ?? "Heat loss diff. through skin";
    public static string ThermalComfort_LossSweating => StringRM.GetString("strThermalComfort_LossSweating", Culture) ?? "Heat loss by sweating";
    public static string ThermalComfort_MetabolicRate => StringRM.GetString("strThermalComfort_MetabolicRate", Culture) ?? "Metabolic rate (met)";
    public static string ThermalComfort_Multipliers => StringRM.GetString("strThermalComfort_Multipliers", Culture) ?? "Heat loss factors";
    public static string ThermalComfort_PMVindex => StringRM.GetString("strThermalComfort_PMVindex", Culture) ?? "The PMV index is:";
    public static string ThermalComfort_PPDindex => StringRM.GetString("strThermalComfort_PPDindex", Culture) ?? "The PPD index is:";
    public static string ThermalComfort_RadiantTemp => StringRM.GetString("strThermalComfort_RadiantTemp", Culture) ?? "Radiant temperature (°C)";
    public static string ThermalComfort_RelHumidity => StringRM.GetString("strThermalComfort_RelHumidity", Culture) ?? "Relative humidity (%)";
    public static string ThermalComfort_Results => StringRM.GetString("strThermalComfort_Results", Culture) ?? "These are the results for the PMV and the PPD indexes according to ISO 7730:";
    public static string ThermalComfort_PlotAbscissa => StringRM.GetString("strThermalComfort_PlotAbscissa", Culture) ?? "Air temperature (°C)";
    public static string ThermalComfort_PlotOrdinate => StringRM.GetString("strThermalComfort_PlotOrdinate", Culture) ?? "g water / kg dry air";
    public static string ThermalComfort_PlotLegend => StringRM.GetString("strThermalComfort_PlotLegend", Culture) ?? "Convection, Radiation, Dry resp., Latent resp., Sweating, Skin";
    public static string[] ThermalComfort_RowHeaders => [
        ThermalComfort_Data,
        ThermalComfort_AirTemp,
        ThermalComfort_RadiantTemp,
        ThermalComfort_AirVel,
        ThermalComfort_RelHumidity,
        ThermalComfort_Clothing,
        ThermalComfort_MetabolicRate,
        ThermalComfort_ExternalWork,
        ThermalComfort_Multipliers,
        ThermalComfort_LossSkin,
        ThermalComfort_LossSweating,
        ThermalComfort_LossRespiration,
        ThermalComfort_LossDry,
        ThermalComfort_LossRadiation,
        ThermalComfort_LossConvection,
        ThermalComfort_PMVindex,
        ThermalComfort_PPDindex
    ];
    public static string[] ThermalComfort_ColumnHeaders => [
        $"{Case} A"
    ];
    public static string[] ThermalComfort_ResultsHeaders => [
        Case,                           // 0
        ThermalComfort_Results,
        ThermalComfort_Data,
        ThermalComfort_AirTemp,
        ThermalComfort_RadiantTemp,
        ThermalComfort_AirVel,          // 5
        ThermalComfort_RelHumidity,
        ThermalComfort_Clothing,
        ThermalComfort_MetabolicRate,
        ThermalComfort_ExternalWork,
        ThermalComfort_Multipliers,     // 10
        ThermalComfort_LossSkin,
        ThermalComfort_LossSweating,
        ThermalComfort_LossRespiration,
        ThermalComfort_LossDry,
        ThermalComfort_LossRadiation,   // 15
        ThermalComfort_LossConvection,
        ThermalComfort_PMVindex,
        ThermalComfort_PPDindex
    ];
    public static string[] ThermalComfort_DataInputHeaders => [
        ThermalComfort_AirTemp,
        ThermalComfort_RadiantTemp,
        ThermalComfort_AirVel,
        ThermalComfort_RelHumidity,
        ThermalComfort_Clothing,
        ThermalComfort_MetabolicRate,
        ThermalComfort_ExternalWork
    ];

    // Comprehensive lifting index
    public static string CLM_Age => StringRM.GetString("strCLM_Age", Culture) ?? "Age (years)";
    public static string CLM_AM => StringRM.GetString("strCLM_AM", Culture) ?? "Twisting angle multiplier (AM)";
    public static string CLM_Angle => StringRM.GetString("strCLM_Angle", Culture) ?? "Twisting angle (°)";
    public static string CLM_BaseWeight => StringRM.GetString("strCLM_BaseWeight", Culture) ?? "Base weight (kg)";
    public static string CLM_BM => StringRM.GetString("strCLM_BM", Culture) ?? "Body weight multiplier (BM)";
    public static string CLM_BodyWeight => StringRM.GetString("strCLM_BodyWeight", Culture) ?? "Body weight (kg)";
    public static string CLM_CM => StringRM.GetString("strCLM_CM", Culture) ?? "Coupling multiplier (CM)";
    public static string CLM_Coupling => StringRM.GetString("strCLM_Coupling", Culture) ?? "Coupling";
    public static string CLM_Data => StringRM.GetString("strCLM_Data", Culture) ?? "Initial data";
    public static string CLM_DM => StringRM.GetString("strCLM_DM", Culture) ?? "Task duration multiplier (HM)";
    public static string CLM_FM => StringRM.GetString("strCLM_FM", Culture) ?? "Frequency multiplier (FM)";
    public static string CLM_Frequency => StringRM.GetString("strCLM_Frequency", Culture) ?? "Lifting frequency (actions/min)";
    public static string CLM_Gender => StringRM.GetString("strCLM_Gender", Culture) ?? "Gender";
    public static string CLM_HM => StringRM.GetString("strCLM_HM", Culture) ?? "Horizontal multiplier (HM)";
    public static string CLM_HorizontalD => StringRM.GetString("strCLM_HorizontalD", Culture) ?? "Horizontal distance (cm)";
    public static string CLM_HoursTask => StringRM.GetString("strCLM_HoursTask", Culture) ?? "Task duration (h)";
    public static string CLM_LSIindex => StringRM.GetString("strCLM_LSIindex", Culture) ?? "The LSI index is:";
    public static string CLM_Multipliers => StringRM.GetString("strCLM_Multipliers", Culture) ?? "Multipliers";
    public static string CLM_PopPercentage => StringRM.GetString("strCLM_PopPercentage", Culture) ?? "Population percentage";
    public static string CLM_Results => StringRM.GetString("strCLM_Results", Culture) ?? "These are the results from the Comprehensive Lifting Model:";
    public static string CLM_Temperature => StringRM.GetString("strCLM_Temperature", Culture) ?? "WBGT temperature (°C)";
    public static string CLM_TM => StringRM.GetString("strCLM_TM", Culture) ?? "WBGT temperature multiplier (TM)";
    public static string CLM_VDM => StringRM.GetString("strCLM_VDM", Culture) ?? "Vertical distance multiplier (VDM)";
    public static string CLM_VerticalD => StringRM.GetString("strCLM_VerticalD", Culture) ?? "Vertical distance (cm)";
    public static string CLM_VerticalH => StringRM.GetString("strCLM_VerticalH", Culture) ?? "Vertical height (cm)";
    public static string CLM_VHM => StringRM.GetString("strCLM_VM", Culture) ?? "Vertical height multiplier (VHM)";
    public static string CLM_Weight => StringRM.GetString("strCLM_Weight", Culture) ?? "Weight lifted (kg)";
    public static string CLM_YM => StringRM.GetString("strCLM_YM", Culture) ?? "Age multiplier (YM)";
    public static string CLM_CouplingType => StringRM.GetString("strCLM_CouplingType", Culture) ?? "No handles, Poor, Good";
    public static string CLM_GenderType => Gender;
    public static string[] CLM_RowHeaders => [
        CLM_Data,
        CLM_Gender,
        CLM_Weight,
        CLM_HorizontalD,
        CLM_VerticalH,
        CLM_VerticalD,
        CLM_Frequency,
        CLM_HoursTask,
        CLM_Angle,
        CLM_Coupling,
        CLM_Temperature,
        CLM_Age,
        CLM_BodyWeight,
        CLM_Multipliers,
        CLM_HM,
        CLM_VHM,
        CLM_VDM,
        CLM_FM,
        CLM_DM,
        CLM_AM,
        CLM_CM,
        CLM_TM,
        CLM_YM,
        CLM_BM,
        CLM_BaseWeight,
        CLM_PopPercentage
    ];  // This is used for tabs calculation
    public static string[] CLM_ColumnHeaders => [
        $"{Case} A",
        CLM_CouplingType.Split(", ")[0],
        CLM_CouplingType.Split(", ")[1],
        CLM_CouplingType.Split(", ")[2],
        CLM_GenderType.Split(", ")[0],
        CLM_GenderType.Split(", ")[1]
    ];
    public static string[] CLM_ResultsHeaders => [
        Task,               // 0
        CLM_Results,
        CLM_Data,
        CLM_Gender,
        CLM_Weight,
        CLM_HorizontalD,    // 5
        CLM_VerticalH,
        CLM_VerticalD,
        CLM_Frequency,
        CLM_HoursTask,
        CLM_Angle,          // 10
        CLM_Coupling,
        CLM_Temperature,
        CLM_Age,
        CLM_BodyWeight,
        CLM_Multipliers,    // 15
        CLM_HM,
        CLM_VHM,
        CLM_VDM,
        CLM_FM,
        CLM_DM,             // 20
        CLM_AM,
        CLM_CM,
        CLM_TM,
        CLM_YM,
        CLM_BM,             // 25
        CLM_BaseWeight,
        CLM_PopPercentage,
        CLM_LSIindex,
        CLM_CouplingType,
        CLM_GenderType      // 30
    ];
    public static string[] CLM_DataInputHeaders => [
        CLM_Gender,
        CLM_Weight,
        CLM_HorizontalD,
        CLM_VerticalH,
        CLM_VerticalD,
        CLM_Frequency,
        CLM_HoursTask,
        CLM_Angle,
        CLM_Coupling,
        CLM_Temperature,
        CLM_Age,
        CLM_BodyWeight
    ];


    // Work-rest static postures
    public static string WR_SeriesName => StringRM.GetString("WR_SeriesName", Culture) ?? "Name";
    public static string WR_MVC => StringRM.GetString("WR_MVC", Culture) ?? "Max. voluntary contraction (%)";
    public static string WR_MHT => StringRM.GetString("WR_MHT", Culture) ?? "Maximum holding time (min)";
    public static string WR_WorkingTimes => StringRM.GetString("WR_WorkingTimes", Culture) ?? "Working times (min)";
    public static string WR_RestTimes => StringRM.GetString("WR_RestTimes", Culture) ?? "Rest times (min)";
    public static string WR_NumberCycles => StringRM.GetString("WR_NumberCycles", Culture) ?? "Number of cycles";
    public static string WR_Step => StringRM.GetString("WR_Step", Culture) ?? "Numeric step";

    public static string[] WR_ColumnHeaders => [
        $"{Case} A"
    ];
    public static string[] WR_DataInputHeaders => [
        WR_SeriesName,
        WR_MVC,
        WR_MHT,
        WR_WorkingTimes,
        WR_RestTimes,
        WR_NumberCycles,
        WR_Step
    ];


    // OCRA check
    // https://kuliahdianmardi.files.wordpress.com/2016/04/ocra-1.pdf
    // https://www.ergonautas.upv.es/metodos/ocra/ocra-ayuda.php
    public static string OCRA_ShiftDuration => StringRM.GetString("OCRA_ShiftDuration", Culture) ?? "Shift duration (min)";
    public static string OCRA_OfficialPauses => StringRM.GetString("OCRA_OfficialPauses", Culture) ?? "Official pauses (min)";
    public static string OCRA_LunchBreak => StringRM.GetString("OCRA_LunchBreak", Culture) ?? "Lunch break (min)";
    public static string OCRA_NonRepetitive => StringRM.GetString("OCRA_NonRepetitive", Culture) ?? "No repetitive work (min)";
    public static string OCRA_NumberCycles => StringRM.GetString("OCRA_NumberCycles", Culture) ?? "Number of cycles";
    public static string OCRA_TNTR => StringRM.GetString("OCRA_TNTR", Culture) ?? "Net cycle time (sec)";
    public static string OCRA_TNC => StringRM.GetString("OCRA_TNC", Culture) ?? "Repetitive net cycle time (sec)";

    public static string OCRA_TaskBreaks => StringRM.GetString("OCRA_TaskBreaks", Culture) ?? "Work interruptions";
    public static string OCRA_RecoveryM => StringRM.GetString("OCRA_TaskBreaksM", Culture) ?? "Recovery score";
    public static string OCRA_TaskBreaks_Types => StringRM.GetString("OCRA_TaskBreaks_Types", Culture) ?? "8 min per hour or 1:5, 4x 8 min per 7-8 h shift, 3x 8 min per 7-8 h shift, 2x 8 min per 7-8 h shift, 1x 8 min per 7-8 h shift, Less than 5 min per 7-8 h shift";
    public static string OCRA_FrequencyM => StringRM.GetString("OCRA_FrequencyM", Culture) ?? "Frequency score";
    public static string OCRA_TaskATD => StringRM.GetString("OCRA_TaskATD", Culture) ?? "Arm dynamic actions";
    public static string OCRA_TaskATD_Types => StringRM.GetString("OCRA_TaskATD_Types", Culture) ?? "20 actions/min, 30 actions/min, 40 actions/min, 40 actions/min irregular breaks, 50 actions/min, 60 actions/min, 70 actions/min";
    public static string OCRA_TaskATE => StringRM.GetString("OCRA_TaskATE", Culture) ?? "Arm static actions";
    public static string OCRA_TaskATE_Types => StringRM.GetString("OCRA_TaskATE_Types", Culture) ?? "5 sec for 2/3 cycle, 5 sec for 3/3 cycle";

    public static string OCRA_Force => StringRM.GetString("OCRA_Force", Culture) ?? "Force type and frequency";
    public static string OCRA_ForceM => StringRM.GetString("OCRA_ForceM", Culture) ?? "Force score";
    public static string OCRA_BorgCR10 => StringRM.GetString("OCRA_BorgCR10", Culture) ?? "Nothing 0, Weak 1, Light 2, Moderate 3, Strong 4, Strong-Heavy 5, Very strong 6, Very strong 7, Very strong 8, Extremely strong 9, Maximal 10";
    public static string OCRA_ForceLight => StringRM.GetString("OCRA_ForceLight", Culture) ?? "1/3 of the time, 50%, > 50%, Almost all the time";
    public static string OCRA_ForceMax => StringRM.GetString("OCRA_ForceMax", Culture) ?? "2 sec every 10 min, 1% of the time, 5% of the time, > 10% of the time";

    public static string OCRA_PostureM => StringRM.GetString("OCRA_PostureM", Culture) ?? "Posture score";
    public static string OCRA_Arm => StringRM.GetString("OCRA_Arm", Culture) ?? "Arm posture";
    public static string OCRA_Arm_Types => StringRM.GetString("OCRA_Arm_Types", Culture) ?? "Slightly uplifted for 1/2 of the time, shoulder height for 10% of the time, shoulder height for 1/3 of the time, shoulder height for 1/2 of the time, Almost all the time";
    public static string OCRA_Elbow => StringRM.GetString("OCRA_Elbow", Culture) ?? "Elbow posture";
    public static string OCRA_Elbow_Types => StringRM.GetString("OCRA_Elbow_Types", Culture) ?? "Wide or sudden movs for 1/3 of the time, Wide or sudden movs for 1/2 of the time, Allmost all the time";
    public static string OCRA_Wrist => StringRM.GetString("OCRA_Wrist", Culture) ?? "Wrist posture";
    public static string OCRA_Wrist_Types => StringRM.GetString("OCRA_Wrist_Types", Culture) ?? "Extreme for 1/3 of the time, Extreme for 1/2 of the time, Almost all the time";
    public static string OCRA_Grip => StringRM.GetString("OCRA_Grip", Culture) ?? "Grip posture";
    public static string OCRA_Grip_Types => StringRM.GetString("OCRA_Grip_Types", Culture) ?? "Grip for 1/3 of the time, Grip for 1/2 of the time, Almost all the time";
    public static string OCRA_Stereotypy => StringRM.GetString("OCRA_Stereotypy", Culture) ?? "Lack of variation or stereotypy";
    public static string OCRA_Stereotypy_Types => StringRM.GetString("OCRA_Stereotypy_Types", Culture) ?? "Same movs for 2/3 of the time, Almost all the time";

    public static string OCRA_Results => StringRM.GetString("OCRA_Results", Culture) ?? "These are the results from the OCRA checklist model:";
    public static string OCRA_Data => StringRM.GetString("OCRA_Data", Culture) ?? "Initial data";
    public static string OCRA_Multipliers => StringRM.GetString("OCRA_Multipliers", Culture) ?? "Multipliers";
    public static string OCRA_Index => StringRM.GetString("OCRA_Index", Culture) ?? "The OCRA checklist index is:";
    public static string[] OCRA_ResultsHeaders => [
        Task,                       // 0
        OCRA_Results,
        OCRA_Data,
        OCRA_ShiftDuration,
        OCRA_NonRepetitive,
        OCRA_OfficialPauses,        // 5
        OCRA_LunchBreak,
        OCRA_NumberCycles,
        OCRA_TaskBreaks,
        OCRA_TaskATD,
        OCRA_TaskATE,               // 10
        OCRA_Force,
        OCRA_Arm,
        OCRA_Elbow,
        OCRA_Wrist,
        OCRA_Grip,
        OCRA_Stereotypy,

        OCRA_Multipliers,
        OCRA_TNTR,
        OCRA_TNC,
        OCRA_RecoveryM,
        OCRA_FrequencyM,
        OCRA_ForceM,
        OCRA_PostureM,
        OCRA_Index
    ];
    public static string[] OCRA_ColumnHeaders => [
        $"{Task} A"
    ];
    public static string[] OCRA_DataInputHeaders => [
        OCRA_ShiftDuration,
        OCRA_NonRepetitive,
        OCRA_OfficialPauses,
        OCRA_LunchBreak,
        OCRA_NumberCycles,
        OCRA_TaskBreaks,
        OCRA_TaskATD,
        OCRA_TaskATE,
        OCRA_Force,
        OCRA_Arm,
        OCRA_Elbow,
        OCRA_Wrist,
        OCRA_Grip,
        OCRA_Stereotypy
    ];
}
