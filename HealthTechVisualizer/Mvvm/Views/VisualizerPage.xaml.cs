namespace HealthTechVisualizer.Mvvm.Views;

public partial class VisualizerPage : ContentPage
{
	List<string> MenuList;
	int MenuIndex;
	public VisualizerPage()
	{
		InitializeComponent();
		MenuList = new List<string>();
		MenuIndex = 1;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        MenuList.Add("Visualizer");
        MenuList.Add("Marker");     
        MenuList.Add("Rapor");
        TitleIndicator.Count = MenuList.Count;
        TitleIndicator.Position = 1;
    }
    private void HeaderChangerTap(object sender, TappedEventArgs e)
    {
		string result =  e.Parameter.ToString();
		
		switch (result)
		{
			case "Next":
				if(MenuIndex<=1)
				{ MenuIndex += 1; }

                break;
            case "Back":
				if(MenuIndex>=1)
                { MenuIndex -= 1; }
                
                break;
				
        }
        MenuTitle.Text = MenuList[MenuIndex];
        TitleIndicator.Position = MenuIndex;
        switch (MenuIndex)
        {
            case 1:
                MarkerLayout.IsVisible = true;
                RaporLayout.IsVisible = false;
                break;
            case 2:
                MarkerLayout.IsVisible = false;
                RaporLayout.IsVisible = true;
                break;
        }
    }
}