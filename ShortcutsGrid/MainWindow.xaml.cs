using ShortcutsGrid.Controls;
using ShortcutsGrid.Models;
using ShortcutsGrid.Services;
using ShortcutsGrid.Services.Run;
using System;
using System.Windows;
using System.Windows.Input;

namespace ShortcutsGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            #region setup window
            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;
            this.Background = System.Windows.Media.Brushes.Transparent;
            this.Topmost = true;

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.ResizeMode = ResizeMode.NoResize;

            this.PreviewKeyDown += (s, e) => { if (e.Key == Key.Escape) Close(); };
            #endregion

            var shortcuts = ReadShortcuts.FileToShortcuts();

            shortcuts.Add(new Shortcut() { AppName = "Cancel", ImgPath = AppValues.CloseImage });

            foreach (var shortcut in shortcuts)
            {
                if (stkPnl1.Children.Count < 6)
                {
                    stkPnl1.Children.Add(ShortcutButton(shortcut));
                }
                else if (stkPnl2.Children.Count < 6)
                {
                    stkPnl2.Children.Add(ShortcutButton(shortcut));
                }
                else if (stkPnl3.Children.Count < 6)
                {
                    stkPnl3.Children.Add(ShortcutButton(shortcut));
                }
                else if (stkPnl4.Children.Count < 6)
                {
                    stkPnl4.Children.Add(ShortcutButton(shortcut));
                }
            }

        }

        private ImageButton ShortcutButton(Shortcut shortcutItem)
        {
            var imageButton = new ImageButton(shortcutItem.Image, shortcutItem.AppName)
            {
                Height = 110,
                Width = 110
            };

            // TODO double click open without close
            imageButton.btn.PreviewMouseLeftButtonDown += new((sender, e) =>
            {
                string msg = RunProcess.Run(shortcutItem.ExePath);
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    IsErrorDisplayed(msg);
                }
                else
                {
                    if (!IsErrorDisplayed(msg))
                    {
                        this.Close();
                    }
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

        private string FolderOpeningString(string? filePath)
        {
            return "explorer.exe " + "/select, \"" + filePath + "\"";
        }

        private bool IsErrorDisplayed(string error)
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
