extends Spatial

var id = ""
var per_send = null

# Initialization methods
func _ready():
	$BulletArea.connect("body_entered", self, "body_entered_vision")

func peripheral_initialization(peripheral_id, sending_function):
	id = peripheral_id
	per_send = sending_function


# Turret stuff

func shoot(body):
	var bodies = $BulletArea.get_overlapping_bodies()
	
	$ShootySound.play()
	
	for body in bodies:
		if body.has_method("damage"):
			body.damage()


# Peripheral Stuff

func body_entered_vision(body):
	per_send.call_func(id, "1")

func peripheral_receive(data):
	shoot(data)
