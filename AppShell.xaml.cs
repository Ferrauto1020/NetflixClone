using NetflixClone.Pages;
using NetflixClone.ViewModel;

namespace NetflixClone;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(CategoriesPage),typeof(CategoriesPage));
		Routing.RegisterRoute(nameof(DetailsPage),typeof(DetailsPage));
	}
}
