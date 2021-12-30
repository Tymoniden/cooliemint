using CoolieMint.WindowsClient.Services.Container;
using CoolieMint.WindowsClient.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoolieMint.WindowsClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ContainerService.GetInstance<MainViewModel>();
        }

        private async void OpenFileSelection(object sender, RoutedEventArgs e)
        {
            if(DataContext is MainViewModel mainViewModel)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    mainViewModel.PathToNewPackage = openFileDialog.FileName;
                    //mainViewModel.SelectedPackageName = new FileInfo(openFileDialog.FileName).Name;

                    await mainViewModel.VerifyPackage();
                }
            }
        }

        private void UploadPackage(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.UploadPackage();
            }
        }
    }
}
