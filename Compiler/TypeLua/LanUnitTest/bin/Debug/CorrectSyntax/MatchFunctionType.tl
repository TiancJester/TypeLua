package test;

import Common;

public class ClassA extends Botany

public string Func1()
	return "";
end

public string Func2()
	return this.Name;
end

public string Func3()
	return this.GetColor();
end

public string,number Func4()
	Func6("fmr",this.Owner);
	return this.GetColor(),Func5("red");
end

public number Func5(string c)
	if c == this.GetColor() then
		return 100;
	end
	return 0;
end

public void Func6(string c,Farmer f)
	f.Name = c;
end

public string GetColor()
	return "red";
end

public void TestClass()
	Owner = new Farmer();
	Name = "a";
	string s;
	number n;
	s,n = Func4();
	Farmer f = Owner;
	Func6(s,f);
	istrue(Func1()..Func2()..Func3()..s..n..f.Name == "aredred100red");
end