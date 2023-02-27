using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Pr12
{
    public partial class MainPage : ContentPage
    {
        List<movie> _movies;
        object Items = null;
        List<movie> _findMovie;
        string _folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public MainPage()
        {
            InitializeComponent();
            Open();
            ChangeItem.IsEnabled = false;
            Datas.Id = 0;

        }

        private void FindM()
        {
            if (MovieList != null)
            {
                _findMovie = new List<movie>();
                for (int i = 0; i < _movies.Count; i++)
                {
                    if (_findMovie != null)
                    {
                        if (_movies[i].Genre == findGenre.Text)
                        {
                            _findMovie.Add(_movies[i]);
                        }

                    }
                    else
                    {
                        if (_movies[i].Genre == findGenre.Text)
                        {
                            _findMovie = new List<movie>
                             {
                               new movie {IdMovie = _movies[i].IdMovie, NameFilm = _movies[i].NameFilm, Genre = _movies[i].Genre, Length =_movies[i].Length, Year = _movies[i].Year } 
                             };

                        }
                    }
                }

            }
        }

        public void Open()
        {
            if (File.Exists(Path.Combine(_folderPath, "Pass.txt")) == true)
            {
                OpenPass();
                Datas.Movies = _movies.ToList();
                MovieList.ItemsSource = _movies;
            }
            else
            {
                _movies = new List<movie>
               {
                  new movie {IdMovie = 0,NameFilm = "fhalsjh", Genre = "QWE", Length = 1, Year = 1999 },
                  new movie {IdMovie = 1,NameFilm = "bckSJ", Genre = "jbvsjb", Length = 1, Year = 1999 },
                  new movie {IdMovie = 2,NameFilm = "bckSJ", Genre = "jbvsjb", Length = 1, Year = 1999 }
               };
                Datas.Id = 3;

            }
            MovieList.ItemsSource = _movies;
            Datas.IdChang = 0;
        }

        private void SavePass()
        {
            if (_movies != null)
            {
                StreamWriter outFile = new StreamWriter(Path.Combine(_folderPath, "Pass.txt"));
                outFile.WriteLine(_movies.Count);
                for (int i = 0; i < _movies.Count; i++)
                {
                    outFile.WriteLine(_movies[i].IdMovie);
                    outFile.WriteLine(_movies[i].NameFilm);
                    outFile.WriteLine(_movies[i].Genre);
                    outFile.WriteLine(_movies[i].Length);
                    outFile.WriteLine(_movies[i].Year);
                }
                outFile.Close();
            }
        }

        public void OpenPass()
        {
            StreamReader outFile = new StreamReader(Path.Combine(_folderPath, "Pass.txt"));
            int len = Convert.ToInt32(outFile.ReadLine());
            for (int i = 0; i < len; i++)
            {
                if (_movies == null)
                {
                    _movies = new List<movie>
                    {

                       new movie { IdMovie = Convert.ToInt32(outFile.ReadLine()), NameFilm = outFile.ReadLine(), Genre = outFile.ReadLine(), Length = Convert.ToInt32(outFile.ReadLine()), Year = Convert.ToInt32(outFile.ReadLine()) }
                    };
                }
                else
                {
                    _movies.Add(new movie { IdMovie = Convert.ToInt32(outFile.ReadLine()), NameFilm = outFile.ReadLine().ToString(), Genre = outFile.ReadLine().ToString(), Length = Convert.ToInt32(outFile.ReadLine()), Year = Convert.ToInt32(outFile.ReadLine()) });

                }

            }
            outFile.Close();
        }

        private void MovieList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                movie SelectedIte;
                Items = e.Item;
                SelectedIte = (movie)e.Item;
                SelectedText.Text = SelectedIte.NameFilm + " " + SelectedIte.Year;
                Datas.IdChang = SelectedIte.IdMovie;
                ChangeItem.IsEnabled = true;
            }
            else
            {
                ChangeItem.IsEnabled = false;
                DisplayAlert("Внимание", "Элемент не выбран", "ок");
            }

        }

        async private void AddItem_Clicked(object sender, EventArgs e)
        {
            Datas.Movies = _movies;
            await Navigation.PushAsync(new AddP());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (MovieList == null)
            {
                _movies = new List<movie>
                {
                     new movie {IdMovie = 0, NameFilm = "fhalsjh", Genre = "QWE", Length = 1, Year = 1999 },
                     new movie {IdMovie = 1, NameFilm = "bckSJ", Genre = "jbvsjb", Length = 1, Year = 1999 },
                     new movie {IdMovie = 2, NameFilm = "bckSJ", Genre = "jbvsjb", Length = 1, Year = 1999 }

                };
                MovieList.ItemsSource = _movies;
            }
            _movies = Datas.Movies;
            SavePass();
            MovieList.ItemsSource = null;
            MovieList.ItemsSource = Datas.Movies;
        }

        async private void ChangeItem_Clicked(object sender, EventArgs e)
        {
            movie movies = MovieList.SelectedItem as movie;
            Datas.IdMovie = movies.IdMovie;
            Datas.mov = movies;
            MovieList.ItemsSource = null;
            await Navigation.PushAsync(new ChangeP());
            SelectedText.Text = "";
        }

        private void RemoveItem_Clicked(object sender, EventArgs e)
        {
            movie movies = MovieList.SelectedItem as movie;
            if (movies != null)
            {
                Datas.Id--;
                _movies.Remove(movies);
            }
            else
            {
                DisplayAlert("Внимание", "Элемент не выбран", "ок");
            }
            Datas.Movies = _movies;
            MovieList.ItemsSource = null;
            MovieList.ItemsSource = Datas.Movies;
            SavePass();
            SelectedText.Text = "";
        }

        private void Find_Clicked(object sender, EventArgs e)
        {
            FindM();
            MovieList.ItemsSource = null;
            MovieList.ItemsSource = _findMovie;
        }
    }
}
