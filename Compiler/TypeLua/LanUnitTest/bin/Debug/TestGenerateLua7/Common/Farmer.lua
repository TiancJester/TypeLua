--== start class define ==--
local Farmer = tlclass("TestGenerateLua7.Common.Farmer")

--== require modules ==--
local Botany
local Land

function Farmer._loadreference()
    Botany = tlload("TestGenerateLua7.Common.Botany")
    Land = tlload("TestGenerateLua7.Common.Land")
end
--== class fileds ==--
Farmer.Name = nil
--== constructor ==--
function Farmer:_ctor()

    self.Name = nil

end
--== end class define ==--
return Farmer
