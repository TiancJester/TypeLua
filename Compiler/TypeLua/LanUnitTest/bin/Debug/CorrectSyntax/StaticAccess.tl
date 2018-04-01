package test;

import Common;
import System;

public class ClassA

static ClassA()
	ClassA.code = 123;
end

public static number code;

public void TestClass()
	ClassA.code = ClassA.code * 2;
	istrue(ClassA.code == 246 and Land.Instructions == "some words.");
end
