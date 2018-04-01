--== start class define ==--
local ClassA = tlclass("TestGenerateLua13.CorrectSyntax.NewClass")
tlmethod(ClassA,"TestClass")

--== require modules ==--
local SystemUtil
local Botany
local Farmer
local Land
local Hoe
local flt
local Apple

function ClassA._loadreference()
    SystemUtil = tlload("TestGenerateLua13.Common.System.SystemUtil")
    Botany = tlload("TestGenerateLua13.Common.Botany")
    Farmer = tlload("TestGenerateLua13.Common.Farmer")
    Land = tlload("TestGenerateLua13.Common.Land")
    Hoe = tlload("TestGenerateLua13.Common.Tools.Hoe")
    flt = tlload("TestGenerateLua13.Common.Fruits.Fruit")
    Apple = tlload("TestGenerateLua13.Common.Fruits.Apple")
end
--== constructor ==--
function ClassA:_ctor()
end
--== class functions ==--
function ClassA:TestClass()
    local o0 = Farmer.new()
    local o1 = Hoe.new(nil)
    local o1_1 = Hoe.new(o0)
    local o2 = flt.new(nil)
    local o3 = Apple.new()
    istrue(o1 ~= o1_1)
end

--== end class define ==--
return ClassA
