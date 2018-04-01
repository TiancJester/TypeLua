--== start class define ==--
local ClassA = tlclass("TestGenerateLua1.CorrectSyntax.AssignmentExp")
tlmethod(ClassA,"TestClass","Func_any","Func_as","Func0","Func1","Func2","Func3","Func4","Func5")

--== require modules ==--
local SystemUtil
local Botany
local Farmer
local Land

function ClassA._loadreference()
    SystemUtil = tlload("TestGenerateLua1.Common.System.SystemUtil")
    Botany = tlload("TestGenerateLua1.Common.Botany")
    Farmer = tlload("TestGenerateLua1.Common.Farmer")
    Land = tlload("TestGenerateLua1.Common.Land")
end
--== class fileds ==--
ClassA.s = nil
ClassA.s2 = nil
ClassA.a = nil
ClassA.n = nil
ClassA.n2 = nil
ClassA.fa = nil
ClassA.fa2 = nil
ClassA.ht = nil
--== constructor ==--
function ClassA:_ctor()

    self.s = "aaaa"
    self.s2 = nil
    self.a = "bbb"
    self.n = 123
    self.n2 = 123.456
    self.fa = Farmer.new()
    self.fa2 = nil
    self.ht = {}
end
--== class functions ==--
function ClassA:TestClass()
    self.Func_any()
    self.Func_as()
    self.Func0()
    istrue(may_be_a_global_variable == "ss330")
end

function ClassA:Func_any()
    local anyObj1 = self.s
    local anyObj2
    anyObj1, anyObj2 = self.s, self.s2
    local anyObj3
    anyObj1 = self.n
    anyObj1 = self.fa
    anyObj1 = self.fa2
    anyObj1 = self.ht
    anyObj1 = nil
    anyObj1 = self.Func1()
    anyObj1, anyObj2 = self.Func2()
    anyObj1, anyObj2, anyObj3 = self.Func3()
    may_be_a_global_variable = format("%s%s%s", tostring(anyObj1), tostring(anyObj2), anyObj3.Name)
end

function ClassA:Func_as()
    local str1 = self.s
    str1 = self.a
    str1 = self.fa
    str1 = self.Func1()
    str1 = self.Func5()
end

function ClassA:Func0()
    local fal = self.fa
    self.fa = self.fa2
    self.fa = self.fa2
    self.fa = self.fa2
    self.fa = self.fa2
    self.fa = fal
    self.s2, self.n, self.fa = self.Func3()
    local fs = {}
    local x = fs[0]
    local ff = self.Func4()[1][self.n]
end

function ClassA:Func1()
end

function ClassA:Func2()
end

function ClassA:Func3()
    return "ss", 33, self.Func4()[1][1]
end

function ClassA:Func4()
    if self.fa == nil then
    end
    self.fa.Name = "0"
    local ht1 = {}
    ht1[1] = {}
    ht1[1][1] = self.fa
    return ht1
end

function ClassA:Func5()
end

--== end class define ==--
return ClassA
