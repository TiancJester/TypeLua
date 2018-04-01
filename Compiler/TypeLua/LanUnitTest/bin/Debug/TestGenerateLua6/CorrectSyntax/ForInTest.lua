--== start class define ==--
local ClassA = tlclass("TestGenerateLua6.CorrectSyntax.ForInTest")
tlmethod(ClassA,"TestClass","ForIn1","ForIn2","ForIn3","ForIn4")

--== require modules ==--
local SystemUtil
local Botany
local Farmer
local Land

function ClassA._loadreference()
    SystemUtil = tlload("TestGenerateLua6.Common.System.SystemUtil")
    Botany = tlload("TestGenerateLua6.Common.Botany")
    Farmer = tlload("TestGenerateLua6.Common.Farmer")
    Land = tlload("TestGenerateLua6.Common.Land")
end
--== class fileds ==--
ClassA.total = nil
--== constructor ==--
function ClassA:_ctor()

    self.total = 0
end
--== class functions ==--
function ClassA:TestClass()
    self.ForIn1()
    self.ForIn2()
    self.ForIn3()
    self.ForIn4()
    istrue(self.total == 8)
end

function ClassA:ForIn1()
    local t = {a = 1,b = 2}
    for k, v in pairs(t) do
        self.total = self.total + (v)
    end
end

function ClassA:ForIn2()
    local t = {a = 1,b = 1}
    for k, v in pairs(t) do
        self.total = self.total + (v)
    end
end

function ClassA:ForIn3()
    local t = {}
    t["a"] = Farmer.new()
    t["b"] = Farmer.new()
    for k, v in pairs(t) do
        self.total = self.total + 1
        local farmerName = v.Name
        v.Name = k
    end
end

function ClassA:ForIn4()
    local t = {}
    t[1] = Farmer.new()
    t[1] = Farmer.new()
    for i, v in pairs(t) do
        self.total = self.total + 1
        local farmerName = v.Name
        v.Name = tostring(i * 2)
    end
end

--== end class define ==--
return ClassA
