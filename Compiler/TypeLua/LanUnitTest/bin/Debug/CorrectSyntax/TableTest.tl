package test;

import System;

public class ClassA

public void TestClass()
	istrue(TableTest() and HashTableTest() and ListTableTest());
end

public boolean TableTest()
	table t;
	t = {};

	table t2 = {};

	table t3 = 
	{
		key1 = 123,
		key2 = "asd",
		key3 = function(a)
			if a then
				return true
			end
			return false;
		end,
	};

	t3.key3(t3.key2);
	t3.key3(t3.key1);
	t3.key0 = 10;
	t3.key2 = 10;
	t3.key9 = {};
	t3.key9.x = {};

	return t3.key3(t3.keyX) == false and t3.key3(t3.key9.x);
end

public boolean HashTableTest()
	number n = 1;
	string s = "1";
	string s2 = "1";

	HashTable<number,string> nst = {};
	s = nst[0];
	s = nst[n];

	HashTable<number,HashTable<number,string>> nnst = {};
	if nnst[n] == nil then
		nnst[n] = {};
		s2 = nnst[n][n];
	end

	return s == nil and nnst[n] ~= nil and s2 == nil;
end


public boolean ListTableTest()
	number n = 1;
	string s = "1";

	ListTable<string> lt = {};
	lt[0] = s;

	return lt[0] == "1" and lt[n] == nil;
end
