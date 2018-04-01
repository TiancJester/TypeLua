--== start class define ==--
local Cabbage = tlclass("TestGenerateLua6.Common.Vegetables.Cabbage","TestGenerateLua6.Common.Botany")

--== require modules ==--
local Botany
local Farmer
local Land

function Cabbage._loadreference()
    Botany = tlload("TestGenerateLua6.Common.Botany")
    Farmer = tlload("TestGenerateLua6.Common.Farmer")
    Land = tlload("TestGenerateLua6.Common.Land")
end
--== constructor ==--
function Cabbage:_ctor()
end
--== end class define ==--
return Cabbage
