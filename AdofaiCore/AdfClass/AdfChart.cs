using MagicShaper.AdofaiCore.AdfEvents;
using MagicShaper.AdofaiCore.AdfEvents.Dlc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace MagicShaper.AdofaiCore.AdfClass
{
    internal class AdfChart
    {
        public AdfChart(List<AdfTile> chartTiles, JsonObject chartJson, List<AdfEventAddDecoration> decorations, List<AdfEventAddObject> objects)
        {
            ChartTiles = chartTiles;
			_chartJson = chartJson;
            Decorations = decorations;
            Objects = objects;
            FileLocation = new("C:\\");
        }

        public List<AdfTile> ChartTiles { get; set; }

        public List<AdfEventAddDecoration> Decorations { get; set; }
        public List<AdfEventAddObject> Objects { get; set; }

        private JsonObject _chartJson;

        public JsonObject ChartJson
		{
			get
			{
				_chartJson["actions"]!.AsArray().Clear();
				_chartJson["decorations"]!.AsArray().Clear();
                _chartJson["actions"] = JsonNode.Parse('[' + GetEvents() + ']',
										  null,
										  new System.Text.Json.JsonDocumentOptions() { AllowTrailingCommas = true })!.AsArray();
                _chartJson["decorations"] = JsonNode.Parse('[' + GetDecos() + GetObjects() + ']',
										  null,
										  new System.Text.Json.JsonDocumentOptions() { AllowTrailingCommas = true })!.AsArray();
				return _chartJson;
			}
            init 
            {
                _chartJson = value;
			}
		}

		internal static AdfChart ParseJsonToChart(JsonDocument chartJson)
        {
            List<AdfTile> tiles = new();
            foreach (var o in chartJson.RootElement.GetProperty("angleData").EnumerateArray())
            {
                double angle = o.GetDouble();

                if (tiles.Count == 0)
                {
                    tiles.Add(new(angle, null, new()));
                }
                else
                {
                    tiles.Add(new(angle, tiles[^1], new()));
                }
            }
            // This is to make sure that final tile can have events.
            // When saving, this tile is ignored.
            tiles.Add(new(0d, tiles[^1], new()));

            foreach (var action in chartJson.RootElement.GetProperty("actions").EnumerateArray())
            {

                var tileAction = ParseAction(action);
                if (tileAction is not null)
                {
                    tiles[action.GetProperty("floor").GetInt32()].TileEvents.Add(tileAction);
                }
                else
                {
                    Console.WriteLine($"Potential Error! Cannot parse action: {action}");
                }
            }

			List<AdfEventAddDecoration> decorations = new();
			List<AdfEventAddObject> objects = new();
			foreach (var action in chartJson.RootElement.GetProperty("decorations").EnumerateArray())
			{

				var decoAction = ParseAction(action);
				if (decoAction is not null && decoAction is AdfEventAddDecoration ed)
				{
					decorations.Add(ed);
				}
                else if (decoAction is not null && decoAction is AdfEventAddObject eo)
                {
                    objects.Add(eo);
                }
			}

			return new AdfChart(
                tiles, 
                JsonNode.Parse(chartJson.RootElement.ToString(), null, new() { AllowTrailingCommas = true })!.AsObject(),
                decorations, objects);

        }

        /// <summary>
        /// DEPRECATED. Use <see cref="Parse(string)"/>.
        /// </summary>
        /// <param name="chartText"></param>
        /// <returns></returns>
        [Obsolete]
        internal static AdfChart ParseChart(string chartText) => ParseJsonToChart(JsonDocument.Parse(chartText, new() { AllowTrailingCommas = true }));

        internal static AdfChart Parse(string fileLocation)
        {
            var c = ParseJsonToChart(JsonDocument.Parse(File.ReadAllText(fileLocation), new() { AllowTrailingCommas = true }));
            c.FileLocation = new(fileLocation);
            return c;
        }
        
        
        internal string GetEvents()
        {
            StringBuilder sb = new();

            foreach (var tile in ChartTiles)
            {
                foreach (var action in tile.TileEvents)
                {
                    sb.AppendLine(action.JsonString(tile.TileIndex) + ',');

                }
            }
            return sb.ToString().Replace("True", "true").Replace("False", "false");
        }
        
        internal string GetDecos()
        {
            StringBuilder sb = new();
			foreach (var deco in Decorations)
            {
				sb.AppendLine(deco.JsonString(deco.Floor) + ',');
			}
            return sb.ToString().Replace("True", "true").Replace("False", "false");
        }

		internal string GetObjects()
		{
			StringBuilder sb = new();
			foreach (var obj in Objects)
			{
				sb.AppendLine(obj.JsonString(obj.Floor) + ',');
			}
			return sb.ToString().Replace("True", "true").Replace("False", "false");
		}

		public static JsonSerializerOptions GetJsonOptions()
        {
			JsonSerializerOptions option = new()
			{
				AllowTrailingCommas = true,
				PropertyNameCaseInsensitive = true,

			};
			option.Converters.Add(new AdfNullableConverter<AdfTrackAnimationType>());
			option.Converters.Add(new AdfNullableConverter<AdfTrackDisappearAnimationType>());
			option.Converters.Add(new AdfConverter<AdfGameSoundType>());
			option.Converters.Add(new AdfConverter<AdfHitsoundType>());
			option.Converters.Add(new AdfConverter<AdfFilter>());
			option.Converters.Add(new AdfConverter<AdfEaseType>());
			option.Converters.Add(new AdfConverter<AdfEasePartBehaviorType>());
			option.Converters.Add(new AdfConverter<AdfRepeatEventType>());
			option.Converters.Add(new AdfConverter<AdfTrackColorType>());
			option.Converters.Add(new AdfConverter<AdfTrackColorPulseType>());
			option.Converters.Add(new AdfConverter<AdfTrackStyle>());
			option.Converters.Add(new AdfConverter<AdfTargetPlanetType>());
			option.Converters.Add(new AdfConverter<AdfEventSpeedType>());
			option.Converters.Add(new AdfConverter<AdfBackgroundDisplayMode>());
			option.Converters.Add(new AdfConverter<AdfFlashPlaneType>());
			option.Converters.Add(new AdfConverter<AdfMaskingType>());
			option.Converters.Add(new AdfNullableConverter<AdfCameraRelativeToType>());
			option.Converters.Add(new AdfConverter<AdfMoveDecorationRelativeToType>());
			option.Converters.Add(new AdfConverter<AdfBlendMode>());
			option.Converters.Add(new AdfConverter<AdfHitbox>());
			option.Converters.Add(new AdfConverter<AdfHitboxType>());
			option.Converters.Add(new AdfConverter<AdfObjectType>());
			option.Converters.Add(new AdfConverter<AdfTrackType>());
			option.Converters.Add(new AdfConverter<AdfPlanetColorType>());
			option.Converters.Add(new AdfConverter<AdfTrackIcon>());
            option.Converters.Add(new AdfConverter<AdfAngleCorrectionDirection>());

            option.Converters.Add(new AdfConverter<AdfHoldMidSoundTimingRelativeToType>());
            option.Converters.Add(new AdfConverter<AdfHoldMidSoundType>());
            option.Converters.Add(new AdfConverter<AdfHoldSoundType>());
            option.Converters.Add(new AdfConverter<AdfPlanetsType>());

			option.Converters.Add(new AdfPosition.AdfPositionConverter());
			option.Converters.Add(new AdfTileReference.AdfTileReferenceConverter());
			option.Converters.Add(new AdfColor.AdfColorConverter());
			option.Converters.Add(new AdfScale.AdfScaleConverter());

            return option;
		}

        public static IAdfEvent? ParseAction(JsonElement action)
        {
            var option = GetJsonOptions();

            List<IAdfEvent> events = new()
            {
                new AdfEventAddDecoration(),
                new AdfEventAddObject(),

                new AdfEventAnimateTrack(),
                new AdfEventAutoPlayTiles(),
                new AdfEventBloom(),
                new AdfEventCheckpoint(),
                new AdfEventColorTrack(),
                new AdfEventCustomBackground(),
                new AdfEventFlash(),
                new AdfEventHallOfMirrors(),
                new AdfEventMoveCamera(),
                new AdfEventMoveDecorations(),
                new AdfEventMoveTrack(),
                new AdfEventPause(),
                new AdfEventPlaySound(),
                new AdfEventPositionTrack(),
                new AdfEventRecolorTrack(),
                new AdfEventRepeatEvents(),
                new AdfEventScalePlanets(),
                new AdfEventScreenScroll(),
                new AdfEventScreenTile(),
                new AdfEventSetConditionalEvents(),
                new AdfEventSetFilter(),
                new AdfEventSetFrameRate(),
                new AdfEventSetHitsound(),
                new AdfEventSetPlanetRotation(),
                new AdfEventSetSpeed(),
                new AdfEventSetText(),
                new AdfEventShakeScreen(),
                new AdfEventTwirl(),
                new AdfEventEditorComment(),

                new AdfEventHide(),
                new AdfEventHold(),
                new AdfEventScaleRadius(),
                new AdfEventScaleMargin(),
                new AdfEventSetHoldSound(),
                new AdfEventMultiPlanet(),
            };

			foreach (var e in events)
            {
                if (action.GetProperty("eventType").GetString() == e.EventType)
                {
                    return (IAdfEvent?)action.Deserialize(e.GetType(), option);
				}
            }

            return null;


			
        }

        /// <summary>
        /// This Property is used for OpenCV to location images.
        /// </summary>
        public DirectoryInfo FileLocation;
    }
}
