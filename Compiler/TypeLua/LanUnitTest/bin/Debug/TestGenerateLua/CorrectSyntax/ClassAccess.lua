--== start class define ==--
local ClassA = tlclass("TestGenerateLua.CorrectSyntax.ClassAccess","TestGenerateLua.Common.Fruits.Apple")
tlmethod(ClassA,"TestClass","GetColor","GetPreserve")

--== require modules ==--
local Apple
local Fruit
local SystemUtil

function ClassA._loadreference()
    Apple = tlload("TestGenerateLua.Common.Fruits.Apple")
    Fruit = tlload("TestGenerateLua.Common.Fruits.Fruit")
    SystemUtil = tlload("TestGenerateLua.Common.System.SystemUtil")
end
--== class fileds ==--
ClassA.p = nil
--== constructor ==--
function ClassA:_ctor()

    self.p = nil

end
--== class functions ==--
function ClassA:TestClass()
    local check = true
    local a1 = ClassA.new()
    a1.IsRed = true
    check = check and a1.GetColor() == "black-red"
    local a2 = ClassA.new()
    a2.IsRed = false
    check = check and a2.GetColor() == "black-green"
    self.IsRed = true
    check = check and self.GetColor() == "black-red"
    check = check and self.GetColor() == "black-red"
    check = check and self.super.GetColor() == "red"
    self.IsRed = false
    check = check and self.GetColor() == "black-green"
    check = check and self.GetColor() == "black-green"
    check = check and self.super.GetColor() == "green"
    self.Preserve("this")
    check = check and self.GetPreserve() == "non"
    self.Preserve("this")
    check = check and self.GetPreserve() == "non"
    self.super.Preserve("this")
    check = check and self.super.GetPreserve() == "this"
    istrue(check)
end

function ClassA:GetColor()
    if self.IsRed then
        return "black-red"
    end
    return "black-green"
end

function ClassA:GetPreserve()
    return "non"
end

--== end class define ==--
return ClassA
