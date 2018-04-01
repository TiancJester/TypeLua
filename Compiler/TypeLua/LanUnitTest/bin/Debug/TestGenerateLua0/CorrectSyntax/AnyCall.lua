--== start class define ==--
local ClassA = tlclass("TestGenerateLua0.CorrectSyntax.AnyCall")
tlmethod(ClassA,"Func1","TestClass")

--== require modules ==--
local SystemUtil
local luautil

function ClassA._loadreference()
    SystemUtil = tlload("TestGenerateLua0.Common.System.SystemUtil")
    luautil = require("TestGenerateLua0.Common.System.luautil")
end
--== class fileds ==--
ClassA.str = nil
--== constructor ==--
function ClassA:_ctor()

    self.str = nil

end
--== class functions ==--
function ClassA:Func1()
    istrue(self.str == "ss330")
end

function ClassA:TestClass()
    globalfunction("ss", 33).b = 0
    self.str = string.format("%s%s%s", globaltable[1], globaltable[2], globaltable.b)
    self.Func1()
end

--== end class define ==--
return ClassA
