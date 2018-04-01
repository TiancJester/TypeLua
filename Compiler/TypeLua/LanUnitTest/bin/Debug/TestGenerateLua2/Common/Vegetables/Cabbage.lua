--== start class define ==--
local Cabbage = tlclass("TestGenerateLua2.Common.Vegetables.Cabbage","TestGenerateLua2.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua2.Common.Botany")
    Farmer = tlload("TestGenerateLua2.Common.Farmer")
    Land = tlload("TestGenerateLua2.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
