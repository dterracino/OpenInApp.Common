using NUnit.Framework;
using OpenInApp.Common.Helpers;
using System.Collections.Generic;

namespace OpenInApp.Common.Tests.Helpers
{
    [TestFixture()]
    public class OpenInAppHelperTests
    {
        [Test()]
        //[TestCase(@"C:\Program Files (x86)\Altova\XMLSpy2016\XMLSpy.exe", null)]
        //[TestCase(@"C:\Program Files (x86)\bin\gimp-2.8.exe", null)]
        //[TestCase(@"C:\Program Files (x86)\Markdown Monster\MarkdownMonster.exe", null)]
        //[TestCase(@"C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\devenv.exe", false)]
        //[TestCase(@"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe", false)]
        [TestCase(@"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.exe", false)]
        //[TestCase(@"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe", false)]
        //[TestCase(@"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\devenv.exe", false)]
        //[TestCase(@"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\devenv.exe", false)]
        //[TestCase(@"C:\Program Files (x86)\Paint.NET\PaintDotNet.exe", null)]
        [Category("I")]
        public void InvokeCommandTest(string executableFullPath, bool? useShellExecute)
        {
            // Arrange
            var actualFilesToBeOpened = new List<string>
            {
                @"C:\Temp\a.txt",
                @"C:\Temp\b.txt"
            };

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