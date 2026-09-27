using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using MagicShaper.AdfExtensions;
using MagicShaper.AdfExtensions.Gimmicks;
using MagicShaper.AdofaiCore.AdfClass;
using MagicShaper.AdofaiCore.AdfEvents;
using MagicShaper.AdofaiCore.AdfEvents.Dlc;
using MagicShaper.AdofaiCore.DerivedClass;
using OpenCvSharp;

namespace MagicShaper.VfxProjects
{
    [SupportedOSPlatform("windows")]
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




            //{
            //    var lyrics = File.ReadAllText(@"G:\Adofai levels\larping\_lyrics.txt");
            //    HashSet<char> possibleCharacters = [];
            //    foreach (var it in lyrics)
            //    {
            //        if (it == ' ' || it == '\n' || it == '\r' || it == '\t') continue;
            //        possibleCharacters.Add(it);
            //    }
            //    foreach (var it in possibleCharacters) RenderCharacter(chart, it);
            //}

            DarkRoomLyrics(chart);
            HallwayLyrics(chart);
            ChorusLyrics(chart, 221);
            LongFallLyrics(chart);
            ManyTenKeysLyrics(chart);
            ChorusLyrics(chart, 538);
            TrackSpiralLyrics(chart);



            chart.RenderCreditRoleAndName(838, "Track", "SSSeanLB", -ExtensionSharedConstants.TileWidth * 5, ExtensionSharedConstants.TileWidth * 6, 0, 4, 64, 100);
            chart.RenderCreditRoleAndName(838, "Visuals", "quartrond", -ExtensionSharedConstants.TileWidth * 5, (int)(ExtensionSharedConstants.TileWidth * 3.5), 0, 4, 64, 100);
            chart.RenderCreditRoleAndName(838, "Library", "MagicShaper-AdfEventLib", -ExtensionSharedConstants.TileWidth * 5, ExtensionSharedConstants.TileWidth * -6, 0, 4, 64, 100);

            chart.RenderCreditRoleAndName(838, "Artist", "The Larping Tombstone", ExtensionSharedConstants.TileWidth * 5, ExtensionSharedConstants.TileWidth * 6, 0, 4, 64, 100);
            chart.RenderCreditRoleAndName(838, "Song", "\"Looping the Rooms (Larping the Rooms)\" (Remix)", ExtensionSharedConstants.TileWidth * 5, (int)(ExtensionSharedConstants.TileWidth * 3.5), 0, 4, 64, 100);













            File.WriteAllText(@"G:\Adofai levels\larping\level-effect.adofai", chart.ChartJson.ToString());
        }





        private static void TrackSpiralLyrics(AdfChart chart)
        {
            List<Tuple<int, string>> lyrics = [new(594, "ああ、扉を開け"), new(617, "意味も忘れ"), new(633, "いかれた夢の奥へ"), new(662, "どこまでも続いてく"), new(706, "")];

            for (int l = 0; l < lyrics.Count - 1; l++)
            {
                var lyric = lyrics[l];
                var floor = lyric.Item1; var text = lyric.Item2;

                for (int i = 0; i < text.Length; i++)
                {
                    var it = text[i]; var byteString = $"{Convert.ToUInt16(it):X2}";
                    chart.AddDecorationToChart(new AdfDecoration()
                    {
                        Floor = floor,
                        RelativeTo = AdfMoveDecorationRelativeToType.Camera,
                        DecorationImage = $"quartrond_lyric_{byteString}.png",
                        Tag = $"quartrond-spiral-{floor}-{i}",
                        Color = new("000000FF"),
                        ImageSmoothing = false,
                        Locked = true,
                        LockScale = true,
                        Scale = new(45),
                        Position = new((-((text.Length - 1) / 2d) + i) * 0.9d, 0d),
                        Opacity = 0,
                        Depth = -1,
                    });
                    chart.ChartTiles[floor].TileEvents.Add(new AdfEventMoveDecorations()
                    {
                        Tag = $"quartrond-spiral-{floor}-{i}",
                        RelativeTo = AdfMoveDecorationRelativeToType.LastPosition,
                        PositionOffset = new(-(-((text.Length - 1) / 2d) + i) * 0.2d, null),
                        Scale = new(55),
                        Duration = 9d,
                        Ease = AdfEaseType.OutCirc,
                    });
                    chart.ChartTiles[floor].TileEvents.Add(new AdfEventMoveDecorations()
                    {
                        Tag = $"quartrond-spiral-{floor}-{i}",
                        Opacity = 100,
                        Duration = 6d,
                        Ease = AdfEaseType.OutSine,
                    });

                    chart.ChartTiles[lyrics[l + 1].Item1].TileEvents.Add(new AdfEventMoveDecorations()
                    {
                        Tag = $"quartrond-spiral-{floor}-{i}",
                        RelativeTo = AdfMoveDecorationRelativeToType.LastPosition,
                        PositionOffset = new(-(-((text.Length - 1) / 2d) + i) * 0.2d, null),
                        Scale = new(35),
                        Opacity = 0,
                        Duration = 6d,
                        Ease = AdfEaseType.OutCirc,
                    });
                }
            }
        }

        private static void ManyTenKeysLyrics(AdfChart chart)
        {
            List<Tuple<int, string>> lyrics = [new(331, "落ちてた柘榴で飢えを凌いだ"), new(378, "頭蓋の中から耳鳴りがした"), new(434, "進めど進めど変わりないなら"), new(490, "抗う意味など"),];

            var random = new Random();
            foreach (var lyric in lyrics)
            {
                var floor = lyric.Item1; var text = lyric.Item2;
                for (int i = 0; i < text.Length; i++)
                {
                    var it = text[i]; var byteString = $"{Convert.ToUInt16(it):X2}";
                    var rotationOffset = random.RandBetween(-135, 135);
                    chart.AddDecorationToChart(new AdfDecoration()  // main
                    {
                        Floor = floor,
                        RelativeTo = AdfMoveDecorationRelativeToType.Camera,
                        DecorationImage = $"quartrond_lyric_{byteString}.png",
                        Tag = $"quartrond-char-{floor}-{i} quartrond-char-manytenkeys",
                        BlendMode = AdfBlendMode.Screen,
                        ImageSmoothing = false,
                        Locked = true,
                        Scale = new(100),
                        Position = new(-6d + ((-((text.Length - 1) / 2d) + i) * 0.6d), 0d),
                        PivotOffset = new(0, -0.3d),
                        Rotation = rotationOffset,
                        Opacity = 0,
                        Depth = -1,
                    });

                    chart.ChartTiles[floor].TileEvents.Add(new AdfEventMoveDecorations()
                    {
                        Tag = $"quartrond-char-{floor}-{i}",
                        Duration = random.RandBetween(4, 6) * 64,
                        Opacity = 100,
                        PositionOffset = new(4d, null),
                        Ease = AdfEaseType.OutBack,
                        AngleOffset = 15 * i * 64,
                    });
                    chart.ChartTiles[floor].TileEvents.Add(new AdfEventMoveDecorations()
                    {
                        Tag = $"quartrond-char-{floor}-{i}",
                        Duration = random.RandBetween(14, 18) * 64,
                        RotationOffset = -rotationOffset,
                        Ease = AdfEaseType.OutElastic,
                        AngleOffset = 15 * i * 64,
                    });
                    chart.ChartTiles[floor].TileEvents.Add(new AdfEventMoveDecorations()
                    {
                        Tag = $"quartrond-char-{floor}-{i}",
                        Duration = random.RandBetween(9, 13) * 64,
                        Opacity = 0,
                        PositionOffset = new(random.RandBetween(8, 13), random.RandBetween(-3, 3)),
                        Ease = AdfEaseType.OutCirc,
                        AngleOffset = 2400 * 64,
                    });
                }
            }
            chart.ChartTiles[513].TileEvents.Add(new AdfEventMoveDecorations()
            {
                Tag = $"quartrond-char-manytenkeys",
                Duration = 0,
                Opacity = 0,
            });
        }


        private static void LongFallLyrics(AdfChart chart)
        {
            List<Tuple<int, string>> lyrics = [new(0, "空回るソウト"), new(180, "終わらないロード"), new(360, "鉄臭い酸素"), new(540, "届かないSOS")];

            var random = new Random();
            foreach (var lyric in lyrics)
            {
                var floor = 328; var text = lyric.Item2; var angleOffsetOffset = lyric.Item1;
                var textAlignmentOffset = -(text.Length / 2d * 0.8d);
                for (int i = 0; i < text.Length; i++)
                {
                    var it = text[i]; var byteString = $"{Convert.ToUInt16(it):X2}";
                    var offsetRotation = random.RandBetween(30, 60);
                    chart.AddDecorationToChart(new AdfDecoration()  // main
                    {
                        Floor = floor,
                        RelativeTo = AdfMoveDecorationRelativeToType.Tile,
                        DecorationImage = $"quartrond_lyric_{byteString}.png",
                        Tag = $"quartrond-char-longfall-{angleOffsetOffset}-{i}",
                        BlendMode = AdfBlendMode.Screen,
                        ImageSmoothing = false,
                        Locked = true,
                        Scale = new(0),
                        Rotation = offsetRotation,
                        Position = new((i * 0.8d) + textAlignmentOffset, (angleOffsetOffset * (-0.05d)) - 5.5d),
                        PivotOffset = new(0, 0.3d),
                        Opacity = 0,
                        Depth = 2,
                    });
                    chart.AddDecorationToChart(new AdfDecoration()  // shadow
                    {
                        Floor = floor,
                        RelativeTo = AdfMoveDecorationRelativeToType.Tile,
                        DecorationImage = $"quartrond_lyric_{byteString}.png",
                        Tag = $"quartrond-char-longfall-{angleOffsetOffset}-{i}",
                        BlendMode = AdfBlendMode.Screen,
                        ImageSmoothing = false,
                        Locked = true,
                        Scale = new(0),
                        Rotation = offsetRotation,
                        Position = new((i * 0.8d) + textAlignmentOffset, (angleOffsetOffset * (-0.05d)) - 5.5d),
                        PivotOffset = new(0.05d, 0.25d),
                        Color = new("8F7921FF"),
                        Opacity = 0,
                        Depth = 3,
                    });

                    chart.ChartTiles[floor].TileEvents.Add(new AdfEventMoveDecorations()
                    {
                        Tag = $"quartrond-char-longfall-{angleOffsetOffset}-{i}",
                        Opacity = 100,
                        RotationOffset = -offsetRotation,
                        Scale = new(55),
                        Duration = random.RandBetween(0.25, 0.5),
                        Ease = AdfEaseType.OutBack,
                        AngleOffset = (4 * i) + angleOffsetOffset,
                    });
                    chart.ChartTiles[floor].TileEvents.Add(new AdfEventMoveDecorations()
                    {
                        Tag = $"quartrond-char-longfall-{angleOffsetOffset}-{i}",
                        Opacity = 0,
                        RotationOffset = offsetRotation,
                        Scale = new(0),
                        Duration = random.RandBetween(0.25, 0.5),
                        Ease = AdfEaseType.InBack,
                        AngleOffset = 160 + random.RandBetween(-10, 10) + angleOffsetOffset,
                    });
                }
            }
        }
        private static void ChorusLyrics(AdfChart chart, int tile)
        {
            List<int> characterCounts = [8, 23, 8, 23];
            string lyrics = "くるくるくるくる1くりかえす2くりかえす2くりかえす2くりかえす1ふらふらふらふら1ふらくたる2ふらくたる2ふらくたる2ふらくたる";
            var characterPointer = 0; var characterCountPointer = 0;
            var characterXPosition = (characterCounts[characterCountPointer] - 1) / 2d * (-0.4d);
            var random = new Random();

            for (int i = tile; i < tile + 56; i++)
            {
                if (lyrics[characterPointer] == '1')
                {
                    // Annihilate previous lyrics
                    for (int j = 0; j < characterCounts[characterCountPointer]; j++)
                    {
                        var disappearAngleOffset = random.RandBetween(0, 60);
                        chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveDecorations()
                        {
                            Tag = $"quartrond-char-chorus1-{i - j - 1}",
                            PositionOffset = new(null, random.RandBetween(-10, -5)),
                            AngleOffset = disappearAngleOffset,
                            RotationOffset = random.RandBetween(-45, 45),
                            Duration = random.RandBetween(1, 2),
                            Scale = new(10),
                            Ease = AdfEaseType.InQuad,
                        });
                        chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveDecorations()
                        {
                            Tag = $"quartrond-char-chorus1-{i - j - 1}",
                            PositionOffset = new(random.RandBetween(-3, 3), null),
                            AngleOffset = disappearAngleOffset,
                            Duration = random.RandBetween(1, 2),
                            Ease = AdfEaseType.Linear,
                        });
                    }
                    chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveDecorations()
                    {
                        Tag = $"quartrond-char-chorus1-group-{characterCountPointer}",
                        Opacity = 0,
                        Duration = 3,
                        Ease = AdfEaseType.OutSine,
                    });

                    characterCountPointer++;
                    characterXPosition = (characterCounts[characterCountPointer] - 1) / 2d * (-0.4d);

                    characterPointer++;
                }

                if (lyrics[characterPointer] == '2') { characterPointer++; characterXPosition += 0.4d; }  // empty space, make sure it takes one block.

                var it = lyrics[characterPointer]; var byteString = $"{Convert.ToUInt16(it):X2}";
                var rotationOffset = random.RandBetween(-30, 30);

                chart.AddDecorationToChart(new AdfDecoration()
                {
                    Floor = i,
                    RelativeTo = AdfMoveDecorationRelativeToType.Camera,
                    DecorationImage = $"quartrond_lyric_{byteString}.png",
                    Tag = $"quartrond-char-chorus1-{i} quartrond-char-chorus1-group-{characterCountPointer}",
                    BlendMode = AdfBlendMode.Screen,
                    ImageSmoothing = false,
                    Locked = true,
                    Scale = new(45),
                    Position = new(characterXPosition, -8.5d),
                    Rotation = rotationOffset,
                    Opacity = 0,
                    Depth = -114,
                });
                chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-char-chorus1-{i}",
                    Scale = new(30, 90),
                    Duration = 0.33333d,
                    Opacity = 100,
                    Ease = AdfEaseType.InElastic,
                    AngleOffset = -60,
                });
                chart.ChartTiles[i].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-char-chorus1-{i}",
                    Scale = new(45),
                    RotationOffset = -rotationOffset,
                    Duration = 1d,
                    Ease = AdfEaseType.OutElastic,
                    AngleOffset = 0,
                });



                characterXPosition += 0.4d; characterPointer++;
            }




            // Annihilate previous lyrics
            for (int j = 0; j < characterCounts[characterCountPointer]; j++)
            {
                var disappearAngleOffset = random.RandBetween(0, 60);
                chart.ChartTiles[tile + 57].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-char-chorus1-{tile + 57 - j - 1}",
                    PositionOffset = new(null, random.RandBetween(-10, -5)),
                    AngleOffset = disappearAngleOffset,
                    RotationOffset = random.RandBetween(-45, 45),
                    Duration = random.RandBetween(1, 2),
                    Scale = new(10),
                    Ease = AdfEaseType.InQuad,
                });
                chart.ChartTiles[tile + 57].TileEvents.Add(new AdfEventMoveDecorations()
                {
                    Tag = $"quartrond-char-chorus1-{tile + 57 - j - 1}",
                    PositionOffset = new(random.RandBetween(-3, 3), null),
                    AngleOffset = disappearAngleOffset,
                    Duration = random.RandBetween(1, 2),
                    Ease = AdfEaseType.Linear,
                });
            }
            chart.ChartTiles[tile + 57].TileEvents.Add(new AdfEventMoveDecorations()
            {
                Tag = $"quartrond-char-chorus1-group-{characterCountPointer}",
                Opacity = 0,
                Duration = 3,
                Ease = AdfEaseType.OutSine,
            });
        }

        private static void HallwayLyrics(AdfChart chart)
        {
            List<Tuple<int, string>> lyrics = [new(120, "ドアの先に僕の背中が見えた"), new(140, "振り向いた先に希望が見えた"), new(156, "地獄の果てなどどこにあるのか"), new(187, "出口はまだなの？"),];

            var random = new Random();
            foreach (var lyric in lyrics)
            {
                var floor = lyric.Item1; var text = lyric.Item2;
                for (int i = 0; i < text.Length; i++)
                {
                    var it = text[i]; var byteString = $"{Convert.ToUInt16(it):X2}";
                    var rotationOffset = random.RandBetween(-135, 135);
                    chart.AddDecorationToChart(new AdfDecoration()  // main
                    {
                        Floor = floor,
                        RelativeTo = AdfMoveDecorationRelativeToType.Camera,
                        DecorationImage = $"quartrond_lyric_{byteString}.png",
                        Tag = $"quartrond-char-{floor}-{i}",
                        BlendMode = AdfBlendMode.Screen,
                        ImageSmoothing = false,
                        Locked = true,
                        Scale = new(46),
                        Position = new(-8d, -4d),
                        PivotOffset = new(0, -0.3d),
                        Rotation = rotationOffset,
                        Opacity = 0,
                        Depth = -1,
                    });

                    chart.ChartTiles[floor].TileEvents.Add(new AdfEventMoveDecorations()
                    {
                        Tag = $"quartrond-char-{floor}-{i}",
                        Duration = random.RandBetween(9, 12),
                        Opacity = 100,
                        PositionOffset = new(3d + (i * 0.3d), null),
                        Ease = AdfEaseType.OutBack,
                        AngleOffset = 15 * i,
                    });
                    chart.ChartTiles[floor].TileEvents.Add(new AdfEventMoveDecorations()
                    {
                        Tag = $"quartrond-char-{floor}-{i}",
                        Duration = random.RandBetween(14, 18),
                        RotationOffset = -rotationOffset,
                        Ease = AdfEaseType.OutElastic,
                        AngleOffset = 15 * i,
                    });
                    chart.ChartTiles[floor].TileEvents.Add(new AdfEventMoveDecorations()
                    {
                        Tag = $"quartrond-char-{floor}-{i}",
                        Duration = random.RandBetween(6, 8),
                        Scale = new(25),
                        PositionOffset = new(null, random.RandBetween(-10d, -6d)),
                        Ease = AdfEaseType.InBack,
                        AngleOffset = 2400 + random.RandBetween(-90, 90),
                    });
                    chart.ChartTiles[floor].TileEvents.Add(new AdfEventMoveDecorations()
                    {
                        Tag = $"quartrond-char-{floor}-{i}",
                        Duration = random.RandBetween(9, 13),
                        Opacity = 0,
                        PositionOffset = new(random.RandBetween(5, 10), null),
                        Ease = AdfEaseType.InSine,
                        AngleOffset = 2400 + random.RandBetween(-90, 90),
                    });
                }
            }
        }
        private static void DarkRoomLyrics(AdfChart chart)
        {
            List<Tuple<int, string>> lyrics = [new(77, "引き抜くカセット"), new(88, "押し込むリセット"), new(99, "迷い込む迷路"), new(110, "進めどもダルセーニョ"),];

            var random = new Random();
            foreach (var lyric in lyrics)
            {
                var floor = lyric.Item1; var text = lyric.Item2;
                var textAlignmentOffset = -(text.Length / 2d * 0.8d) + (chart.ChartTiles[floor].TargetAngle == 0d ? -4 : 4);
                for (int i = 0; i < text.Length; i++)
                {
                    var it = text[i]; var byteString = $"{Convert.ToUInt16(it):X2}";
                    var offsetRotation = random.RandBetween(30, 60);
                    chart.AddDecorationToChart(new AdfDecoration()  // main
                    {
                        Floor = floor,
                        RelativeTo = AdfMoveDecorationRelativeToType.Tile,
                        DecorationImage = $"quartrond_lyric_{byteString}.png",
                        Tag = $"quartrond-char-{floor}-{i}",
                        BlendMode = AdfBlendMode.Screen,
                        ImageSmoothing = false,
                        Locked = true,
                        Scale = new(0),
                        Rotation = offsetRotation,
                        Position = new((i * 0.8d) + textAlignmentOffset, 1.5d),
                        PivotOffset = new(0, 0.3d),
                        Opacity = 0,
                        Depth = 2,
                    });
                    chart.AddDecorationToChart(new AdfDecoration()  // shadow
                    {
                        Floor = floor,
                        RelativeTo = AdfMoveDecorationRelativeToType.Tile,
                        DecorationImage = $"quartrond_lyric_{byteString}.png",
                        Tag = $"quartrond-char-{floor}-{i}",
                        BlendMode = AdfBlendMode.Screen,
                        ImageSmoothing = false,
                        Locked = true,
                        Scale = new(0),
                        Rotation = offsetRotation,
                        Position = new((i * 0.8d) + textAlignmentOffset, 1.5d),
                        PivotOffset = new(0.05d, 0.25d),
                        Color = new("8F7921FF"),
                        Opacity = 0,
                        Depth = 3,
                    });

                    chart.ChartTiles[floor].TileEvents.Add(new AdfEventMoveDecorations()
                    {
                        Tag = $"quartrond-char-{floor}-{i}",
                        Opacity = 100,
                        RotationOffset = -offsetRotation,
                        Scale = new(55),
                        Duration = random.RandBetween(3, 4),
                        Ease = AdfEaseType.OutBack,
                        AngleOffset = 30 * i,
                    });
                    chart.ChartTiles[floor].TileEvents.Add(new AdfEventMoveDecorations()
                    {
                        Tag = $"quartrond-char-{floor}-{i}",
                        Opacity = 0,
                        RotationOffset = offsetRotation,
                        Scale = new(0),
                        Duration = random.RandBetween(3, 4) * (floor == 110 ? 0.6 : 1),
                        Ease = AdfEaseType.InBack,
                        AngleOffset = 900 + random.RandBetween(-90, 90),
                    });
                }
            }
        }









        #region GENERIC_EFFECT

        private static void ManyTenKeys(AdfChart chart)
        {
            Mat mat = Cv2.ImRead(chart.FileLocation?.Parent?.FullName + $"\\flashlight.jpg");
            double defaultCameraZoom = 500;

            double widthMultiplier = (double)ExtensionSharedConstants.CanvasWidth / mat.Width * defaultCameraZoom / 100d;
            double heightMultiplier = (double)ExtensionSharedConstants.CanvasHeight / mat.Height * defaultCameraZoom / 100d;

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

                double widthMultiplier = (double)ExtensionSharedConstants.CanvasWidth / mat.Width * defaultCameraZoom / 100d;
                double heightMultiplier = (double)ExtensionSharedConstants.CanvasHeight / mat.Height * defaultCameraZoom / 100d;

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
            for (int i = start; i < end; i++)
            {
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
        #endregion GENERIC_EFFECT

        private static AdfPosition PositionFromPolar(double radius, double angleDegrees) => new(radius * Math.Cos(angleDegrees / 180d * Math.PI), radius * Math.Sin(angleDegrees / 180d * Math.PI));














        private static void RenderCharacter(AdfChart chart, char ch)
        {
            var byteString = $"{Convert.ToUInt16(ch):X2}";

            if (!File.Exists(chart.FileLocation?.Parent?.FullName + $"\\quartrond_lyric_{byteString}.png"))
            {
                Image image = new Bitmap(256, 256);

                Font font = new("Microsoft Yahei UI", 120f, FontStyle.Bold);
                Graphics graphics = Graphics.FromImage(image);

                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                graphics.DrawString(ch.ToString(),
                                    font,
                                    new SolidBrush(Color.FromArgb(255, 255, 255, 255)),
                                    new PointF(256 / 2f, (256 - 210) / 2f),
                                    new StringFormat() { Alignment = StringAlignment.Center });


                image.Save(chart.FileLocation?.Parent?.FullName + $"\\quartrond_lyric_{byteString}.png");
            }
        }
    }
}
