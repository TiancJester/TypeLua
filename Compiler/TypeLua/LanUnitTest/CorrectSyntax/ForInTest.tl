package test;

import System;
import Common;

public class ClassA

private number total = 0;

public void TestClass()
	ForIn1();
	ForIn2();
	ForIn3();
	ForIn4();

	istrue(total == 8);
end

public void ForIn1()
	table t = {a = 1,b = 2};
	for	k,v in t do
		total = total + (v as number);
	end
end

public void ForIn2()
	any t = {a = 1,b = 1};
	for	k,v in t do
		total = total + (v as number);
	end
end

public void ForIn3()
	HashTable<string,Farmer> t = {};
	t["a"] = new Farmer();
	t["b"] = new Farmer();
	for	k,v in t do
		total = total + 1;
		string farmerName = v.Name;
		v.Name = k;
	end
end

public void ForIn4()
	ListTable<Farmer> t = {};
	t[1] = new Farmer();
	t[1] = new Farmer();
	for	i,v in t do
		total = total + 1;
		string farmerName = v.Name;
		v.Name = tostring(i*2);
	end
end