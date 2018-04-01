package test;

import Common.Fruits;
import System;

public class ClassA extends Apple

private string p;

public ClassA()

end


public void TestClass()
	boolean check = true;
	ClassA a1 = new ClassA();
	a1.IsRed = true;
	check = check and a1.GetColor() == "black-red";

	ClassA a2 = new ClassA();
	a2.IsRed = false;
	check = check and a2.GetColor() == "black-green";

	IsRed = true;
	check = check and GetColor() == "black-red";
	check = check and this.GetColor() == "black-red";
	check = check and super.GetColor() == "red";

	IsRed = false;
	check = check and GetColor() == "black-green";
	check = check and this.GetColor() == "black-green";
	check = check and super.GetColor() == "green";

	Preserve("this");
	check = check and GetPreserve() == "non";

	this.Preserve("this");
	check = check and GetPreserve() == "non";

	super.Preserve("this");
	check = check and super.GetPreserve() == "this";

	istrue(check);
end

public string GetColor()
	if IsRed then
		return "black-red";
	end
	return "black-green";
end

protected string GetPreserve()
	return "non";
end