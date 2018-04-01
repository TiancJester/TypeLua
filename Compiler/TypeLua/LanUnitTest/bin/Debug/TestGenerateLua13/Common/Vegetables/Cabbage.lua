--== start class define ==--
local Cabbage = tlclass("TestGenerateLua13.Common.Vegetables.Cabbage","TestGenerateLua13.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua13.Common.Botany")
    Farmer = tlload("TestGenerateLua13.Common.Farmer")
    Land = tlload("TestGenerateLua13.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
