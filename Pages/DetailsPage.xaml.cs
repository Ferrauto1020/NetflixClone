using NetflixClone.Models;
using NetflixClone.ViewModel;

namespace NetflixClone.Pages;

public partial class DetailsPage : ContentPage
{
	private readonly DetailsViewModel _detailsViewModel;
	
	public DetailsPage(DetailsViewModel detailsViewModel)
	{
		_detailsViewModel = detailsViewModel;
		InitializeComponent();
		BindingContext = _detailsViewModel;
	}

	protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width,height);
		if(width>0)
		{
			_detailsViewModel.SimilarItemWidth= Convert.ToInt32(width/3) - 3 ;
		}
	}
	protected async override void OnAppearing()
	{
		base.OnAppearing();
		await _detailsViewModel.InitializeAsync();
	}
	private void TrailersTab_Tapped(object sender, TappedEventArgs e)
	{
		similarTabIndicator.Color = Colors.Black;
		similarTabContent.IsVisible = false;
		trailersTabIndicator.Color = Colors.Red;
		trailersTabContent.IsVisible = true;

	}
	private void Similar_Tapped(object sender, TappedEventArgs e)
	{
		trailersTabIndicator.Color = Colors.Black;
		trailersTabContent.IsVisible = false;
		similarTabIndicator.Color = Colors.Red;
		similarTabContent.IsVisible = true;

	}

	
}