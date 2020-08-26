extends Control

var center = null;

func _ready():
	$hint_arrow.visible = false
	$hint_rect.visible = false

func point_to(mesh_path):
	print("[GUI] [HINTS] Preparing pointer")
	var mdt = MeshDataTool.new() 
	var nd = get_tree().get_root().get_node(mesh_path)
	var m = nd.get_mesh()
	
	mdt.create_from_surface(m, 0)
	
	var v_min = null;
	var v_max = null;
	
	for vtx in range(mdt.get_vertex_count()):
		var vert=mdt.get_vertex(vtx)
		var global = nd.global_transform.xform(vert);
		
		if (v_min == null):
			v_min = global
			v_max = global
		
		if v_min.x > global.x:
			v_min.x = global.x
			
		if v_min.y > global.y:
			v_min.y = global.y
			
		if v_min.z > global.z:
			v_min.z = global.z
		
		if v_max.x < global.x:
			v_max.x = global.x
			
		if v_max.y < global.y:
			v_max.y = global.y
			
		if v_max.z < global.z:
			v_max.z = global.z
		
		#print("global vertex: "+str(global))
	
	center = (v_min + v_max) / 2
	print("min vertex: "+str(v_min))
	print("max vertex: "+str(v_max))
	print("center: "+str(center))

func _process(delta):
	
	if (center == null):
		return
	
	var screen_pos = $"../RotationHelper/Camera".unproject_position(center)
	#print("onscreen: "+str(screen_pos))
	
	screen_pos -= $hint_rect.get_combined_minimum_size() / 2
	
	$hint_rect.visible = true
	$hint_rect.margin_left = screen_pos.x
	$hint_rect.margin_top = screen_pos.y
