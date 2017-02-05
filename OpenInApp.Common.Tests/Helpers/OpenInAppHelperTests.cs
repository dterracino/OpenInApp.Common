using NUnit.Framework;
using OpenInApp.Common.Helpers;
using System.Collections.Generic;

namespace OpenInApp.Common.Tests.Helpers
{
    [TestFixture()]
    public class OpenInAppHelperTests
    {
        //gregt %windir% %programfiles% %LocalAppData%

        [Test()]
        [Category("I")]
        //TESTED & LIVE [TestCase(@"C:\Program Files (x86)\Altova\XMLSpy2016\XMLSpy.exe", null)]
        //TESTED & LIVE [TestCase(@"C:\Program Files (x86)\Markdown Monster\MarkdownMonster.exe", null)]
        //nothing happens [TestCase(@"C:\Users\greg\AppData\Local\atom\app-1.13.1\atom.exe", null)]
        //happily opens last file in arguments list, but only last item         [TestCase(@"C:\Users\greg\Desktop\ZZZ open in\_emacs-25.1-2-x86_64-w64-mingw32\bin\runemacs.exe", null)]
        //fails with one or two args     [TestCase(@"C:\Program Files (x86)\LINQPad5\LINQPad.exe", null)]
        //fails with 1 arg prob 2 args also [TestCase(@"C:\Program Files (x86)\LINQPad4\LINQPad.exe", null)]
     //does nothing  [TestCase(@"C:\Program Files (x86)\LINQPad4\LPRun.exe", null)]
        //happily opens last file in arguments list, but only last item         [TestCase(@"C:\Windows\system32\notepad.exe", false)]
        //nothing happens  [TestCase(@"C:\Program Files (x86)\Vim\vim80\vim.exe", null)]//without mouse
        //works but for one and only ine file   [TestCase(@"C:\Program Files (x86)\Vim\vim80\gvim.exe", null)]//with mouse - graphical vim
        //fails with 1 and 2 arguments  [TestCase(@"C:\Program Files\Windows NT\Accessories\wordpad.exe", null)]
        public void InvokeCommandTest_TextApps(string executableFullPath, bool? useShellExecute)
        {
            // Arrange
            var actualFilesToBeOpened = new List<string>
            {
           //     @"C:\Temp\a.txt",
               // @"C:\Temp\b.txt",
                @"C:\Temp\a.linq",
                @"C:\Temp\b.sql",
            };

            // Act
            Act(executableFullPath, useShellExecute, actualFilesToBeOpened);
        }

        [Test()]
        [Category("I")]
        //works with 2 files, 1 browser window 2 tabs but opens canary        [TestCase(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", null)]
        //works succesfully    [TestCase(@"C:\Users\greg\AppData\Local\Google\Chrome SxS\Application\chrome.exe", null)]//(canary)
        //works with 2 files, 1 browser window 2 tabs but opens aurora         [TestCase(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe", null)]
        //works succesfully  [TestCase(@"C:\Program Files\Firefox Developer Edition\firefox.exe", null)]//(aurora)
        //works but ignores the args, when 2 argsat least  [TestCase(@"C:\Program Files (x86)\Internet Explorer\iexplore.exe", null)]
        //nothing happens  [TestCase(@"C:\Windows\SystemApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\MicrosoftEdge.exe", null)]
        //nothing happens  [TestCase(@"C:\Windows\SystemApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\MicrosoftEdge.exe", false)]
      //nothing happens [TestCase(@"C:\Windows\SystemApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\MicrosoftEdgeCP.exe", null)]// cp = content process
      //nothing happens  [TestCase(@"C:\Windows\SystemApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\MicrosoftEdgeCP.exe", false)]// cp = content process
        //works fully, 2 files, 2 tabs, not the dev browser          [TestCase(@"C:\Program Files (x86)\Opera\opera.exe", null)]
        //works fully, 2 files, 2 tabs, in dev browser          [TestCase(@"C:\Program Files\Opera developer\launcher.exe", null)]
        //works fully, 2 files, 2 tabs        [TestCase(@"C:\Users\greg\AppData\Local\Vivaldi\Application\vivaldi.exe", null)]
        public void InvokeCommandTest_BrowserApps(string executableFullPath, bool? useShellExecute)
        {
            // Arrange
            var actualFilesToBeOpened = new List<string>
            {
                @"C:\Temp\a.txt",
                @"C:\Temp\b.txt"
            };

            // Act
            Act(executableFullPath, useShellExecute, actualFilesToBeOpened);
        }

        [Test()]
        [Category("I")]
        //TESTED & LIVE [TestCase(@"C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\devenv.exe", false)]
        //TESTED & LIVE [TestCase(@"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe", false)]
        //TESTED & LIVE [TestCase(@"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.exe", false)]
        //TESTED & LIVE [TestCase(@"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe", false)]
        //TESTED & LIVE [TestCase(@"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\devenv.exe", false)]
        //TESTED & LIVE [TestCase(@"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\devenv.exe", false)]
       //tested two files successfully  [TestCase(@"C:\Program Files (x86)\Xamarin Studio\bin\XamarinStudio.exe", null)]
       //app splash screen shows but app doesnt open [TestCase(@"C:\Program Files (x86)\Microsoft SQL Server\90\Tools\Binn\VSShell\Common7\IDE\ssmsee.exe", null)]
        public void InvokeCommandTest_StudioApps(string executableFullPath, bool? useShellExecute)
        {
            // Arrange
            var actualFilesToBeOpened = new List<string>
            {
                @"C:\Temp\a.txt",
                @"C:\Temp\b.txt"
            };

            // Act
            Act(executableFullPath, useShellExecute, actualFilesToBeOpened);
        }

        [Test()]
        [Category("I")]
        //TESTED & LIVE [TestCase(@"C:\Program Files (x86)\bin\gimp-2.8.exe", null)]
        //TESTED & LIVE [TestCase(@"C:\Program Files (x86)\Paint.NET\PaintDotNet.exe", null)]
        //nothing happens,even with 1 file [TestCase(@"C:\Windows\system32\mspaint.exe", null)]
        //works but only with first jpeg   [TestCase(@"C:\Windows\system32\mspaint.exe", false)]
        // nothing happens  [TestCase(@"C:\Program Files (x86)\DeDup\DeDup.exe", null)]
        //works but doesnt use argument, evn if single arg that is a directory   [TestCase(@"C:\Program Files (x86)\DeDup\DeDup.exe", false)]
        //works with multiple args, file or folder, but only uses the final argument   [TestCase(@"C:\Program Files (x86)\FastStone Image Viewer\FSViewer.exe", null)]
        //  works but args ignored even 1 jpg arg or 1 directory arg    [TestCase(@"C:\Program Files (x86)\Windows Live\Photo Gallery\WLXPhotoGallery.exe", null)]
        public void InvokeCommandTest_ImageApps(string executableFullPath, bool? useShellExecute)
        {
            // Arrange
            var actualFilesToBeOpened = new List<string>
            {
                //  @"C:\Temp\a.jpg",
           //     @"C:\Temp\b.jpg",
                 @"C:\Temp",
            };

            // Act
            Act(executableFullPath, useShellExecute, actualFilesToBeOpened);
        }

        [Test()]
        [Category("I")]
   //works for first entry in list, file or directory    [TestCase(@"C:\Program Files (x86)\WinDirStat\windirstat.exe", null)]
   //works for all 3, although rejects files     [TestCase(@"C:\Program Files (x86)\JAM Software\TreeSize Free\TreeSizeFree.exe", null)]//NOADMIN=no network drives
        public void InvokeCommandTest_FolderSizes(string executableFullPath, bool? useShellExecute)
        {
            // Arrange
            var actualFilesToBeOpened = new List<string>
            {
                @"C:\Temp\a.txt",
                @"C:\Temp\b.txt",
                @"C:\Temp",
            };

            // Act
            Act(executableFullPath, useShellExecute, actualFilesToBeOpened);
        }

        [Test()]
        [Category("I")]
     //works, but needs testing with files actually locked   [TestCase(@"C:\Program Files\Unlocker\Unlocker.exe", null)]
     //opens app but even with just 1 arg containing folder name the app doesnt use the argument [TestCase(@"C:\Program Files (x86)\File Renamer\FileRenamer.exe", null)]
  //nothing happens even just 1 file      [TestCase(@"C:\Program Files (x86)\Windows Media Player\wmplayer.exe", null)]
    //works, with 1 file at least, but video is black!    [TestCase(@"C:\Program Files (x86)\Windows Media Player\wmplayer.exe", false)]
        public void InvokeCommandTest_TODOCategory(string executableFullPath, bool? useShellExecute)
        {
            // Arrange
            var actualFilesToBeOpened = new List<string>
            {
                @"C:\Temp\a.mpg",
         //       @"C:\Temp\b.mpeg",
             //   @"C:\Temp\b.txt",
              //  @"C:\Temp",
            };

            // Act
            Act(executableFullPath, useShellExecute, actualFilesToBeOpened);
        }

        private void Act(string executableFullPath, bool? useShellExecute, IEnumerable<string> actualFilesToBeOpened)
        {
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