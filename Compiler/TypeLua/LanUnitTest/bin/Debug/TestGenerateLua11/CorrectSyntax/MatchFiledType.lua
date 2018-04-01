--== start class define ==--
local ClassA = tlclass("TestGenerateLua11.CorrectSyntax.MatchFiledType","TestGenerateLua11.Common.Botany")
tlmethod(ClassA,"TestClass")

--== require modules ==--
local Botany
local Farmer
local Land

function ClassA._loadreference()
    Botany = tlload("TestGenerateLua11.Common.Botany")
    Farmer = tlload("TestGenerateLua11.Common.Farmer")
    Land = tlload("TestGenerateLua11.Common.Land")
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
    istrue(self.s == "aaaa" and self.fa2 == nil and self.ht ~= nil)
end

--== end class define ==--
return ClassA
