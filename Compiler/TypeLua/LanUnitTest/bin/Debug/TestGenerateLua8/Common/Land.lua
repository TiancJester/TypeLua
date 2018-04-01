--== start class define ==--
local Land = tlclass("TestGenerateLua8.Common.Land")
tlmethod(Land,"Plant","GetPlantedBotany","GetInfo")

--== require modules ==--
local Botany
local Farmer

function Land._loadreference()
    Botany = tlload("TestGenerateLua8.Common.Botany")
    Farmer = tlload("TestGenerateLua8.Common.Farmer")
end
--== static constructor ==--
function Land._staticctor()

    Land.Instructions = "some words."
end
--== class fileds ==--
Land.Name = nil
Land.Owner = nil
Land.plantedBotany = nil
--== constructor ==--
function Land:_ctor(landName)

    self.Name = nil

    self.Owner = nil

    self.plantedBotany = nil

    self.Name = landName
end
--== class functions ==--
function Land:Plant(botany)
    self.plantedBotany = botany
end

function Land:GetPlantedBotany()
    return self.plantedBotany
end

function Land:GetInfo()
    local p
    if self.plantedBotany == nil then
        p = Botany.new()
    else
        p = self.plantedBotany
    end
    return self.Name, self.Owner, p
end

--== end class define ==--
return Land
