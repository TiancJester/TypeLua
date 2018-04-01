--== start class define ==--
local Cabbage = tlclass("TestGenerateLua1.Common.Vegetables.Cabbage","TestGenerateLua1.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua1.Common.Botany")
    Farmer = tlload("TestGenerateLua1.Common.Farmer")
    Land = tlload("TestGenerateLua1.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
