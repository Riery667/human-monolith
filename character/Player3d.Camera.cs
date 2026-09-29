using Godot;

public partial class Player
{

    private static readonly StringName ActionCaptureMouse = "left_click";
    private static readonly StringName ActionReleaseMouse = "ui_cancel";
    private static readonly StringName ActionLookLeft = "look_left";
    private static readonly StringName ActionLookRight = "look_right";
    private static readonly StringName ActionLookUp = "look_up";
    private static readonly StringName ActionLookDown = "look_down";


    private const float CameraPitchMin = -Mathf.Pi / 3.0f;
    private const float CameraPitchMax = Mathf.Pi / 6.0f;

    private Node3D _camera_pivot = null!;
    private Camera3D _camera = null!;
    private Vector2 _camera_input_direction = Vector2.Zero;

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("left_click"))
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
        }
        else if (@event.IsActionPressed("ui_cancel"))
        {
            Input.MouseMode = Input.MouseModeEnum.Visible;

        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {

        bool is_camera_motion = @event is InputEventMouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured;
        if (is_camera_motion)
        {
            InputEventMouseMotion _mouse_motion = (InputEventMouseMotion)@event;
            _camera_input_direction = _mouse_motion.ScreenRelative * mouse_sensitivity;

            // GD.Print("X " + _mouse_motion.ScreenRelative.X);
            // GD.Print("Y " + _mouse_motion.ScreenRelative.Y);

        }
    }

    private void UpdateCameraRotation(float delta)
    {
        var combined = _camera_input_direction;

        var invert_x = -1.0f;
        var invert_y = 1.0f;

        var camera_rotation = _camera_pivot.Rotation;
        camera_rotation.X = Mathf.Clamp(camera_rotation.X - combined.Y * invert_y, CameraPitchMin, CameraPitchMax);
        camera_rotation.Y += combined.X * invert_x;
        camera_rotation.Z = 0.0f;

        _camera_pivot.Rotation = camera_rotation;
        _camera_input_direction = Vector2.Zero;
    }
}
