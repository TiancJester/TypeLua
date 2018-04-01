--== start class define ==--
local Cabbage = tlclass("TestGenerateLua9.Common.Vegetables.Cabbage","TestGenerateLua9.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua9.Common.Botany")
    Farmer = tlload("TestGenerateLua9.Common.Farmer")
    Land = tlload("TestGenerateLua9.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
