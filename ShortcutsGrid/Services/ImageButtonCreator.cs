namespace ShortcutsGrid.Services;

using Controls;
using Models;
using Services.Run;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Windows;

public static class ImageButtonCreator
{

    public static ImageButton GetButton(MainWindow mainWindow, Shortcut shortcutItem)
    {
        MessageDialogs messageDialogs = new MessageDialogs(new MessageBoxWrapper());
        bool closeDragButton = shortcutItem.Tag is string && shortcutItem.Tag.ToString() == AppValues.CloseDragId;
        var imageButton = new ImageButton(shortcutItem)
        {
            Height = 110,
            Width = 110
        };

        bool doubleClicked = false;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        dispatcherTimer.Interval = TimeSpan.FromSeconds(0.2); //wait for the other click for 200ms
        dispatcherTimer.Tick += delegate
        {
            dispatcherTimer.Stop();
            if (doubleClicked)// this should not be necessary but some apps take too long to start
            {
                doubleClicked = false;
                return;
            }
            // Handle Single Click Actions
            string msg = RunProcess.Run(shortcutItem.ExePath);
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                messageDialogs.IsErrorDisplayed(msg);
            }
            else
            {
                if (!messageDialogs.IsErrorDisplayed(msg))
                {
                    AppValues.Exit();
                }
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
                string msg = RunProcess.Run(shortcutItem.ExePath);
                messageDialogs.IsErrorDisplayed(msg);
            }
            else if (e.ClickCount == 1 && closeDragButton)
            {
                try { mainWindow.DragMove(); }
                catch (Exception ex) { _ = ex.Message; }
            }
        });
        imageButton.MnOpen.Click += delegate
        {
            string msg = RunProcess.Run(shortcutItem.ExePath);
            messageDialogs.IsErrorDisplayed(msg);
        };
        imageButton.MnAdmin.Click += delegate
        {
            string msg = RunProcess.Run(shortcutItem.ExePath, true);
            messageDialogs.IsErrorDisplayed(msg);
        };
        imageButton.MnShortcutsAdd.Click += delegate
        {
            messageDialogs.IsErrorDisplayed("todo");
        };
        imageButton.MnShortcutsRemove.Click += delegate
        {
            messageDialogs.IsErrorDisplayed("todo");
        };
        imageButton.MnShortcutsList.Click += delegate
        {
            messageDialogs.IsErrorDisplayed("todo");
        };
        imageButton.MnFolderExe.Click += delegate
        {
            string msg = RunProcess.Run(FolderOpeningString(shortcutItem.ExePath));
            messageDialogs.IsErrorDisplayed(msg);
        };
        imageButton.MnFolderImg.Click += delegate
        {
            string msg = RunProcess.Run(FolderOpeningString(shortcutItem.ImgPath));
            messageDialogs.IsErrorDisplayed(msg);
        };
        imageButton.MnFolderThis.Click += delegate
        {
            string msg = RunProcess.Run(FolderOpeningString(AppValues.ExePath));
            messageDialogs.IsErrorDisplayed(msg);
        };
        imageButton.MnAbout.Click += delegate
        {
            new About().ShowDialog();
        };
        imageButton.MnExit.Click += delegate
        {
            AppValues.Exit();
        };
        return imageButton;
    }

    private static string FolderOpeningString(string? filePath)
    {
        return "explorer.exe " + "/select, \"" + filePath + "\"";
    }

}
