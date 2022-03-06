namespace ShortcutsGrid.Services;

using ShortcutsGrid.Controls;
using ShortcutsGrid.Models;
using ShortcutsGrid.Services.Run;
using ShortcutsGrid.Windows;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

public static class ImageButtonCreator
{

    public static ImageButton GetButton(MainWindow mainWindow, Shortcut shortcutItem)
    {
        MessageDialogs messageDialogs = new MessageDialogs(new MessageBoxWrapper());
        var imageButton = new ImageButton(shortcutItem.Image, shortcutItem.AppName)
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
                    ///Environment.Exit(0);
                    ///App.Current.Shutdown();
                    ///System.Diagnostics.Process.GetCurrentProcess().Kill();
                    Application.Current.Shutdown();
                }
            }
        };
        imageButton.btn.Click += new((sender, e) => { dispatcherTimer.Start(); });
        imageButton.btn.PreviewMouseDown += new((sender, e) =>
        {
            bool cancelButton = shortcutItem.AppName == "Drag or Close" && shortcutItem.ExePath == "";
            if (e.ClickCount > 1)
            {
                if (cancelButton)
                {
                    Application.Current.Shutdown();
                    return;
                }
                dispatcherTimer.Stop();

                doubleClicked = true;
                string msg = RunProcess.Run(shortcutItem.ExePath);
                messageDialogs.IsErrorDisplayed(msg);
            }
            else if (e.ClickCount == 1 && cancelButton)
            {
                try { mainWindow.DragMove(); }
                catch (Exception ex) { _ = ex.Message; }
            }
        });
        imageButton.mnOpen.Click += delegate
        {
            string msg = RunProcess.Run(shortcutItem.ExePath);
            messageDialogs.IsErrorDisplayed(msg);
        };
        imageButton.mnAdmin.Click += delegate
        {
            string msg = RunProcess.Run(shortcutItem.ExePath, true);
            messageDialogs.IsErrorDisplayed(msg);
        };
        imageButton.mnFolderExe.Click += delegate
        {
            string msg = RunProcess.Run(FolderOpeningString(shortcutItem.ExePath));
            messageDialogs.IsErrorDisplayed(msg);
        };
        imageButton.mnFolderImg.Click += delegate
        {
            string msg = RunProcess.Run(FolderOpeningString(shortcutItem.ImgPath));
            messageDialogs.IsErrorDisplayed(msg);
        };
        imageButton.mnFolderThis.Click += delegate
        {
            string msg = RunProcess.Run(FolderOpeningString(AppValues.ExePath));
            messageDialogs.IsErrorDisplayed(msg);
        };
        imageButton.mnAbout.Click += delegate
        {
            new About().ShowDialog();
        };
        return imageButton;
    }

    private static string FolderOpeningString(string? filePath)
    {
        return "explorer.exe " + "/select, \"" + filePath + "\"";
    }

}
