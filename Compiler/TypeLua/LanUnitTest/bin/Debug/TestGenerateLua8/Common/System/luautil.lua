function istrue(expression)
	if expression then
		print("pass")
	else
		print("failed")
	end
end

function format(s,...)
	return string.format(s,...)
end
