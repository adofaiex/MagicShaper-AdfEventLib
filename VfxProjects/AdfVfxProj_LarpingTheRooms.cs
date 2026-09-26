using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using MagicShaper.AdfExtensions;
using MagicShaper.AdfExtensions.Gimmicks;
using MagicShaper.AdofaiCore.AdfClass;
using MagicShaper.AdofaiCore.AdfEvents;
using MagicShaper.AdofaiCore.AdfEvents.Dlc;
using OpenCvSharp;

namespace MagicShaper.VfxProjects
{
    public class AdfVfxProj_LarpingTheRooms
    {
        public static void ProjMain()
        {
			AdfChart chart = AdfChart.Parse(@"G:\Adofai levels\larping\level-base.adofai");

            PrepareFlashes(chart);


            ScreenshotFlashes(chart, 25, 68, -0.98, 4);
            Verse(chart);
            Chorus(chart, 221, 276);
            ScreenshotFlashes(chart, 277, 320, 0.98, 4);

            LongFall(chart);
            ManyTenKeys(chart);

            Chorus(chart, 538, 593);
			TrackSpiralPart(chart);
            ScreenshotFlashes(chart, 722, 765, 0.98, 8);


















            File.WriteAllText(@"G:\Adofai levels\larping\level-effect.adofai", chart.ChartJson.ToString());
        }




        private static void ManyTenKeys(AdfChart chart)
        {
            Mat mat = Cv2.ImRead(chart.FileLocation?.Parent?.FullName + $"\\flashlight.jpg");
            double defaultCameraZoom = 500;

            double widthMultiplier = (double) ExtensionSharedConstants.CanvasWidth / mat.Width * defaultCameraZoom / 100d;
            double heightMultiplier = (double) ExtensionSharedConstants.CanvasHeight / mat.Height  * defaultCameraZoom / 100d;

            double scale = 100d * Math.Max(widthMultiplier, heightMultiplier);


            chart.ChartTiles[0].TileEvents.Add(new AdfEventAddDecoration()
            {
                DecorationImage = $"flashlight.jpg",
                Tag = $"quartrond-image-tenkeyflash",
                Scale = new(scale),
                Opacity = 0,
                LockRotation = true,
                RelativeTo = AdfMoveDecorationRelativeToType.Camera,
                Parallax = new(0, 0),
                Depth = 6,
            });



            chart.ChartTiles[330].TileEvents.Add(new AdfEventAddDecoration()
            {
                RelativeTo = AdfMoveDecorationRelativeToType.Camera,
                Tag = "quartrond-10key-center",
                DecorationImage = "10key.png",
                LockScale = true,
                LockRotation = true,
                Scale = new(100),
                Opacity = 0,
                Position = new(0, -7),
                Depth = 1,
            });

            chart.ChartTiles[330].TileEvents.Add(new AdfEventMoveDecorations()
            {
                Tag = "quartrond-10key-center",
                Opacity = 90,
                Ease = AdfEaseType.OutBack,
                Duration = 2d,
                AngleOffset = -360,
            });


            for (int i = 331; i <= 503; i++)
            {
                if (!chart.ChartTiles[i].TileEvents.Any(e => e is AdfEventEditorComment)) continue;
                chart.ChartTiles[i].TileEvents.Add(new AdfEventAddDecoration()
                {
                    RelativeTo = AdfMoveDecorationRelativeToType.Camera,
                    Tag = $"quartrond-10key-{i}",
                    DecorationImage = "ring.png",
                    LockScale = true,
                    LockRotation = true,
                    Scale = new(300),
                    Position = new(0, -7),
                    Opacity = 0,
                });

                chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-10key-{i}",
                    Scale = new(90),
                    Duration = 256,
                    Ease = AdfEaseType.InBack,
                    AngleOffset = -256 * 180,
                });
                chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-10key-{i}",
                    Opacity = 100,
                    Duration = 256,
                    Ease = AdfEaseType.OutBounce,
                    AngleOffset = -256 * 180,
                });
                chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-10key-{i}",
                    Scale = new(130),
                    Opacity = 0,
                    Duration = 128,
                    Ease = AdfEaseType.OutCirc,
                    AngleOffset = 0.001,
                });

                chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-10key-center",
                    Opacity = 100,
                    Duration = 0,
                    AngleOffset = -0.001,
                });
                chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-10key-center",
                    Opacity = 30,
                    Duration = 256,
                    Ease = AdfEaseType.OutCirc,
                    AngleOffset = 0,
                });

                chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = "quartrond-image-tenkeyflash",
                    Opacity = 50,
                    AngleOffset = -0.001,
                });
                chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = "quartrond-image-tenkeyflash",
                    Opacity = 0,
                    Duration = 256,
                    Ease = AdfEaseType.OutCirc,
                });

                chart.ChartTiles[i].TileEvents.Add(new AdfEventSetFilter() { Filter = AdfFilter.Aberration, Duration = 0, Intensity = 40, AngleOffset = -0.001 });
                chart.ChartTiles[i].TileEvents.Add(new AdfEventSetFilter() { Filter = AdfFilter.Aberration, Duration = 256, Intensity = 49, Ease = AdfEaseType.OutExpo });
                chart.ChartTiles[i].TileEvents.Add(new AdfEventSetFilter() { Filter = AdfFilter.MotionBlur, Duration = 0, Intensity = 1000, AngleOffset = -0.001 });
                chart.ChartTiles[i].TileEvents.Add(new AdfEventSetFilter() { Filter = AdfFilter.MotionBlur, Duration = 256, Intensity = 0, Ease = AdfEaseType.OutExpo });
                chart.ChartTiles[i].TileEvents.Add(new AdfEventSetFilter() { Filter = AdfFilter.Fisheye, Duration = 0, Intensity = 47, AngleOffset = -0.001 });
                chart.ChartTiles[i].TileEvents.Add(new AdfEventSetFilter() { Filter = AdfFilter.Fisheye, Duration = 256, Intensity = 49, Ease = AdfEaseType.OutExpo });
                chart.ChartTiles[i].TileEvents.Add(new AdfEventBloom() { Threshold = 0, Duration = 0, Intensity = 100, AngleOffset = -0.001 });
                chart.ChartTiles[i].TileEvents.Add(new AdfEventBloom() { Threshold = 0, Duration = 256, Intensity = 30, Ease = AdfEaseType.OutExpo });
                chart.ChartTiles[i].TileEvents.Add(new AdfEventFlash() { Plane = AdfFlashPlaneType.Foreground, Duration = 256, StartOpacity = 10, EndOpacity = 0, Ease = AdfEaseType.OutExpo });
            }

            chart.ChartTiles[513].TileEvents.Add(new AdfEventMoveDecorations()
            {
                Tag = "quartrond-10key-center",
                Opacity = 0,
                Duration = 4d,
                Ease = AdfEaseType.OutCirc,
            });
        }




        private static void LongFall(AdfChart chart)
        {
			Random random = new();

			for (int i = 0; i < 48; i++)
			{
				chart.AddObjectToChart(new()
				{
					ObjectType = AdfObjectType.Floor,
					Parallax = new(random.RandBetween(15, 30), random.RandBetween(-5, 5)),
					Depth = (int)random.RandBetween(-5, 5),
					Tag = $"quartrond-broken-tiles-{i}",
					Scale = new(random.RandBetween(50, 130)),
					Position = new(random.RandBetween(0, 15), random.RandBetween(-20, -10)),
                    TrackOpacity = 0,
					TrackStyle = AdfTrackStyle.Neon,
					TrackColor = new("FFFFFFFF"),
					RelativeTo = AdfMoveDecorationRelativeToType.Tile,
					Floor = 328,
					TrackAngle = 180,
					Rotation = random.NextDouble() * 360d,
				});

				chart.ChartTiles[328].TileEvents.Add(new AdfEventMoveDecorations()
				{
					Tag = $"quartrond-broken-tiles-{i}",
					Duration = 0d,
					Opacity = random.RandBetween(50, 100),
				});
				chart.ChartTiles[328].TileEvents.Add(new AdfEventMoveDecorations()
				{
					Tag = $"quartrond-broken-tiles-{i}",
					Duration = 4,
                    PositionOffset = PositionFromPolar(random.RandBetween(0, 15), random.RandBetween(25, 45)),
                    Ease = AdfEaseType.OutQuint,
				});
				chart.ChartTiles[328].TileEvents.Add(new AdfEventMoveDecorations()
				{
					Tag = $"quartrond-broken-tiles-{i}",
					Duration = 0d,
					Opacity = 0d,
					AngleOffset = 360,
					Visible = false
				});
			}
			for (int i = 48; i < 48 * 2; i++)
			{
				chart.AddObjectToChart(new()
				{
					ObjectType = AdfObjectType.Floor,
					Parallax = new(random.RandBetween(15, 30), random.RandBetween(-5, 5)),
					Depth = (int)random.RandBetween(-5, 5),
					Tag = $"quartrond-broken-tiles-{i}",
					Scale = new(random.RandBetween(50, 130)),
					Position = new(random.RandBetween(-15, 0), random.RandBetween(-20, -10)),
                    TrackOpacity = 0,
					TrackStyle = AdfTrackStyle.Neon,
					TrackColor = new("FFFFFFFF"),
					RelativeTo = AdfMoveDecorationRelativeToType.Tile,
					Floor = 328,
					TrackAngle = 180,
					Rotation = random.NextDouble() * 360d,
				});

				chart.ChartTiles[328].TileEvents.Add(new AdfEventMoveDecorations()
				{
					Tag = $"quartrond-broken-tiles-{i}",
					Duration = 0d,
					Opacity = random.RandBetween(50, 100),
				});
				chart.ChartTiles[328].TileEvents.Add(new AdfEventMoveDecorations()
				{
					Tag = $"quartrond-broken-tiles-{i}",
					Duration = 4,
                    PositionOffset = PositionFromPolar(random.RandBetween(0, 15), random.RandBetween(135, 155)),
                    Ease = AdfEaseType.OutQuint,
				});
				chart.ChartTiles[328].TileEvents.Add(new AdfEventMoveDecorations()
				{
					Tag = $"quartrond-broken-tiles-{i}",
					Duration = 0d,
					Opacity = 0d,
					AngleOffset = 360,
					Visible = false
				});
			}






            List<double> angleOffsetsForWaves = [
                0d, 1 * (180d/8), 1.666666 * (180d/8), 2 * (180d/8), 3 * (180d/8), 4 * (180d/8), 5 * (180d/8), 6 * (180d/8), 7 * (180d/8), -1,

                0d, 1 * (180d/8), 1.666666 * (180d/8), 2 * (180d/8), 3 * (180d/8), 4 * (180d/8), 5 * (180d/8), 6 * (180d/8), -1,

                0d, 1 * (180d/8), 1.666666 * (180d/8), 2 * (180d/8), 3 * (180d/8), 4 * (180d/8), 5 * (180d/8), 6 * (180d/8), 7 * (180d/8), -1,

                0d, 1 * (180d/8), 1.666666 * (180d/8), 2 * (180d/8), 3 * (180d/8), 4 * (180d/8), 5 * (180d/8), -1,
            ];
            var approximateXPosition = -9.5d; var additionalAngleOffset = 0d;
            for (int i = 0; i < angleOffsetsForWaves.Count; i++)
            {
                if (angleOffsetsForWaves[i] < 0) { approximateXPosition = -9.5d; additionalAngleOffset += 180d; continue; }

                chart.ChartTiles[328].TileEvents.Add(new AdfEventAddDecoration()
                {
                    DecorationImage = "wave.png",
                    Tag = $"quartrond-wave-{i}",
                    Scale = new(0),
                    RelativeTo = AdfMoveDecorationRelativeToType.Camera,
                    LockScale = true, 
                    Position = new(approximateXPosition, random.RandBetween(-8, 8)),
                });
                chart.ChartTiles[328].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-wave-{i}",
                    Scale = new(random.RandBetween(50, 250)),
                    Duration = 0.25d,
                    Ease = AdfEaseType.OutCirc,
                    AngleOffset = angleOffsetsForWaves[i] + additionalAngleOffset,
                });
                chart.ChartTiles[328].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-wave-{i}",
                    Opacity = 0,
                    Duration = 0.25d,
                    Ease = AdfEaseType.Linear,
                    AngleOffset = angleOffsetsForWaves[i] + additionalAngleOffset,
                });

                approximateXPosition += random.RandBetween(1.5d, 3d);
            }
        }




        private static void Chorus(AdfChart chart, int start, int end)
        {
            var random = new Random();
            for (int i = start; i < end; i++)
            {
                foreach (var it in chart.ChartTiles[i].TileEvents)
                {
                    if (it is not AdfEventPositionTrack positionTrackEvent) continue;
                    positionTrackEvent.PositionOffset.X -= .5;
                }
            }

            // 1. [start, +28)
            List<int> partOneAnchorTiles = [.. Enumerable.Range(start, 28).Where(i => chart.ChartTiles[i].TileEvents.Any(e => e is AdfEventPositionTrack))];
            partOneAnchorTiles.Add(start + 28);
            for (int i = 0; i < partOneAnchorTiles.Count - 1; i++)
            {
                chart.ChartTiles[partOneAnchorTiles[i]].TileEvents.Add(new AdfEventMoveTrack()
                {
                    StartTile = new(partOneAnchorTiles[i], AdfTileReferenceType.Start),
                    EndTile = new(partOneAnchorTiles[i + 1] - 1, AdfTileReferenceType.Start),
                    Opacity = 0,
                    Duration = 0,
                    AngleOffset = -114514,
                });
                if (partOneAnchorTiles[i + 1] - partOneAnchorTiles[i] == 2)
                {
                    chart.ChartTiles[partOneAnchorTiles[i]].TileEvents.Add(new AdfEventMoveTrack()
                    {
                        StartTile = new(partOneAnchorTiles[i], AdfTileReferenceType.Start),
                        EndTile = new(partOneAnchorTiles[i], AdfTileReferenceType.Start),
                        PositionOffset = new((chart.ChartTiles[partOneAnchorTiles[i]].TargetAngle == 120d ? 1 : -1) * 4, (chart.ChartTiles[partOneAnchorTiles[i]].TargetAngle == 120d ? -1 : 1) * 2 * 1.732),
                        Duration = 0.5d,
                        AngleOffset = -180,
                        Ease = AdfEaseType.OutElastic,
                    });
                    chart.ChartTiles[partOneAnchorTiles[i]].TileEvents.Add(new AdfEventMoveTrack()
                    {
                        StartTile = new(partOneAnchorTiles[i] + 1, AdfTileReferenceType.Start),
                        EndTile = new(partOneAnchorTiles[i] + 1, AdfTileReferenceType.Start),
                        PositionOffset = new(0, (chart.ChartTiles[partOneAnchorTiles[i]].TargetAngle == 120d ? 1 : -1) * 2 * 1.732),
                        Duration = 0.5d,
                        AngleOffset = -180,
                        Ease = AdfEaseType.OutElastic,
                    });
                }
                chart.ChartTiles[partOneAnchorTiles[i]].TileEvents.Add(new AdfEventMoveTrack()
                {
                    StartTile = new(partOneAnchorTiles[i], AdfTileReferenceType.Start),
                    EndTile = new(partOneAnchorTiles[i + 1] - 1, AdfTileReferenceType.Start),
                    Opacity = 200,
                    Scale = new(random.RandBetween(600, 1000)),
                    RotationOffset = random.RandBetween(-5, 5),
                    Duration = 0.5d,
                    AngleOffset = -180,
                    Ease = AdfEaseType.OutElastic,
                });
                chart.ChartTiles[partOneAnchorTiles[i]].TileEvents.Add(new AdfEventMoveTrack()
                {
                    StartTile = new(partOneAnchorTiles[i], AdfTileReferenceType.Start),
                    EndTile = new(partOneAnchorTiles[i + 1] - 1, AdfTileReferenceType.Start),
                    Opacity = 100,
                    Scale = new(100),
                    PositionOffset = new(0, 0),
                    RotationOffset = 0,
                    Duration = 0.5d,
                    AngleOffset = -90,
                    Ease = AdfEaseType.InBack,
                });

            }

            // 2. [+28, end)
            chart.ChartTiles[start + 28].TileEvents.Add(new AdfEventRecolorTrack()
            {
                StartTile = new(start + 1, AdfTileReferenceType.Start),
                EndTile = new(-1, AdfTileReferenceType.ThisTile),
                TrackStyle = AdfTrackStyle.Minimal,
                TrackColor = new("888888FF"),
                TrackGlowIntensity = 0,
                Duration = 0d,
            });
            for (int i = start + 1; i < start + 28; i++)
            {
                chart.ChartTiles[start + 28].TileEvents.Add(new AdfEventMoveTrack()
                {
                    StartTile = new(i, AdfTileReferenceType.Start),
                    EndTile = new(i, AdfTileReferenceType.Start),
                    Scale = new(random.RandBetween(300, 600)),
                    PositionOffset = new((i >= start + 15 ? 1 : -1) * random.RandBetween(15, 25), random.RandBetween(-6, 6)),
                    Duration = random.RandBetween(3d, 5d),
                    Opacity = 0,
                    RotationOffset = random.RandBetween(-180, 180),
                    Ease = AdfEaseType.OutCirc,
                });
            }
            chart.ChartTiles[start + 28].TileEvents.Add(new AdfEventMoveTrack()
            {
                StartTile = new(start, AdfTileReferenceType.Start),
                EndTile = new(start, AdfTileReferenceType.Start),
                Scale = new(0),
                Opacity = 0,
                Duration = 0d,
            });
			for (int i = 0; i < 24; i++)
			{
                var gid = random.Next(1000000).ToString().PadLeft(6, '0');
				chart.AddDecorationToChart(new()
				{
					DecorationImage = "circle.png",
					Opacity = 0d,
					Tag = $"quartrond-random-starting-${start}-circle quartrond-random-starting-${start}-circle-{gid}",
					RelativeTo = AdfMoveDecorationRelativeToType.Tile,
					Position = new(random.RandBetween(-18, 18), random.RandBetween(-8, 8)),
					Floor = start + 16,
					Depth = 5,
					BlendMode = AdfBlendMode.Multiply,
					Scale = new(0)
				});

				chart.ChartTiles[start + 28].TileEvents.Add(new AdfEventMoveDecorations()
				{
					Duration = 0d,
					Opacity = 100d,
					Tag = $"quartrond-random-starting-${start}-circle-{gid}"
				});
				chart.ChartTiles[start + 28].TileEvents.Add(new AdfEventMoveDecorations()
				{
					Ease = AdfEaseType.OutCirc,
					Scale = new(random.RandBetween(100, 300)),
                    RelativeTo = AdfMoveDecorationRelativeToType.LastPosition,
					Duration = 4d,
					Tag = $"quartrond-random-starting-${start}-circle-{gid}",
                    AngleOffset = random.RandBetween(0, 180),
				});
			}

            chart.ChartTiles[end + 1].TileEvents.Add(new AdfEventMoveDecorations()
            {
                Tag = $"quartrond-random-starting-${start}-circle",
                Opacity = 0,
                Visible = false,
                Duration = 0,
            });




            chart.ChartTiles[start + 28].TileEvents.Add(new AdfEventAddDecoration()
            {
                DecorationImage = "white.png",
                Tag = $"quartrond-chorus-{start}-white",
                Floor = start + 28,
                Rotation = 0,
                RelativeTo = AdfMoveDecorationRelativeToType.Tile,
                Position = new(-1, 0),
                Scale = new(75, 1000),
                Opacity = 0,
                Color = new("D3B666FF"),
                Depth = 1,
            });

            List<int> partTwoAnchorTiles = [.. Enumerable.Range(start + 28, end - start - 28).Where(i => chart.ChartTiles[i].TileEvents.Any(e => e is AdfEventPositionTrack))];
            partTwoAnchorTiles.Add(end);
            chart.ChartTiles[start + 28].TileEvents.Add(new AdfEventMoveDecorations()
            {
                Tag = $"quartrond-chorus-{start}-white",
                Opacity = 100,
                Duration = 0,
            });

            var rectangleMoveAmount = 6d;
            for (int i = 0; i < partTwoAnchorTiles.Count; i++)
            {
                chart.ChartTiles[partTwoAnchorTiles[i]].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-chorus-{start}-white",
                    RelativeTo = AdfMoveDecorationRelativeToType.LastPosition,
                    PositionOffset = new((i % 4 == 0 ? -3 : 1) * rectangleMoveAmount, 0),
                    Duration = 1d,
                    Ease = AdfEaseType.OutBack,
                });
            }

            chart.ChartTiles[end + 1].TileEvents.Add(new AdfEventMoveDecorations()
            {
                Tag = $"quartrond-chorus-{start}-white",
                Opacity = 0,
                Duration = 0,
            });


            for (int i = 0; i < partTwoAnchorTiles.Count - 1; i++)
            {
                var xOffset = (-1d * i) + (-22d) + (i % 4 * 6d);  // a.k.a. rectangleMoveAmount

                chart.ChartTiles[partTwoAnchorTiles[i]].TileEvents.Add(new AdfEventMoveTrack()
                {
                    StartTile = new(partTwoAnchorTiles[i], AdfTileReferenceType.Start),
                    EndTile = new(partTwoAnchorTiles[i + 1] - 1, AdfTileReferenceType.Start),
                    PositionOffset = new(xOffset, null),
                    Opacity = 0,
                    Duration = 0,
                    AngleOffset = -114514,
                });
                if (partTwoAnchorTiles[i + 1] - partTwoAnchorTiles[i] == 2)
                {
                    chart.ChartTiles[partTwoAnchorTiles[i]].TileEvents.Add(new AdfEventMoveTrack()
                    {
                        StartTile = new(partTwoAnchorTiles[i], AdfTileReferenceType.Start),
                        EndTile = new(partTwoAnchorTiles[i], AdfTileReferenceType.Start),
                        PositionOffset = new(xOffset + ((chart.ChartTiles[partTwoAnchorTiles[i]].TargetAngle == 120d ? 1 : -1) * 4), (chart.ChartTiles[partTwoAnchorTiles[i]].TargetAngle == 120d ? -1 : 1) * 2 * 1.732),
                        Duration = 0.5d,
                        AngleOffset = -180,
                        Ease = AdfEaseType.OutElastic,
                    });
                    chart.ChartTiles[partTwoAnchorTiles[i]].TileEvents.Add(new AdfEventMoveTrack()
                    {
                        StartTile = new(partTwoAnchorTiles[i] + 1, AdfTileReferenceType.Start),
                        EndTile = new(partTwoAnchorTiles[i] + 1, AdfTileReferenceType.Start),
                        PositionOffset = new(xOffset, (chart.ChartTiles[partTwoAnchorTiles[i]].TargetAngle == 120d ? 1 : -1) * 2 * 1.732),
                        Duration = 0.5d,
                        AngleOffset = -180,
                        Ease = AdfEaseType.OutElastic,
                    });
                }
                chart.ChartTiles[partTwoAnchorTiles[i]].TileEvents.Add(new AdfEventMoveTrack()
                {
                    StartTile = new(partTwoAnchorTiles[i], AdfTileReferenceType.Start),
                    EndTile = new(partTwoAnchorTiles[i + 1] - 1, AdfTileReferenceType.Start),
                    Opacity = 200,
                    Scale = new(random.RandBetween(600, 1000)),
                    RotationOffset = random.RandBetween(-5, 5),
                    Duration = 0.5d,
                    AngleOffset = -180,
                    Ease = AdfEaseType.OutElastic,
                });
                chart.ChartTiles[partTwoAnchorTiles[i]].TileEvents.Add(new AdfEventMoveTrack()
                {
                    StartTile = new(partTwoAnchorTiles[i], AdfTileReferenceType.Start),
                    EndTile = new(partTwoAnchorTiles[i + 1] - 1, AdfTileReferenceType.Start),
                    Opacity = 100,
                    Scale = new(100),
                    PositionOffset = new(xOffset, 0),
                    RotationOffset = 0,
                    Duration = 0.5d,
                    AngleOffset = -90,
                    Ease = AdfEaseType.InBack,
                });
                chart.ChartTiles[partTwoAnchorTiles[i]].TileEvents.Add(new AdfEventRecolorTrack()
                {
                    StartTile = new(partTwoAnchorTiles[i], AdfTileReferenceType.Start),
                    EndTile = new(partTwoAnchorTiles[i + 1] - 1, AdfTileReferenceType.Start),
                    TrackColor = new("FFFFFFFF"),
                    TrackStyle = AdfTrackStyle.NeonLight,
                    Duration = 0,
                    TrackGlowIntensity = 0,
                    AngleOffset = 180,
                });
            }

            chart.ChartTiles[end + 1].TileEvents.Add(new AdfEventMoveTrack()
            {
                StartTile = new(start + 28, AdfTileReferenceType.Start),
                EndTile = new(end, AdfTileReferenceType.Start),
                Duration = 0,
                Opacity = 0,
            });
        }




        private static void Verse(AdfChart chart)
            {
                chart.ModernTrackAppear(77, 120, 4d, 4d, -2, 2, -4, -2, -45, 45, 25, 50, 60, 200, 100, 0.8);
            chart.ModernTrackDisappear(76, 120, 4d, -4d, -2, 2, -4, -2, -45, 45, 25, 50, 60, 0.8);

            foreach (var tile in Enumerable.Range(120, 221 - 120).Where(i => !chart.ChartTiles[i].TileEvents.Any(e => e is AdfEventEditorComment)))
            {
                chart.ModernTrackAppear(tile, tile + 1, 4d, 4d, 0, 0, -0.5, -0.1, -5, 5, 95, 100, 30, 100, 100, 1);
                if (tile == 127 || tile == 129 || tile == 131) continue;
                chart.ModernTrackDisappear(tile, tile + 1, 4d, -5d, -4, -1, 0, 0, -25, 25, 95, 100, 80, 1);
            }

            var random = new Random();
            for (int i = 156; i < 204; i++)
            {
                if (!chart.ChartTiles[i].TileEvents.Any(e => e is AdfEventEditorComment)) continue;
                chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveTrack()
                {
                    StartTile = new(0, AdfTileReferenceType.ThisTile),
                    EndTile = new(0, AdfTileReferenceType.ThisTile),
                    PositionOffset = new(0, 1.5),
                    RotationOffset = random.RandBetween(-180, 180),
                    Scale = new(random.RandBetween(150, 200)),
                    Opacity = 0,
                    Duration = 0,
                    AngleOffset = -114514,
                });
                chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveTrack()
                {
                    StartTile = new(0, AdfTileReferenceType.ThisTile),
                    EndTile = new(0, AdfTileReferenceType.ThisTile),
                    Opacity = 800,
                    Duration = 2d,
                    AngleOffset = -1080,
                    Ease = AdfEaseType.InElastic,
                });
                chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveTrack()
                {
                    StartTile = new(0, AdfTileReferenceType.ThisTile),
                    EndTile = new(0, AdfTileReferenceType.ThisTile),
                    Opacity = 0,
                    Duration = 8d,
                    AngleOffset = -720,
                    Ease = AdfEaseType.InBounce,
                });
                chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveTrack()
                {
                    StartTile = new(0, AdfTileReferenceType.ThisTile),
                    EndTile = new(0, AdfTileReferenceType.ThisTile),
                    PositionOffset = new(0, 0),
                    RotationOffset = 0,
                    Scale = new(100),
                    Duration = 4d,
                    AngleOffset = -720,
                    Ease = AdfEaseType.InBack,
                });
            }

            for (int i = 0; i < 48; i++)
            {
                chart.ChartTiles[205].TileEvents.Add(new AdfEventAddObject()
                {
                    ObjectType = AdfObjectType.Floor,
                    Floor = 205,
                    Tag = $"quartrond-manytiles-205 quartrond-manytiles-205-${i}",
                    TrackColor = new("d3b666ff"),
                    TrackStyle = AdfTrackStyle.Neon,
                    TrackAngle = 180,
                    TrackOpacity = 0,
                    Depth = (int)random.RandBetween(-5, 5),
                    Position = new(random.RandBetween(15, 30), random.RandBetween(-10, 10)),
                    Rotation = 0,
                });

                chart.ChartTiles[205].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-manytiles-205-${i}",
                    RelativeTo = AdfMoveDecorationRelativeToType.LastPosition,
                    PositionOffset = new(random.RandBetween(-100, -50), null),
                    Duration = 16d,
                    Ease = AdfEaseType.Linear,
                });
                chart.ChartTiles[205].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-manytiles-205-${i}",
                    RelativeTo = AdfMoveDecorationRelativeToType.LastPosition,
                    PositionOffset = new(null, random.RandBetween(-50, -25)),
                    Duration = 12d,
                    Ease = AdfEaseType.InBack,
                });
                chart.ChartTiles[205].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-manytiles-205-${i}",
                    RelativeTo = AdfMoveDecorationRelativeToType.LastPosition,
                    RotationOffset = random.RandBetween(-180, 180),
                    Duration = 16d,
                    Ease = AdfEaseType.OutCirc,
                });
            }
            chart.ChartTiles[205].TileEvents.Add(new AdfEventMoveDecorations()
            {
                Tag = $"quartrond-manytiles-205",
                Opacity = 100,
                Duration = 0d,
                Ease = AdfEaseType.Linear,
            });
            chart.ChartTiles[221].TileEvents.Add(new AdfEventMoveDecorations()
            {
                Tag = $"quartrond-manytiles-205",
                Opacity = 0,
                Duration = 0d,
                Ease = AdfEaseType.Linear,
            });
        }


        private static void PrepareFlashes(AdfChart chart)
        {
            for (int i = 0; i < 8; i++)
            {
                Mat mat = Cv2.ImRead(chart.FileLocation?.Parent?.FullName + $"\\larping-{i + 1}.png");
                double defaultCameraZoom = 250;

                double widthMultiplier = (double) ExtensionSharedConstants.CanvasWidth / mat.Width * defaultCameraZoom / 100d;
                double heightMultiplier = (double) ExtensionSharedConstants.CanvasHeight / mat.Height  * defaultCameraZoom / 100d;

                double scale = 100d * Math.Max(widthMultiplier, heightMultiplier);


                chart.ChartTiles[0].TileEvents.Add(new AdfEventAddDecoration()
                {
                    DecorationImage = $"larping-{i + 1}.png",
                    Tag = $"quartrond-image-flash quartrond-image-flash-{i}",
                    Scale = new(scale),
                    Opacity = 0,
                    LockRotation = true,
                    RelativeTo = AdfMoveDecorationRelativeToType.Camera,
                    Parallax = new(0, 0),
                    Depth = 6,
                });
            }
        }

        private static void ScreenshotFlashes(AdfChart chart, int start, int end, double trackOffsetX, int loop)
        {
            AdfColor[] trackColors = [new("BF7915CC"), new("D1B4C1CC"), new("878787CC"), new("C9BC99CC"), new("ACB59ACC"), new("378C53CC"), new("5493A1CC"), new("7D3838CC")];

            List<int> switchTiles = [start - 1];
            var flashIndex = 0;
            for (int i = start; i < end; i++) {
                if (!chart.ChartTiles[i].TileEvents.Any(e => e is AdfEventMoveCamera)) continue;

                switchTiles.Add(i);

                chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-image-flash-{(flashIndex + loop - 1) % loop}",
                    Opacity = 0,
                    Duration = 0d,
                });
                chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-image-flash-{flashIndex}",
                    Opacity = 40,
                    Duration = 0d,
                });

                flashIndex++; flashIndex %= loop;
            }
            switchTiles.Add(end); switchTiles.Add(end + 8);
            chart.ChartTiles[end + 8].TileEvents.Add(new AdfEventMoveDecorations()
            {
                Tag = $"quartrond-image-flash-{(flashIndex + loop - 1) % loop}",
                Opacity = 0,
                Duration = 0d,
            });

            chart.ChartTiles[start - 1].TileEvents.Add(new AdfEventMoveTrack()
            {
                StartTile = new(start - 1, AdfTileReferenceType.Start),
                EndTile = new(end + 8, AdfTileReferenceType.Start),
                Opacity = 0,
                Duration = 0d,
                AngleOffset = -114514,
            });
            for (int i = 1; i < switchTiles.Count - 2; i++)
            {
                chart.ChartTiles[switchTiles[i]].TileEvents.Add(new AdfEventMoveTrack()
                {
                    StartTile = new(switchTiles[i - 1], AdfTileReferenceType.Start),
                    EndTile = new(switchTiles[i] - 1, AdfTileReferenceType.Start),
                    Opacity = 0,
                    Duration = 0d,
                });
                chart.ChartTiles[switchTiles[i]].TileEvents.Add(new AdfEventMoveTrack()
                {
                    StartTile = new(switchTiles[i], AdfTileReferenceType.Start),
                    EndTile = new(switchTiles[i + 1] - 1, AdfTileReferenceType.Start),
                    Opacity = 300,
                    Duration = 0d,
                    AngleOffset = -180,
                });
                chart.ChartTiles[switchTiles[i]].TileEvents.Add(new AdfEventRecolorTrack()
                {
                    StartTile = new(switchTiles[i - 1], AdfTileReferenceType.Start),
                    EndTile = new(switchTiles[i + 2], AdfTileReferenceType.Start),
                    TrackColor = trackColors[(i + loop - 1) % loop],
                    TrackColorType = AdfTrackColorType.Single,
                    TrackStyle = AdfTrackStyle.NeonLight,
                    Duration = 0d,
                    AngleOffset = 0,
                });
            }

            chart.MultipleTrack(start, end + 8, trackOffsetX, -0.0006, "000000", 100, 100, 0.001, 0.001, AdfTrackStyle.Minimal, true, 1);

            chart.ModernTrackAppear(end, end + 9, 4d, 4d, -0.5, 0.5, -1.5, -0.5, -20, 02, 80, 95, 45, 300, 100);

			//Random random = new();
			//for (int i = start; i < end; i++)
			//{
   //             if (chart.ChartTiles[i].TargetAngle == 999d) continue;

   //             var gid = random.Next(1000000).ToString().PadLeft(6, '0');
			//	chart.AddDecorationToChart(new()
			//	{
			//		DecorationImage = "circle.png",
			//		Opacity = 0d,
			//		Tag = $"quartrond-random-circle-starting-${start} quartrond-random-circle-starting-${start}-{gid}",
			//		RelativeTo = AdfMoveDecorationRelativeToType.Tile,
			//		Position = new(0, 0),
			//		Floor = i,
			//		Depth = 5,
			//		BlendMode = AdfBlendMode.Difference,
			//		Scale = new(0)
			//	});

			//	chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveDecorations()
			//	{
			//		Duration = 0d,
			//		Opacity = 100d,
			//		Tag = $"quartrond-random-circle-starting-${start}-{gid}"
			//	});
			//	chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveDecorations()
			//	{
			//		Ease = AdfEaseType.OutCirc,
			//		Scale = new(random.RandBetween(30, 100)),
   //                 RelativeTo = AdfMoveDecorationRelativeToType.LastPosition,
			//		PositionOffset = new(random.RandBetween(-1, 1), random.RandBetween(-2, 2)),
			//		Duration = 1d,
			//		Tag = $"quartrond-random-circle-starting-${start}-{gid}"
			//	});
			//}

   //         chart.ChartTiles[end + 8].TileEvents.Add(new AdfEventMoveDecorations()
   //         {
   //             Visible = false,
   //             Opacity = 0,
   //             Tag = $"quartrond-random-circle-starting-${start}",
   //             Duration = 0,
   //         });
        }







        private static void TrackSpiralPart(AdfChart chart)
        {
			Random random = new();
            for (int i = 0; i < 32; i++)
            {
                chart.ChartTiles[594 + (4 * i)].TileEvents.Add(new AdfEventScaleRadius() { Scale = 400 - ((400 - 100) / 31d * i) });
                chart.ChartTiles[594 + (4 * i)].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = "manual-planet",
                    Scale = new(400 - ((400 - 100) / 31d * i)),
                    Duration = 0d
                });
                chart.ChartTiles[594 + (4 * i)].TileEvents.Add(new AdfEventPositionTrack() { Scale = 400 - ((400 - 100) / 31d * i) });
                chart.ChartTiles[594 + (4 * i)].TileEvents.Add(new AdfEventMoveCamera()
                {
                    Zoom = 400 - ((400 - 100) / 31d * i),
                    Duration = 3d,
                    Ease = AdfEaseType.OutCirc,
                });



                for (int j = 0; j < 4; j++)
                {
                    chart.ChartTiles[594 + (i * 4)].TileEvents.Add(new AdfEventMoveTrack()  // Move before appearance
                    {
                        StartTile = new(j, AdfTileReferenceType.ThisTile),
                        EndTile = new(j, AdfTileReferenceType.ThisTile),
                        PositionOffset = PositionFromPolar(random.RandBetween(1, 2), 180 - (45 * i) + random.RandBetween(-5, 5)),
                        RotationOffset = random.RandBetween(-25, 25),
                        Opacity = 0,
                        AngleOffset = -114514,
                        Duration = 0d,
                    });
                    if (i >= 30) continue;
                    double trackAppearAngleOffset = (-6 * (360 - 135)) + random.RandBetween(-20, 20);
                    chart.ChartTiles[594 + (i * 4)].TileEvents.Add(new AdfEventMoveTrack()  // Opacity blink animation
                    {
                        StartTile = new(j, AdfTileReferenceType.ThisTile),
                        EndTile = new(j, AdfTileReferenceType.ThisTile),
                        Opacity = 1000,
                        AngleOffset = trackAppearAngleOffset - 0.001,
                        Duration = 0d,
                    });
                    chart.ChartTiles[594 + (i * 4)].TileEvents.Add(new AdfEventMoveTrack()  // Opacity blink animation
                    {
                        StartTile = new(j, AdfTileReferenceType.ThisTile),
                        EndTile = new(j, AdfTileReferenceType.ThisTile),
                        Opacity = 100,
                        AngleOffset = trackAppearAngleOffset,
                        Duration = 4d,
                        Ease = AdfEaseType.OutElastic,
                    });
                    chart.ChartTiles[594 + (i * 4)].TileEvents.Add(new AdfEventMoveTrack()  // Move to place
                    {
                        StartTile = new(j, AdfTileReferenceType.ThisTile),
                        EndTile = new(j, AdfTileReferenceType.ThisTile),
                        PositionOffset = new(0, 0),
                        RotationOffset = 0,
                        AngleOffset = trackAppearAngleOffset,
                        Duration = random.RandBetween(3, 4),
                        Ease = AdfEaseType.OutExpo,
                    });
                    chart.ChartTiles[594 + (i * 4)].TileEvents.Add(new AdfEventMoveTrack()  // Disappear
                    {
                        StartTile = new(j, AdfTileReferenceType.ThisTile),
                        EndTile = new(j, AdfTileReferenceType.ThisTile),
                        PositionOffset = PositionFromPolar(random.RandBetween(1, 2), 180 - (45 * i) + random.RandBetween(-5, 5)),
                        RotationOffset = random.RandBetween(-45, 45),
                        Opacity = 0,
                        AngleOffset = (9 * (360 - 135)) + random.RandBetween(-20, 20),
                        Duration = random.RandBetween(3, 4),
                        Ease = AdfEaseType.OutCubic,
                    });
                }
            }


            for (int i = 0; i < 48; i++)
            {
                chart.ChartTiles[714].TileEvents.Add(new AdfEventAddObject()
                {
                    ObjectType = AdfObjectType.Floor,
                    Floor = 714,
                    Tag = $"quartrond-manytiles-706 quartrond-manytiles-706-${i}",
                    TrackColor = new("d3b666ff"),
                    TrackStyle = AdfTrackStyle.Neon,
                    TrackAngle = 180,
                    TrackOpacity = 0,
                    Depth = (int)random.RandBetween(-5, 5),
                    Position = new(random.RandBetween(-5, 5), random.RandBetween(-2, 6)),
                    Rotation = 0,
                });

                chart.ChartTiles[706].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-manytiles-706-${i}",
                    RelativeTo = AdfMoveDecorationRelativeToType.LastPosition,
                    PositionOffset = new(random.RandBetween(-10, 10), null),
                    Duration = 16d,
                    Ease = AdfEaseType.Linear,
                });
                chart.ChartTiles[706].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-manytiles-706-${i}",
                    RelativeTo = AdfMoveDecorationRelativeToType.LastPosition,
                    PositionOffset = new(null, random.RandBetween(-40, -10)),
                    Duration = 12d,
                    Ease = AdfEaseType.InBack,
                });
                chart.ChartTiles[706].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-manytiles-706-${i}",
                    RelativeTo = AdfMoveDecorationRelativeToType.LastPosition,
                    RotationOffset = random.RandBetween(-180, 180),
                    Duration = 16d,
                    Ease = AdfEaseType.OutCirc,
                });
            }
            chart.ChartTiles[706].TileEvents.Add(new AdfEventMoveDecorations()
            {
                Tag = $"quartrond-manytiles-706",
                Opacity = 100,
                Duration = 0d,
                Ease = AdfEaseType.Linear,
            });
            chart.ChartTiles[722].TileEvents.Add(new AdfEventMoveDecorations()
            {
                Tag = $"quartrond-manytiles-706",
                Opacity = 0,
                Duration = 0d,
                Ease = AdfEaseType.Linear,
            });



            for (int i = 706; i < 722; i++)
            {
                chart.ChartTiles[706].TileEvents.Add(new AdfEventMoveTrack()
                {
                    StartTile = new(i, AdfTileReferenceType.Start),
                    EndTile = new(i, AdfTileReferenceType.Start),
                    PositionOffset = new(random.RandBetween(-10, 10), random.RandBetween(-20, -10)),
                    RotationOffset = random.RandBetween(-180, 180),
                    Ease = AdfEaseType.OutExpo,
                    Duration = 5d,
                });
                chart.ChartTiles[706].TileEvents.Add(new AdfEventMoveTrack()
                {
                    StartTile = new(i, AdfTileReferenceType.Start),
                    EndTile = new(i, AdfTileReferenceType.Start),
                    Opacity = 0d,
                    Ease = AdfEaseType.OutCubic,
                    Duration = 3d,
                });
            }
        }

        private static AdfPosition PositionFromPolar(double radius, double angleDegrees) => new(radius * Math.Cos(angleDegrees / 180d * Math.PI), radius * Math.Sin(angleDegrees / 180d * Math.PI));
    }
}
