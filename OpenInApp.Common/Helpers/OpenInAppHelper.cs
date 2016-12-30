using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace OpenInApp.Common.Helpers
{
    public class OpenInAppHelper
    {
        public static void InvokeCommand(IEnumerable<string> actualFilesToBeOpened, string fullPath)
        {
            var arguments = " ";

            foreach (var actualFileToBeOpened in actualFilesToBeOpened)
            {
                arguments += "\"" + actualFileToBeOpened + "\"" + " ";
            }

            var start = new ProcessStartInfo()
            {
                WorkingDirectory = Path.GetDirectoryName(fullPath),
                FileName = Path.GetFileName(fullPath),
                Arguments = arguments,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            try
            {
                using (Process.Start(start)) { }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static bool ConfirmProceedToExecute(string caption, string messageText)
        {
            bool proceedToExecute = false;

            messageText += Environment.NewLine + Environment.NewLine + CommonConstants.ContinueAnyway;

            var box = MessageBox.Show(
                messageText,
                caption,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

            if (box == DialogResult.OK)
            {
                proceedToExecute = true;
            }

            return proceedToExecute;
        }

        public static void InformUserMissingFile(string caption, string missingFileName)
        {
            MessageBox.Show(
                CommonConstants.InformUserMissingFile(missingFileName),
                caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop);
        }

        public static void ShowUnexpectedError(string caption)
        {
            MessageBox.Show(
                CommonConstants.UnexpectedError,
                caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
