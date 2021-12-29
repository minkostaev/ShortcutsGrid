﻿using ShortcutsGrid.Controls;
using ShortcutsGrid.Models;
using ShortcutsGrid.Services;
using ShortcutsGrid.Services.Run;
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
                var shortcutButton = ShortcutButton(shortcut);
                #region fill panels with buttons
                if (stkPnl1.Children.Count < 6)
                {
                    stkPnl1.Children.Add(shortcutButton);
                }
                else if (stkPnl2.Children.Count < 6)
                {
                    stkPnl2.Children.Add(shortcutButton);
                }
                else if (stkPnl3.Children.Count < 6)
                {
                    stkPnl3.Children.Add(shortcutButton);
                }
                else if (stkPnl4.Children.Count < 6)
                {
                    stkPnl4.Children.Add(shortcutButton);
                }
                #endregion
            }

        }

        private ImageButton ShortcutButton(Shortcut shortcutItem)
        {
            var imageButton = new ImageButton(shortcutItem.Image, shortcutItem.AppName)
            {
                Height = 110,
                Width = 110
            };
            imageButton.btn.Click += delegate
            {
                string msg = RunProcess.Run(shortcutItem.ExePath);
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    this.Close();
                }
            };
            imageButton.mnOpen.Click += delegate
            {
                string msg = RunProcess.Run(shortcutItem.ExePath);
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };
            imageButton.mnAdmin.Click += delegate
            {
                string msg = RunProcess.Run(shortcutItem.ExePath, true);
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };
            imageButton.mnFolder.Click += delegate { MessageBox.Show("Folder"); };
            return imageButton;
        }

    }
}
