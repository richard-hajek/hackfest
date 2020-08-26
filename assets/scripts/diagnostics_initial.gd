extends Node

func diagnostics_vagrant():
	
	var output = []
	
	OS.execute('vagrant', ['-v'], true, output)
	
	if output.size() >= 1 and output[0].begins_with("Vagrant "):
		print("[DIAGNOSTICS A] [OK] Vagrant Executable Found")
	else:
		printerr("[DIAGNOSTICS A] [ERROR] Vagrant Not Found!")

func diagnostics_virtualbox():
	
	var output = []
	
	OS.execute('VBoxHeadless', ['--version'], true, output)
	
	if output.size() >= 1 and output[0].begins_with("Oracle VM VirtualBox Headless Interface "):
		print("[DIAGNOSTICS A] [OK] VirtualBox Executable Found")
	else:
		printerr("[DIAGNOSTICS A] [ERROR] VirtualBox Not Found!")

func diagnostics_mono():
	
	var output = []
	
	OS.execute('mono', ['--version'], true, output)
	
	if output.size() >= 1 and output[0].begins_with("Mono JIT compiler version "):
		print("[DIAGNOSTICS A] [OK] C# Mono Found")
	else:
		printerr("[DIAGNOSTICS A] [ERROR] C# Not Found!")

func diagnostics_ssh():
	
	var output = []
	
	OS.execute('ssh', [], true, output)
	
	print(output[1])
	
	if output.size() >= 1 and output[0].begins_with("usage: ssh "):
		print("[DIAGNOSTICS A] [OK] SSH Found")
	else:
		printerr("[DIAGNOSTICS A] [ERROR] SSH Not Found!")

func _ready():
	if (ProjectSettings.has_setting("global/Diagnostics") and ProjectSettings.get_setting("global/Diagnostics")) == true:
		diagnostics_vagrant()
		diagnostics_virtualbox()
		diagnostics_mono()
		#diagnostics_ssh() #SSH doesn't seem to have --version or --help or anything when called >:(
