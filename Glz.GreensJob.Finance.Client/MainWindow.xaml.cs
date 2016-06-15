using System;
using System.Collections.Generic;
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
using Glz.GreensJob.Finance.Client.ViewModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Glz.GreensJob.Finance.Client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly MainViewModel _viewModel;
        public MainWindow()
        {
            _viewModel = new MainViewModel();
            InitializeComponent();

            DataContext = _viewModel;
        }

        private async void MetroWindow_ContentRendered(object sender, EventArgs e)
        {
            var progress = await this.ShowProgressAsync("正在加载", "请稍后...");
            await _viewModel.LoadExtractApplysAsync();
            await progress.CloseAsync();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var progress = await this.ShowProgressAsync("正在加载", "请稍后...");
            await _viewModel.LoadExtractApplysAsync();
            await progress.CloseAsync();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var progress = await this.ShowProgressAsync("提交中", "请稍后...");
            await _viewModel.CompleteExtractApplys();
            await progress.CloseAsync();
        }

        private void AllChk_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.AllSelected(AllChk.IsChecked.Value);
        }
    }
}
