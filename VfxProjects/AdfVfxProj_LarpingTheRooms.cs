using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicShaper.AdofaiCore.AdfClass;

namespace MagicShaper.VfxProjects
{
    public class AdfVfxProj_LarpingTheRooms
    {
        public static void ProjMain()
        {
			AdfChart chart = AdfChart.Parse(@"G:\Adofai levels\larping\level-base.adofai");

			File.WriteAllText(@"G:\Adofai levels\larping\level-effect.adofai", chart.ChartJson.ToString());
        }
    }
}
