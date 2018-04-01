--== start class define ==--
local Fruit = tlclass("TestGenerateLua14.Common.Fruits.Fruit","TestGenerateLua14.Common.Botany")
tlmethod(Fruit,"Eat","Preserve","GetPreserve")

--== require modules ==--
local Apple
local Botany
local Farmer
local Land

function Fruit._loadreference()
    Apple = tlload("TestGenerateLua14.Common.Fruits.Apple")
    Botany = tlload("TestGenerateLua14.Common.Botany")
    Farmer = tlload("TestGenerateLua14.Common.Farmer")
    Land = tlload("TestGenerateLua14.Common.Land")
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
