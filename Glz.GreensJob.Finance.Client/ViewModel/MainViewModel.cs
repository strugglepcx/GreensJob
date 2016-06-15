using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using Glz.GreensJob.Dto;
using Glz.GreensJob.Dto.RequestParams;
using Glz.Infrastructure;

namespace Glz.GreensJob.Finance.Client.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private string _host = ConfigurationManager.AppSettings["Host"];
        public ObservableCollection<ExtractApplyVewModel> ExtractApplys { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            ExtractApplys = new ObservableCollection<ExtractApplyVewModel>();
        }

        public async Task LoadExtractApplysAsync()
        {
            try
            {
                using (var proxy = new HttpClient())
                {
                    proxy.BaseAddress = new Uri(_host);
                    var result =
                        await proxy.PostAsJsonAsync("api/finance/v1/getExtractApplys", new GetExtractApplysRequestParam());
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var resultData = await result.Content.ReadAsAsync<ResultBase<IEnumerable<ExtractApplyVewModel>>>();
                        if (resultData.code == StatusCodes.Success)
                        {
                            ExtractApplys.Clear();
                            foreach (var extractApplyModel in resultData.Data)
                            {
                                ExtractApplys.Add(extractApplyModel);
                            }
                        }
                        else
                        {
                            MessageBox.Show(resultData.message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("服务器异常");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task CompleteExtractApplys()
        {
            try
            {
                var selectedItem = ExtractApplys.Where(x => x.IsSelected == true).Select(x => x.ID);
                if (!selectedItem.Any())
                {
                    throw new Exception("请选择需要确认的项");
                }
                using (var proxy = new HttpClient())
                {
                    proxy.BaseAddress = new Uri(_host);
                    var result =
                        await proxy.PostAsJsonAsync("api/finance/v1/completeExtractApplys", new CompleteExtractApplysRequestParam
                        {
                            ExtractApplyIdList = selectedItem
                        });
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var resultData = await result.Content.ReadAsAsync<ResultBase<IEnumerable<ExtractApplyVewModel>>>();
                        if (resultData.code == StatusCodes.Success)
                        {
                            await LoadExtractApplysAsync();
                        }
                        else
                        {
                            MessageBox.Show(resultData.message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("服务器异常");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AllSelected(bool selected)
        {
            foreach (var extractApplyVewModel in ExtractApplys)
            {
                extractApplyVewModel.IsSelected = selected;
            }
        }
    }
}