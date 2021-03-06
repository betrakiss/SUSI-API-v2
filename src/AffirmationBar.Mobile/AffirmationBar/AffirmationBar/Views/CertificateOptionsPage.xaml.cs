﻿using AffirmationBar.Services;
using AffirmationBar.ViewModels;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using SusiAPIClient;
using SusiAPICommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AffirmationBar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CertificateOptionsPage : ContentPage
    {
        public CertificateOptionsViewModel CertificateOptionsViewModel { get; private set; }
        public CertificateOptionsPage(StudentInfo info)
        {
            CertificateOptionsViewModel = new CertificateOptionsViewModel(info);
            CertificateOptionsViewModel.Student.Reason = CertificateOptionsViewModel.Reasons[0];

            BindingContext = CertificateOptionsViewModel;

            InitializeComponent();
        }

        private async void OnGenerateClicked(object sender, EventArgs e)
        {
            Certificate certificate = await CertificateOptionsViewModel.GetCertificateAsync();
            if (certificate != null)
            {
                IStorageService storageService = DependencyService.Get<IStorageService>();

                if (await storageService.SaveFile($"certificate_{CertificateOptionsViewModel.Student.FacultyNumber}.html", certificate.ToByteArray()))
                    await DisplayAlert("Успех", $"Файлът беше запазен в {storageService.FilePath}.", "OK");
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await grid.FadeTo(1, 500);
        }
    }
}
