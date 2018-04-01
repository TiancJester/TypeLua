--== start class define ==--
local Farmer = tlclass("TestGenerateLua.Common.Farmer")

--== require modules ==--
local Botany
local Land

function Farmer._loadreference()
    Botany = tlload("TestGenerateLua.Common.Botany")
    Land = tlload("TestGenerateLua.Common.Land")
end
--== class fileds ==--
Farmer.Name = nil
--== constructor ==--
function Farmer:_ctor()

    self.Name = nil

end
--== end class define ==--
return Farmer
