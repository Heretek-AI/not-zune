using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Iris;
using MicrosoftZuneLibrary;

namespace ZuneUI;

internal static class DeviceIconSetFactory
{
	internal delegate void DeviceIconSetConstructionCompletedCallback(IDeviceIconSet iconSet);

	private class DeviceIconSetBuilder
	{
		private class ImageIdAndConstraint
		{
			private DeviceAssetImages _id;

			private DeviceIconSizeConstraint _constraint;

			public DeviceAssetImages ID => _id;

			public DeviceIconSizeConstraint Constraint => _constraint;

			public ImageIdAndConstraint(DeviceAssetImages id, DeviceIconSizeConstraint constraint)
			{
				//IL_0007: Unknown result type (might be due to invalid IL or missing references)
				//IL_0008: Unknown result type (might be due to invalid IL or missing references)
				_id = id;
				_constraint = constraint;
			}
		}

		private class DetailedImages
		{
			private ISimpleDeviceIconSet _mediumImages;

			private ISimpleDeviceIconSet _largeImages;

			public ISimpleDeviceIconSet MediumImages => _mediumImages;

			public ISimpleDeviceIconSet LargeImages => _largeImages;

			public DetailedImages(ISimpleDeviceIconSet mediumImages, ISimpleDeviceIconSet largeImages)
			{
				_mediumImages = mediumImages;
				_largeImages = largeImages;
			}
		}

		private class BackgroundImageAndColors
		{
			private Image _backgroundImage;

			private IColorSet _colors;

			public Image BackgroundImage => _backgroundImage;

			public IColorSet Colors => _colors;

			public BackgroundImageAndColors(Image backgroundImage, IColorSet colors)
			{
				_backgroundImage = backgroundImage;
				_colors = colors;
			}
		}

		private class ImageListLoaderAndVerifier
		{
			public delegate void LoadAndVerificationCompletedHandler(IDictionary<DeviceAssetImages, Image> images);

			private IList<string> _paths;

			private IList<ImageIdAndConstraint> _imagesToLoad;

			private LoadAndVerificationCompletedHandler _callback;

			private int _asynchronousLoadsRemaining;

			private bool _imagesAreValid;

			private Dictionary<DeviceAssetImages, Image> _images;

			public void LoadAndVerify(IList<string> paths, IList<ImageIdAndConstraint> imagesToLoad, LoadAndVerificationCompletedHandler callback)
			{
				//IL_008b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0096: Expected O, but got Unknown
				if (!Application.IsApplicationThread)
				{
					throw new Exception("DeviceIconSet loading is only supported on the application thread.  This feature is not thread-safe.");
				}
				if (_paths != null)
				{
					throw new Exception("LoadAndVerify was called while a set was already being built.  Calls to LoadAndVerify cannot be reentrant.");
				}
				if (paths == null)
				{
					throw new ArgumentNullException("path");
				}
				if (imagesToLoad == null)
				{
					throw new ArgumentNullException("imagesToLoad");
				}
				if (callback == null)
				{
					throw new ArgumentNullException("callback");
				}
				_paths = paths;
				_imagesToLoad = imagesToLoad;
				_callback = callback;
				_asynchronousLoadsRemaining = 0;
				_imagesAreValid = true;
				_images = new Dictionary<DeviceAssetImages, Image>(23, DeviceAssetImagesEqualityComparer.Instance);
				Application.DeferredInvoke((DeferredInvokeHandler)delegate
				{
					LoadAndVerifyWorker();
				}, (object)null);
			}

			private void LoadAndVerifyWorker()
			{
				//IL_001a: Unknown result type (might be due to invalid IL or missing references)
				//IL_001f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0028: Unknown result type (might be due to invalid IL or missing references)
				int num = 0;
				foreach (ImageIdAndConstraint item in _imagesToLoad)
				{
					DeviceAssetImages iD = item.ID;
					DeviceIconSizeConstraint constraint = item.Constraint;
					if (LoadAndVerifyImage(iD, constraint))
					{
						num++;
					}
					if (!_imagesAreValid)
					{
						break;
					}
				}
				SetAsynchronousLoadsRemaining(num);
			}

			private bool LoadAndVerifyImage(DeviceAssetImages imageId, DeviceIconSizeConstraint constraint)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0002: Expected I4, but got Unknown
				//IL_007d: Unknown result type (might be due to invalid IL or missing references)
				//IL_0087: Expected O, but got Unknown
				//IL_010a: Unknown result type (might be due to invalid IL or missing references)
				//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
				//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
				//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
				//IL_00fe: Expected O, but got Unknown
				int num = (int)imageId;
				bool result = false;
				if (num >= _paths.Count)
				{
					_imagesAreValid = false;
				}
				else
				{
					string text = _paths[num];
					if (text == null)
					{
						_imagesAreValid = false;
					}
					else if (!IsPathValid(text))
					{
						_imagesAreValid = false;
					}
					else
					{
						Image tempImage = null;
						bool flag = false;
						try
						{
							Image.RemoveCache(text, 1024, 1024);
							tempImage = new Image(text, 1024, 1024);
							flag = tempImage.Load();
						}
						catch (Exception)
						{
							_imagesAreValid = false;
						}
						if (tempImage == null)
						{
							_imagesAreValid = false;
						}
						else if (_imagesAreValid)
						{
							if (flag)
							{
								DeviceAssetImages tempImageId = imageId;
								DeviceIconSizeConstraint tempConstraint = constraint;
								tempImage.ImageLoadComplete += (ImageLoadCompleteHandler)delegate
								{
									//IL_0012: Unknown result type (might be due to invalid IL or missing references)
									VerifyImage(tempImage, tempImageId, tempConstraint);
									DecrementAsynchronousLoadsRemaining();
								};
								result = true;
							}
							else
							{
								VerifyImage(tempImage, imageId, constraint);
							}
						}
					}
				}
				return result;
			}

			private bool IsPathValid(string path)
			{
				if (!string.IsNullOrEmpty(path))
				{
					if (path.StartsWith("file://"))
					{
						return File.Exists(path.Substring("file://".Length));
					}
					return true;
				}
				return false;
			}

			private void VerifyImage(Image image, DeviceAssetImages imageId, DeviceIconSizeConstraint constraint)
			{
				//IL_000f: Unknown result type (might be due to invalid IL or missing references)
				if (ImageFitsConstraint(image, constraint))
				{
					_images[imageId] = image;
				}
				else
				{
					_imagesAreValid = false;
				}
			}

			private void SetAsynchronousLoadsRemaining(int value)
			{
				_asynchronousLoadsRemaining = value;
				CheckForAsynchronousPortionCompleted();
			}

			private void DecrementAsynchronousLoadsRemaining()
			{
				_asynchronousLoadsRemaining--;
				CheckForAsynchronousPortionCompleted();
			}

			private void CheckForAsynchronousPortionCompleted()
			{
				//IL_0014: Unknown result type (might be due to invalid IL or missing references)
				//IL_001a: Expected O, but got Unknown
				DeferredInvokeHandler val = null;
				if (_asynchronousLoadsRemaining != 0)
				{
					return;
				}
				if (val == null)
				{
					val = (DeferredInvokeHandler)delegate
					{
						LoadingComplete();
					};
				}
				Application.DeferredInvoke(val, (object)null);
			}

			private void LoadingComplete()
			{
				//IL_0030: Unknown result type (might be due to invalid IL or missing references)
				//IL_003b: Expected O, but got Unknown
				LoadAndVerificationCompletedHandler callback = _callback;
				IDictionary<DeviceAssetImages, Image> result = (_imagesAreValid ? _images : null);
				Application.DeferredInvoke((DeferredInvokeHandler)delegate
				{
					callback(result);
				}, (object)null);
				_paths = null;
				_imagesToLoad = null;
				_callback = null;
			}

			private static bool ImageFitsConstraint(Image image, DeviceIconSizeConstraint constraint)
			{
				if (image != null && constraint != null && image.Width >= constraint.MinWidth && image.Width <= constraint.MaxWidth && image.Height >= constraint.MinHeight)
				{
					return image.Height <= constraint.MaxHeight;
				}
				return false;
			}
		}

		private class DeviceAssetImagesEqualityComparer : IEqualityComparer<DeviceAssetImages>
		{
			public static DeviceAssetImagesEqualityComparer Instance = new DeviceAssetImagesEqualityComparer();

			private DeviceAssetImagesEqualityComparer()
			{
			}

			public bool Equals(DeviceAssetImages x, DeviceAssetImages y)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return x == y;
			}

			public int GetHashCode(DeviceAssetImages obj)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				return ((object)obj).GetHashCode();
			}
		}

		private DeviceAssetSet _assetSet;

		private DeviceIconSetConstructionCompletedCallback _callback;

		private ImageListLoaderAndVerifier _loader;

		private IInteractiveDeviceIconSet _smallImageSubset;

		private DetailedImages _detailedImageSubset;

		private BackgroundImageAndColors _backgroundImageAndColorSubset;

		private static readonly ImageIdAndConstraint[] _smallImagesToLoad = new ImageIdAndConstraint[18]
		{
			new ImageIdAndConstraint((DeviceAssetImages)0, _smallConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)10, _smallConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)5, _smallConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)14, _smallConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)1, _smallConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)11, _smallConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)6, _smallConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)15, _smallConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)3, _smallConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)13, _smallConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)8, _smallConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)17, _smallConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)2, _smallConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)12, _smallConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)7, _smallConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)16, _smallConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)4, _smallConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)9, _smallConstraint)
		};

		private static readonly ImageIdAndConstraint[] _detailedImagesToLoad = new ImageIdAndConstraint[4]
		{
			new ImageIdAndConstraint((DeviceAssetImages)20, _mediumConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)21, _mediumConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)18, _largeConstraint),
			new ImageIdAndConstraint((DeviceAssetImages)19, _largeConstraint)
		};

		private static readonly ImageIdAndConstraint[] _backgroundImagesToLoad = new ImageIdAndConstraint[1]
		{
			new ImageIdAndConstraint((DeviceAssetImages)22, _backgroundConstraint)
		};

		public DeviceIconSetBuilder()
		{
			_loader = new ImageListLoaderAndVerifier();
		}

		public void BuildIconSet(DeviceAssetSet assetSet, DeviceIconSetConstructionCompletedCallback callback)
		{
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_0061: Expected O, but got Unknown
			if (!Application.IsApplicationThread)
			{
				throw new Exception("DeviceIconSet loading is only supported on the application thread.  This feature is not thread-safe.");
			}
			if (_assetSet != null)
			{
				throw new Exception("BuildIconSet was called while a set was already being built.  Calls to BuildIconSet cannot be reentrant.");
			}
			if (assetSet == null)
			{
				throw new ArgumentNullException("assetSet");
			}
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			_assetSet = assetSet;
			_callback = callback;
			Application.DeferredInvoke((DeferredInvokeHandler)delegate
			{
				LoadSmallImages();
			}, (object)null);
		}

		private void End()
		{
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Expected O, but got Unknown
			DeviceIconSetConstructionCompletedCallback callback = _callback;
			IDeviceIconSet result = null;
			if (_smallImageSubset != null && _detailedImageSubset != null && _backgroundImageAndColorSubset != null)
			{
				result = new DeviceIconSet(_detailedImageSubset.LargeImages, _detailedImageSubset.MediumImages, _smallImageSubset, _backgroundImageAndColorSubset.BackgroundImage, _backgroundImageAndColorSubset.Colors);
			}
			else
			{
				result = DefaultIconSet;
			}
			Application.DeferredInvoke((DeferredInvokeHandler)delegate
			{
				callback(result);
			}, (object)null);
			_assetSet = null;
			_callback = null;
		}

		private void LoadSmallImages()
		{
			_loader.LoadAndVerify(_assetSet.ImageUris, _smallImagesToLoad, SmallCustomImageLoadComplete);
		}

		private void SmallCustomImageLoadComplete(IDictionary<DeviceAssetImages, Image> images)
		{
			if (images != null)
			{
				_smallImageSubset = ConstructSmallImageSubset(images);
				LoadDetailedImages();
			}
			else
			{
				_loader.LoadAndVerify(_assetSet.DefaultImageUris, _smallImagesToLoad, SmallDefaultImageLoadComplete);
			}
		}

		private void SmallDefaultImageLoadComplete(IDictionary<DeviceAssetImages, Image> images)
		{
			if (images != null)
			{
				_smallImageSubset = ConstructSmallImageSubset(images);
			}
			else
			{
				_smallImageSubset = DefaultIconSet.Small;
			}
			LoadDetailedImages();
		}

		private IInteractiveDeviceIconSet ConstructSmallImageSubset(IDictionary<DeviceAssetImages, Image> images)
		{
			return new InteractiveIconSet(new BackgroundAwareIconSet(new SimpleIconSet(images[(DeviceAssetImages)0], images[(DeviceAssetImages)10]), new SimpleIconSet(images[(DeviceAssetImages)5], images[(DeviceAssetImages)14])), new BackgroundAwareIconSet(new SimpleIconSet(images[(DeviceAssetImages)1], images[(DeviceAssetImages)11]), new SimpleIconSet(images[(DeviceAssetImages)6], images[(DeviceAssetImages)15])), new BackgroundAwareIconSet(new SimpleIconSet(images[(DeviceAssetImages)3], images[(DeviceAssetImages)13]), new SimpleIconSet(images[(DeviceAssetImages)8], images[(DeviceAssetImages)17])), new BackgroundAwareIconSet(new SimpleIconSet(images[(DeviceAssetImages)2], images[(DeviceAssetImages)12]), new SimpleIconSet(images[(DeviceAssetImages)7], images[(DeviceAssetImages)16])), new BackgroundAwareIconSet(new SimpleIconSet(images[(DeviceAssetImages)4], images[(DeviceAssetImages)4]), new SimpleIconSet(images[(DeviceAssetImages)9], images[(DeviceAssetImages)9])));
		}

		private void LoadDetailedImages()
		{
			_loader.LoadAndVerify(_assetSet.ImageUris, _detailedImagesToLoad, DetailedCustomImageLoadComplete);
		}

		private void DetailedCustomImageLoadComplete(IDictionary<DeviceAssetImages, Image> images)
		{
			if (images != null)
			{
				_detailedImageSubset = ConstructDetailedImageSubset(images);
				LoadBackgroundImageAndColors();
			}
			else
			{
				_loader.LoadAndVerify(_assetSet.DefaultImageUris, _detailedImagesToLoad, DetailedDefaultImageLoadComplete);
			}
		}

		private void DetailedDefaultImageLoadComplete(IDictionary<DeviceAssetImages, Image> images)
		{
			if (images != null)
			{
				_detailedImageSubset = ConstructDetailedImageSubset(images);
			}
			else
			{
				_detailedImageSubset = new DetailedImages(DefaultIconSet.Medium, DefaultIconSet.Large);
			}
			LoadBackgroundImageAndColors();
		}

		private DetailedImages ConstructDetailedImageSubset(IDictionary<DeviceAssetImages, Image> images)
		{
			return new DetailedImages(new SimpleIconSet(images[(DeviceAssetImages)20], images[(DeviceAssetImages)21]), new SimpleIconSet(images[(DeviceAssetImages)18], images[(DeviceAssetImages)19]));
		}

		private void LoadBackgroundImageAndColors()
		{
			_loader.LoadAndVerify(_assetSet.ImageUris, _backgroundImagesToLoad, BackgroundCustomImageLoadComplete);
		}

		private void BackgroundCustomImageLoadComplete(IDictionary<DeviceAssetImages, Image> images)
		{
			if (images != null)
			{
				_backgroundImageAndColorSubset = ConstructBackgroundImageAndColorsSubset(images);
				End();
			}
			else
			{
				_loader.LoadAndVerify(_assetSet.DefaultImageUris, _backgroundImagesToLoad, BackgroundDefaultImageLoadComplete);
			}
		}

		private void BackgroundDefaultImageLoadComplete(IDictionary<DeviceAssetImages, Image> images)
		{
			if (images != null)
			{
				_backgroundImageAndColorSubset = ConstructBackgroundImageAndColorsSubset(images);
			}
			else
			{
				_backgroundImageAndColorSubset = new BackgroundImageAndColors(DefaultIconSet.Background, DefaultIconSet.Colors);
			}
			End();
		}

		private BackgroundImageAndColors ConstructBackgroundImageAndColorsSubset(IDictionary<DeviceAssetImages, Image> images)
		{
			return new BackgroundImageAndColors(images[(DeviceAssetImages)22], new ColorSet(new DeviceColor(_assetSet.Colors[0]), new DeviceColor(_assetSet.Colors[1]), new DeviceColor(_assetSet.Colors[2]), new DeviceColor(_assetSet.Colors[3])));
		}
	}

	private class DeviceIconSizeConstraint
	{
		private int _minWidth;

		private int _maxWidth;

		private int _minHeight;

		private int _maxHeight;

		public int MinWidth => _minWidth;

		public int MaxWidth => _maxWidth;

		public int MinHeight => _minHeight;

		public int MaxHeight => _maxHeight;

		public DeviceIconSizeConstraint(int minWidth, int maxWidth, int minHeight, int maxHeight)
		{
			_minWidth = minWidth;
			_maxWidth = maxWidth;
			_minHeight = minHeight;
			_maxHeight = maxHeight;
		}
	}

	private class DeviceIconSet : IDeviceIconSet
	{
		private ISimpleDeviceIconSet _large;

		private ISimpleDeviceIconSet _medium;

		private IInteractiveDeviceIconSet _small;

		private Image _background;

		private IColorSet _colors;

		public ISimpleDeviceIconSet Large => _large;

		public ISimpleDeviceIconSet Medium => _medium;

		public IInteractiveDeviceIconSet Small => _small;

		public Image Background => _background;

		public IColorSet Colors => _colors;

		public DeviceIconSet(ISimpleDeviceIconSet large, ISimpleDeviceIconSet medium, IInteractiveDeviceIconSet small, Image background, IColorSet colors)
		{
			_large = large;
			_medium = medium;
			_small = small;
			_background = background;
			_colors = colors;
		}
	}

	private class ColorSet : IColorSet
	{
		private IDeviceColor _light;

		private IDeviceColor _dark;

		private IDeviceColor _text;

		private IDeviceColor _hoverText;

		public IDeviceColor Light => _light;

		public IDeviceColor Dark => _dark;

		public IDeviceColor Text => _text;

		public IDeviceColor HoverText => _hoverText;

		public ColorSet(IDeviceColor light, IDeviceColor dark, IDeviceColor text, IDeviceColor hoverText)
		{
			_light = light;
			_dark = dark;
			_text = text;
			_hoverText = hoverText;
		}
	}

	private class DeviceColor : IDeviceColor
	{
		private float _r;

		private float _g;

		private float _b;

		public float R => _r;

		public float G => _g;

		public float B => _b;

		public DeviceColor(uint packedColor)
		{
			_r = (float)(int)((packedColor & 0xFF0000) >> 16) / 255f;
			_g = (float)(int)((packedColor & 0xFF00) >> 8) / 255f;
			_b = (float)(int)(packedColor & 0xFF) / 255f;
		}

		public DeviceColor(byte r, byte g, byte b)
		{
			_r = (float)(int)r / 250f;
			_g = (float)(int)g / 250f;
			_b = (float)(int)b / 250f;
		}

		public DeviceColor(float r, float g, float b)
		{
			_r = r;
			_g = g;
			_b = b;
		}
	}

	private class SimpleIconSet : ISimpleDeviceIconSet
	{
		private Image _connected;

		private Image _disconnected;

		public Image Connected => _connected;

		public Image Disconnected => _disconnected;

		public SimpleIconSet(Image connected, Image disconnected)
		{
			_connected = connected;
			_disconnected = disconnected;
		}

		public Image GetImageForConnectedness(bool isConnected)
		{
			if (!isConnected)
			{
				return Disconnected;
			}
			return Connected;
		}
	}

	private class BackgroundAwareIconSet : IBackgroundAwareDeviceIconSet
	{
		private ISimpleDeviceIconSet _forLightBackground;

		private ISimpleDeviceIconSet _forDarkBackground;

		public ISimpleDeviceIconSet ForLightBackground => _forLightBackground;

		public ISimpleDeviceIconSet ForDarkBackground => _forDarkBackground;

		public BackgroundAwareIconSet(ISimpleDeviceIconSet forLightBackground, ISimpleDeviceIconSet forDarkBackground)
		{
			_forLightBackground = forLightBackground;
			_forDarkBackground = forDarkBackground;
		}

		public ISimpleDeviceIconSet GetSetForBackground(bool backgroundIsDark)
		{
			if (!backgroundIsDark)
			{
				return ForLightBackground;
			}
			return ForDarkBackground;
		}
	}

	private class InteractiveIconSet : IInteractiveDeviceIconSet
	{
		private IBackgroundAwareDeviceIconSet _default;

		private IBackgroundAwareDeviceIconSet _hover;

		private IBackgroundAwareDeviceIconSet _drag;

		private IBackgroundAwareDeviceIconSet _click;

		private IBackgroundAwareDeviceIconSet _syncing;

		public IBackgroundAwareDeviceIconSet Default => _default;

		public IBackgroundAwareDeviceIconSet Hover => _hover;

		public IBackgroundAwareDeviceIconSet Drag => _drag;

		public IBackgroundAwareDeviceIconSet Click => _click;

		public IBackgroundAwareDeviceIconSet Syncing => _syncing;

		public InteractiveIconSet(IBackgroundAwareDeviceIconSet defaultSet, IBackgroundAwareDeviceIconSet hover, IBackgroundAwareDeviceIconSet drag, IBackgroundAwareDeviceIconSet click, IBackgroundAwareDeviceIconSet syncing)
		{
			_default = defaultSet;
			_hover = hover;
			_drag = drag;
			_click = click;
			_syncing = syncing;
		}
	}

	private const int _reasonableMaximumSurfaceSize = 1024;

	private static List<DeviceIconSetBuilder> _builders = new List<DeviceIconSetBuilder>();

	private static DeviceIconSizeConstraint _backgroundConstraint = new DeviceIconSizeConstraint(2, 1023, 2, 1023);

	private static DeviceIconSizeConstraint _largeConstraint = new DeviceIconSizeConstraint(100, 225, 265, 400);

	private static DeviceIconSizeConstraint _mediumConstraint = new DeviceIconSizeConstraint(50, 110, 50, 200);

	private static DeviceIconSizeConstraint _smallConstraint = new DeviceIconSizeConstraint(15, 42, 25, 49);

	private static DeviceIconSet _defaultIconSet = new DeviceIconSet(new SimpleIconSet(new Image("res://ZuneShellResources!DefaultDevice.Large.Default.png"), new Image("res://ZuneShellResources!DefaultDevice.Large.Disconnected.png")), new SimpleIconSet(new Image("res://ZuneShellResources!DefaultDevice.Medium.Default.png"), new Image("res://ZuneShellResources!DefaultDevice.Medium.Unsupported.png")), new InteractiveIconSet(new BackgroundAwareIconSet(new SimpleIconSet(new Image("res://ZuneShellResources!DefaultDevice.Connected.Default.png"), new Image("res://ZuneShellResources!DefaultDevice.Disconnected.Default.png")), new SimpleIconSet(new Image("res://ZuneShellResources!DefaultDevice.Connected.Default.Dark.png"), new Image("res://ZuneShellResources!DefaultDevice.Disconnected.Default.Dark.png"))), new BackgroundAwareIconSet(new SimpleIconSet(new Image("res://ZuneShellResources!DefaultDevice.Connected.Hover.png"), new Image("res://ZuneShellResources!DefaultDevice.Disconnected.Hover.png")), new SimpleIconSet(new Image("res://ZuneShellResources!DefaultDevice.Connected.Hover.Dark.png"), new Image("res://ZuneShellResources!DefaultDevice.Disconnected.Hover.Dark.png"))), new BackgroundAwareIconSet(new SimpleIconSet(new Image("res://ZuneShellResources!DefaultDevice.Connected.Drag.png"), new Image("res://ZuneShellResources!DefaultDevice.Disconnected.Drag.png")), new SimpleIconSet(new Image("res://ZuneShellResources!DefaultDevice.Connected.Drag.Dark.png"), new Image("res://ZuneShellResources!DefaultDevice.Disconnected.Drag.Dark.png"))), new BackgroundAwareIconSet(new SimpleIconSet(new Image("res://ZuneShellResources!DefaultDevice.Connected.Click.png"), new Image("res://ZuneShellResources!DefaultDevice.Disconnected.Click.png")), new SimpleIconSet(new Image("res://ZuneShellResources!DefaultDevice.Connected.Click.Dark.png"), new Image("res://ZuneShellResources!DefaultDevice.Disconnected.Click.Dark.png"))), new BackgroundAwareIconSet(new SimpleIconSet(new Image("res://ZuneShellResources!DefaultDevice.Connected.Syncing.png"), new Image("res://ZuneShellResources!DefaultDevice.Connected.Syncing.png")), new SimpleIconSet(new Image("res://ZuneShellResources!DefaultDevice.Connected.Syncing.Dark.png"), new Image("res://ZuneShellResources!DefaultDevice.Connected.Syncing.Dark.png")))), new Image("res://ZuneShellResources!DefaultDevice.Background.png"), new ColorSet(new DeviceColor(87, 87, 87), new DeviceColor(0, 0, 2), new DeviceColor(0, 0, 2), new DeviceColor(0, 0, 2)));

	private static DeviceIconSet _unloadedIconSet = new DeviceIconSet(new SimpleIconSet(new Image("res://ZuneShellResources!Unloaded.Large.Default.png"), new Image("res://ZuneShellResources!Unloaded.Large.Disconnected.png")), new SimpleIconSet(new Image("res://ZuneShellResources!Unloaded.Medium.Default.png"), new Image("res://ZuneShellResources!Unloaded.Medium.Unsupported.png")), new InteractiveIconSet(new BackgroundAwareIconSet(new SimpleIconSet(new Image("res://ZuneShellResources!Unloaded.Connected.Default.png"), new Image("res://ZuneShellResources!Unloaded.Disconnected.Default.png")), new SimpleIconSet(new Image("res://ZuneShellResources!Unloaded.Connected.Default.Dark.png"), new Image("res://ZuneShellResources!Unloaded.Disconnected.Default.Dark.png"))), new BackgroundAwareIconSet(new SimpleIconSet(new Image("res://ZuneShellResources!Unloaded.Connected.Hover.png"), new Image("res://ZuneShellResources!Unloaded.Disconnected.Hover.png")), new SimpleIconSet(new Image("res://ZuneShellResources!Unloaded.Connected.Hover.Dark.png"), new Image("res://ZuneShellResources!Unloaded.Disconnected.Hover.Dark.png"))), new BackgroundAwareIconSet(new SimpleIconSet(new Image("res://ZuneShellResources!Unloaded.Connected.Drag.png"), new Image("res://ZuneShellResources!Unloaded.Disconnected.Drag.png")), new SimpleIconSet(new Image("res://ZuneShellResources!Unloaded.Connected.Drag.Dark.png"), new Image("res://ZuneShellResources!Unloaded.Disconnected.Drag.Dark.png"))), new BackgroundAwareIconSet(new SimpleIconSet(new Image("res://ZuneShellResources!Unloaded.Connected.Click.png"), new Image("res://ZuneShellResources!Unloaded.Disconnected.Click.png")), new SimpleIconSet(new Image("res://ZuneShellResources!Unloaded.Connected.Click.Dark.png"), new Image("res://ZuneShellResources!Unloaded.Disconnected.Click.Dark.png"))), new BackgroundAwareIconSet(new SimpleIconSet(new Image("res://ZuneShellResources!Unloaded.Connected.Syncing.png"), new Image("res://ZuneShellResources!Unloaded.Connected.Syncing.png")), new SimpleIconSet(new Image("res://ZuneShellResources!Unloaded.Connected.Syncing.Dark.png"), new Image("res://ZuneShellResources!Unloaded.Connected.Syncing.Dark.png")))), new Image("res://ZuneShellResources!Unloaded.Background.png"), new ColorSet(new DeviceColor(87, 87, 87), new DeviceColor(0, 0, 2), new DeviceColor(0, 0, 2), new DeviceColor(0, 0, 2)));

	public static IDeviceIconSet DefaultIconSet => _defaultIconSet;

	private static IDeviceIconSet UnloadedIconSet => _unloadedIconSet;

	public static IDeviceIconSet BuildDeviceIconSet(DeviceAssetSet assetSet, DeviceIconSetConstructionCompletedCallback callback)
	{
		DeviceIconSetBuilder builder = new DeviceIconSetBuilder();
		_builders.Add(builder);
		builder.BuildIconSet(assetSet, delegate(IDeviceIconSet result)
		{
			callback(result);
			_builders.Remove(builder);
		});
		return UnloadedIconSet;
	}
}
