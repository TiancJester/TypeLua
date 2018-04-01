--== start class define ==--
local ClassA = tlclass("TestGenerateLua16.CorrectSyntax.TableTest")
tlmethod(ClassA,"TestClass","TableTest","HashTableTest","ListTableTest")

--== require modules ==--
local SystemUtil

function ClassA._loadreference()
    SystemUtil = tlload("TestGenerateLua16.Common.System.SystemUtil")
end
--== constructor ==--
function ClassA:_ctor()
end
--== class functions ==--
function ClassA:TestClass()
    istrue(self.TableTest() and self.HashTableTest() and self.ListTableTest())
end

function ClassA:TableTest()
    local t
    t = {}
    local t2 = {}
    local t3 = {
		key1 = 123,
		key2 = "asd",
		key3 = function(a)
			if a then
				return true
			end
			return false;
		end,
	}
    t3.key3(t3.key2)
    t3.key3(t3.key1)
    t3.key0 = 10
    t3.key2 = 10
    t3.key9 = {}
    t3.key9.x = {}
    return t3.key3(t3.keyX) == false and t3.key3(t3.key9.x)
end

function ClassA:HashTableTest()
    local n = 1
    local s = "1"
    local s2 = "1"
    local nst = {}
    s = nst[0]
    s = nst[n]
    local nnst = {}
    if nnst[n] == nil then
        nnst[n] = {}
        s2 = nnst[n][n]
    end
    return s == nil and nnst[n] ~= nil and s2 == nil
end

function ClassA:ListTableTest()
    local n = 1
    local s = "1"
    local lt = {}
    lt[0] = s
    return lt[0] == "1" and lt[n] == nil
end

--== end class define ==--
return ClassA
