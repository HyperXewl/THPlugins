using Turbo.Plugins.Default;
namespace Turbo.Plugins.HandH
{

    public class HandHBuffListPlugin : BasePlugin, IInGameTopPainter
    {

        public BuffPainter BuffPainter { get; set; }
        public BuffRuleCalculator RuleCalculator { get; private set; }
        public float PositionOffset { get; set; }

        public HandHBuffListPlugin()
        {
            Enabled = true;
            PositionOffset = 0.04f;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            BuffPainter = new BuffPainter(Hud, true)
            {
                Opacity = 0.75f,
                ShowTimeLeftNumbers = false,
                ShowTooltips = false,
                TimeLeftFont = Hud.Render.CreateFont("tahoma", 7, 255, 255, 255, 255, false, false, 255, 0, 0, 0, true),
                StackFont = Hud.Render.CreateFont("tahoma", 6, 255, 255, 255, 255, false, false, 255, 0, 0, 0, true),
            };

            RuleCalculator = new BuffRuleCalculator(Hud);
            RuleCalculator.SizeMultiplier = 0.75f;
			
			BuffPainter.ShowTimeLeftNumbers = true;
			
			// All procs
            RuleCalculator.Rules.Add(new BuffRule(156484) { IconIndex = 1, MinimumIconCount = 1, ShowTimeLeft = true, IconSizeMultiplier = 2.0f, }); // Near Death Experience
            RuleCalculator.Rules.Add(new BuffRule(208474) { IconIndex = 1, MinimumIconCount = 1, ShowTimeLeft = true, IconSizeMultiplier = 2.0f, }); // Unstable Anomaly
            RuleCalculator.Rules.Add(new BuffRule(359580) { IconIndex = 1, MinimumIconCount = 1, ShowTimeLeft = true, IconSizeMultiplier = 2.0f, }); // Firebird's Finery
            RuleCalculator.Rules.Add(new BuffRule(324770) { IconIndex = 1, MinimumIconCount = 1, ShowTimeLeft = true, IconSizeMultiplier = 2.0f, }); // Awareness
            RuleCalculator.Rules.Add(new BuffRule(218501) { IconIndex = 1, MinimumIconCount = 1, ShowTimeLeft = true, IconSizeMultiplier = 2.0f, }); // Spirit Vessel
            RuleCalculator.Rules.Add(new BuffRule(309830) { IconIndex = 1, MinimumIconCount = 1, ShowTimeLeft = true, IconSizeMultiplier = 2.0f, }); // Indestructible
            RuleCalculator.Rules.Add(new BuffRule(217819) { IconIndex = 1, MinimumIconCount = 1, ShowTimeLeft = true, IconSizeMultiplier = 2.0f, }); // Nerves of Steel
            RuleCalculator.Rules.Add(new BuffRule(465952) { IconIndex = 1, MinimumIconCount = 1, ShowTimeLeft = true, IconSizeMultiplier = 2.0f, }); // Final Service 

			// 465350 - Simulacrum
			RuleCalculator.Rules.Add(new BuffRule(465350)
			{
				IconIndex = 1,
				MinimumIconCount = 0,
				ShowTimeLeft = true,
				IconSizeMultiplier = 1.2f,
			});
						
			// 453801 - command skel
			RuleCalculator.Rules.Add(new BuffRule(453801)
			{
				IconIndex = 1,
				MinimumIconCount = 0,
				IconSizeMultiplier = 1.2f,
			});
			
			// 465839 - Land of the Dead
			RuleCalculator.Rules.Add(new BuffRule(465839)
			{
				IconIndex = 1,
				MinimumIconCount = 0,
				IconSizeMultiplier = 1.2f,
			});

			//x position relative to the center of screen, buffbar centered around this point. 
			var y = Hud.Window.Size.Height * 0.5f;  //center of screen 
			var x = Hud.Window.Size.Width * 0.0f;   //center of screen 
			BuffPainter.PaintHorizontalCenter(RuleCalculator.PaintInfoList, x, y, Hud.Window.Size.Width, RuleCalculator.StandardIconSize, RuleCalculator.StandardIconSpacing);  
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (Hud.Render.UiHidden) return;
            if (clipState != ClipState.BeforeClip) return;

            RuleCalculator.CalculatePaintInfo(Hud.Game.Me);
            if (RuleCalculator.PaintInfoList.Count == 0) return;

            var y = Hud.Window.Size.Height * 0.5f + Hud.Window.Size.Height * PositionOffset;
            BuffPainter.PaintHorizontalCenter(RuleCalculator.PaintInfoList, 0, y, Hud.Window.Size.Width, RuleCalculator.StandardIconSize, RuleCalculator.StandardIconSpacing);
        }

    }

}