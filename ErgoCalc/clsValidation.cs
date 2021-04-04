using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgoCalc
{
    class Validation
    {
        /// <summary>
        /// Checks whether the object passed is of type double and whether its value is withing the lower and upper limits
        /// </summary>
        /// <param name="obj">Object cointaining a double value</param>
        /// <param name="lowerBound">Lower limit to check</param>
        /// <param name="upperBound">Upper limit to check</param>
        /// <returns>The original value if it's within the limits. The lower or upper limits otherwise if it's off-limits, or 0 if it's not a valid double and there's no lower limit</returns>
        public static double ValidateNumber(object obj, double? lowerBound = null, double? upperBound = null)
        {
            if (!IsValidDouble(obj)) return lowerBound.HasValue ? lowerBound.Value : 0.0;
            
            double value = Convert.ToDouble(obj.ToString());
            //double value = double.TryParse(obj.ToString());
            if (lowerBound.HasValue && value < lowerBound.Value) return lowerBound.Value;
            if (upperBound.HasValue && value > upperBound.Value) return upperBound.Value;

            return value;
        }

        /// <summary>
        /// Checks whether the object passed is of type double and whether its value is withing the lower and upper limits
        /// </summary>
        /// <param name="obj">Object cointaining a double value</param>
        /// <param name="lowerBound">Lower limit to check</param>
        /// <param name="upperBound">Upper limit to check</param>
        /// <param name="showMsgBox">True if a MessageBox is to be shown</param>
        /// <returns>True if obj is within the bound limits, false otherwise</returns>
        public static bool IsValidRange(object obj, double? lowerBound = null, double? upperBound = null, bool? showMsgBox = false, System.Windows.Forms.Form parent = null)
        {
            if (!IsValidDouble(obj))
            {
                if (showMsgBox.HasValue && showMsgBox.Value)
                {
                    using (new System.Windows.Forms.CenterWinDialog(parent))
                    {
                        System.Windows.Forms.MessageBox.Show(
                          "The input data could not be converted to a number\nPlease, check and modify the highlighted field",
                          "Data error",
                          System.Windows.Forms.MessageBoxButtons.OK,
                          System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
                return false;
            }

            double value = Convert.ToDouble(obj.ToString());
            if (lowerBound.HasValue && value < lowerBound.Value)
            {
                if (showMsgBox.HasValue && showMsgBox.Value)
                {
                    System.Windows.Forms.MessageBox.Show(
                      "The input data is off-limits. Please, check the highlighted field\nand make sure it is equal or above " + lowerBound.ToString(),
                      "Data error",
                      System.Windows.Forms.MessageBoxButtons.OK,
                      System.Windows.Forms.MessageBoxIcon.Error);
                }
                return false;
            }
            if (upperBound.HasValue && value > upperBound.Value)
            {
                if (showMsgBox.HasValue && showMsgBox.Value)
                {
                    System.Windows.Forms.MessageBox.Show(
                      "The input data is off-limits. Please, check the highlighted field\nand make sure it is equal or below " + upperBound.ToString(),
                      "Data error",
                      System.Windows.Forms.MessageBoxButtons.OK,
                      System.Windows.Forms.MessageBoxIcon.Error);
                }
                return false;
            }

            return true;
        }

        public static bool IsValidDouble(object str)
        {
            // https://stackoverflow.com/questions/894263/identify-if-a-string-is-a-number
            // https://stackoverflow.com/questions/33939770/regex-for-decimal-number-validation-in-c-sharp
            //string input = "132456789";

            if (str == null) return false;

            var m = System.Text.RegularExpressions.Regex.Match(str.ToString(), @"^-?\+?[0-9]*\.?\,?[0-9]+$");
            return m.Success && m.Value != "";
        }

        public static bool IsValidInteger(object str)
        {
            if (str == null) return false;

            var m = System.Text.RegularExpressions.Regex.Match(str.ToString(), @"^-?\+?[0-9]+$");
            return m.Success && m.Value != "";
        }

    }
}
