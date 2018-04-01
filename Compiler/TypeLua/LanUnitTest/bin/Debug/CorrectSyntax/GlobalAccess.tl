package test;

import Common.Fruits;
import System;

public class ClassA extends Apple

public static global string code = 'pass';
public static string staticField = "{}";

public global string GFunc1()
	return 'pass';
end

public void TestClass()
	istrue(code..GFunc1()..staticField == "passpass{}");
end

private void decode(string s)
	
end