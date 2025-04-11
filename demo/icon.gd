extends Sprite2D
func _ready() -> void:
	LimboConsole.register_command(multiply, "m1")
	LimboConsole.register_command(
		func multiply(a: float, b: float) -> void:
			LimboConsole.info("a * b: " + str(a * b)),
		"m2")
	LimboConsole.register_command(
		func(a: float, b: float) -> void:
			LimboConsole.info("a * b: " + str(a * b)),
		"m3")

func multiply(a: float, b: float) -> void:
	LimboConsole.info("a * b: " + str(a * b))
