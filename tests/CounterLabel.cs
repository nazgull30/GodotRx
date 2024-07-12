using Godot;
using System;
using GodotRx;

namespace Tests
{
  public partial class CounterLabel : Label
  {
    public override void _Ready()
    {
      var v = new Vector2i();
      var vx = ReactiveProperty.FromMember(v, v => v.x);
      var vy = ReactiveProperty.FromMember(v, v => v.y);
      var vs = ReactiveProperty.Computed(vx, vy, (x, y) => x + y);

      vs.Subscribe(x => Text = $"{x} ({v.x}, {v.y})")
        .DisposeWith(this);

      this.OnActionJustPressed("ui_up")
        .Subscribe(_ => vx.Value++)
        .DisposeWith(this);

      this.OnActionJustReleased("ui_up")
        .Subscribe(_ => vy.Value++)
        .DisposeWith(this);
    }

    public partial class Vector2i
    {
      public int x = 0;
      public int y = 0;
    }
  }
}