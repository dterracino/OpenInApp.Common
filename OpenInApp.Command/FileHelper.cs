using OpenInApp.Common.Helpers;
using System.Windows.Forms;

namespace OpenInApp.Command
{
    public class FileHelper
    {
        private string caption { get; set; }
        private string constantsForAppExecutableFileToBrowseFor { get; set; }

        public FileHelper(string caption, string constantsForAppExecutableFileToBrowseFor)
        {
            this.caption = caption;
            this.constantsForAppExecutableFileToBrowseFor = constantsForAppExecutableFileToBrowseFor;
        }

        public SaveSettingsDto PromptForActualExeFile(string originalPathToFile)
        {
            var saveSettingsDto = new SaveSettingsDto();

            var box = MessageBox.Show(
               CommonConstants.PromptForActualExeFile(originalPathToFile),
               caption,
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);

            switch (box)
            {
                case DialogResult.Yes:
                    var resultAndNamePicked = CommonFileHelper.BrowseToFileLocation(constantsForAppExecutableFileToBrowseFor);
                    if (resultAndNamePicked.DialogResult == DialogResult.OK)
                    {
                        PersistVSToolOptions(saveSettingsDto, resultAndNamePicked.FileNameChosen);
                    }
                    break;
                case DialogResult.No:
                    PersistVSToolOptions(saveSettingsDto, originalPathToFile);
                    break;
            }

            return saveSettingsDto;
        }

        private void PersistVSToolOptions(SaveSettingsDto saveSettingsDto, string fileName)
        {
            saveSettingsDto.StringToPersist = fileName;
            saveSettingsDto.SaveSettings = true;
        }
    }
}
