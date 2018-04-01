package test;

import System;
import Common;
import Common.Tools;
import flt = Common.Fruits.Fruit;
import Common.Fruits.Apple;

public class ClassA

public void TestClass()
	Farmer o0 = new Farmer();
	Hoe o1 = new Hoe(nil);
	Hoe o1_1 = new Hoe(o0);
	flt o2 = new flt(nil);
	Apple o3 = new Apple();

	istrue(o1 ~= o1_1);
end
