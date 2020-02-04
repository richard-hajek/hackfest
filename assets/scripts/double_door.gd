extends Spatial

func peripheral_initialization(id, function):
	print("[Double Door] Received id: " + id)
	$door.peripheral_receive("49");
	$door.peripheral_receive("10");

func peripheral_receive(data):
	var data_char = char(int(data))
	print("[Double Door] Received ASCII value " + data + " parsing as " + data_char)
	
	if data_char == "0":
		$door.peripheral_receive("49");
		$door.peripheral_receive("10");
		$door2.peripheral_receive("48");
		$door2.peripheral_receive("10");
	
	if data_char == "1":
		$door.peripheral_receive("48");
		$door.peripheral_receive("10");
		$door2.peripheral_receive("49");
		$door2.peripheral_receive("10");
