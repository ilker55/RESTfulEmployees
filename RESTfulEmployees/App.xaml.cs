﻿using RESTfulEmployees.Services;
using System.Configuration;
using System.Data;
using System.Windows;

namespace RESTfulEmployees
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IApiService ApiService;

        public App()
        {
            ApiService = new ApiService();
        }
    }
}