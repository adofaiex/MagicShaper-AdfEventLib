using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicShaper.AdofaiCore.AdfClass;

namespace MagicShaper.AdofaiCore.AdfEvents
{
    internal class AdfEventColorTrack : AdfEventBase
    {

        public override string EventType => "ColorTrack";


		public AdfTrackColorType TrackColorType { get; set; } = AdfTrackColorType.Single;


		public AdfColor TrackColor { get; set; } = new("debb7b");

		public AdfColor SecondaryTrackColor { get; set; } = new("ffffff");


		public double TrackColorAnimDuration { get; set; } = 2d;

		public AdfTrackColorPulseType TrackColorPulse { get; set; } = AdfTrackColorPulseType.None;

		public int TrackPulseLength { get; set; } = 10;

		public AdfTrackStyle TrackStyle { get; set; } = AdfTrackStyle.Standard;

		public double? TrackGlowIntensity { get; set; } = null;


		public string TrackTexture { get; set; } = "";

		public double TrackTextureScale { get; set; } = 1d;

		public bool JustThisTile { get; set; } = false;


    }
}
