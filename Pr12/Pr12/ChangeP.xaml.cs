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
    public partial class ChangeP : ContentPage
    {

            List<movie> _movies;
            movie movies;
        public ChangeP()
        {
            InitializeComponent();
            movies = Datas.mov;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            nameFilm.Text = movies.NameFilm;
            genre.Text = movies.Genre;
            lenght.Text = movies.Length.ToString();
            year.Text = movies.Year.ToString();
            Enable(0);
        }
        private void Save_Clicked(object sender, EventArgs e)
        {
            if (Datas.IdChang != -1 && int.TryParse(lenght.Text, out int lenght1) == true && int.TryParse(year.Text, out int year1) == true)
            {
                _movies = Datas.Movies;
                if (_movies == null)
                {
                    _movies.Add(new movie { NameFilm = nameFilm.Text, Genre = genre.Text, Length = lenght1, Year = year1 });
                    Datas.Movies = _movies;
                }
                else
                {
                    int x = _movies.IndexOf(movies);
                    _movies[x]= new movie {IdMovie = Datas.IdMovie, NameFilm = nameFilm.Text, Genre = genre.Text, Length = lenght1, Year = year1 };
                    Datas.Movies = _movies;
                }

            }
            else DisplayAlert("Внимание", "Введенные данные неверны", "ок");
            Enable(0);
        }

        private void Enable(int x)
        {
            if (x == 0)
            {
                name.IsEnabled = false;
                gen.IsEnabled = false;
                yea.IsEnabled = false;
                len.IsEnabled = false;
                Save.IsVisible = false;
                Chang.IsVisible = true;
            }
            else
            {
                name.IsEnabled = true;
                gen.IsEnabled = true;
                yea.IsEnabled= true;
                len.IsEnabled = true;
                Save.IsVisible = true;
                Chang.IsVisible = false;
            }
        }

        private void Chang_Clicked(object sender, EventArgs e)
        {
            Enable(1);
        }
    }
}