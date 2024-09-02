using NetflixClone.Models;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

using System.Windows.Input;
using NetflixClone.ViewModel;
using NetflixClone.Pages;
namespace NetflixClone.Controls;


public partial class MovieInfoBox : ContentView
{
	public static readonly BindableProperty MediaProperty =
	BindableProperty.Create("Media", typeof(Media), typeof(MovieInfoBox), null);
	public event EventHandler Closed;
	public MovieInfoBox()
	{
		InitializeComponent();
		ClosedCommand = new Command(ExecuteClosedCommand);
	}
	public Media Media
	{
		get => (Media)GetValue(MovieInfoBox.MediaProperty);
		set => SetValue(MovieInfoBox.MediaProperty, value);
	}
	public ICommand ClosedCommand { get; private set; }
	private void ExecuteClosedCommand()
	{
		Closed?.Invoke(this, EventArgs.Empty);
	}

	private void Button_Clicked(object sender, EventArgs e)
	=> Closed?.Invoke(this, EventArgs.Empty);

	private async void TapGestureRecognizer_Tapped	(object sender, TappedEventArgs e)
	{
		var parameters =  new Dictionary<string,object>
		{
			[nameof(DetailsViewModel.Media)] = Media,
		};
		await Shell.Current.GoToAsync(nameof(DetailsPage),true,parameters);
	}
}