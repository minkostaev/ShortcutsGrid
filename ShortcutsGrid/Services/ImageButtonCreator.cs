namespace ShortcutsGrid.Services;

using Controls;
using Models;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Windows;

public static class ImageButtonCreator
{

    public static ImageButton GetButton(Shortcut shortcutItem, MainWindow window)
    {
        var messageDialogs = new MessageDialogs();
        bool closeDragButton = shortcutItem.Tag is string && shortcutItem.Tag.ToString() == AppValues.CloseDragId;
        var imageButton = new ImageButton(shortcutItem)
        {
            Height = 110,
            Width = 110
        };

        string execution = shortcutItem.Execution;

        bool doubleClicked = false;
        var dispatcherTimer = new DispatcherTimer
        {//wait for the other click for 200ms
            Interval = TimeSpan.FromSeconds(0.2)
        };
        dispatcherTimer.Tick += delegate
        {
            dispatcherTimer.Stop();
            if (doubleClicked)// this should not be necessary but some apps take too long to start
            {
                doubleClicked = false;
                return;
            }
            // Handle Single Click Actions
            AppValues.WillBeClosed = true;
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (!RunProcess.Run(execution))
                    messageDialogs.IsErrorDisplayed(execution);
            }
            else
            {
                if (!RunProcess.Run(execution))
                    messageDialogs.IsErrorDisplayed(execution);
                else
                    window.Exit();
            }
        };
        imageButton.btn.Click += new((sender, e) => { if (!closeDragButton) dispatcherTimer.Start(); });
        imageButton.btn.PreviewMouseDown += new((sender, e) =>
        {
            if (e.ClickCount > 1)
            {
                if (closeDragButton)
                {
                    Application.Current.Shutdown();
                    return;
                }
                dispatcherTimer.Stop();

                doubleClicked = true;
                if (!RunProcess.Run(execution))
                    messageDialogs.IsErrorDisplayed(execution);
            }
            else if (e.ClickCount == 1 && closeDragButton)
            {
                try { window.DragMove(); }
                catch (Exception ex) { _ = ex.Message; }
            }
        });
        imageButton.MnOpen.Click += delegate
        {
            if (!RunProcess.Run(execution))
                messageDialogs.IsErrorDisplayed(execution);
        };
        imageButton.MnAdmin.Click += delegate
        {
            if (!RunProcess.Run(execution, true))
                messageDialogs.IsErrorDisplayed(execution);
        };
        imageButton.MnManageList.Click += delegate
        {
            new List().ShowDialog();
        };
        imageButton.MnFolderExe.Click += delegate
        {
            if (!RunProcess.Run(FolderOpeningString(execution)))
                messageDialogs.IsErrorDisplayed(execution);
        };
        imageButton.MnFolderImg.Click += delegate
        {
            if (!RunProcess.Run(FolderOpeningString(shortcutItem.Image)))
                messageDialogs.IsErrorDisplayed("Image path missing.");
        };
        imageButton.MnFolderThis.Click += delegate
        {
            if (!RunProcess.Run(FolderOpeningString(AppValues.ExePath)))
                messageDialogs.IsErrorDisplayed("Exe path missing.");
        };
        imageButton.MnAbout.Click += delegate
        {
            new About().ShowDialog();
        };
        imageButton.MnExit.Click += delegate
        {
            window.Exit();
        };
        return imageButton;
    }

    private static string FolderOpeningString(string? filePath)
    {
        return "explorer.exe " + "/select, \"" + filePath + "\"";
    }

}