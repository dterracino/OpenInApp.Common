using OpenInApp.Common.Helpers;
using System.Windows.Forms;

namespace OpenInApp.Command
{
    public class CommandFileHelper
    {
        private string caption { get; set; }
        private string executableFileToBrowseFor { get; set; }

        public CommandFileHelper(string caption, string executableFileToBrowseFor)
        {
            this.caption = caption;
            this.executableFileToBrowseFor = executableFileToBrowseFor;
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
                    var resultAndNamePicked = CommonFileHelper.BrowseToFileLocation(executableFileToBrowseFor);
                    if (resultAndNamePicked.DialogResult == DialogResult.OK)
                    {
                        SetSaveSettingsDto(saveSettingsDto, resultAndNamePicked.FileNameChosen);
                    }
                    break;
                case DialogResult.No:
                    SetSaveSettingsDto(saveSettingsDto, originalPathToFile);
                    break;
            }

            return saveSettingsDto;
        }

        private void SetSaveSettingsDto(SaveSettingsDto saveSettingsDto, string fileName)
        {
            saveSettingsDto.StringToPersist = fileName;
            saveSettingsDto.SaveSettings = true;
        }
    }
}
