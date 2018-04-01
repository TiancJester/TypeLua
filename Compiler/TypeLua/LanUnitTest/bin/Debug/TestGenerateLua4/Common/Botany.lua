--== start class define ==--
local Botany = tlclass("TestGenerateLua4.Common.Botany")
tlmethod(Botany,"Plant","GetColor")

--== require modules ==--
local Farmer
local Land
local SystemUtil

function Botany._loadreference()
    Farmer = tlload("TestGenerateLua4.Common.Farmer")
    Land = tlload("TestGenerateLua4.Common.Land")
    SystemUtil = tlload("TestGenerateLua4.Common.System.SystemUtil")
end
--== class fileds ==--
Botany.Name = nil
Botany.Owner = nil
--== constructor ==--
function Botany:_ctor()

    self.Name = nil

    self.Owner = nil
end
--== class functions ==--
function Botany:Plant(land)
    local a = -1
    local b = 1
    print("plant to " .. land.Name)
end

function Botany:GetColor()
    return nil
end

--== end class define ==--
return Botany
