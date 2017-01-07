using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace OpenInApp.Common.Helpers
{
    /// <summary>
    /// Helper class containing generic methods for 'OpenInApp' VS packages
    /// </summary>
    public class OpenInAppHelper
    {
        /// <summary>
        /// Invokes the specified executable file, passing the file(s) to be opened as arguments.
        /// </summary>
        /// <param name="actualFilesToBeOpened">The actual files to be opened.</param>
        /// <param name="executableFullPath">The full path to the executable.</param>
        public static void InvokeCommand(IEnumerable<string> actualFilesToBeOpened, string executableFullPath)
        {
            var arguments = " ";

            foreach (var actualFileToBeOpened in actualFilesToBeOpened)
            {
                arguments += "\"" + actualFileToBeOpened + "\"" + " ";
            }

            var start = new ProcessStartInfo()
            {
                WorkingDirectory = Path.GetDirectoryName(executableFullPath),
                FileName = Path.GetFileName(executableFullPath),
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

        /// <summary>
        /// Displays a simple message box prompting the user to proceed with action or cancel.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="messageText">The message text.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Displays a simple message box informing the user of a missing file.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="missingFileName">Name of the missing file.</param>
        public static void InformUserMissingFile(string caption, string missingFileName)
        {
            MessageBox.Show(
                CommonConstants.InformUserMissingFile(missingFileName),
                caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop);
        }

        /// <summary>
        /// Displays a simple message box informing the user of an unexpected error.
        /// </summary>
        /// <param name="caption">The caption.</param>
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
