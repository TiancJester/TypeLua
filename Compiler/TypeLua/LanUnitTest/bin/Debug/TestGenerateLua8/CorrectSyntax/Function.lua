--== start class define ==--
local ClassA = tlclass("TestGenerateLua8.CorrectSyntax.Function")
tlmethod(ClassA,"TestClass","Func2","Func3","Func4","Func5","Func6")

--== require modules ==--
local SystemUtil
local Botany
local Farmer
local Land

function ClassA._loadreference()
    SystemUtil = tlload("TestGenerateLua8.Common.System.SystemUtil")
    Botany = tlload("TestGenerateLua8.Common.Botany")
    Farmer = tlload("TestGenerateLua8.Common.Farmer")
    Land = tlload("TestGenerateLua8.Common.Land")
end
--== class fileds ==--
ClassA.f1 = nil
ClassA.f2 = nil
ClassA.f3 = nil
ClassA.f4 = nil
--== constructor ==--
function ClassA:_ctor()

    self.f1 = nil

    self.f2 = nil

    self.f3 = nil

    self.f4 = nil

end
--== class functions ==--
function ClassA:TestClass()
    local check = ""
    self.f1 = self.Func2
    self.f1()
    self.Func2()
    check = check .. self.Func2()
    local f1x = self.Func2
    check = check .. f1x()
    self.f2 = self.Func3
    check = check .. self.f2(123)
    self.f3 = self.Func4
    local ss = self.f3(321)
    check = check .. ss
    self.f4 = self.Func5
    local land
    ss, land = self.f4(123, Farmer.new())
    land.Plant(nil)
    self.f3 = function(x)
        return ""
    end
    local farmer = Farmer.new()
    check = check .. self.Func6(farmer, function(fm)
        local condition = function()
            if self.Func4(1) == "1" then
                return true
            end
            return false
        end
        return condition()
    end)
    istrue(check == "f2f2123321pass")
end

function ClassA:Func2()
    return "f2"
end

function ClassA:Func3(n)
    return tostring(n)
end

function ClassA:Func4(n)
    return tostring(n)
end

function ClassA:Func5(n, f)
    return tostring(n), Land.new("aland")
end

function ClassA:Func6(f, callback)
    if callback(f) then
        return "pass"
    end
end

--== end class define ==--
return ClassA
