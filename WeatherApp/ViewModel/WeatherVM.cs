﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        private string query;

        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged("Query");
            }
        }

        public ObservableCollection<City> Cities { get; set; }

        private CurrentConditions currrentConditions;

        public CurrentConditions CurrrentConditions
        {
            get { return currrentConditions; }
            set
            {
                currrentConditions = value;
                OnPropertyChanged("CurrrentConditions");
            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
                GetCurrentConditions();
            }
        }

        public SearchCommand SearchCommand { get; set; }

        public WeatherVM()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City
                {
                    LocalizedName = "New York"
                };
                CurrrentConditions = new CurrentConditions
                {
                    WeatherText = "Partly cloudy",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = "21"
                        }
                    }
                };
            }

            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }

        private async void GetCurrentConditions()
        {
            Query = string.Empty;
            Cities.Clear();
            CurrrentConditions = await AccuWeatherHelper.GetCurrrentConditions(SelectedCity.Key);
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);

            Cities.Clear();
            foreach(var city in cities)
            {
                Cities.Add(city);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
