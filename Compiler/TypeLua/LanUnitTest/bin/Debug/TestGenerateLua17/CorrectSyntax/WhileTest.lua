--== start class define ==--
local ClassA = tlclass("TestGenerateLua17.CorrectSyntax.WhileTest")
tlmethod(ClassA,"TestClass")

--== require modules ==--
local SystemUtil

function ClassA._loadreference()
    SystemUtil = tlload("TestGenerateLua17.Common.System.SystemUtil")
end
--== constructor ==--
function ClassA:_ctor()
end
--== class functions ==--
function ClassA:TestClass()
    local b = 10
    while (true) do
        b = b - 1
        if b < 0 then
            break
        end
    end
    istrue(b == -1)
end

--== end class define ==--
return ClassA
