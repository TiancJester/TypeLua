package test;

import System;
import Common;

public class ClassA

public string s = "aaaa";

public string s2 = nil;

public any a = "bbb";

public number n = 123;

public number n2 = 123.456;

public Farmer fa = new Farmer();

public Farmer fa2 = nil;

public HashTable<number,Farmer> ht = {};

public void TestClass()
	Func_any();
	Func_as();
	Func0();
	istrue(any.may_be_a_global_variable == "ss330");
end

public string Func_any()
	
	any anyObj1 = s;
	any anyObj2;
	anyObj1,anyObj2 = s,s2;
	any anyObj3;
	anyObj1 = n;
	anyObj1 = fa;
	anyObj1 = fa2;
	anyObj1 = ht;
	anyObj1 = nil;
	anyObj1 = Func1();
	anyObj1,anyObj2 = Func2();
	anyObj1,anyObj2,anyObj3 = Func3();

	any.may_be_a_global_variable = any.format("%s%s%s",tostring(anyObj1),tostring(anyObj2),anyObj3.Name);
end

public string Func_as()
	string str1 = s;
	str1 = a as string;
	str1 = fa as string;
	str1 = Func1();
	str1 = Func5() as string;
end

public string Func0()
	Farmer fal = fa;
	fa = this.fa2;
	this.fa = this.fa2;
	this.fa = fa2;
	fa = fa2;
	fa = fal;
	s2,this.n,fa = Func3();

	ListTable<Farmer> fs = {};
	any x = fs[0];
	Farmer ff = Func4()[1][n];
end

public string Func1()
	
end

public string,number Func2()
	
end

public string,number,Farmer Func3()
	return "ss",33,Func4()[1][1];
end

public HashTable<number,HashTable<number,Farmer>> Func4()
	if fa == nil then
	end
	fa.Name = "0";
	HashTable<number,HashTable<number,Farmer>> ht1 = {};
	ht1[1] = {};
	ht1[1][1] = fa;
	return ht1;
end

public number Func5()
	
end