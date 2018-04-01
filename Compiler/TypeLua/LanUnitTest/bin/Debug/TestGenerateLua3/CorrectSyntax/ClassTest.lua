--== start class define ==--
local ClassA = tlclass("TestGenerateLua3.CorrectSyntax.ClassTest","TestGenerateLua3.Common.Fruits.Apple")
tlmethod(ClassA,"MFunc1","MFunc2","TestClass")

--== require modules ==--
local Apple
local Fruit
local SystemUtil

function ClassA._loadreference()
    Apple = tlload("TestGenerateLua3.Common.Fruits.Apple")
    Fruit = tlload("TestGenerateLua3.Common.Fruits.Fruit")
    SystemUtil = tlload("TestGenerateLua3.Common.System.SystemUtil")
end
--== static constructor ==--
function ClassA._staticctor()

    globalField1 = "{}"
    globalField2 = 1

    ClassA.staticField1 = "{}"
    ClassA.staticField2 = Apple.new()
    globalField2 = 22
    ClassA.staticField1 = "ss"
end
--== global functions ==--
function GFunc1()
end

function GFunc2()
end

--== static functions ==--
function ClassA.SFunc1()
end

function ClassA.SFunc2()
end

--== class fileds ==--
ClassA.f1 = nil
ClassA.f2 = nil
ClassA.f3 = nil
ClassA.f4 = nil
--== constructor ==--
function ClassA:_ctor()

    self.f1 = nil

    self.f2 = 1
    self.f3 = nil

    self.f4 = 1
    self.f1 = 33
    self.f3 = 44
end
--== class functions ==--
function ClassA:MFunc1()
end

function ClassA:MFunc2()
end

function ClassA:TestClass()
    istrue(globalField2 .. ClassA.staticField1 .. self.f1 .. self.f3 == "22ss3344")
end

--== end class define ==--
return ClassA
