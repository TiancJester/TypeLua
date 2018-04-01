package test;

import System;
import Common;

public class ClassA

public void TestClass()
	number n = Forx(101);
	istrue(n == 126);
end

public number Forx(number a)
	for i = 1,10,2 do
		a = a + 1;
	end

	for i = 1,10 do
		a = a + 1;
	end

	number len = 10;

	for i = 1,len do
		a = a + 1;
	end
	return a;
end