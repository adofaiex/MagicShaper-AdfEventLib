using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicShaper.AdofaiCore.AdfClass;

namespace MagicShaper.AdofaiCore.AdfEvents.Dlc
{
    internal class AdfEventMultiPlanet : AdfEventBase
    {
		public override string EventType => "MultiPlanet";

        public AdfPlanetsType Planets { get; set; } = AdfPlanetsType.TwoPlanets;
    }
}
