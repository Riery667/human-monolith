using Godot;

public partial class Player : CharacterBody3D
{

    [ExportGroup("Camera")]
    [Export(PropertyHint.Range, "0.0, 0.01")]
    public float mouse_sensitivity { get; set; } = 0.003f;
    [Export(PropertyHint.Range, "0.0, 10.0")]
    public float gamepad_camera_sensitivity { get; set; } = 3.0f;



    public override void _Ready()
    {
        _camera_pivot = GetNode<Node3D>("%CameraPivot");
        _camera = GetNode<Camera3D>("%Camera3D");
    }

    public override void _Process(double delta)
    {
        var delta_seconds = (float)delta;
        UpdateCameraRotation(delta_seconds);
    }
}
