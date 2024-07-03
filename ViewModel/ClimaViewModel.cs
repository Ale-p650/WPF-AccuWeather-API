using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.View;
using WpfApp1.ViewModel.API;
using WpfApp1.ViewModel.Comandos;

namespace WpfApp1.ViewModel
{
    public class ClimaViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Ciudad> Ciudades { get; set; }
        public ComandoBusqueda cb { get; set; }

        public ClimaViewModel()
        {
            if(DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                miCiudad = new Ciudad() { LocalizedName = "Buenos Aires" };
                miClima = new Clima() { WeatherText = "Soleado", Temperature = new Temperature() {Metric= new Unidad {Value="20" } } };
            }

            cb = new ComandoBusqueda(this);
            Ciudades = new ObservableCollection<Ciudad>();
            
        }

        public async void hacerQuery()
        {
            var ciudades = await AccuWeatherAPI.GetCiudad(Query);

            Ciudades.Clear();

            foreach(Ciudad c in ciudades)
            {
                Ciudades.Add(c);
            }
            
        }

        public async void obtenerClima()
        {
            Query = string.Empty;
            
            //Ciudades.Clear();

            miClima = await AccuWeatherAPI.GetCLima(miCiudad.Key);
        }

        private string _query;

        public string Query
        {
            get { return _query; }
            set { 
                
                _query = value;
                OnPropertyChanged("Query");

            
            }
        }

        private Clima _miclima;

        public Clima miClima
        {
            get { return _miclima; }
            set { 
                
                _miclima = value;
                OnPropertyChanged("miClima");
            
            
            }
        }

        private Ciudad _miciudad;

        public Ciudad miCiudad
        {
            get { return _miciudad; }
            set { _miciudad = value;
                OnPropertyChanged("miCiudad");
                obtenerClima();

            }
        }

        


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string parameter)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(parameter));
        }
    }
}
