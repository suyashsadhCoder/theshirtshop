using System;
using Xamarin.Forms;
using Android.Media;
using theshirtshopApp.Interface;
using theshirtshopApp.Droid;

[assembly: Dependency(typeof(AudioService))]

namespace theshirtshopApp.Droid
{
	public class AudioService : IAudio
	{
		public AudioService() {}

		private MediaPlayer _mediaPlayer;

		public bool PlayMp3File(string fileName)
		{
			_mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.coin);
			_mediaPlayer.Start();
			return true;
		}

		public bool PlayWavFile(string fileName)
		{
			_mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.ding_persevy);
			_mediaPlayer.Start();
			return true;
		}
	}
}