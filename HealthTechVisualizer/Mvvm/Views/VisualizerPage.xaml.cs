using CommunityToolkit.Maui.Core.Primitives;
using Microsoft.Maui.Controls.Shapes;
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
    BoxView boxView1 = new BoxView() { Color = Colors.Yellow, HeightRequest = 10, WidthRequest = 10, CornerRadius = 5 };
    BoxView boxView2 = new BoxView() { Color = Colors.Orange, HeightRequest = 10, WidthRequest = 10, CornerRadius = 5 };
    BoxView boxView3 = new BoxView() { Color = Colors.Yellow, HeightRequest = 10, WidthRequest = 10, CornerRadius = 5 };
    Polyline line = new Polyline() {Stroke=Colors.Gray,StrokeThickness=2 };
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
        mylistColors.Add(Colors.White);
        mylistColors.Add(Colors.Black);
        mylistColors.Add(Colors.Blue);
        mylistColors.Add(Colors.Purple);
        mylistColors.Add(Colors.Orange);
        mylistColors.Add(Colors.Gray);
        mylistColors.Add(Colors.Pink);
        mylistColors.Add(Colors.Yellow);      
        ColorPicker.ItemsSource = mylistColors;
        GoniometerLineColorPicker.ItemsSource= mylistColors;
        CentreGoniometerPointColorPicker.ItemsSource = mylistColors;
        SideGoniometerPointColorPicker.ItemsSource=mylistColors;
        //Açý Ölçer
        tiklamaSayisi = 0;

        //boxView1.IsVisible = false;
        //boxView2.IsVisible = false;
        //boxView3.IsVisible = false;
        //line.IsVisible = false;
        GoniometerAbsoluteLayout.IsVisible = false;
        DrawingThicknessSlider.Value = 10;
        GoniometerLineThickness.Value = 2;
        GoniometerPointSize.Value = 10;
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
        Image senderr=sender as Image;
        if (MediaElement1.CurrentState == MediaElementState.Paused || MediaElement1.CurrentState == MediaElementState.Stopped)
        {
            MediaElement1.Play();
            //  LblVideoStartStop.Text = "\uf04d";
            senderr.Source = "durdur.png";

        }
        else
        {
            MediaElement1.Pause();
            senderr.Source = "baslat.png";
          //  LblVideoStartStop.Text = "\uf04b";

        }
    }
    double secondvalue = 50000;
    private void TapGestureRecognizer_Tapped_Forward(object sender, TappedEventArgs e)
    {
        mySliderVideo.Value = mySliderVideo.Value + secondvalue;
    }

    private void TapGestureRecognizer_Tapped_Back(object sender, TappedEventArgs e)
    {
        mySliderVideo.Value = mySliderVideo.Value - secondvalue;
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





    //Alt kýsým açý ölçer le ilgili

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

    private void GoniometerPointSize_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        boxView1.WidthRequest = (int)e.NewValue;
        boxView1.HeightRequest = (int)e.NewValue;
        boxView1.CornerRadius = (int)e.NewValue / 2;
        boxView2.WidthRequest = (int)e.NewValue;
        boxView2.HeightRequest = (int)e.NewValue;
        boxView2.CornerRadius = (int)e.NewValue / 2;
        boxView3.WidthRequest = (int)e.NewValue;
        boxView3.HeightRequest = (int)e.NewValue;
        boxView3.CornerRadius = (int)e.NewValue / 2;

    }

    private void CentreGoniometerPointColorPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;

        if (drawingMod)
        {
            if (picker.SelectedItem != null)
            {
                boxView2.Color = picker.SelectedItem as Microsoft.Maui.Graphics.Color;
            }
        }
    }

    private void SideGoniometerPointColorPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;

        if (drawingMod)
        {
            if (picker.SelectedItem != null)
            {
                boxView1.Color = picker.SelectedItem as Microsoft.Maui.Graphics.Color;
                boxView3.Color = picker.SelectedItem as Microsoft.Maui.Graphics.Color;
            }
        }
    }

    private void GoniometerLineColorPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;

        if (drawingMod)
        {
            if (picker.SelectedItem != null)
            {
                line.Stroke = picker.SelectedItem as Microsoft.Maui.Graphics.Color;
            }
        }
    }

    private void TapGestureRecognizer_Tapped_Mute(object sender, TappedEventArgs e)
    {
        Image senderr = sender as Image;
        if (MediaElement1.ShouldMute)
        {
            MediaElement1.ShouldMute = false;
            // LblMute.Text = "\uf028";
            senderr.Source = "sound.png";
        }
        else
        {
            MediaElement1.ShouldMute=true;
            //  LblMute.Text = "\uf6a9";
            senderr.Source = "mute.png";
        }
    }

    private void MenuFlyoutItem_ClickedSelectSecondBack(object sender, EventArgs e)
    {
        MenuFlyoutItem result = sender as MenuFlyoutItem;
        double newsecondvalue = Convert.ToDouble(result.Text);
        secondvalue = newsecondvalue * 1000000;
    }

    private void GoniometerLineThickness_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        line.StrokeThickness = (int)e.NewValue;
    }
    private void RadioButton_CheckedChangedAciOlcer1(object sender, CheckedChangedEventArgs e)
    {
        if (GoniometerRadioButton.IsChecked)
        {

            //boxView1.IsVisible = true;
            //boxView2.IsVisible = true;
            //boxView3.IsVisible = true;
            //line.IsVisible= true;
            if (!GoniometerAbsoluteLayout.Contains(boxView1))
            {
                GoniometerAbsoluteLayout.Children.Add(boxView1);
                GoniometerAbsoluteLayout.Children.Add(boxView2);
                GoniometerAbsoluteLayout.Children.Add(boxView3);
                GoniometerAbsoluteLayout.Children.Add(line);
            }


            GoniometerAbsoluteLayout.IsVisible = true;
            DrawingSwitch.IsToggled = false;

        }
    }
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (!myDrawingView.IsEnabled)
        {
            if (GoniometerRadioButton.IsChecked)
            {
                if (!GoniometerAbsoluteLayout.Children.Contains(boxView1))
                {
                    GoniometerAbsoluteLayout.Children.Add(boxView1);
                    GoniometerAbsoluteLayout.Children.Add(boxView2);
                    GoniometerAbsoluteLayout.Children.Add(boxView3);
                    GoniometerAbsoluteLayout.Children.Add(line);

                }
                tiklamaSayisi++; Microsoft.Maui.Graphics.Point? relativeToContainerPosition = e.GetPosition((View)sender);
                double x = relativeToContainerPosition.Value.X;
                double y = relativeToContainerPosition.Value.Y;
                // AbsoluteLayout result = sender as AbsoluteLayout;

                switch (tiklamaSayisi)
                {
                    case 1:
                        x1 = x;
                        y1 = y;
                        GoniometerAbsoluteLayout.SetLayoutBounds(boxView1, new Rect(x - 50, y - 50, 100, 100));

                        break;
                    case 2:
                        x2 = x;
                        y2 = y;
                        GoniometerAbsoluteLayout.SetLayoutBounds(boxView2, new Rect(x - 50, y - 50, 100, 100));

                        break;
                    case 3:
                        x3 = x;
                        y3 = y;
                        GoniometerAbsoluteLayout.SetLayoutBounds(boxView3, new Rect(x - 50, y - 50, 100, 100));

                        tiklamaSayisi = 0;

                        line.Points = new Microsoft.Maui.Controls.PointCollection
                {
                    new Microsoft.Maui.Graphics.Point(x1, y1),
                    new Microsoft.Maui.Graphics.Point(x2, y2),
                    new Microsoft.Maui.Graphics.Point(x3, y3)
                };
                        //line.Stroke = SolidColorBrush.Green;
                        //line.StrokeThickness = 2;
                        double angle = Math.Atan2(y3 - y2, x3 - x2) - Math.Atan2(y1 - y2, x1 - x2);
                        if (angle > 0)
                        {
                            angle = angle * (180 / Math.PI);
                            LabelAciOlcer1.Text = string.Format("{0:F2}", angle);
                            LabelAciOlcer2.Text = string.Format("{0:F2}", (360 - angle));
                        }
                        else
                        {
                            //angle = angle * (-1);
                            angle = angle * (180 / Math.PI);
                            LabelAciOlcer1.Text = string.Format("{0:F2}", angle);
                            LabelAciOlcer2.Text = string.Format("{0:F2}", (360 + angle));
                        }
                        //angle = angle * (180 / Math.PI);
                        //LabelAciOlcer1.Text = string.Format("{0:F2}", angle);
                        //LabelAciOlcer2.Text = string.Format("{0:F2}", (360 - angle));
                        break;
                }


            }
        }
    }

    private void TapGestureRecognizer_Tapped_AciSil(object sender, TappedEventArgs e)
    {

        GoniometerAbsoluteLayout.SetLayoutBounds(boxView1, new Rect(0, 0, 50, 50));
        GoniometerAbsoluteLayout.SetLayoutBounds(boxView2, new Rect(0, 0, 50, 50));
        GoniometerAbsoluteLayout.SetLayoutBounds(boxView3, new Rect(0, 0, 50, 50));
        line.Points = new Microsoft.Maui.Controls.PointCollection
                {
                    new Microsoft.Maui.Graphics.Point(0, 0),
                    new Microsoft.Maui.Graphics.Point(0, 0),
                    new Microsoft.Maui.Graphics.Point(0, 0) };
        GoniometerAbsoluteLayout.Remove(boxView1);
        GoniometerAbsoluteLayout.Remove(boxView2);
        GoniometerAbsoluteLayout.Remove(boxView3);
        GoniometerAbsoluteLayout.Remove(line);

    }

}