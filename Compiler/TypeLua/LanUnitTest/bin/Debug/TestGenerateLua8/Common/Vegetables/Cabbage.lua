--== start class define ==--
local Cabbage = tlclass("TestGenerateLua8.Common.Vegetables.Cabbage","TestGenerateLua8.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua8.Common.Botany")
    Farmer = tlload("TestGenerateLua8.Common.Farmer")
    Land = tlload("TestGenerateLua8.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
