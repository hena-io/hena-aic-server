using Hena.Shared.Data;
using SkiaSharp;
using System;
using System.Drawing;
using System.IO;

namespace HenaSampleImageCreator
{
	class Program
	{
		static void Main(string[] args)
		{
			CreateSampleImages();
			Console.WriteLine("Press any key");
			Console.ReadKey();
		}


		static void CreateSampleImages()
		{
			Console.WriteLine("Begin");

			var outputDirName = "output";
			var enums = (AdDesignTypes.en[])Enum.GetValues(typeof(AdDesignTypes.en));
			Directory.CreateDirectory(outputDirName);

			WriteImageFile($"{outputDirName}/invalid-size-40x40-blue.png", new Size(24, 24), new SKColor(0xFF66d9ff), new SKColor(0xFF212121));
			WriteImageFile($"{outputDirName}/invalid-size-40x40-orange.png", new Size(24, 24), new SKColor(0xFFff9933), new SKColor(0xFF212121));

			foreach ( var it in enums )
			{
				if (it == AdDesignTypes.en.None)
					continue;

				var name = it.ToString();
				var size = AdDesignTypes.ToSize(it);
				var hdSize = AdDesignTypes.ToHDSize(it);

				WriteImageFile($"{outputDirName}/{name}-{size.Width}x{size.Height}-blue.png", size, new SKColor(0xFF66d9ff), new SKColor(0xFF212121));
				WriteImageFile($"{outputDirName}/{name}-{size.Width}x{size.Height}-orange.png", size, new SKColor(0xFFff9933), new SKColor(0xFF212121));
				WriteImageFile($"{outputDirName}/{name}-hd-{hdSize.Width}x{hdSize.Height}-blue.png", hdSize, new SKColor(0xFF66d9ff), new SKColor(0xFF212121));
				WriteImageFile($"{outputDirName}/{name}-hd-{hdSize.Width}x{hdSize.Height}-orange.png", hdSize, new SKColor(0xFFff9933), new SKColor(0xFF212121));
			}

			Console.WriteLine("Completed");
		}

		static void WriteImageFile(string path, Size size, SKColor innerColor, SKColor boundColor)
		{
			Console.WriteLine($"Write to image - {path}");
			var image = CreateImage(size, innerColor, boundColor);
			Stream fs = new FileStream(path, FileMode.Create);
			var imageData = image.Encode(SKEncodedImageFormat.Png, 100);
			imageData.SaveTo(fs);
			fs.Close();
		}

		static SKImage CreateImage(Size size, SKColor innerColor, SKColor boundColor)
		{
			SKBitmap bitmap = new SKBitmap(size.Width, size.Height, false);
			for( int x = 0; x < size.Width; ++x)
			{
				for (int y = 0; y < size.Height; ++y)
				{
					SKColor color = innerColor;

					if (x == 0 || y == 0 || x == size.Width - 1 || y == size.Height - 1)
						color = boundColor;
					
					bitmap.SetPixel(x, y, color);
				}
			}
			return SKImage.FromBitmap(bitmap);
		}
	}
}
