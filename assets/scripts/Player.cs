using Godot;

public class Player : KinematicBody
{
	private Camera _camera;
	private Vector3 _dir;
	private Vector3 _direction;
	private SpotLight _flashlight;

	private HotCode _hotcode1Node;

	private bool _isSprinting;
	private RayCast _rayCast;
	private Spatial _rotationHelper;
	private int _speed = 200;
	private TerminalControl _terminalControl;
	
	private Vector3 _spawn;
	
	private Vector3 _vel;

	private Vector3 _velocity;

	[Export] public float Accel = 4.5f;

	[Export] public float Deaccel = 16.0f;

	[Export] public float Gravity = -24.8f;

	[Export] public float JumpSpeed = 18.0f;

	[Export] public float MaxSlopeAngle = 45.0f;

	[Export] public float MaxSpeed = 15.0f;

	[Export] public float MaxSprintSpeed = 30.0f;

	[Export] public float MouseSensitivity = 0.20f;

	[Export] public float SprintAccel = 18.0f;

	public override void _Ready()
	{
		_camera = GetNode<Camera>("RotationHelper/Camera");
		_rotationHelper = GetNode<Spatial>("RotationHelper");
		_flashlight = GetNode<SpotLight>("RotationHelper/Camera/Flashlight");
		_rayCast = GetNode<RayCast>("RotationHelper/Camera/RayCast");
		_terminalControl = GetNode<TerminalControl>("GUI/TerminalWrapper/Terminal");

		_hotcode1Node = GetNode<HotCode>("GUI/HotCode1");

		Input.SetMouseMode(Input.MouseMode.Captured);
		
		_spawn = GlobalTransform.origin;

		var gui = GetNode<Control>("GUI").Call("point_to", "Spatial/Room1/Docker Container/key_reader/Device");
	}

	public override void _PhysicsProcess(float delta)
	{
		ProcessInput(delta);

		if (Input.GetMouseMode() == Input.MouseMode.Captured)
			ProcessMovement(delta);
	}

	private void ProcessInput(float delta)
	{
		//  Walking
		var camGlobalTransform = _camera.GetGlobalTransform();
		var inputMovementVector = new Vector3();

		if (Input.IsActionPressed("char_forward"))
			inputMovementVector.z += 1;
		if (Input.IsActionPressed("char_backward"))
			inputMovementVector.z -= 1;
		if (Input.IsActionPressed("char_left"))
			inputMovementVector.x -= 1;
		if (Input.IsActionPressed("char_right"))
			inputMovementVector.x += 1;

		inputMovementVector = inputMovementVector.Normalized();

		// Basis vectors are already normalized.
		_dir = -camGlobalTransform.basis.z * inputMovementVector.z + camGlobalTransform.basis.x * inputMovementVector.x;

		//  Jumping
		if (IsOnFloor())
			if (Input.IsActionJustPressed("char_jump"))
				_vel.y = JumpSpeed;

		if (Input.IsActionPressed("char_sprint"))
			_isSprinting = true;
		else
			_isSprinting = false;

		//  Turning the flashlight on/off
		if (Input.IsActionJustPressed("char_flashlight"))
		{
			if (_flashlight.IsVisibleInTree())
				_flashlight.Hide();
			else
				_flashlight.Show();
		}

		if (Input.IsActionJustPressed("action_1"))
		{
			var lookingAtObject = _rayCast.GetCollider();

			if (lookingAtObject is Node node && node.GetParent() is ContainerNode container)
				container.HotCode(_hotcode1Node.Command);
		}

		//  Capturing/Freeing the cursor
		if (Input.IsActionJustPressed("mouse_grab"))
			Input.SetMouseMode(Input.GetMouseMode() == Input.MouseMode.Visible
				? Input.MouseMode.Captured
				: Input.MouseMode.Visible);
	}

	private void ProcessMovement(float delta)
	{
		_dir.y = 0;
		_dir = _dir.Normalized();

		_vel.y += delta * Gravity;

		var hvel = _vel;
		hvel.y = 0;

		var target = _dir;

		if (_isSprinting)
			target *= MaxSprintSpeed;
		else
			target *= MaxSpeed;

		float accel;
		if (_dir.Dot(hvel) > 0)
			accel = _isSprinting ? SprintAccel : Accel;
		else
			accel = Deaccel;

		hvel = hvel.LinearInterpolate(target, accel * delta);
		_vel.x = hvel.x;
		_vel.z = hvel.z;
		_vel = MoveAndSlide(_vel, new Vector3(0, 1, 0), false, 4, Mathf.Deg2Rad(MaxSlopeAngle));
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseEvent && Input.GetMouseMode() == Input.MouseMode.Captured)
		{
			RotateY(Mathf.Deg2Rad(-mouseEvent.Relative.x * MouseSensitivity));

			_rotationHelper.RotateX(Mathf.Deg2Rad(-mouseEvent.Relative.y * MouseSensitivity));
			var cameraRot = _rotationHelper.RotationDegrees;
			cameraRot.x = Mathf.Clamp(cameraRot.x, -70, 70);
			_rotationHelper.RotationDegrees = cameraRot;
		}

		if (@event is InputEventMouseButton mouseButton && mouseButton.Pressed &&
			Input.GetMouseMode() == Input.MouseMode.Captured)
		{
			var lookingAtObject = _rayCast.GetCollider();

			if (lookingAtObject != null)
			{
				var parent = ((Node) lookingAtObject).GetParent();

				if (parent is ContainerNode c)
				{
					Input.SetMouseMode(Input.MouseMode.Visible);
					_terminalControl.Open(c);
				}
				else if (parent.GetParent() is ContainerNode c2)
				{
					Input.SetMouseMode(Input.MouseMode.Visible);
					_terminalControl.Open(c2);
				}
			}
		}
	}
	
	public void damage()
	{
		var newTransform = GlobalTransform;
		newTransform.origin = _spawn;
		GlobalTransform = newTransform;
	}
}
