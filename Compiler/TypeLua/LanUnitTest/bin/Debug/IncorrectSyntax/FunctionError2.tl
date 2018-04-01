package test;

import System;
import Common;

public class ClassA

public function<void:void> f1;

public function<void:number> f2;

public function<string:number> f3;

public function<string,Land:number,Farmer> f4;


public void Func1()
	f1 = f2;
end

public void Func2()
	
end

public void Func3(number n)
	
end

public string Func4(number n)
	
end

public string,Land Func5(number n,Farmer f)
	
end

public string Func6(Farmer f,function<boolean:Farmer> callback)
	if callback(f) then
		print("pass");
	end
end