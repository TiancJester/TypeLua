package test;

import System;
import luautil = Common.System.luautil;

public class ClassA



private string str;

public ClassA()
	
end

public void Func1()
	any.istrue(str == "ss330");
end

public void TestClass()
	any.globalfunction("ss",33).b = 0;
	str = any.string.format("%s%s%s",any.globaltable[1],any.globaltable[2],any.globaltable.b) as string;
	Func1();
end
