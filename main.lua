require("framework.typelua")

globaltable = nil
globalnumber = 1000

function globalfunction(...)
	globaltable = {...}
	return globaltable
end


--[[
tlload("TestGenerateLua8.Common.Farmer")
local Apple = tlload("TestGenerateLua8.Common.Fruits.Apple")
local Cabbage = tlload("TestGenerateLua8.Common.Vegetables.Cabbage")
local Land = tlload("TestGenerateLua8.Common.Land")


local greenLand = Land.new("greenLand")
local cab = Cabbage.new()
local cab2 = Cabbage.new()
cab.Plant(greenLand)
print("cab:"..tostring(cab2.Name))
print("cab:"..tostring(cab))
print("app:"..tostring(Apple.new():GetColor()))
]]--

local files = {
	"TestGenerateLua0.CorrectSyntax.AnyCall",
	"TestGenerateLua1.CorrectSyntax.AssignmentExp",
	"TestGenerateLua2.CorrectSyntax.ClassAccess",
	"TestGenerateLua3.CorrectSyntax.ClassTest",
	"TestGenerateLua4.CorrectSyntax.CrossLua",
	"TestGenerateLua5.CorrectSyntax.DoEnd",
	"TestGenerateLua6.CorrectSyntax.ForInTest",
	"TestGenerateLua7.CorrectSyntax.ForTest",
	"TestGenerateLua8.CorrectSyntax.Function",
	"TestGenerateLua9.CorrectSyntax.GlobalAccess",
	"TestGenerateLua10.CorrectSyntax.IfTest",
	"TestGenerateLua11.CorrectSyntax.MatchFiledType",
	"TestGenerateLua12.CorrectSyntax.MatchFunctionType",
	"TestGenerateLua13.CorrectSyntax.NewClass",
	"TestGenerateLua14.CorrectSyntax.RepeatTest",
	"TestGenerateLua15.CorrectSyntax.StaticAccess",
	"TestGenerateLua16.CorrectSyntax.TableTest",
	"TestGenerateLua17.CorrectSyntax.WhileTest",
}

for i,v in ipairs(files) do
	local testClass = tlload(v)
	local obj = testClass.new()
	print(i)
	obj.TestClass()
end


