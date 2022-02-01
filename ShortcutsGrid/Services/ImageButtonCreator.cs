namespace ShortcutsGrid.Services
{
    using ShortcutsGrid.Controls;
    using ShortcutsGrid.Models;
    using ShortcutsGrid.Services.Run;
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    public static class ImageButtonCreator
    {

        public static ImageButton GetButton(Shortcut shortcutItem)
        {
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
                    IsErrorDisplayed(msg);
                }
                else
                {
                    if (!IsErrorDisplayed(msg))
                    {
                        ///Environment.Exit(0);
                        ///App.Current.Shutdown();
                        Application.Current.Shutdown();
                        ///System.Diagnostics.Process.GetCurrentProcess().Kill();
                    }
                }
            };
            imageButton.btn.Click += new((sender, e) => { dispatcherTimer.Start(); });
            imageButton.btn.PreviewMouseDown += new((sender, e) =>
            {
                if (e.ClickCount > 1)
                {
                    dispatcherTimer.Stop();

                    doubleClicked = true;
                    string msg = RunProcess.Run(shortcutItem.ExePath);
                    IsErrorDisplayed(msg);
                }
            });
            imageButton.mnOpen.Click += delegate
            {
                string msg = RunProcess.Run(shortcutItem.ExePath);
                IsErrorDisplayed(msg);
            };
            imageButton.mnAdmin.Click += delegate
            {
                string msg = RunProcess.Run(shortcutItem.ExePath, true);
                IsErrorDisplayed(msg);
            };
            imageButton.mnFolderExe.Click += delegate
            {
                string msg = RunProcess.Run(FolderOpeningString(shortcutItem.ExePath));
                IsErrorDisplayed(msg);
            };
            imageButton.mnFolderImg.Click += delegate
            {
                string msg = RunProcess.Run(FolderOpeningString(shortcutItem.ImgPath));
                IsErrorDisplayed(msg);
            };
            imageButton.mnFolderThis.Click += delegate
            {
                string msg = RunProcess.Run(FolderOpeningString(AppValues.ExePath));
                IsErrorDisplayed(msg);
            };
            return imageButton;
        }

        private static string FolderOpeningString(string? filePath)
        {
            return "explorer.exe " + "/select, \"" + filePath + "\"";
        }

        private static bool IsErrorDisplayed(string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
            {
                MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
            return false;
        }

    }
}
