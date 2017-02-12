using EnvDTE;
using EnvDTE80;
using OpenInApp.Common.Helpers;
using System;
//using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace OpenInApp.Command
{
    public class OpenInAppCommand2
    {
        public SaveSettingsDto MenuItemCallback(
            bool isFromSolutionExplorer, 
            string ConstantsForAppCaption, 
            string ConstantsForAppExecutableFileToBrowseFor,
            string Caption, 
            IServiceProvider ServiceProvider, 
            string ActualPathToExe, 
            string FileQuantityWarningLimit,
            bool SuppressTypicalFileExtensionsWarning, 
            string TypicalFileExtensions)
        {
            var dte = (DTE2)ServiceProvider.GetService(typeof(DTE));
            var saveSettingsDto = new SaveSettingsDto();

            try
            {
                var actualPathToExeExists = CommonFileHelper.DoesFileExist(ActualPathToExe);

                bool proceedToExecute = true;
                if (!actualPathToExeExists)
                {
                    proceedToExecute = false;
                    var fileHelper = new FileHelper(ConstantsForAppCaption, ConstantsForAppExecutableFileToBrowseFor);
                    saveSettingsDto = fileHelper.PromptForActualExeFile(ActualPathToExe);
                    //if (saveSettingsDto.SaveSettings)
                    //{
                    //    VSPackage.Options.PersistVSToolOptions(saveSettingsDto.StringToPersist);
                    //}

                    var newActualPathToExeExists = CommonFileHelper.DoesFileExist(ActualPathToExe);
                    if (newActualPathToExeExists)
                    {
                        proceedToExecute = true;
                    }
                    else
                    {
                        // User somehow managed to browse/select a new location for the exe that doesn't actually exist - virtually impossible, but you never know...
                        OpenInAppHelper.InformUserMissingFile(Caption, ActualPathToExe);
                    }
                }
                if (proceedToExecute)
                {
                    var actualFilesToBeOpened = CommonFileHelper.GetFileNamesToBeOpened(dte, isFromSolutionExplorer);
                    var actualFilesToBeOpenedExist = CommonFileHelper.DoFilesExist(actualFilesToBeOpened);
                    if (!actualFilesToBeOpenedExist)
                    {
                        var missingFileName = CommonFileHelper.GetMissingFileName(actualFilesToBeOpened);
                        OpenInAppHelper.InformUserMissingFile(Caption, missingFileName);
                    }
                    else
                    {
                        int fileQuantityWarningLimitInt;
                        int.TryParse(FileQuantityWarningLimit, out fileQuantityWarningLimitInt);//gregt check the bool output of this try parse
                        proceedToExecute = false;
                        if (actualFilesToBeOpened.Count() > fileQuantityWarningLimitInt)
                        {
                            proceedToExecute = OpenInAppHelper.ConfirmProceedToExecute(Caption, CommonConstants.ConfirmOpenFileQuantityExceedsWarningLimit);
                        }
                        else
                        {
                            proceedToExecute = true;
                        }
                        if (proceedToExecute)
                        {
                            var typicalFileExtensionAsList = CommonFileHelper.GetTypicalFileExtensionAsList(TypicalFileExtensions);
                            var areTypicalFileExtensions = CommonFileHelper.AreTypicalFileExtensions(actualFilesToBeOpened, typicalFileExtensionAsList);
                            if (!areTypicalFileExtensions)
                            {
                                if (SuppressTypicalFileExtensionsWarning)
                                {
                                    proceedToExecute = true;
                                }
                                else
                                {
                                    proceedToExecute = OpenInAppHelper.ConfirmProceedToExecute(Caption, CommonConstants.ConfirmOpenNonTypicalFile);
                                }
                            }
                            if (proceedToExecute)
                            {
                                /* gregtgregt delete this comment
                                 * true for sublime text, vs code, etc
                                 * false for devenv.exe */
                                OpenInAppHelper.InvokeCommand(actualFilesToBeOpened, ActualPathToExe, true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                OpenInAppHelper.ShowUnexpectedError(Caption);
            }

            return saveSettingsDto;
        }

    }
}
