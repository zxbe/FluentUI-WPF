using FluentUI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FluentUI
{
    public enum DialogMode
    {
        MessageDialog,

        ProgressDialog,

        CustomContent
    }

    public enum DialogResult
    {
        OK,

        Cancel,

        Delete
    }
    
    public class FluentDialog : Control
    {
        static internal TaskCompletionSource<DialogResult> TaskCompletionSource { get; set; }
        static internal CancellationTokenSource CancellationTokenSource { get; set; }            
        private static List<FrameworkElement> dialogStack = new List<FrameworkElement>();
        private static Grid hostControl;
        private static Grid childHost;
        private static ContentControl viewControl;
        private static ScrollViewer scrollViewer;
        private static Progress<string> ProgressHandler;    

        public static object ExternalTaskCompletionSource { get; set; }

        public FluentDialog() { }

        private static void GetHostWindow()
        {
            hostControl = Application.Current.MainWindow.Template
                .FindName("PART_WindowChromeHost", Application.Current.MainWindow) as Grid;
        }

        private static void DisplayDialog(object content)
        {
            childHost = new Grid();

            //Create a new brush to add a transparent effect
            var brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#f3f2f1");
            brush.Opacity = 0.50;

            childHost.Background = brush;

            viewControl = new ContentControl();
            viewControl.Content = content;

            //Create ScrollViewer and sets its content to the viewControl to prevent views that have a height greater than the screen.
            scrollViewer = new ScrollViewer();
            scrollViewer.Content = viewControl;

            childHost.Children.Add(scrollViewer);

            //Sets the MainWindow grid row
            Grid.SetRow(childHost, 1);

            //Adds the new content into the MainWindow Grid
            hostControl.Children.Add(childHost);

            //Adds the new content to the dialogStack (List<FrameworkElement>)
            dialogStack.Add(childHost);

            //Disable all controls except the last one
            foreach (UIElement ctrl in hostControl.Children)
            {
                ctrl.IsEnabled = false;
            }

            var control = dialogStack.Last();
            control.IsEnabled = true;
        }

        private static void HideDialog()
        {
            if (dialogStack.Count > 0)
            {
                // Removes the last overlay from the MainWindow Grid
                hostControl.Children.Remove(dialogStack.Last());

                // Removes the last content from the CallingStack
                dialogStack.RemoveAt(dialogStack.Count - 1);
                if (dialogStack.Count == 0)
                {
                    foreach (UIElement ctrl in hostControl.Children)
                    {
                        ctrl.IsEnabled = true;
                    }
                }
                else
                {
                    dialogStack.Last().IsEnabled = true;
                }
            }
        }        

        public static void Show(object content)
        {
            GetHostWindow();

            DisplayDialog(new CustomDialog(content));
        }

        public static async Task<T> ShowAsync<T>(object content)
        {
            GetHostWindow();

            ExternalTaskCompletionSource = new TaskCompletionSource<T>();           

            DisplayDialog(new CustomDialog(content));

            return await ((TaskCompletionSource<T>)ExternalTaskCompletionSource).Task;
        }

        public static void Show(string title, string message, FluentDialogOptions dialogOptions = null)
        {
            GetHostWindow();

            var dialog = new MessageDialog(dialogOptions);
            dialog.Message = message;
            dialog.Title = title;

            DisplayDialog(dialog);
        }

        public static async Task<DialogResult> ShowAsync(string title, string message, FluentDialogOptions dialogOptions = null)
        {
            TaskCompletionSource = new TaskCompletionSource<DialogResult>();

            GetHostWindow();

            var dialog = new MessageDialog(dialogOptions);
            dialog.Message = message;
            dialog.Title = title;

            DisplayDialog(dialog);

            return await TaskCompletionSource.Task;           
        }

        public static void ShowProgress(string title, string message, CancellationTokenSource cancellationTokenSource = null)
        {
            CancellationTokenSource = cancellationTokenSource;

            GetHostWindow();

            var dialog = new ProgressDialog();
            dialog.Message = message;
            dialog.Title = title;

            DisplayDialog(dialog);

            ProgressHandler = new Progress<string>(message =>
            {
                dialog.Message = message;
            });            
        }       

        public static async Task ShowProgressAsync(string title, string message, Action action, CancellationTokenSource cancellationTokenSource = null)
        {
            CancellationTokenSource = cancellationTokenSource;

            GetHostWindow();

            var dialog = new ProgressDialog();
            dialog.Message = message;
            dialog.Title = title;

            DisplayDialog(dialog);

            ProgressHandler = new Progress<string>(message =>
            {
                dialog.Message = message;
            });

            await Task.Run(action, cancellationTokenSource.Token);

            Close();
        }

        public static void UpdateDialog(string message)
        {
            ((IProgress<string>)ProgressHandler).Report(message);
        }

        public static void Close()
        {
            HideDialog();            
        }        
    }        
}
