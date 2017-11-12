using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace Cardozo_Claudia_Clima
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string APPID = "80119fb52897c09bafcc0b0f63ed226c";
        string nombreCiudad = "Asuncion";

        public MainWindow()
        {
            InitializeComponent();
            inizializarCbo();
            getClima("Asuncion");
            getPronostico("Asuncion");
                
        }

        void inizializarCbo()
        {
            //Lista de Ciudades
            List<cboData> listCiudades = new List<cboData>();
            listCiudades.Add(new cboData { Id = 0, Nombre = "" });
            listCiudades.Add(new cboData { Id = 1, Nombre = "Asuncion" });
            listCiudades.Add(new cboData { Id = 2, Nombre = "Capiata" });
            listCiudades.Add(new cboData { Id = 3, Nombre = "Cordoba" });
            listCiudades.Add(new cboData { Id = 4, Nombre = "Lambare" });
            listCiudades.Add(new cboData { Id = 5, Nombre = "Kyoto" });
            listCiudades.Add(new cboData { Id = 6, Nombre = "Barcelona" });
            listCiudades.Add(new cboData { Id = 7, Nombre = "Encarnacion" });
            
            cboCiudad.ItemsSource = listCiudades;
            cboCiudad.DisplayMemberPath = "Nombre";
            cboCiudad.SelectedValuePath = "Id";

            cboCiudad.SelectedValue = "0";



        }
        

        void getClima(string city)
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q=Asuncion&appid=80119fb52897c09bafcc0b0f63ed226c&units=metric&cnt=6&lang=es", city, APPID);
                var json = web.DownloadString(url);
                var result = JsonConvert.DeserializeObject<InfoClima.root>(json);
                InfoClima.root outPut = result;

                //**//
                imgClima.Source = setIcon(outPut.weather[0].icon);
                lblCiudad.Content = string.Format("{0}", outPut.name);                
                lblPais.Content = string.Format("{0}", outPut.sys.country);
                lblVelViento.Content = string.Format("{0} km/h", outPut.wind.speed);
                lblGrados.Content = string.Format("{0}°C", outPut.main.temp);
                lblMinimo.Content = string.Format("{0}°C", outPut.main.temp_min);
                lblMaximo.Content = string.Format("{0}°C", outPut.main.temp_max);
                lblHumedad.Content = string.Format("{0}%", outPut.main.humidity);
                lblPresionAtm.Content = string.Format("{0}hPa", outPut.main.pressure);
                lblCielo.Content = string.Format("{0}", outPut.weather[0].main);
                lblCondicion.Content = string.Format("{0}", outPut.weather[0].description);
            

            }
         }
        DateTime getDate(double millisecound)
        {

            DateTime day = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(millisecound).ToLocalTime();

            return day;
        }
        BitmapImage setIcon(string iconID)
        {
            string url = string.Format("https://openweathermap.org/img/w/{0}.png", iconID); // weather icon resource 
            BitmapImage weatherImg = new BitmapImage();
            weatherImg.BeginInit();
            weatherImg.UriSource = new Uri(url);
            weatherImg.EndInit();

            return weatherImg;
        }
        void getPronostico(string city)
        {
            using (WebClient web = new WebClient()) {
                var day = 5;
                string url = string.Format("http://api.openweathermap.org/data/2.5/forecast?q={0}&appid={1}&units=metric&cnt={2}&lang=es", city, APPID, day);
                var json = web.DownloadString(url);
                var Object = JsonConvert.DeserializeObject<InfoPronostico>(json);
                InfoPronostico forcast = Object;
                
                lblGrados_Copy.Content = string.Format("{0}°C", forcast.list[1].main.temp); // por alguna razon no me toma la temperatura (solucionado problema de nombres de clases xd)
                lblCond.Content = string.Format("{0}", forcast.list[1].weather[0].description);
                lblCondicion.Content = string.Format("{0}", forcast.list[1].weather[0].main);
                lblHumedad_Copy.Content = string.Format("{0} %", forcast.list[1].main.humidity); // por alguna razon no me toma la humedad 
                lblMaximo_Copy.Content = string.Format("{0}°C", forcast.list[1].main.temp_max);
                lblMinimo_Copy.Content = string.Format("{0}°C", forcast.list[1].main.temp_min);
            }
        }
       

        private void cboCiudad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           nombreCiudad = cboCiudad.SelectedItem.ToString();
            
        }
    }
      
        
 }

