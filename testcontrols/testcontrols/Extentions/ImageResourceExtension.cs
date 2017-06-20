using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace testcontrols.Extentions
{
	[ContentProperty("Source")]
	public class ImageResourceExtension : IMarkupExtension
	{
		public string Source { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			if (Source == null)
			{
				return null;
			}
			// Do your translation lookup here, using whatever method you require
			//var imageSource = ImageSource.FromResource(Source);
            var source = new UriImageSource()
            {
                Uri = new Uri(Source),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(5, 0, 0, 0)
            };
            return source;
		}
	}
}
