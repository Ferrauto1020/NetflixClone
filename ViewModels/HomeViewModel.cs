using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using NetflixClone.Models;
using NetflixClone.Services;
namespace NetflixClone.ViewModel
{

    public partial class HomeViewModel : ObservableObject
    {
        private readonly TmdbService _tmdbService;

        private Media _trendingMovie;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ShowMovieInfoBox))]
        private Media? _selectedMedia;

        public bool ShowMovieInfoBox => SelectedMedia is not null;

        public HomeViewModel(TmdbService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        public Media TrendingMovie
        {
            get => _trendingMovie;
            set
            {
                if (_trendingMovie != value)
                {
                    _trendingMovie = value;
                    OnPropertyChanged(nameof(TrendingMovie));
                }
            }
        }
        public ObservableCollection<Media> Trending { get; set; } = new();
        public ObservableCollection<Media> TopRated { get; set; } = new();
        public ObservableCollection<Media> NetflixOriginals { get; set; } = new();
        public ObservableCollection<Media> ActionMovies { get; set; } = new();


        public async Task InitializeAsync()
        {
            var trendingListTask = _tmdbService.GetTrendingAsync();
            var netflixOriginalsListTask = _tmdbService.GetNetflixOriginalsAsync();
            var topRatedListTask = _tmdbService.GetTopRatedAsync();
            var actionMovieListTask = _tmdbService.GetActionAsync();

            var medias = await Task.WhenAll(trendingListTask, netflixOriginalsListTask, topRatedListTask, actionMovieListTask);

            var trendingList = medias[0];
            var netflixOriginalsList = medias[1];
            var topRatedList = medias[2];
            var actionMovieList = medias[3];

            var random = new Random();

            var filteredTrendingList = trendingList
                .Where(t => !string.IsNullOrWhiteSpace(t.DisplayTitle) && !string.IsNullOrWhiteSpace(t.Thumbnail))
                .ToList();

            if (filteredTrendingList.Count > 0)
            {
                TrendingMovie = filteredTrendingList[random.Next(filteredTrendingList.Count)];
            }

            SetMediaCollection(trendingList, Trending);
            SetMediaCollection(netflixOriginalsList, NetflixOriginals);
            SetMediaCollection(topRatedList, TopRated);
            SetMediaCollection(actionMovieList, ActionMovies);

        }
        private static void SetMediaCollection(IEnumerable<Media> medias, ObservableCollection<Media> collection)
        {
            collection.Clear();
            foreach (var media in medias)
            {
                collection.Add(media);
            }
        }

        [RelayCommand]
        private void SelectMedia(Media? media = null)
        {
            if (media is not null)
            {
                if (media.Id == SelectedMedia?.Id)
                {

                    media = null;
                }
            }
            SelectedMedia = media;
        }

    }

}