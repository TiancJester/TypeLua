--== start class define ==--
local ClassA = tlclass("TestGenerateLua4.CorrectSyntax.CrossLua")
tlmethod(ClassA,"TestClass")

--== require modules ==--
local SystemUtil
local utf8
local array

function ClassA._loadreference()
    SystemUtil = tlload("TestGenerateLua4.Common.System.SystemUtil")
    utf8 = require("TestGenerateLua4.Common.System.utf8")
    array = require("TestGenerateLua4.Common.System.list")
end
--== constructor ==--
function ClassA:_ctor()
end
--== class functions ==--
function ClassA:TestClass()
    local len = utf8.len("asd")
    istrue(len == 3)
end

--== end class define ==--
return ClassA
