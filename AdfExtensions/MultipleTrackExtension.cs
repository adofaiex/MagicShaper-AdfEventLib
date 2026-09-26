using MagicShaper.AdofaiCore.AdfClass;
using MagicShaper.AdofaiCore.AdfEvents;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShaper.AdfExtensions
{
	internal static class MultipleTrackExtension
	{
		public static void MultipleTrack(this AdfChart chart, int startTile, int endTile,
			double x = 10, double y = 6, string color = "FFFFFF", double opacity = 50, double scale = 100,
			double parallaxX = 50, double parallaxY = 30, AdfTrackStyle style = AdfTrackStyle.Minimal, bool hideIcons = false,
			int depth = 1145)
		{
			Random random = new();
			string gid = random.Next(1000000).ToString().PadLeft(6, '0');

			double prevX = x; 
			double prevY = y;

			double length = (100 - parallaxX) / 100d * scale / 100d;

			for (int i = 0; i < endTile - startTile; i++)
			{
				///THE FOLLOWING SECTION IS ENTIRELY COPIED FROM AdofaiGuessr

				double p = startTile + i == 0 ? 0d : chart.ChartTiles[startTile + i - 1].TargetAngle;
				double t = chart.ChartTiles[startTile + i].TargetAngle;

				// out of nowhere is adofai saving negative angles WTF?
				while (p < 0)
				{
					p += 360d;
				}
				while (t < 0)
				{
					t += 360d;
				}

				// this is for midspins
				if (t > 180d * 2d) { t = p > 180d ? p - 180d : p + Math.PI; }
				if (p > 180d * 2d)
				{
					if (startTile + i == 1) { p = 0d; }
					else
					{
						double pp = chart.ChartTiles[startTile + i - 2].TargetAngle / 180d * 180d;
						p = pp > 180d ? pp - 180d : pp + 180d;
					}
				}


				p = p > 180d ? p - 180d : p + 180d;

				double alpha = Math.Abs(t - p);
				alpha = alpha > 180d ? 180d * 2d - alpha : alpha;

				double rotation = 0d;
				if (p > t)
				{
					if (p - t > 180d)
					{
						rotation = p;
					}
					else
					{
						rotation = t;
					}
				}
				else
				{
					if (t - p > 180d)
					{
						rotation = t;
					}
					else
					{
						rotation = p;
					}
				}

                prevX += length * Math.Cos((p + 180) / 180 * Math.PI);
                prevY += length * Math.Sin((p + 180) / 180 * Math.PI);

                AdfTrackIcon icon = AdfTrackIcon.None;
				bool isRedTwirl = false;
				if (!hideIcons)
				{
					bool hasTwirl = false;
					foreach (var e in chart.ChartTiles[startTile + i].TileEvents)
					{
						if (e is AdfEventTwirl) { hasTwirl = true; break; }
					}
					double speedTimes = chart.GetTileBpmAt(startTile + i) / chart.GetTileBpmAt(startTile + i - 1);

					if (speedTimes >= 2) { icon = AdfTrackIcon.DoubleRabbit; }
					else if (speedTimes <= 0.25) { icon = AdfTrackIcon.DoubleSnail; }
					else if (speedTimes >= 1.05) { icon = AdfTrackIcon.Rabbit; }
					else if (speedTimes <= 0.95) { icon = AdfTrackIcon.Snail; }
					else if (hasTwirl)
					{
						icon = AdfTrackIcon.Swirl;
						if (chart.GetIsLHSAt(i + startTile))

						{
							if (p - 180d < t && t <= p || p + 180d < t)
							{
								isRedTwirl = true;
							}
						}
						else
						{
							if (p <= t && t < p + 180d || t < p - 180d)
							{
								isRedTwirl = true;
							}
						}
					}
				}
				chart.ChartTiles[startTile].TileEvents.Add(new AdfEventAddObject()
				{
					ObjectType = AdfObjectType.Floor,
					RelativeTo = AdfMoveDecorationRelativeToType.Tile,
					Floor = startTile,
                    Rotation = chart.ChartTiles[startTile + i].TargetAngle > 360d ? (p + 180) : (rotation + chart.GetInnerAngleAtTile(startTile + i) + 180d),
                    TrackAngle = chart.GetInnerAngleAtTile(startTile + i),
                    TrackType = chart.ChartTiles[startTile + i].TargetAngle > 360d ? AdfTrackType.Midspin : AdfTrackType.Normal,
					Parallax = new(parallaxX, parallaxY),
					ParallaxOffset = new(prevX, prevY),
					TrackOpacity = 0,
					TrackColor = new(color),
					Scale = new((100 - parallaxX) * scale / 100d),
					TrackStyle = style,
					Tag = $"quartrond_multipleTrack_{gid}",
					TrackIcon = icon,
					TrackRedSwirl = isRedTwirl,
					TrackIconAngle = rotation + chart.GetInnerAngleAtTile(startTile + i) + 180d,
					Depth = depth,
				});
			}

			chart.ChartTiles[startTile].TileEvents.Add(new AdfEventMoveDecorations()
			{
				Tag = $"quartrond_multipleTrack_{gid}",
				Opacity = opacity,
				Duration = 0
			});
			chart.ChartTiles[endTile].TileEvents.Add(new AdfEventMoveDecorations()
			{
				Tag = $"quartrond_multipleTrack_{gid}",
				Opacity = 0,
				Duration = 0
			});
		}
	}
}
