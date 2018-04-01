--== start class define ==--
local ClassA = tlclass("TestGenerateLua7.CorrectSyntax.ForTest")
tlmethod(ClassA,"TestClass","Forx")

--== require modules ==--
local SystemUtil
local Botany
local Farmer
local Land

function ClassA._loadreference()
    SystemUtil = tlload("TestGenerateLua7.Common.System.SystemUtil")
    Botany = tlload("TestGenerateLua7.Common.Botany")
    Farmer = tlload("TestGenerateLua7.Common.Farmer")
    Land = tlload("TestGenerateLua7.Common.Land")
end
--== constructor ==--
function ClassA:_ctor()
end
--== class functions ==--
function ClassA:TestClass()
    local n = self.Forx(101)
    istrue(n == 126)
end

function ClassA:Forx(a)
    for i = 1, 10,2 do
        a = a + 1
    end
    for i = 1, 10 do
        a = a + 1
    end
    local len = 10
    for i = 1, len do
        a = a + 1
    end
    return a
end

--== end class define ==--
return ClassA
