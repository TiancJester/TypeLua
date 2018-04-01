--== start class define ==--
local Cabbage = tlclass("TestGenerateLua10.Common.Vegetables.Cabbage","TestGenerateLua10.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua10.Common.Botany")
    Farmer = tlload("TestGenerateLua10.Common.Farmer")
    Land = tlload("TestGenerateLua10.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
