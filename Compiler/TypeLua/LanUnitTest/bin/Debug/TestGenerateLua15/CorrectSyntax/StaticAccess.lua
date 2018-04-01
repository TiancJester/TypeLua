--== start class define ==--
local ClassA = tlclass("TestGenerateLua15.CorrectSyntax.StaticAccess")
tlmethod(ClassA,"TestClass")

--== require modules ==--
local Botany
local Farmer
local Land
local SystemUtil

function ClassA._loadreference()
    Botany = tlload("TestGenerateLua15.Common.Botany")
    Farmer = tlload("TestGenerateLua15.Common.Farmer")
    Land = tlload("TestGenerateLua15.Common.Land")
    SystemUtil = tlload("TestGenerateLua15.Common.System.SystemUtil")
end
--== static constructor ==--
function ClassA._staticctor()

    ClassA.code = nil

    ClassA.code = 123
end
--== constructor ==--
function ClassA:_ctor()
end
--== class functions ==--
function ClassA:TestClass()
    ClassA.code = ClassA.code * 2
    istrue(ClassA.code == 246 and Land.Instructions == "some words.")
end

--== end class define ==--
return ClassA
