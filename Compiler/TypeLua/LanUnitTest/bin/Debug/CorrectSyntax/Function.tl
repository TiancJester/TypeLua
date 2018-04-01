package test;

import System;
import Common;

public class ClassA

public function<string:void> f1;

public function<string:number> f2;

public function<string:number> f3;

public function<string,Land:number,Farmer> f4;


public void TestClass()
	string check = "";

	f1 = Func2;
	f1();
	Func2();
	check = check..this.Func2();

	function<string:void> f1x = Func2;
	check = check..f1x();

	f2 = Func3;
	check = check..f2(123);

	f3 = Func4;
	string ss = f3(321);
	check = check..ss;

	f4 = Func5;
	Land land;
	ss, land = f4(123,new Farmer());
	land.Plant(nil);

	f3 = (number x)
		return "";
	end;

	Farmer farmer = new Farmer();
	check = check..Func6(farmer,(Farmer fm)
		function<boolean:void> condition = ()
			if Func4(1) == "1" then
				return true;
			end
			return false;
		end;
		return condition();
	end);

	istrue(check == "f2f2123321pass");
end

public string Func2()
	return "f2";
end

public string Func3(number n)
	return tostring(n);
end

public string Func4(number n)
	return tostring(n);
end

public string,Land Func5(number n,Farmer f)
	return tostring(n),new Land("aland");
end

public string Func6(Farmer f,function<boolean:Farmer> callback)
	if callback(f) then
		return "pass";
	end
end