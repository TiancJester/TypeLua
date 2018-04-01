package test;

import System;

public class ClassA

public void TestClass()
	number b = 10;
	while(true) do
		b = b - 1;
		if b < 0 then
			break;
		end
	end
	istrue(b == -1);
end