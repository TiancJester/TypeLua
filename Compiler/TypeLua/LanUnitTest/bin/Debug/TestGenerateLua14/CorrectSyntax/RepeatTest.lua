--== start class define ==--
local ClassA = tlclass("TestGenerateLua14.CorrectSyntax.RepeatTest")
tlmethod(ClassA,"TestClass")

--== require modules ==--
local SystemUtil

function ClassA._loadreference()
    SystemUtil = tlload("TestGenerateLua14.Common.System.SystemUtil")
end
--== constructor ==--
function ClassA:_ctor()
end
--== class functions ==--
function ClassA:TestClass()
    local b = 10
    repeat
        b = b - 1
    until(b == 1)
    istrue(b == 1)
end

--== end class define ==--
return ClassA
