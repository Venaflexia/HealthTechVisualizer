using CommunityToolkit.Maui.Core.Primitives;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace HealthTechVisualizer.Mvvm.Views;

public partial class VisualizerPage : ContentPage
{
	List<string> MenuList;
	int MenuIndex;
    bool drawingMod;
    int tiklamaSayisi;
    double x1, x2, x3;
    double y1, y2, y3;
    BoxView boxView1 = new BoxView() { Color = Colors.Red, HeightRequest = 10, WidthRequest = 10, CornerRadius = 5 };
    BoxView boxView2 = new BoxView() { Color = Colors.DodgerBlue, HeightRequest = 10, WidthRequest = 10, CornerRadius = 5 };
    BoxView boxView3 = new BoxView() { Color = Colors.Red, HeightRequest = 10, WidthRequest = 10, CornerRadius = 5 };
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

        //Çizim Modu
        List<Microsoft.Maui.Graphics.Color> mylistColors = new List<Microsoft.Maui.Graphics.Color>();
        mylistColors.Add(Colors.Red);
        mylistColors.Add(Colors.Green);
        mylistColors.Add(Colors.Yellow);      
        mylistColors.Add(Colors.Black);
        mylistColors.Add(Colors.White);
        ColorPicker.ItemsSource = mylistColors;

        //Açý Ölçer
        tiklamaSayisi = 0;

        //boxView1.IsVisible = false;
        //boxView2.IsVisible = false;
        //boxView3.IsVisible = false;
        //line.IsVisible = false;
        GoniometerAbsoluteLayout.IsVisible = false;
        DrawingThicknessSlider.Value = 10;
    }

    private void MediaElement1_Loaded(object sender, EventArgs e)
    {
        mySliderVideo.Maximum = MediaElement1.Duration.TotalMicroseconds;
        mySliderVideo.Value = 0;
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
            case 0:
                VisualizerLayout.IsVisible = true;
                MarkerLayout.IsVisible = false;
                RaporLayout.IsVisible = false;
                break;
            case 1:
                MarkerLayout.IsVisible = true;
                RaporLayout.IsVisible = false;
                VisualizerLayout.IsVisible = false;
                break;
            case 2:
                MarkerLayout.IsVisible = false;
                RaporLayout.IsVisible = true;
                VisualizerLayout.IsVisible = false;
                break;
        }
    }

    private async void MenuFlyoutItem_ClickedOpenVideo(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Videoyu Seçiniz",
            FileTypes = FilePickerFileType.Videos
        });

        if (result != null)
        {
            MediaElement1.Source = result.FullPath;
            
        }
    }

    private void MenuFlyoutItem_ClickedCloseProject(object sender, EventArgs e)
    {
        if(MediaElement1.Source != null) 
        {

            MediaElement1.Source = null;
        }
    }

    private void MediaElement1_PositionChanged(object sender, CommunityToolkit.Maui.Core.Primitives.MediaPositionChangedEventArgs e)
    {
        mySliderVideo.Maximum = MediaElement1.Duration.TotalMicroseconds;
        mySliderVideo.Value = e.Position.TotalMicroseconds;
    }

    private void mySliderVideo_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        MediaElement1.SeekTo(TimeSpan.FromMicroseconds(e.NewValue));
    }

    private void TapGestureRecognizerPlayPouse_Tapped(object sender, TappedEventArgs e)
    {
        if (MediaElement1.CurrentState == MediaElementState.Paused || MediaElement1.CurrentState == MediaElementState.Stopped)
        {
            MediaElement1.Play();
        }
        else
        {
            MediaElement1.Pause();
        }
    }

    //Alt kýsým çizim ile ilgili
    private void myDrawingView_Loaded(object sender, EventArgs e)
    {
        drawingMod = true;
    }
    private void Switch_ToggledMultiLine(object sender, ToggledEventArgs e)
    {
        if (e.Value && drawingMod)
        {

            myDrawingView.IsMultiLineModeEnabled = true;
        }
        else { myDrawingView.IsMultiLineModeEnabled = false; }
    }
    private void Switch_ToggledDrawingEnable(object sender, ToggledEventArgs e)
    {

        if (e.Value && drawingMod)
        {
            myDrawingView.IsEnabled = true;
            GoniometerRadioButton.IsChecked = false;

        }

        else if (!e.Value && drawingMod)
        {
            myDrawingView.IsEnabled = false;

        }

    }
    private void Button_ClickedCizimiTemizle(object sender, EventArgs e)
    {
        myDrawingView.Clear();
    }


    private void mySlider_ValueChangedDrawingView(object sender, ValueChangedEventArgs e)
    {

        if (drawingMod)
        {
            int value = (int)e.NewValue;
            myDrawingView.LineWidth = value;

        }

    }
    private void Picker_SelectedIndexChangedLineColor(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;

        if (drawingMod)
        {
            if (picker.SelectedItem != null)
            {
                myDrawingView.LineColor = picker.SelectedItem as Microsoft.Maui.Graphics.Color;
            }

        }

    }

    //Alt kýsým açý ölçer le ilgili



}