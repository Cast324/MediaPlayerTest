using CommunityToolkit.Maui.Views;

namespace MediaPlayerTest
{
	public partial class MainPage : ContentPage
	{
		private bool _isLooping;

		private TimeSpan _LoopStartPosition;

		public MainPage()
		{
			InitializeComponent();
			_media.PropertyChanged += _media_PropertyChanged;
		}

		private void _media_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == MediaElement.DurationProperty.PropertyName)
				_positionSlider.Maximum = _media.Duration.TotalSeconds;
		}

		private void OnCounterClicked(object sender, EventArgs e)
		{
			_media.Play();
		}

		private void Button_Clicked(object sender, EventArgs e)
		{
			_media.Pause();
		}

		private void Button_Clicked_1(object sender, EventArgs e)
		{
			_media.SeekTo(TimeSpan.FromSeconds(_positionSlider.Value + 5));
		}

		private void _media_PositionChanged(object sender, CommunityToolkit.Maui.Core.Primitives.MediaPositionChangedEventArgs e)
		{
			_positionSlider.Value = e.Position.TotalSeconds;
			if (_isLooping && e.Position.TotalSeconds >= _LoopStartPosition.TotalSeconds + 10)
				_media.SeekTo(_LoopStartPosition);
		}

		private async void _positionSlider_DragCompleted(object sender, EventArgs e)
		{
			ArgumentNullException.ThrowIfNull(sender);

			var newValue = ((Slider)sender).Value;
			await _media.SeekTo(TimeSpan.FromSeconds(newValue), CancellationToken.None);

			_media.Play();
		}

		private void _positionSlider_DragStarted(object sender, EventArgs e)
		{
			_media.Pause();
		}

		private void Button_Clicked_2(object sender, EventArgs e)
		{
			_isLooping = !_isLooping;
			_loopingText.IsVisible = _isLooping;
			_LoopStartPosition = _media.Position;
		}
	}

}
