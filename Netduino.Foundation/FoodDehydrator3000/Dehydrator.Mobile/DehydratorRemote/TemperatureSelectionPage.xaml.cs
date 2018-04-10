﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace DehydratorRemote
{
    public partial class TemperatureSelectionPage : ContentPage
    {
        ObservableCollection<Temperature> temperatures;
        public TemperatureSelectionPage()
        {
            InitializeComponent();

            temperatures = new ObservableCollection<Temperature>();

            temperatures.Add(new Temperature { DisplayName = "155°F(68°C)", Description = "Jerky", Degrees = 155 });
            temperatures.Add(new Temperature { DisplayName = "145°F(63°C)", Description = "Meat & Fish", Degrees = 145 });
            temperatures.Add(new Temperature { DisplayName = "135°F(57°C)", Description = "Fruits", Degrees = 135 });
            temperatures.Add(new Temperature { DisplayName = "125°F(52°C)", Description = "Vegetables & Herbs", Degrees = 125 });
            temperatures.Add(new Temperature { DisplayName = "115°F(46°C)", Description = "Herbs", Degrees = 115 });
            temperatures.Add(new Temperature { DisplayName = "105°F(41°C)", Description = "Herbs", Degrees = 105 });
            temperatures.Add(new Temperature { DisplayName = "95°F(35°C)", Description = "Herbs", Degrees = 95 });

            TemperatureView.ItemsSource = temperatures;
            TemperatureView.ItemSelected += TemperatureView_ItemSelected;
        }

        async void TemperatureView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((DehydratorRemotePage)this.Navigation.NavigationStack[0]).MyTemp = e.SelectedItem as Temperature;
            await Navigation.PopAsync();
        }
    }
}
