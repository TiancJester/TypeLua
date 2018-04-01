--== start class define ==--
local Cabbage = tlclass("TestGenerateLua12.Common.Vegetables.Cabbage","TestGenerateLua12.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua12.Common.Botany")
    Farmer = tlload("TestGenerateLua12.Common.Farmer")
    Land = tlload("TestGenerateLua12.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
