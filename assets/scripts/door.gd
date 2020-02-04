extends Spatial

export var open_key = ["1"]
export var close_key = ["0"]

var inputs = []

func peripheral_initialization(id, function):
	print("[Door] Received id: " + id)

func peripheral_receive(data):
	var data_char = char(int(data))
	print("[Door] Received ASCII value " + data + " parsing as " + data_char)
	
	inputs.append(data_char)
	
	if inputs == close_key:
		print("Door closing")
		$AnimationPlayer.SetAnimation("closing")
	
	if inputs == open_key:
		print("Door opening")
		$AnimationPlayer.SetAnimation("opening")
	
	if data_char == "\n":
		inputs = []
