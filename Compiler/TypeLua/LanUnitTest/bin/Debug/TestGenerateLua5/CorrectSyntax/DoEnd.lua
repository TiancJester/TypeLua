--== start class define ==--
local ClassA = tlclass("TestGenerateLua5.CorrectSyntax.DoEnd")
tlmethod(ClassA,"TestClass")

--== require modules ==--
local SystemUtil

function ClassA._loadreference()
    SystemUtil = tlload("TestGenerateLua5.Common.System.SystemUtil")
end
--== constructor ==--
function ClassA:_ctor()
end
--== class functions ==--
function ClassA:TestClass()
    local b = 1
    do
        local x = b
        b = x + x
    end
    istrue(b == 2)
end

--== end class define ==--
return ClassA
