package test;

import System;

public class ClassA



public void TestClass()
	string s = "a";
	
	--if
	if s == "a" then
		s = "aa";
	end

	--if else
	if s == "aa" then
		s = "aaa";
	else
		print("error!!");
	end

	--if elseif
	if s == "aaa" then
		s = "aaaa";
	elseif s == "b" then
		print("is b!");
	end

	--if elseif elseif
	if s == "a" then
		s = "aaa";
	elseif s == "aaaa" then
		s = "aaab";
	elseif s == "c" then
		print("is c!");
	end

	--if elseif elseif else
	if s == "a" then
		print(s);
	elseif s == "b" then
		print("is b!");
	elseif s == "c" then
		print("is c!");
	else
		s = "pass";
	end
	istrue(s == "pass");
end