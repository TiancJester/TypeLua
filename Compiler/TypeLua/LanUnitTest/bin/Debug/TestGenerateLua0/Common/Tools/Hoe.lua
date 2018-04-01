--== start class define ==--
local Hoe = tlclass("TestGenerateLua0.Common.Tools.Hoe")

--== require modules ==--
local Botany
local Farmer
local Land

function Hoe._loadreference()
    Botany = tlload("TestGenerateLua0.Common.Botany")
    Farmer = tlload("TestGenerateLua0.Common.Farmer")
    Land = tlload("TestGenerateLua0.Common.Land")
end
--== class fileds ==--
Hoe.Owner = nil
--== constructor ==--
function Hoe:_ctor(owner)

    self.Owner = nil
    self.Owner = owner
end
--== end class define ==--
return Hoe
