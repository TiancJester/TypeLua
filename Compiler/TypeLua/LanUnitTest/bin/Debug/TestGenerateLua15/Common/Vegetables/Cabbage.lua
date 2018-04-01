--== start class define ==--
local Cabbage = tlclass("TestGenerateLua15.Common.Vegetables.Cabbage","TestGenerateLua15.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua15.Common.Botany")
    Farmer = tlload("TestGenerateLua15.Common.Farmer")
    Land = tlload("TestGenerateLua15.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
