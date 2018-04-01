package test;

import Common.Fruits;
import System;

public class ClassA extends Apple

public static global string globalField1 = "{}";
public static global number globalField2 = 1;

public static string staticField1 = "{}";
public static Apple staticField2 = new Apple();

private number f1;
private number f2 = 1;

public number f3;
public number f4 = 1;

static ClassA()
	globalField2 = 22;
	staticField1 = "ss";
end

public ClassA()
    f1 = 33;
    f3 = 44;
end

public global void GFunc1()
end

public global void GFunc2()
end


public static void SFunc1()
end

public static void SFunc2()
end

public void MFunc1()
end

public void MFunc2()
end

public void TestClass()
	istrue(globalField2..staticField1..f1..f3 == "22ss3344");
end