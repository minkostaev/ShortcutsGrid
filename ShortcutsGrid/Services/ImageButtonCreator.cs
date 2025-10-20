namespace ShortcutsGrid.Services;

using Controls;
using Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Windows;

public static class ImageButtonCreator
{

    public static ImageButton GetButton(Shortcut shortcutItem, MainWindow window)
    {
        bool closeDragButton = shortcutItem.Id == AppValues.CloseDragId;
        var imageButton = new ImageButton(shortcutItem)
        {
            Height = 110,
            Width = 110
        };

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
                RunProcess.Run(shortcutItem.Executions);
            }
            else
            {
                if (RunProcess.Run(shortcutItem.Executions))
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
                RunProcess.Run(shortcutItem.Executions);
            }
            else if (e.ClickCount == 1 && closeDragButton)
            {
                try { window.DragMove(); }
                catch (Exception ex) { _ = ex.Message; }
            }
        });
        imageButton.MnOpen.Click += delegate
        {
            RunProcess.Run(shortcutItem.Executions);
        };
        imageButton.MnAdmin.Click += delegate
        {
            RunProcess.Run(shortcutItem.Executions, true);
        };
        imageButton.MnManageList.Click += delegate
        {
            new List(shortcutItem.Id).ShowDialog();
            ShowShortcuts.Load(window);
        };
        imageButton.MnFolderExe.Click += delegate
        {
            RunProcess.OpenExplorer(shortcutItem.ExistingExes.FirstOrDefault());
        };
        imageButton.MnFolderImg.Click += delegate
        {
            RunProcess.OpenExplorer(shortcutItem.Image);
        };
        imageButton.MnFolderThis.Click += delegate
        {
            RunProcess.OpenExplorer(AppValues.ExePath);
        };
        imageButton.MnAbout.Click += delegate
        {
            new About().ShowDialog();
        };
        imageButton.MnExit.Click += delegate
        {
            Application.Current.Shutdown();
        };
        return imageButton;
    }

}