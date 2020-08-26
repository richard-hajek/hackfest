extends Spatial

func _ready():
	$AnimationTree["parameters/playback"].start("idle_bottom")

func peripheral_initialization(id, function):
	print("[Platform] Received id: " + id)

func peripheral_receive(data):
	print("[Platform] Received " + data)
	
	var state_machine = $AnimationTree["parameters/playback"]

	if data == "49":
		state_machine.travel("idle_top")
	
	if data == "48":
		state_machine.travel("idle_bottom")
