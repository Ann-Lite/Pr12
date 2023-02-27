using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pr12
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddP : ContentPage
    {
        public AddP()
        {
            InitializeComponent();
            
        }
        private void Save_Clicked(object sender, EventArgs e)
        {
            if (int.TryParse(lenght.Text, out int lenght1) == true && int.TryParse(year.Text, out int year1) == true)
            {
                Datas.Id += 1;
                if(Datas.Movies != null)
                {
                  Datas.Movies.Add(new movie {IdMovie = Datas.Id, NameFilm = nameFilm.Text, Genre = genre.Text, Length = lenght1, Year = year1 });

                }
                else
                {
                    Datas.Movies = new List<movie>
                    {
                         new movie {IdMovie = Datas.Id, NameFilm = nameFilm.Text, Genre = genre.Text, Length = lenght1, Year = year1 }
                    };
                }

            }
            else DisplayAlert("Внимание", "Введенные данные неверны", "ок");
        }
    }
}