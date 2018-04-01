--== start class define ==--
local ClassA = tlclass("TestGenerateLua10.CorrectSyntax.IfTest")
tlmethod(ClassA,"TestClass")

--== require modules ==--
local SystemUtil

function ClassA._loadreference()
    SystemUtil = tlload("TestGenerateLua10.Common.System.SystemUtil")
end
--== constructor ==--
function ClassA:_ctor()
end
--== class functions ==--
function ClassA:TestClass()
    local s = "a"
    if s == "a" then
        s = "aa"
    end
    if s == "aa" then
        s = "aaa"
    else
        print("error!!")
    end
    if s == "aaa" then
        s = "aaaa"
    elseif s == "b" then
        print("is b!")
    end
    if s == "a" then
        s = "aaa"
    elseif s == "aaaa" then
        s = "aaab"
    elseif s == "c" then
        print("is c!")
    end
    if s == "a" then
        print(s)
    elseif s == "b" then
        print("is b!")
    elseif s == "c" then
        print("is c!")
    else
        s = "pass"
    end
    istrue(s == "pass")
end

--== end class define ==--
return ClassA
