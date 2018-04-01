--== start class define ==--
local Fruit = tlclass("TestGenerateLua11.Common.Fruits.Fruit","TestGenerateLua11.Common.Botany")
tlmethod(Fruit,"Eat","Preserve","GetPreserve")

--== require modules ==--
local Apple
local Botany
local Farmer
local Land

function Fruit._loadreference()
    Apple = tlload("TestGenerateLua11.Common.Fruits.Apple")
    Botany = tlload("TestGenerateLua11.Common.Botany")
    Farmer = tlload("TestGenerateLua11.Common.Farmer")
    Land = tlload("TestGenerateLua11.Common.Land")
end
--== class fileds ==--
Fruit.delicious = nil
Fruit.preserve = nil
--== constructor ==--
function Fruit:_ctor(name)

    self.delicious = false
    self.preserve = nil

    self.Name = name
end
--== class functions ==--
function Fruit:Eat()
end

function Fruit:Preserve(where)
    self.preserve = where
    return true
end

function Fruit:GetPreserve()
    return self.preserve
end

--== end class define ==--
return Fruit
