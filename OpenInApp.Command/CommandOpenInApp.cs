using EnvDTE;
using EnvDTE80;
using OpenInApp.Common.Helpers;
using System;
using System.Linq;

namespace OpenInApp.Command
{
    public class CommandOpenInApp
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
            string TypicalFileExtensions)//gregt shorten this list
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
                    var fileHelper = new CommandFileHelper(ConstantsForAppCaption, ConstantsForAppExecutableFileToBrowseFor);
                    saveSettingsDto = fileHelper.PromptForActualExeFile(ActualPathToExe);

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
                        var isInt = int.TryParse(FileQuantityWarningLimit, out fileQuantityWarningLimitInt);
                        if (isInt)
                        {
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
                        else
                        {
                            Logger.Log(Caption + " unexpected non-integer value found.");
                            OpenInAppHelper.ShowUnexpectedError(Caption);
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