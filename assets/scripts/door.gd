extends Spatial

export var open_key = ["1"]
export var close_key = ["0"]

export var on_out = ""

var inputs = []

func peripheral_initialization(id, f):
	print("[Door] Received id: " + id)
	
	if on_out != "":
		f.call_func(id, on_out + "\n")

func peripheral_receive(data):
	var data_char = char(int(data))
	print("[Door] Received ASCII value " + data + " parsing as " + data_char)
	
	if data_char == "\n":
		if inputs == close_key:
			print("Door closing")
			$AnimationPlayer.SetAnimation("closing")
		
		if inputs == open_key:
			print("Door opening")
			$AnimationPlayer.SetAnimation("opening")
	
		inputs = []
	else:
		inputs.append(data_char)
