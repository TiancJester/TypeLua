package test;

import Common;

public class ClassA extends Botany

public string s = "aaaa";

public string s2 = nil;

public any a = "bbb";

public number n = 123;

public number n2 = 123.456;

public Farmer fa = new Farmer();

public Farmer fa2 = nil;

public HashTable<number,Farmer> ht = {};

public void TestClass()
	istrue(s == "aaaa" and fa2 == nil and ht ~= nil);
end