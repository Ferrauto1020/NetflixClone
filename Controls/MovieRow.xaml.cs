using System.Collections.Generic;
using Microsoft.Maui.Controls;
using NetflixClone.Models;

using System.Windows.Input;
using NetflixClone.ViewModel;

namespace NetflixClone.Controls
{

	public class MediaSelectedEventArgs : EventArgs
	{
		public Media Media
		{
			get; set;
		}
		public MediaSelectedEventArgs(Media media) => Media = media;
	}

	public partial class MovieRow : ContentView
	{
		// BindableProperty per la proprietà Heading
		public static readonly BindableProperty HeadingProperty =
			BindableProperty.Create(
				nameof(Heading), // Nome della proprietà
				typeof(string), // Tipo della proprietà
				typeof(MovieRow), // Tipo del controllo
				string.Empty); // Valore predefinito

		// BindableProperty per la proprietà Movies
		public static readonly BindableProperty MoviesProperty =
			BindableProperty.Create(nameof(Movies), typeof(IEnumerable<Media>), typeof(MovieRow), Enumerable.Empty<Media>());

		// BindableProperty per la proprietà IsLarge
		public static readonly BindableProperty IsLargeProperty =
			BindableProperty.Create(
				nameof(IsLarge), // Nome della proprietà
				typeof(bool), // Tipo della proprietà
				typeof(MovieRow), // Tipo del controllo
				false); // Valore predefinito


		public event EventHandler<MediaSelectedEventArgs> MediaSelected;

		public MovieRow()
		{
			InitializeComponent();
			MediaDetailsCommand = new Command(ExecuteMediaDetailsCommand);
		}


		public string Heading
		{
			get => (string)GetValue(MovieRow.HeadingProperty);
			set => SetValue(HeadingProperty, value);
		}

		public IEnumerable<Media> Movies
		{
			get => (IEnumerable<Media>)GetValue(MovieRow.MoviesProperty);
			set => SetValue(MoviesProperty, value);
		}

		public bool IsLarge
		{
			get => (bool)GetValue(MovieRow.IsLargeProperty);
			set => SetValue(IsLargeProperty, value);
		}

		public bool IsNotLarge => !IsLarge;

		public ICommand MediaDetailsCommand { get; private set; }
		private void ExecuteMediaDetailsCommand(object parameter)
		{
			if(parameter is Media media && media is not null)
			{
				MediaSelected?.Invoke(this, new MediaSelectedEventArgs(media));
			}
		}

	}
}
