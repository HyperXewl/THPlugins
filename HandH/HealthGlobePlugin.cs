using System.Linq;
using Turbo.Plugins.Default;
using System.Collections.Generic;

namespace Turbo.Plugins.HandH
{
    public class HealthGlobePlugin : BasePlugin, IInGameWorldPainter
	{
        public WorldDecoratorCollection PowerGlobeDecorator { get; set; }
        public WorldDecoratorCollection RiftOrbDecorator { get; set; }
        public WorldDecoratorCollection HealthGlobeDecorator { get; set; }

        public HealthGlobePlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);
            HealthGlobeDecorator = new WorldDecoratorCollection(
                new MapShapeDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 255, 0, 0, 0),
                    ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                    Radius = 4.0f,
                    ShapePainter = new CircleShapePainter(Hud),
                }
                //new GroundLabelDecorator(Hud)
                //{
                //    BackgroundBrush = Hud.Render.CreateBrush(255, 255, 0, 0, 0),
                //    TextFont = Hud.Render.CreateFont("tahoma", 6.5f, 255, 0, 0, 0, false, false, false),
                //}
                );
        }

        public void PaintWorld(WorldLayer layer)
        {
            var actors = Hud.Game.Actors.Where(x => x.SnoActor.Kind == ActorKind.HealthGlobe);
            foreach (var actor in actors)
            {
                HealthGlobeDecorator.ToggleDecorators<GroundLabelDecorator>(!actor.IsOnScreen); // do not display ground labels when the actor is on the screen
                HealthGlobeDecorator.Paint(layer, actor, actor.FloorCoordinate, "health globe");
            }
        }
    }
}