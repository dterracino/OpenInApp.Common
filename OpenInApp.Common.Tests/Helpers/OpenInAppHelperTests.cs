using NUnit.Framework;
using OpenInApp.Common.Helpers;
using System.Collections.Generic;

namespace OpenInApp.Common.Tests.Helpers
{
    [TestFixture()]
    public class OpenInAppHelperTests
    {
        [Test()]
        [TestCase(@"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe", true)]
        [TestCase(@"C:\Program Files (x86)\Altova\XMLSpy2016\XMLSpy.exe", null)]
        [TestCase(@"C:\Program Files (x86)\Altova\XMLSpy2016\XMLSpy.exe", true)]
        [TestCase(@"C:\Program Files (x86)\Altova\XMLSpy2016\XMLSpy.exe", false)]
        [Category("I")]
        public void InvokeCommandTest(string executableFullPath, bool? useShellExecute)
        {
            // Arrange
            var actualFileToBeOpened = @"C:\Temp\a.txt";
            var actualFilesToBeOpened = new List<string> { actualFileToBeOpened };

            // Act
            if (useShellExecute.HasValue)
            {
                OpenInAppHelper.InvokeCommand(actualFilesToBeOpened, executableFullPath, useShellExecute.Value);
            }
            else
            {
                OpenInAppHelper.InvokeCommand(actualFilesToBeOpened, executableFullPath);
            }
        }
    }
}