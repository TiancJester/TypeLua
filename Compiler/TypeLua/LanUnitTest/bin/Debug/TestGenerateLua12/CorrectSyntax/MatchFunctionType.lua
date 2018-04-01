--== start class define ==--
local ClassA = tlclass("TestGenerateLua12.CorrectSyntax.MatchFunctionType","TestGenerateLua12.Common.Botany")
tlmethod(ClassA,"Func1","Func2","Func3","Func4","Func5","Func6","GetColor","TestClass")

--== require modules ==--
local Botany
local Farmer
local Land

function ClassA._loadreference()
    Botany = tlload("TestGenerateLua12.Common.Botany")
    Farmer = tlload("TestGenerateLua12.Common.Farmer")
    Land = tlload("TestGenerateLua12.Common.Land")
end
--== constructor ==--
function ClassA:_ctor()
end
--== class functions ==--
function ClassA:Func1()
    return ""
end

function ClassA:Func2()
    return self.Name
end

function ClassA:Func3()
    return self.GetColor()
end

function ClassA:Func4()
    self.Func6("fmr", self.Owner)
    return self.GetColor(), self.Func5("red")
end

function ClassA:Func5(c)
    if c == self.GetColor() then
        return 100
    end
    return 0
end

function ClassA:Func6(c, f)
    f.Name = c
end

function ClassA:GetColor()
    return "red"
end

function ClassA:TestClass()
    self.Owner = Farmer.new()
    self.Name = "a"
    local s
    local n
    s, n = self.Func4()
    local f = self.Owner
    self.Func6(s, f)
    istrue(self.Func1() .. self.Func2() .. self.Func3() .. s .. n .. f.Name == "aredred100red")
end

--== end class define ==--
return ClassA
