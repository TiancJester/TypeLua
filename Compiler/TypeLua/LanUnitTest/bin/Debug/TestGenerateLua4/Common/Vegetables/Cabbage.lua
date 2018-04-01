--== start class define ==--
local Cabbage = tlclass("TestGenerateLua4.Common.Vegetables.Cabbage","TestGenerateLua4.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua4.Common.Botany")
    Farmer = tlload("TestGenerateLua4.Common.Farmer")
    Land = tlload("TestGenerateLua4.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
