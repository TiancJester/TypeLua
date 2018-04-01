--== start class define ==--
local ClassA = tlclass("TestGenerateLua9.CorrectSyntax.GlobalAccess","TestGenerateLua9.Common.Fruits.Apple")
tlmethod(ClassA,"TestClass","decode")

--== require modules ==--
local Apple
local Fruit
local SystemUtil

function ClassA._loadreference()
    Apple = tlload("TestGenerateLua9.Common.Fruits.Apple")
    Fruit = tlload("TestGenerateLua9.Common.Fruits.Fruit")
    SystemUtil = tlload("TestGenerateLua9.Common.System.SystemUtil")
end
--== static constructor ==--
function ClassA._staticctor()

    code = 'pass'

    ClassA.staticField = "{}"
end
--== global functions ==--
function GFunc1()
    return 'pass'
end

--== constructor ==--
function ClassA:_ctor()
end
--== class functions ==--
function ClassA:TestClass()
    istrue(code .. GFunc1() .. ClassA.staticField == "passpass{}")
end

function ClassA:decode(s)
end

--== end class define ==--
return ClassA
